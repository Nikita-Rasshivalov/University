using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Encryption;

namespace Lab3
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

        private void bEF_Click(object sender, RoutedEventArgs e)
        {
            textOut.Text = Feistel.Encode(textIn.Text, textKey.Text);
            textIn.Text = "";
        }

        private void bDF_Click(object sender, RoutedEventArgs e)
        {
            textIn.Text = Feistel.Encode(textOut.Text, textKey.Text);
            textOut.Text = "";
        }
    }
}
