using System.Linq;
using System.Windows;
using Organizations;
using DAO;
using MySQLDB;
using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDAO<OrgType> _typesService = new TypeDAO();
        private IDAO<Organization> _organizationService = new OrganizationDAO();
        public IDAO<Employe> _employeService  = new EmployeDAO();

        public MainWindow()
        {
            InitializeComponent();
            var employes = _employeService.GetAll();
            dataGridEmploye.ItemsSource = employes;
            TypeComboBox.ItemsSource = _typesService.GetAll()?.Select(o => o.TypeName);
            EmplOrgComoBox.ItemsSource = _organizationService.GetAll()?.Select(o => o.OrganizationName);

        }

        private void idOrganizations_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dataGridOrganizations.ItemsSource = _organizationService.GetAll();
        }

        private void idTypes_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            dataGridTypes.ItemsSource = _typesService.GetAll();
        }

        private void AddType_Click(object sender, RoutedEventArgs e)
        {
            OrgType type = new OrgType();
            type.TypeName = TypeBox.Text;
            if (TypeBox.Text == "")
            {
                MessageBox.Show("Заполните данные");
            }
            else
            {
                _typesService.Create(type);
                MessageBox.Show("Успех");
                dataGridTypes.Items.Refresh();
            }
        }

        private void DelType_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridTypes.SelectedItem == null)
            {
                MessageBox.Show("Выберете строку");
            }
            else
            {
                _typesService.Delete((OrgType)dataGridTypes.SelectedItem);
                dataGridTypes.Items.Refresh();
                MessageBox.Show("Успех");
                
            }
        }


        private void DelOrg_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOrganizations.SelectedItem == null)
            {
                MessageBox.Show("Выберете строку");
            }
            else
            {
                _organizationService.Delete((Organization)dataGridOrganizations.SelectedItem);
                dataGridOrganizations.Items.Refresh();
                MessageBox.Show("Успех");
            }
        }

        private void AddOrg_Click(object sender, RoutedEventArgs e)
        {
            Organization org = new Organization();
            if (NameOrgBox.Text == "" && PhoneBox.Text == "" && EmplOrgBox.Text == "" && AdressBox.Text == "")
            {
                MessageBox.Show("Заполните все данные");
            }
            else
            {
                org.OrganizationName = NameOrgBox.Text;
                org.PhoneNumber = Int32.Parse(PhoneBox.Text);
                org.EmployesNumber = Int32.Parse(EmplOrgBox.Text);
                org.Adress = AdressBox.Text;
                org.TypeId = TypeComboBox.SelectedIndex + 1;
                _organizationService.Create(org);              
                MessageBox.Show("Успех");
                dataGridTypes.Items.Refresh();
            }
        }

        private void AddEmplBtn_Click(object sender, RoutedEventArgs e)
        {
            Employe empl = new Employe();

            if (EmplNameBox.Text == "" && SurnameBox.Text == "" && SalaryBox.Text == "" && AgeBox.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                empl.Name = EmplNameBox.Text;
                empl.SecondName = SurnameBox.Text;
                empl.Salary = Int32.Parse(SalaryBox.Text);
                empl.Age = Int32.Parse(AgeBox.Text);
                empl.OrganizationId = EmplOrgComoBox.SelectedIndex + 1;
                _employeService.Create(empl);
                MessageBox.Show("Успех");
                dataGridEmploye.ItemsSource = _employeService.GetAll();
                dataGridEmploye.Items.Refresh();

            }
        }

        private void DelEmplBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEmploye.SelectedItem == null)
            {
                MessageBox.Show("Выбирете строку");
            }
            else
            {
                _employeService.Delete((Employe)dataGridEmploye.SelectedItem);
                MessageBox.Show("Успех");
                dataGridEmploye.ItemsSource = _employeService.GetAll();
                dataGridEmploye.Items.Refresh();
            }
        }

        private void AgeBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SalaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        public int func()
        {
            var s = dataGridOrganizations.SelectedIndex;
            return s;
        }


        private void dataGridOrganizations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s = func();
            new ShowEpl(s).ShowDialog();
        }

    }
}
