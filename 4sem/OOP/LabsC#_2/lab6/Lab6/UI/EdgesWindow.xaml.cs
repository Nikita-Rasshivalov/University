using System;
using System.Windows;
using Graphs;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для EdgesWindow.xaml
    /// </summary>
    public partial class EdgesWindow : Window
    {
      
        /// <summary>
        /// Graph isntance EdgesWindow
        /// </summary>
        Graph<int> Graph { get; set; }
        /// <summary>
        /// Create an isnance 
        /// </summary>
        /// <param name="graph">graph</param>
        public EdgesWindow(Graph<int> graph)
        {
            this.Graph = graph;
            InitializeComponent();
            VertexOne.ItemsSource = graph.Nodes;
            VertexTwo.ItemsSource = graph.Nodes;
           
        }
        /// <summary>
        /// Add Edge function
        /// </summary>
        /// <param name="graph">graph</param>
        public void func(Graph<int> graph)
        {
            try
            {
                graph.AddEdge(((GraphNode<int>)VertexOne.SelectedItem).Value, ((GraphNode<int>)VertexTwo.SelectedItem).Value);

            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
        /// <summary>
        /// Button of add edge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VertexOne.SelectedIndex == VertexTwo.SelectedIndex)
            {
                MessageBox.Show("Edge wasn't added");

            }
            else
            {
                func(Graph);
                MessageBox.Show("Edge was added");
                VertexOne.Text = "";
                VertexTwo.Text = "";
            }

        }
    }
}
