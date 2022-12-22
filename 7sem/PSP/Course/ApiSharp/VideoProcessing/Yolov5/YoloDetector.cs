using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Yolov5.Models;

namespace Yolov5
{
    /// <summary>
    /// Yolov5 prodictor.
    /// </summary>
    public class YoloDetector<T> : IDisposable, IYoloDetector where T : IYoloModel
    {
        private readonly T _model;

        private readonly InferenceSession _inferenceSession;

        /// <summary>
        /// Creates new instance of YoloScorer with weights path and options.
        /// </summary>
        public YoloDetector(T model, string weightsPath, SessionOptions? opts = null)
        {
            _model = model;
            _inferenceSession = new InferenceSession(File.ReadAllBytes(weightsPath), opts ?? SessionOptions.MakeSessionOptionWithCudaProvider(0));
        }

        /// <summary>
        /// Runs object detection.
        /// </summary>
        public List<YoloDetection> Detect(Image image, float? threshold)
        {
            threshold ??= _model.Confidence;
            return Supress(ParseOutput(Inference(image), image, threshold.Value));
        }

        /// <summary>
        /// Disposes YoloScorer instance.
        /// </summary>
        public void Dispose()
        {
            _inferenceSession.Dispose();
        }

        /// <summary>
        /// Resizes image keeping ratio to fit model input size.
        /// </summary>
        private Bitmap ResizeImage(Image image)
        {
            PixelFormat format = image.PixelFormat;

            var output = new Bitmap(_model.Width, _model.Height, format);

            var (w, h) = (image.Width, image.Height); // image width and height
            var (xRatio, yRatio) = (_model.Width / (float)w, _model.Height / (float)h); // x, y ratios
            var ratio = Math.Min(xRatio, yRatio); // ratio = resized / original
            var (width, height) = ((int)(w * ratio), (int)(h * ratio)); // roi width and height
            var (x, y) = ((_model.Width / 2) - (width / 2), (_model.Height / 2) - (height / 2)); // roi x and y coordinates
            var roi = new Rectangle(x, y, width, height); // region of interest

            using (var graphics = Graphics.FromImage(output))
            {
                graphics.Clear(Color.FromArgb(0, 0, 0, 0)); // clear canvas

                graphics.SmoothingMode = SmoothingMode.None; // no smoothing
                graphics.InterpolationMode = InterpolationMode.Bilinear; // bilinear interpolation
                graphics.PixelOffsetMode = PixelOffsetMode.Half; // half pixel offset

                graphics.DrawImage(image, roi); // draw scaled
            }

            return output;
        }

        /// <summary>
        /// Extracts pixels into tensor for net input.
        /// </summary>
        private Tensor<float> ExtractPixels(Image image)
        {
            var bitmap = (Bitmap)image;

            var rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            var tensor = new DenseTensor<float>(new[] { 1, 3, _model.Height, _model.Width });

            unsafe // speed up conversion by direct work with memory
            {
                Parallel.For(0, bitmapData.Height, (y) =>
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);

                    Parallel.For(0, bitmapData.Width, (x) =>
                    {
                        tensor[0, 0, y, x] = row[x * bytesPerPixel + 2] / 255.0F; // r
                        tensor[0, 1, y, x] = row[x * bytesPerPixel + 1] / 255.0F; // g
                        tensor[0, 2, y, x] = row[x * bytesPerPixel + 0] / 255.0F; // b
                    });
                });

                bitmap.UnlockBits(bitmapData);
            }

            return tensor;
        }

        /// <summary>
        /// Runs inference session.
        /// </summary>
        private DenseTensor<float>[] Inference(Image image)
        {
            Bitmap? resized = null;

            if (image.Width != _model.Width || image.Height != _model.Height)
            {
                resized = ResizeImage(image); // fit image size to specified input size
            }

            var inputs = new List<NamedOnnxValue> // add image as onnx input
            {
                NamedOnnxValue.CreateFromTensor("images", ExtractPixels(resized ?? image))
            };

            IDisposableReadOnlyCollection<DisposableNamedOnnxValue> result = _inferenceSession.Run(inputs); // run inference

            var output = new List<DenseTensor<float>>();

            foreach (var item in _model.Outputs) // add outputs for processing
            {
                output.Add(result.First(x => x.Name == item).Value as DenseTensor<float>);
            };

            return output.ToArray();
        }

        /// <summary>
        /// Parses net outputs (sigmoid or detect layer) to predictions.
        /// </summary>
        private List<YoloDetection> ParseOutput(DenseTensor<float>[] output2, Image image, float threshold)
        {
            var output = output2[0];
            var result = new ConcurrentBag<YoloDetection>();

            var (w, h) = (image.Width, image.Height); // image w and h
            var (xGain, yGain) = (_model.Width / (float)w, _model.Height / (float)h); // x, y gains
            var gain = Math.Min(xGain, yGain); // gain = resized / original

            var (xPad, yPad) = ((_model.Width - w * gain) / 2, (_model.Height - h * gain) / 2); // left, right pads

            Parallel.For(0, (int)output.Length / _model.Dimensions, (i) =>
            {
                if (output[0, i, 4] <= threshold) return;

                Parallel.For(5, _model.Dimensions, (j) =>
                {
                    output[0, i, j] = output[0, i, j] * output[0, i, 4]; // class confidence * object confidence
                });

                var max = output[0, i, 5];
                var maxClassJ = 5;
                for (var j = 5; j < _model.Dimensions; j++)
                {
                    if (output[0, i, j] > max)
                    {
                        max = output[0, i, j];
                        maxClassJ = j;
                    }
                }
                if (output[0, i, maxClassJ] <= _model.MulConfidence)
                {
                    return;
                }

                float xMin = ((output[0, i, 0] - output[0, i, 2] / 2) - xPad) / gain; // unpad bbox tlx to original
                float yMin = ((output[0, i, 1] - output[0, i, 3] / 2) - yPad) / gain; // unpad bbox tly to original
                float xMax = ((output[0, i, 0] + output[0, i, 2] / 2) - xPad) / gain; // unpad bbox brx to original
                float yMax = ((output[0, i, 1] + output[0, i, 3] / 2) - yPad) / gain; // unpad bbox bry to original

                xMin = Clamp(xMin, 0, w - 0); // clip bbox tlx to boundaries
                yMin = Clamp(yMin, 0, h - 0); // clip bbox tly to boundaries
                xMax = Clamp(xMax, 0, w - 1); // clip bbox brx to boundaries
                yMax = Clamp(yMax, 0, h - 1); // clip bbox bry to boundaries

                YoloLabel label = _model.Labels[maxClassJ - 5];

                var prediction = new YoloDetection
                {
                    Label = label,
                    Confidence = output[0, i, maxClassJ],
                    Rectangle = new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin),
                };

                result.Add(prediction);
            });

            return result.ToList();
        }

        /// <summary>
        /// Removes overlaped duplicates (nms).
        /// </summary>
        private List<YoloDetection> Supress(List<YoloDetection> items)
        {
            var result = new List<YoloDetection>(items);

            foreach (var item in items) // iterate every prediction
            {
                foreach (var current in result.ToList()) // make a copy for each iteration
                {
                    if (current == item) continue;

                    var (rect1, rect2) = (item.Rectangle, current.Rectangle);

                    RectangleF intersection = RectangleF.Intersect(rect1, rect2);

                    float intArea = intersection.Width * intersection.Height; // intersection area
                    float unionArea = rect1.Width * rect1.Height + rect2.Width * rect2.Height - intArea; // union area
                    float overlap = intArea / unionArea; // overlap ratio

                    if (overlap >= _model.Overlap)
                    {
                        if (item.Confidence >= current.Confidence)
                        {
                            result.Remove(current);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns value clamped to the inclusive range of min and max.
        /// </summary>
        private float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
