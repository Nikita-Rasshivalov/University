using FEM.BLL.Data;
using FEM.BLL.UIElements;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FEM.WPFGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _model;
        public MainWindow(MainViewModel model)
        {
            InitializeComponent();
            _model = model;
            InitializeMaterials();
            InitializeGeometryModel();
        }

        private void InitializeMaterials()
        {
            try
            {
                foreach (var material in _model.Materials)
                {
                    MaterialsBox.Items.Add(material);
                }
                MaterialsBox.SelectedItem = _model.SelectedMaterial;
                MaterialsBox.DisplayMemberPath = "Name";
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка загрузки материалов. {exception.Message}");
            }
        }

        private void InitializeGeometryModel()
        {
            try
            {
                MeshModel.Content = _model.OriginalMeshModel;
                MeshModel.Transform = _model.Transforms;
                HideGradientScale();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка инициализации геометрии. {exception.Message}");
            }
        }

        private void PressureApplyButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _model.GeneratePressLoads(double.Parse(PressureBox.Text));
                MeshModel.Content = _model.OriginalMeshModel;
                HideGradientScale();
                RefreshSolutionButton();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка применения сил к детали. {exception.Message}");
            }
        }

        private void MakeSolutionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startTime = DateTime.Now;
                _model.Solve();
                RefreshSolutionButton(true);
                MessageBox.Show($"Решение успешно завершено. Время, затраченное на решение {(DateTime.Now - startTime).TotalSeconds} с.");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка решения. {exception.Message}");
            }
        }

        private void MaterialApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _model.CurrentMaterial = _model.SelectedMaterial;
                MeshModel.Content = _model.OriginalMeshModel;
                RefreshSolutionButton();
                HideGradientScale();
                MessageBox.Show($"Материал успешно изменён");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка применения материала. {exception.Message}");
            }
        }

        private void ShowOriginalDetailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MeshModel.Content = _model.OriginalMeshModel;
                HideGradientScale();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа исходной детали. {exception.Message}");
            }
        }

        private void ShowDisplacementButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MeshModel.Content = _model.GetDisplacementMeshModel();
                ShowGradientScale(_model.DisplacementGradientScale);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа перемещений детали. Возможно вы забыли произвести решение. {exception.Message}");
            }
        }

        private void ShowStressButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void ShowStrainButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MeshModel.Content = _model.GetStrainMeshModel();
                ShowGradientScale(_model.StrainGradientScale);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка показа деформаций детали. Возможно вы забыли произвести решение. {exception.Message}");
            }

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

        private void MaterialsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _model.SelectedMaterial = (MaterialDTO)MaterialsBox.SelectedItem;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка изменения выбранного материала. {exception.Message}");
            }
        }

        private void SolutionInfoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                ((ToolTip)SolutionInfoButton.ToolTip).Content = _model.GetSolutionInfo();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка просмотра информации о рещении. {exception.Message}");
            }
        }

        private void MaterialInfoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                ((ToolTip)MaterialInfoButton.ToolTip).Content = _model.GetSelectedMaterialInfo();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка просмотра информации о материале. {exception.Message}");
            }
        }

        private void RefreshSolutionButton(bool isSolved = false)
        {
            MakeSolutionButton.Background = isSolved ? Brushes.LightGreen : new SolidColorBrush(Color.FromArgb(0xFF, 0xC5, 0x53, 0x78));
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
        }
    }
}
