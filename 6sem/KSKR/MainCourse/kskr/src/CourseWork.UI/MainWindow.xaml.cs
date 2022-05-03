using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using CourseWork.BLL.Interfaces;
using CourseWork.BLL.ModelMakers;
using CourseWork.BLL.Models;
using CourseWork.BLL.Models.GridSettings;
using CourseWork.BLL.Services;
using CourseWork.BLL.Services.ElementLoaders.Json;
using CourseWork.BLL.Services.ElementLoaders.Txt;
using CourseWork.BLL.Tools;
using CourseWork.BLL.UIElements;
using Microsoft.Win32;
using Point = CourseWork.Common.Models.Point;

namespace CourseWork.UI
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _model;
        public MainWindow()
        {
            var serializer = new GridSerializer();
            #region load1
            //var grid = new GridSettings
            //{
            //    Lenght = 500,
            //    Width = 500,
            //    Height = 200,
            //    Figures = new List<IFigure>
            //    {
            //        new Circle
            //        {
            //            Height = 20,
            //            IsFill = true,
            //            Radius = 60,
            //            StartPoint = new Point { X = 250, Y = 250, Z = 0 }
            //        },
            //        new Circle
            //        {
            //            Height = 10,
            //            IsFill = false,
            //            Radius = 40,
            //            StartPoint = new Point { X = 250, Y = 250, Z = 10 }
            //        },
            //        new Circle
            //        {
            //            Height = 20,
            //            IsFill = false,
            //            Radius = 30,
            //            StartPoint = new Point { X = 250, Y = 250, Z = 0 }
            //        },
            //        new Circle
            //        {
            //            Height = 20,
            //            IsFill = false,
            //            Radius = 3,
            //            StartPoint = new Point { X = 300, Y = 250, Z = 0 }
            //        },
            //        new Circle
            //        {
            //            Height = 20,
            //            IsFill = false,
            //            Radius = 3,
            //            StartPoint = new Point { X = 200, Y = 250, Z = 0 }
            //        },
            //        new Circle
            //        {
            //            Height = 20,
            //            IsFill = false,
            //            Radius = 3,
            //            StartPoint = new Point { X = 250, Y = 300, Z = 0 }
            //        },
            //        new Circle
            //        {
            //            Height = 20,
            //            IsFill = false,
            //            Radius = 3,
            //            StartPoint = new Point { X = 250, Y = 200, Z = 0 }
            //        }
            //        //new Rectangle
            //        //{
            //        //    Height = 1,
            //        //    Width = 1,
            //        //    Length = 1,
            //        //    IsFill = true,
            //        //    StartPoint = new Point { X = 2, Y = 2, Z = 0 }
            //        //}
            //    }
            //};
            #endregion
            #region load2
            var grid = new GridSettings
            {
                Lenght = 500,
                Width = 500,
                Height = 200,
                Figures = new List<IFigure>
                {
                    new Circle
                    {
                        Height = 20,
                        IsFill = true,
                        Radius = 50,
                        StartPoint = new Point { X = 250, Y = 250, Z = 0 }
                    },
                    new Circle
                    {
                        Height = 10,
                        IsFill = false,
                        Radius = 20,
                        StartPoint = new Point { X = 250, Y = 250, Z = 10 }
                    },
                    new Circle
                    {
                        Height = 20,
                        IsFill = false,
                        Radius = 10,
                        StartPoint = new Point { X = 250, Y = 250, Z = 0 }
                    },
                    new Circle
                    {
                        Height = 20,
                        IsFill = false,
                        Radius = 3,
                        StartPoint = new Point { X = 300, Y = 250, Z = 0 }
                    },
                    new Circle
                    {
                        Height = 20,
                        IsFill = false,
                        Radius = 3,
                        StartPoint = new Point { X = 200, Y = 250, Z = 0 }
                    },
                    new Circle
                    {
                        Height = 20,
                        IsFill = false,
                        Radius = 3,
                        StartPoint = new Point { X = 250, Y = 300, Z = 0 }
                    },
                    new Circle
                    {
                        Height = 20,
                        IsFill = false,
                        Radius = 3,
                        StartPoint = new Point { X = 250, Y = 200, Z = 0 }
                    }
                    //new Rectangle
                    //{
                    //    Height = 1,
                    //    Width = 1,
                    //    Length = 1,
                    //    IsFill = true,
                    //    StartPoint = new Point { X = 2, Y = 2, Z = 0 }
                    //}
                }
            };
            #endregion
            serializer.Serialize(grid, @".\..\..\..\..\Data\elements.json");
            var loadsProvider = new TxtLoadsProvider(@".\..\..\..\..\Data\nodeLoadsEMPTY.txt");
            var elementsProvider = new TxtElementsProvider(@".\..\..\..\..\Data\elementsMetric.txt", @".\..\..\..\..\Data\nodesMetric.txt", loadsProvider);
            //var elementsProvider = new JsonElementsProvider(@".\..\..\..\..\Data\elements.json", new GridSerializer());
            var materialProvider = new TxtMaterialsProvider(@".\..\..\..\..\Data\materials.txt");
            var solutionMaker = new SolutionMaker();
            _model = new MainViewModel(elementsProvider, materialProvider, solutionMaker, new SimpleModelMaker(new GradientMaker()));
            //var elementsProvider = new JsonElementsProvider(@".\..\..\..\..\Data\elements.json", new GridSerializer());
            var model = elementsProvider.GetModel();
            var minY = model.Nodes.Min(n => n.Position.Y);
            var minNodes = model.Nodes.Where(n => n.Position.Y == minY).ToList();
            var color = (Color)ColorConverter.ConvertFromString("Red");
            //var writer = new StreamWriter(@"D:\university\kskr\src\Data\myloads.txt", true);
            //foreach(var node in minNodes)
            //{
            //    writer.WriteLine($"{node.Id}\tFixed\t0\t0\t0");
            //}
            //var solutionMaker = new SolutionMaker(model, new DetailMaterial("steel", 200000, 0.3, color), new SlauCalculator());
            //var displacements = solutionMaker.GetNodeDisplacements();
            //var elements = new List<Element>()
            //{
            //    new Element(){ Nodes = new List<Node>
            //    {
            //        new Node
            //        {
            //            Position = new Point(3,0,0),
            //        },
            //        new Node
            //        {
            //            Position = new Point(4,0,0),
            //        },
            //        new Node
            //        {
            //            Position = new Point(3,0,2),
            //        },
            //        new Node
            //        {
            //            Position = new Point(3,1,1),
            //        },
            //    }}
            //};
            InitializeComponent();
            InitializeGeometryModel();
            MaterialsBox.SelectionChanged -= MaterialsBox_SelectionChanged;
            MaterialsBox.ItemsSource = _model.Materials;
            MaterialsBox.SelectedIndex = 0;
            MaterialsBox.SelectionChanged += MaterialsBox_SelectionChanged;
        }


        private void InitializeGeometryModel()
        {
            try
            {
                MeshModel.Content = _model.OriginalModelGeometry;
                MeshModel.Transform = _model.Transforms;
                HideGradientScale();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка инициализации геометрии. {exception.Message}");
            }
        }

        private Transform3DGroup GetTransformations()
        {
            const int scale = 7;
            var transform = new Transform3DGroup();
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 30)));
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 30)));
            transform.Children.Add(new ScaleTransform3D(scale, scale, scale, 0, 0, 0));
            return transform;
        }

        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            try
            {
                _model.Scale(e.Delta > 0);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка масштабирования. {exception.Message}");
            }
        }

        private double startXPos;
        private double startYPos;
        private void Viewport_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void MaterialsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _model.CurrentMaterial = (DetailMaterial)MaterialsBox.SelectedItem;
                MeshModel.Content = _model.OriginalModelGeometry;
                RefreshSolution();
                HideGradientScale();
                MessageBox.Show($"Материал успешно изменён");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка изменения выбранного материала. {exception.Message}");
            }
        }

        private void MaterialInfoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                ((ToolTip)MaterialInfoButton.ToolTip).Content = _model.CurrentMaterial.GetInfo();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка просмотра информации о материале. {exception.Message}");
            }
        }

        private void RefreshSolution(bool isSolved = false)
        {
            if (!isSolved)
            {
                _model.CurrentModel = null;
            }
            MakeSolutionButton.Background = isSolved ? Brushes.LightGreen : (SolidColorBrush)new BrushConverter().ConvertFrom("#ffa899");
        }

        private void HideGradientScale()
        {
            GradientScaleGrid.Visibility = Visibility.Hidden;
        }

        private void ShowGradientScale(GradientScale gradientScale)
        {
            GradientScaleGrid.Visibility = Visibility.Visible;
            GradientScaleRectangle.Fill = gradientScale.Brush;
            GradientScaleMaxValue.Text = gradientScale.MaxValueText;
            GradientScaleMinValue.Text = gradientScale.MinValueText;
            GradientScaleMaxValueUnder.Text = gradientScale.MaxValueTextUnder;
            GradientScaleMinValueUnder.Text = gradientScale.MinValueTextUnder;
        }

        private void ShowStressButton_Click(object sender, RoutedEventArgs e)
        {
            if (_model.CurrentModel == null)
            {
                MessageBox.Show("Небходимо произвести решение.");
                return;
            }
            try
            {
                MeshModel.Content = _model.GetStressMeshModel();
                ShowGradientScale(_model.StressesGradientScale);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа перемещения детали. Возможно вы забыли произвести решение. {exception.Message}");
            }

        }

        private void MakeSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startTime = DateTime.Now;
                _model.Solve();
                RefreshSolution(true);
                MessageBox.Show($"Решение успешно выполнено. Затраченное время: {(DateTime.Now - startTime).TotalSeconds} с.");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка решения. {exception.Message}");
            }
        }

        private void PressureApplyButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HideGradientScale();
                RefreshSolution();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка применения сил к детали. {exception.Message}");
            }
        }

        private void ShowOriginalDetailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeformationCoefBox.Visibility = Visibility.Hidden;
                MeshModel.Content = _model.OriginalModelGeometry;
                HideGradientScale();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа исходной детали. {exception.Message}");
            }
        }

        private void ShowDisplacementButton_Click(object sender, RoutedEventArgs e)
        {
            if (_model.CurrentModel == null)
            {
                MessageBox.Show("Небходимо произвести решение.");
                return;
            }
            try
            {
                DeformationCoefBox.Visibility = Visibility.Visible;
                _model.Coefficient = double.Parse(DeformationCoefBox.Text);
                MeshModel.Content = _model.DisplacedModelGeometry;
                MeshModel.Transform = _model.Transforms;
                ShowGradientScale(_model.DisplacementGradientScale);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа перемещений детали. Возможно вы забыли произвести решение. {exception.Message}");
            }
        }

        private void ShowStrainButton_Click(object sender, RoutedEventArgs e)
        {
            if (_model.CurrentModel == null)
            {
                MessageBox.Show("Небходимо произвести решение.");
                return;
            }
            try
            {
                DeformationCoefBox.Visibility = Visibility.Hidden;
                MeshModel.Content = _model.GetStrainMeshModel();
                ShowGradientScale(_model.StrainGradientScale);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа деформаций детали. Возможно вы забыли произвести решение. {exception.Message}");
            }

        }

        private void FileTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _model.CurrentFileType = (FileTypeBox.SelectedItem as ComboBoxItem)?.Content as string;
            switch (_model.CurrentFileType)
            {
                case "json":
                    NodesBox.Visibility = Visibility.Hidden;
                    StepBox.Visibility = Visibility.Visible;

                    break;
                case "txt":
                    NodesBox.Visibility = Visibility.Visible;
                    StepBox.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void ElementsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ElementsBox.Text = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = GetCurrentFileFilter();
                if (openFileDialog.ShowDialog() == true)
                {
                    ElementsBox.Text = openFileDialog.FileName;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка. Выберите корректный файл\nc элементами!","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NodesBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                NodesBox.Text = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = GetCurrentFileFilter();
                if (openFileDialog.ShowDialog() == true)
                {
                    NodesBox.Text = openFileDialog.FileName;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка. Выберите корректный файл\nc узлами!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadsBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LoadsBox.Text = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    LoadsBox.Text = openFileDialog.FileName;
                }
                var loadsProvider = new TxtLoadsProvider(LoadsBox.Text);
                var loads = loadsProvider.GetNodeLoads();
                _model.OriginalModel.NodeLoads = loads;
                InitializeGeometryModel();
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка. Выберите корректный файл\nc нагрузками!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetCurrentFileFilter()
        {
            return _model.CurrentFileType == "json" ? "Json files (*.json)|*.json" : "Text files (*.txt)|*.txt";
        }



        private void ModelApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IElementsProvider elementsProvider = _model.CurrentFileType switch
                {
                    "json" => new JsonElementsProvider(ElementsBox.Text, new GridSerializer()),
                    "txt" => new TxtElementsProvider(ElementsBox.Text, NodesBox.Text),
                    _ => throw new NotSupportedException($"File type '{_model.CurrentFileType}' is not supported."),
                };
                var loads = _model.OriginalModel.NodeLoads;
                _model.OriginalModel = elementsProvider.GetModel();
                _model.OriginalModel.NodeLoads = loads;
                _model.CurrentModel = null;
                InitializeGeometryModel();
            }
            catch (Exception)
            {

                MessageBox.Show("Поверьте корректность введенных данных","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var xRotate = (e.GetPosition(Canvas).X - startXPos);
                    var yRotate = (e.GetPosition(Canvas).Y - startYPos);

                    _model.Rotate(xRotate, yRotate);
                }
                startXPos = e.GetPosition(Canvas).X;
                startYPos = e.GetPosition(Canvas).Y;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка вращения. {exception.Message}");
            }
        }

        private void DeformationCoefBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    _model.Coefficient = double.Parse(DeformationCoefBox.Text);
                    MeshModel.Content = _model.DisplacedModelGeometry;
                    MeshModel.Transform = _model.Transforms;
                    ShowGradientScale(_model.DisplacementGradientScale);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Не удалось применить коэффицент. {exception.Message}");
            }
        }
    }
}
