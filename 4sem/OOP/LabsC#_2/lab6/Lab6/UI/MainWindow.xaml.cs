using System;
using System.Diagnostics;
using System.Windows;
using Graphs;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Graph isnance
        /// </summary>
        Graph<int> Graph { get; set; } = new Graph<int>();
        /// <summary>
        /// Create mainwindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
          
        }
        /// <summary>
        /// Add node function
        /// </summary>
        public void funk()
        {
            try
            {
                if (VertexBox.Text == "")
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    var a = Int32.Parse(VertexBox.Text);
                    Graph.AddNode(a);
                    VertexBox.Text = "";
                  
                }
            }
            catch (System.Exception)
            {

                MessageBox.Show("Error");
            }
            
        }
        /// <summary>
        /// Button of add node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            funk();
        }


        /// <summary>
        /// Open EdgesWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EdgesWindow edge = new EdgesWindow(Graph);
            edge.ShowDialog();
        }
        /// <summary>
        /// Open srchwindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SrchWindow src = new SrchWindow(Graph);
            src.ShowDialog();
        }
    }
}
