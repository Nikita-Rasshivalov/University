using DirectoryOrganizations;
using System.Windows;


namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// class instance directory
        /// </summary>
        Directory directory = new Directory();
        


        /// <summary>
        /// Create main window
        /// </summary>

        public MainWindow()
        {
          
            InitializeComponent();
            DrawTable();
            Records record = new Records(null, null, null, null, null);
            GetProperties get = new GetProperties(null, null, null, null, null);
            get.CallMethods(record);
            XMLWriter read = new XMLWriter();
            read.Serializer();
        }

        /// <summary>
        /// Draw table
        /// </summary>
        public void DrawTable()
        {
            showTable.Items.Clear();
            foreach ( var  dir  in GetProperties.DirList)
            {
                showTable.Items.Add(dir);
                
            }
        }

 
        /// <summary>
        /// Add record button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            AddWindow add = new AddWindow(directory);
            add.ShowDialog();
            DrawTable();
        }

        private void Get_methods_list_Click(object sender, RoutedEventArgs e)
        {
            Records record = new Records(null, null, null, null, null);
            GetProperties get = new GetProperties(null, null, null, null, null);
            var methods =  get.GetMethods(record);
            MessageBox.Show(methods);
        }
    }
}
