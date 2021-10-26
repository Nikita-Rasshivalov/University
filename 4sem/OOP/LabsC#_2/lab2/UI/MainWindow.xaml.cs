using Microsoft.Win32;
using StreamDecorators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Create an istance of mainwindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Start button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveAndUseStreams();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("The number of bytes is less than the specified number of bytes!!","Information",MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            
        }

        /// <summary>
        /// SaveText and use streams
        /// </summary>
        private void SaveAndUseStreams()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                using (var stream = File.Create(filePath))
                {
                    CalcStreamDecorator decoratedStream = new CalcStreamDecorator(Convert.ToInt32(nums.Text), stream);
                    byte[] array = Encoding.Default.GetBytes(StringInput.Text);
                    decoratedStream.Write(array, 0, array.Length);
                }

                byte[] storage = new byte[StringInput.Text.Length];
                using (var stream = File.OpenRead(filePath))
                {
                    CalcStreamDecorator decoratedStream = new CalcStreamDecorator(Convert.ToInt32(nums.Text), stream);
                    decoratedStream.Read(storage, 0, storage.Length);
                    string s = "";
                    foreach (var i in decoratedStream.Values)
                    {
                        s = s+ " " + i;
                    }
                    MessageBox.Show(s);
                }
                byte[] storage2 = new byte[StringInput.Text.Length];

                using (var stream = new MemoryStream(storage))
                {
                    CalcStreamDecorator decoratedStream = new CalcStreamDecorator(Convert.ToInt32(nums.Text), stream);
                  
                    decoratedStream.Read(storage2, 0, storage2.Length);
                    MessageBox.Show(Encoding.Default.GetString(decoratedStream.Values.ToArray()));
                }

                byte[] storage3 = new byte[StringInput.Text.Length];

                using (var stream = new BufferedStream(new MemoryStream(storage)))
                {
                    CalcStreamDecorator decoratedStream = new CalcStreamDecorator(Convert.ToInt32(nums.Text), stream);
                    decoratedStream.Read(storage3, 0, storage3.Length);
                    MessageBox.Show(Encoding.Default.GetString(decoratedStream.Values.ToArray()));
                }
            }
        }
        /// <summary>
        /// input only numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nums_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
