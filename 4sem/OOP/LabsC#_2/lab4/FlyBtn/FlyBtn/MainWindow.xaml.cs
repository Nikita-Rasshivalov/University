using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FlyBtn
{
    /// <summary>
    /// Class main window
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Yes button
        /// </summary>
        Button Yesbtn = new Button();
        /// <summary>
        /// No button
        /// </summary>
        Button Nobtn = new Button();
        /// <summary>
        /// Create ai isnance mainwindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            StackPanel stackPanel = new StackPanel();
            this.Content = stackPanel;
            MainWin.Width = 600;
            MainWin.Height = 400;
            MainWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWin.ResizeMode = ResizeMode.CanMinimize;
            ChinazesMethod(stackPanel);
            MoveMethod(stackPanel);

        }
        /// <summary>
        /// Create button of closing app and output message
        /// </summary>
        public void ChinazesMethod(StackPanel stackPanel)
        {
            Yesbtn.Content = "Yes";
            Yesbtn.Height = 20;
            Yesbtn.Width = 80;
            Yesbtn.Margin = new Thickness(12);
            stackPanel.Children.Add(Yesbtn);
            Yesbtn.Click += (s, e) => { Close(); MessageBox.Show("OiBOY"); };
        }
        /// <summary>
        /// Create fly button 
        /// </summary>
        public void MoveMethod(StackPanel stackPanel)
        {
            Nobtn.Content = "No";
            Nobtn.Height = 20;
            Nobtn.Width = 33;
            Nobtn.Margin = new Thickness(22);
            stackPanel.Children.Add(Nobtn);
            MainWin.MouseMove += (s, e) =>
            {
                var mousePos = e.GetPosition(Nobtn);
                var distance = Math.Sqrt(Math.Pow((0 - mousePos.X + 15), 2) + Math.Pow((0 - mousePos.Y + 10), 2));
                if (distance < 25)
                {
                    Nobtn.Margin = new Thickness(Nobtn.Margin.Left - mousePos.X, Nobtn.Margin.Top - mousePos.Y, Nobtn.Margin.Right, Nobtn.Margin.Bottom);
                }
            };
        }


    }
}

