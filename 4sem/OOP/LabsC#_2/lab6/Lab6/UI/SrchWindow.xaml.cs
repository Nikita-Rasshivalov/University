using Graphs;
using System.Windows;


namespace UI
{
    /// <summary>
    /// Логика взаимодействия для SrchWindow.xaml
    /// </summary>
    public partial class SrchWindow : Window
    {

        /// <summary>
        /// Graph isntance EdgesWindow
        /// </summary>
        /// 

        Graph<int> Graph { get; set; }
        /// <summary>
        /// Create an isnance 
        /// </summary>
        /// <param name="graph">graph</param>
        public SrchWindow(Graph<int> graph)
        {
            this.Graph = graph;
            InitializeComponent();
            VertexOne.ItemsSource = graph.Nodes;
            EdgeOne.ItemsSource = graph.Nodes;
            EdgeTwo.ItemsSource = graph.Nodes;                              
        }
        /// <summary>
        /// Delete node
        /// </summary>
        /// <param name="graph"></param>
        public void DelNode(Graph<int> graph)
        {
            graph.Nodes.Remove((GraphNode<int>)VertexOne.SelectedItem);
            MessageBox.Show("Node was deleted");
            VertexOne.Items.Refresh();
            VertexOne.ItemsSource = Graph.Nodes;
            EdgeOne.Items.Refresh();
            EdgeTwo.Items.Refresh();
        }
        /// <summary>
        /// Delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (EdgeOne.SelectedIndex == EdgeTwo.SelectedIndex || EdgeOne.Text == "" || EdgeTwo.Text == "")
            {
                MessageBox.Show("Error");
            }
            else
            {
                GraphNode<string> node1 = (GraphNode<string>)EdgeOne.SelectedItem;
                GraphNode<string> node2 = (GraphNode<string>)EdgeTwo.SelectedItem;

                if (!node1.Neighbors.Contains(node2))
                {
                    MessageBox.Show("There isn't connection");
                }
                else
                {
                    Graph.RemoveEdge(((GraphNode<int>)EdgeOne.SelectedItem).Value, ((GraphNode<int>)EdgeTwo.SelectedItem).Value);
                    EdgeOne.Items.Refresh();
                    EdgeTwo.Items.Refresh();
                    MessageBox.Show("Edge was deleted");
                }
        
            }
        }
        /// <summary>
        /// Delete edge
        /// </summary>

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DelNode(Graph);
            VertexOne.Text = "";

        }
    }
}
