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

        }

        /// <summary>
        /// Draw table
        /// </summary>
        public void DrawTable()
        {
            showTable.Items.Clear();
            foreach ( var  dir  in directory.DirectoryList)
            {
                showTable.Items.Add(dir);
                
            }
        }

        /// <summary>
        /// Read file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Do you want to read file?";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.DefaultExt = ".xml";
                    dlg.Filter = "Text documents (.xml)|*.xml;*.txt";
                    bool? res = dlg.ShowDialog();
                    if (res == true)
                    {
                        string path = dlg.FileName;
                        string schemePaath = (@"C:\Users\nikit\Desktop\OOP\LabsC#_2\lab1\Lab1\scheme.xsd");
                        bool validate = XMLValidator.ValidateXml(path, schemePaath);
                        if (validate == false)
                        {
                            MessageBox.Show("The scheme does not match the specified","Error");
                        }
                        else
                        {  
                            directory = DirectoryOrganizations.XMLReader.ReadXML(path);
                            showTable.Items.Clear();
                            DrawTable();
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Not readed");
                    break;
            }
        }
        /// <summary>
        /// Write file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteResult_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Do you want to save file?";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.DefaultExt = ".xml";
                    dlg.Filter = "Text documents (.xml)|*.xml";
                    bool? res = dlg.ShowDialog();
                    if (res == true)
                    {
                        string path = dlg.FileName;

                        DirectoryOrganizations.XMLWriter.WriteXML(directory, path);
                        MessageBox.Show("Success", "Information");
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Not saved");
                    break;
            }

        }
        /// <summary>
        /// Remove record out directory button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (showTable.SelectedItem == null)
            {
                MessageBox.Show("Please, choose element", "information");
            }
            else
            {
                var elem = showTable.SelectedIndex;
                directory.DirectoryList.RemoveAt(elem);
                showTable.Items.Clear();
                DrawTable();
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
    }
}
