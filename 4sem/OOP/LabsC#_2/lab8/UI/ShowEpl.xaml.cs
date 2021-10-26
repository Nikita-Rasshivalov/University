using DAO;
using MySQLDB;
using Organizations;
using System.Linq;
using System.Windows;


namespace UI
{
    /// <summary>
    /// Логика взаимодействия для ShowEpl.xaml
    /// </summary>
    public partial class ShowEpl : Window
    {
        public IDAO<Employe> _employeService = new EmployeDAO();
        public ShowEpl(int s)
        {
            InitializeComponent();
            var empl = _employeService.GetAll()?.Where(o => o.OrganizationId == s + 1);
            Datagrid.ItemsSource = empl;
            Datagrid.IsReadOnly = true;
        }

    }
}
