using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ReaderWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Reader_Click(object sender, RoutedEventArgs e)
        {
            if (new OpenFileDialog() is var dialog && dialog.ShowDialog() == true)
                Output.Text = Regex.Split(File.ReadAllText(dialog.FileName).ToLower(), @"[\s, \., \!, \?, \,, \n, \t]").Select(word => Regex.Replace(word, @"\P{L}", "")).
                    Where(str => str.Length > 3).GroupBy(filter => filter).OrderBy(count => count.Count()).Reverse().Distinct().Select(i => i.Key).Select(item => item.ToString()).Aggregate((x, y) => x + Environment.NewLine + y);
        }

        private void Output_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
} 