using Common;
using Common.SharedMemory;

namespace WorkerProcessing;

public class Worker
{
    private readonly IAsyncDataReader<ImageFigures> _figureReader;
    private readonly IAsyncDataReader<int> _intReader;
    private readonly string _figureIdFileName;
    private readonly string _figureFileName;
    private SynchronizedQueue<ImageFigures> _buffer;

    public Worker(string figureIdFileName,
        string figureFileName,
        IAsyncDataReader<ImageFigures> figureReader,
        IAsyncDataReader<int> intReader)
    {
        _figureIdFileName = figureIdFileName;
        _figureFileName = figureFileName;
        _figureReader = figureReader;
        _intReader = intReader;
        _buffer = new SynchronizedQueue<ImageFigures>();
    }

    public async Task ExecuteAsync(string directory)
    {
        var threadsCount = Environment.ProcessorCount;
        var tasks = new Task[threadsCount];
        tasks[0] = Produce();
        for (var i = 1; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(() => Consume(directory));
        }

        await Task.WhenAll(tasks);
    }

    public async Task ExecuteAsSingleAsync(string directory)
    {
        var tasks = new Task[2];
        tasks[0] = Produce();
        tasks[1] = Task.Run(() => Consume(directory));

        await Task.WhenAll(tasks);
    }

    private async Task Produce()
    {
        int n = 0;
        while(true)
        {
            try
            {
                n = await _intReader.ReadAsync(_figureIdFileName, n * 4);
                if(n == -1)
                {
                    break;
                }

                var figure = await _figureReader.ReadAsync($"{_figureFileName}{n}");
                _buffer.Enqueue(figure);
            }
            catch(Exception ex)
            {
            }
        }
        _buffer.Stop();
    }

    private void Consume(object obj)
    {
        while (true)
        {
            try
            {
                var taskData = _buffer.Dequeue();
                if (taskData != null)
                {
                    Console.WriteLine("Processing " + taskData.FileName);
                    var squares = taskData.Figures.Select(f => Square.FromPoints(f.Points)).Where(s => s != null).ToList();
                    using var sw = new StreamWriter((string)obj + $"/results_{taskData.FileName}.txt");
                    sw.WriteLine($"Number of squares on image: {squares.Count}");
                    sw.WriteLine($"Line equations:");
                    foreach (var square in squares)
                    {
                        sw.WriteLine(string.Join("    ", square.LineEquation));
                    }
                }
                else
                {
                    if (!_buffer.IsActive)
                    {
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}