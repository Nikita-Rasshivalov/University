using DirectoryOrganizations;
using System;
using System.Windows;
using System.Windows.Input;
namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        /// <summary>
        /// class instance directory
        /// </summary>
        Directory Dir { get; set; }
        /// <summary>
        /// Create add window
        /// </summary>
        /// <param name="directory"></param>
        public AddWindow(Directory directory)
        {
            this.Dir = directory;
            
            InitializeComponent();
        }
        /// <summary>
        /// Add reccord
        /// </summary>
        /// <param name="directory">directory </param>
        public void AddRecord(Directory directory)
        {
            string name = OrgName.Text;
            string type = OrgType.Text;
            string adress = OrgAdress.Text;
            string phone = OrgPhone.Text;
            string emplNums = OrgEmplNums.Text;
            Records record = new Records(name,type,adress,phone,emplNums);
            directory.MakeRecordList(record);
            MessageBox.Show("Success", "Information");
            GetProperties get = new GetProperties(name, type, adress, phone, emplNums);
            get.GetRecords(record);
            get.CreateList();
            this.Close();
        }
        /// <summary>
        /// Barring charracters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgEmplNums_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));

        }

        /// <summary>
        /// Button of add record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddRecord(Dir);
        }
        /// <summary>
        /// Barring charracters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrgPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
