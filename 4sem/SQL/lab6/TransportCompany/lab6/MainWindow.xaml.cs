using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Локальное хранилище
        private DataSet ds = new DataSet();
        // Адаптер между локальным хранилищем и базой данных
        SqlDataAdapter dataAdapter;

        // Строка соединения с базой данных
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TransportCompanyConnectionString"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        //Отображает заданную таблицу в заданном DataGrid
        private void DisplayData(DataGrid grid, DataView view)
        {
            grid.ItemsSource = view;
        }

        private void InitializeData()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                ds.Clear();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = command;

                //Заполнение dataset данными из таблиц
                command.CommandText = "select EmployeId,EmplName, EmplSurname,EmplMiddlename," +
                    "EmplAge,EmplAdress,EmplPhoneNumber,PassportData,Positions.PositionName" +
                    " from Employes inner join Positions on Employes.PositionId = Positions.PositionId";

                dataAdapter.Fill(ds, "Employes");

                command.CommandText = "select * from Positions";
                dataAdapter.Fill(ds, "Positions");

                command.CommandText = "select * from Rates";
                dataAdapter.Fill(ds, "Rates");

                command.CommandText = "select * from Stamps";
                dataAdapter.Fill(ds, "Stamps");

                command.CommandText = "select AutomobileId, Stamps.StampName,RegNumber,WIN,EngineNumber," +
                    "Release,Mileage,Employes.EmplName,LastTI from Automobiles " +
                    "inner join Stamps on Automobiles.StampId = Stamps.StampId" +
                    " inner join Employes on Automobiles.DriverId = Employes.EmployeId";
                dataAdapter.Fill(ds, "Automobiles");

                command.CommandText = "select CompanyServicesId,CompanyServicesData,CompanyServicesTime," +
                    "CompanyServicesPhNumber,DeparturePoint,DestinationPoint,RateName,EmplSurname,AutomobileId " +
                    "from CompanyServices inner join Employes on CompanyServices.OperatorId = Employes.EmployeId" +
                    " inner join Rates on CompanyServices.RateId = Rates.RateId";
                dataAdapter.Fill(ds, "CompanyServices");

                //Установка источников для Grid
                DisplayData(dataGridEmploye, ds.Tables["Employes"].DefaultView);
                DisplayData(dataGridPosition, ds.Tables["Positions"].DefaultView);
                DisplayData(dataGridCompanyServices, ds.Tables["CompanyServices"].DefaultView);
                DisplayData(dataGridRates, ds.Tables["Rates"].DefaultView);
                DisplayData(dataGridAutomobiles, ds.Tables["Automobiles"].DefaultView);
                DisplayData(dataGridStamps, ds.Tables["Stamps"].DefaultView);



                //Установка источников для Combobox
                PosComboBox.ItemsSource = ds.Tables["Positions"].DefaultView;
                PosComboBox.DisplayMemberPath = "PositionName";
                PosComboBox.SelectedValuePath = "PositionId";

                ServRateComboBox.ItemsSource = ds.Tables["Rates"].DefaultView;
                ServRateComboBox.DisplayMemberPath = "RateName";
                ServRateComboBox.SelectedValuePath = "RateId";

                ServEmplComboBox.ItemsSource = ds.Tables["Employes"].DefaultView;
                ServEmplComboBox.DisplayMemberPath = "EmplSurname";
                ServEmplComboBox.SelectedValuePath = "EmployeId";

                ServAutoComboBox.ItemsSource = ds.Tables["Automobiles"].DefaultView;
                ServAutoComboBox.DisplayMemberPath = "StampName";
                ServAutoComboBox.SelectedValuePath = "AutomobileId";

                FindPosComboBox.ItemsSource = ds.Tables["Positions"].DefaultView;
                FindPosComboBox.DisplayMemberPath = "PositionName";
                FindPosComboBox.SelectedValuePath = "PositionId";
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void dataGridEmploye_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmployeId").Header = "Код сотрудника";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplName").Header = "Имя";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplSurname").Header = "Фамилия";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplMiddlename").Header = "Отчество";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplAge").Header = "Возраст";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplAdress").Header = "Адрес";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplPhoneNumber").Header = "Номер телефона";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "PassportData").Header = "Паспортные данные";
            dataGridEmploye.Columns.FirstOrDefault(t => t.Header.ToString() == "PositionName").Header = "Должность";
        }
        private void dataGridPosition_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridPosition.Columns.FirstOrDefault(t => t.Header.ToString() == "PositionId").Header = "Код должности";
            dataGridPosition.Columns.FirstOrDefault(t => t.Header.ToString() == "PositionName").Header = "Название";
            dataGridPosition.Columns.FirstOrDefault(t => t.Header.ToString() == "Salary").Header = "Зарплата";
            dataGridPosition.Columns.FirstOrDefault(t => t.Header.ToString() == "Responsibility").Header = "Обязанности";
            dataGridPosition.Columns.FirstOrDefault(t => t.Header.ToString() == "Requirements").Header = "Требование";
        }
        private void dataGridRates_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridRates.Columns.FirstOrDefault(t => t.Header.ToString() == "RateId").Header = "Код тарифа";
            dataGridRates.Columns.FirstOrDefault(t => t.Header.ToString() == "RateName").Header = "Название";
            dataGridRates.Columns.FirstOrDefault(t => t.Header.ToString() == "Specification").Header = "Спецификация";
            dataGridRates.Columns.FirstOrDefault(t => t.Header.ToString() == "Cost").Header = "Цена";
        }
        private void dataGridStamps_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridStamps.Columns.FirstOrDefault(t => t.Header.ToString() == "StampId").Header = "Код марки";
            dataGridStamps.Columns.FirstOrDefault(t => t.Header.ToString() == "StampName").Header = "Название";
            dataGridStamps.Columns.FirstOrDefault(t => t.Header.ToString() == "Specification").Header = "Спецификация";
            dataGridStamps.Columns.FirstOrDefault(t => t.Header.ToString() == "Specificity").Header = "Класс";
            dataGridStamps.Columns.FirstOrDefault(t => t.Header.ToString() == "Cost").Header = "Цена";
        }
        private void dataGridAutomobiles_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "AutomobileId").Header = "Код авто";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "StampName").Header = "Марка";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "RegNumber").Header = "Регистрационный номер";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "WIN").Header = "ВИН";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "EngineNumber").Header = "Номер двигателя";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "Release").Header = "Дата выпуска";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "Mileage").Header = "Пробег";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplName").Header = "Имя водителя";
            dataGridAutomobiles.Columns.FirstOrDefault(t => t.Header.ToString() == "LastTI").Header = "Дата последнего ТО";
        }
        private void dataGridCompanyServices_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "CompanyServicesId").Header = "Код сервиса";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "CompanyServicesData").Header = "Дата";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "CompanyServicesTime").Header = "Время";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "CompanyServicesPhNumber").Header = "Номер";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "DeparturePoint").Header = "Точка отправления";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "DestinationPoint").Header = "Точка прибытия";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "RateName").Header = "Тариф";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "EmplSurname").Header = "Фамилия Оператора";
            dataGridCompanyServices.Columns.FirstOrDefault(t => t.Header.ToString() == "AutomobileId").Header = "Код автомобиля";
        }

        private void EmpAddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание подключения
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Создать команду на добавление с параметрами
                    SqlCommand insertCommand = new SqlCommand
                    {
                        CommandText = "Insert into Employes (EmplName,EmplSurname,EmplMiddlename,EmplAge,EmplAdress,EmplPhoneNumber,PassportData,PositionId)" +
                        " values (@EmplName, @EmplSurname, @EmplMiddlename, @EmplAge, @EmplAdress, @EmplPhoneNumber, @PassportData, @PositionId);" +
                        " SELECT * FROM Employes WHERE EmployeId = SCOPE_IDENTITY();",
                        Connection = conn
                    };
                    insertCommand.Parameters.AddWithValue("@EmplName", NameBox.Text);
                    insertCommand.Parameters.AddWithValue("@EmplSurname", SurnmBox.Text);
                    insertCommand.Parameters.AddWithValue("@EmplMiddlename", MiddleBox.Text);
                    insertCommand.Parameters.AddWithValue("@EmplAge", AgeBox.Text);
                    insertCommand.Parameters.AddWithValue("@EmplAdress", AdressBox.Text);
                    insertCommand.Parameters.AddWithValue("@EmplPhoneNumber", PhoneBox.Text);
                    insertCommand.Parameters.AddWithValue("@PassportData", PassportBox.Text);
                    insertCommand.Parameters.AddWithValue("@PositionId", PosComboBox.SelectedValue);
                    insertCommand.UpdatedRowSource = UpdateRowSource.Both;
                    dataAdapter.InsertCommand = insertCommand;
                    MessageBox.Show("Запись добавлена");
                    DataTable table = ds.Tables["Employes"];
                    DataRow newRow = table.NewRow();
                    table.Rows.Add(newRow);
                    newRow["EmplName"] = NameBox.Text;
                    newRow["EmplSurname"] = SurnmBox.Text;
                    newRow["EmplMiddlename"] = MiddleBox.Text;
                    newRow["EmplAge"] = AgeBox.Text;
                    newRow["EmplAdress"] = AdressBox.Text;
                    newRow["EmplPhoneNumber"] = PhoneBox.Text;
                    newRow["PassportData"] = PassportBox.Text;
                    newRow["PositionName"] = PosComboBox.Text;
                    dataAdapter.Update(ds, "Employes");
                    table.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void EmpDelBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (dataGridEmploye.SelectedItem != null)
            {
                int selectedId = (int)((DataRowView)dataGridEmploye.SelectedItem)["EmployeId"];
                try
                {
                    dataAdapter = new SqlDataAdapter();
                    SqlCommand deleteCommand = new SqlCommand("DELETE FROM Employes WHERE EmployeId = @EmployeId", connection);
                    deleteCommand.Parameters.AddWithValue("@EmployeId", selectedId);
                    dataAdapter.DeleteCommand = deleteCommand;

                    DataTable table = ds.Tables["Employes"];
                    DataRow[] deletedRows = table.Select($"EmployeId = {selectedId}");
                    foreach (DataRow row in deletedRows)
                    {
                        row.Delete();
                    }

                    dataAdapter.Update(table);
                    table.AcceptChanges();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }

            }
        }

        private void ClearEmplBtn_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Text = "";
            SurnmBox.Text = "";
            MiddleBox.Text = "";
            AgeBox.Text = "";
            AdressBox.Text = "";
            PhoneBox.Text = "";
            PassportBox.Text = "";
            PosComboBox.SelectedItem = null;
        }

        private void EmpUpdBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (dataGridEmploye.SelectedItem != null)
            {
                try
                {
                    int selectedId = (int)((DataRowView)dataGridEmploye.SelectedItem)["EmployeId"];
                    dataAdapter = new SqlDataAdapter();
                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.CommandText = "UPDATE Employes " +
                        "SET EmplName=@EmplName, EmplSurname=@EmplSurname,EmplMiddlename=@EmplMiddlename,EmplAge=@EmplAge," +
                        "EmplAdress=@EmplAdress,EmplPhoneNumber=@EmplPhoneNumber,PassportData=@PassportData,PositionId =@PositionId" +
                        " WHERE EmployeId=@EmployeId";
                    updateCommand.Connection = connection;
                    updateCommand.Parameters.AddWithValue("@EmployeId", selectedId);
                    updateCommand.Parameters.AddWithValue("@EmplName", NameBox.Text);
                    updateCommand.Parameters.AddWithValue("@EmplSurname", SurnmBox.Text);
                    updateCommand.Parameters.AddWithValue("@EmplMiddlename", MiddleBox.Text);
                    updateCommand.Parameters.AddWithValue("@EmplAge", AgeBox.Text);
                    updateCommand.Parameters.AddWithValue("@EmplAdress", AdressBox.Text);
                    updateCommand.Parameters.AddWithValue("@EmplPhoneNumber", PhoneBox.Text);
                    updateCommand.Parameters.AddWithValue("@PassportData", PassportBox.Text);
                    updateCommand.Parameters.AddWithValue("@PositionId", PosComboBox.SelectedValue);

                    dataAdapter.UpdateCommand = updateCommand;

                    DataTable table = ds.Tables["Employes"];
                    DataRow updatedRow = table.Select($"EmployeId = {selectedId}").FirstOrDefault();
                    updatedRow["EmplName"] = NameBox.Text;
                    updatedRow["EmplSurname"] = SurnmBox.Text;
                    updatedRow["EmplMiddlename"] = MiddleBox.Text;
                    updatedRow["EmplAge"] = AgeBox.Text;
                    updatedRow["EmplAdress"] = AdressBox.Text;
                    updatedRow["EmplPhoneNumber"] = PhoneBox.Text;
                    updatedRow["PassportData"] = PassportBox.Text;
                    updatedRow["PositionName"] = PosComboBox.Text;
                    dataAdapter.Update(ds, "Employes");
                    table.AcceptChanges();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }
            }
            else
            {
                MessageBox.Show("Select row to update.");
            }
        }

        private void ClearFindBtn_Click(object sender, RoutedEventArgs e)
        {
            FindEmplBoxName.Text = "";
            FindEmplBoxSurname.Text = "";
            DisplayData(dataGridEmploye, ds.Tables["Employes"].DefaultView);
        }

        private void FindEmplBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ds.Tables["Employes"];
            var filterRows = table.AsEnumerable();
            if (FindEmplBoxName.Text != "")
                filterRows = filterRows.Where(row => row.Field<string>("EmplName").ToLower().Contains(FindEmplBoxName.Text.ToLower()));
            if (FindEmplBoxSurname.Text != "")
                filterRows = filterRows.Where(row => row.Field<string>("EmplSurname").ToLower().Contains( FindEmplBoxSurname.Text.ToLower()));
            DisplayData(dataGridEmploye, filterRows.AsDataView());
        }

        private void DelPosBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (dataGridPosition.SelectedItem != null)
            {
                int selectedId = (int)((DataRowView)dataGridPosition.SelectedItem)["PositionId"];
                try
                {
                    dataAdapter = new SqlDataAdapter();
                    SqlCommand deleteCommand = new SqlCommand("DELETE FROM Positions WHERE PositionId = @PositionId", connection);
                    deleteCommand.Parameters.AddWithValue("@PositionId", selectedId);
                    dataAdapter.DeleteCommand = deleteCommand;

                    DataTable table = ds.Tables["Positions"];
                    DataRow[] deletedRows = table.Select($"PositionId = {selectedId}");
                    foreach (DataRow row in deletedRows)
                    {
                        row.Delete();
                    }

                    dataAdapter.Update(table);
                    table.AcceptChanges();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }
            }
        }

        private void AddPosBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание подключения
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Создать команду на добавление с параметрами
                    SqlCommand insertCommand = new SqlCommand
                    {
                        CommandText = "Insert into Positions (PositionName,Salary,Responsibility,Requirements)" +
                        " values (@PositionName, @Salary, @Responsibility, @Requirements);" +
                        " SELECT * FROM Positions WHERE PositionId = SCOPE_IDENTITY();",
                        Connection = conn
                    };
                    insertCommand.Parameters.AddWithValue("@PositionName", PosNameBox.Text);
                    insertCommand.Parameters.AddWithValue("@Salary", PosSalaryBox.Text);
                    insertCommand.Parameters.AddWithValue("@Responsibility", PosRespBox.Text);
                    insertCommand.Parameters.AddWithValue("@Requirements", PosReqBox.Text);
                    insertCommand.UpdatedRowSource = UpdateRowSource.Both;
                    dataAdapter.InsertCommand = insertCommand;
                    MessageBox.Show("Запись добавлена");
                    DataTable table = ds.Tables["Positions"];
                    DataRow newRow = table.NewRow();
                    table.Rows.Add(newRow);
                    newRow["PositionName"] = PosNameBox.Text;
                    newRow["Salary"] = PosSalaryBox.Text;
                    newRow["Responsibility"] = PosRespBox.Text;
                    newRow["Requirements"] = PosReqBox.Text;
                    dataAdapter.Update(ds, "Positions");
                    table.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PosUpdtBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (dataGridPosition.SelectedItem != null)
            {
                try
                {
                    int selectedId = (int)((DataRowView)dataGridPosition.SelectedItem)["PositionId"];
                    dataAdapter = new SqlDataAdapter();
                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.CommandText = "UPDATE Positions " +
                        "SET PositionName=@PositionName, Salary=@Salary,Responsibility=@Responsibility,Requirements=@Requirements" +
                        " WHERE PositionId=@PositionId";

                    updateCommand.Connection = connection;
                    updateCommand.Parameters.AddWithValue("@PositionId", selectedId);
                    updateCommand.Parameters.AddWithValue("@PositionName", PosNameBox.Text);
                    updateCommand.Parameters.AddWithValue("@Salary", PosSalaryBox.Text);
                    updateCommand.Parameters.AddWithValue("@Responsibility", PosRespBox.Text);
                    updateCommand.Parameters.AddWithValue("@Requirements", PosReqBox.Text);

                    dataAdapter.UpdateCommand = updateCommand;
                    DataTable table = ds.Tables["Positions"];

                    DataRow updatedRow = table.Select($" PositionId = {selectedId}").FirstOrDefault();
                    updatedRow["PositionName"] = PosNameBox.Text;
                    updatedRow["Salary"] = PosSalaryBox.Text;
                    updatedRow["Responsibility"] = PosRespBox.Text;
                    updatedRow["Requirements"] = PosReqBox.Text;
                    dataAdapter.Update(ds, "Positions");
                    table.AcceptChanges();

                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }
            }
            else
            {
                MessageBox.Show("Select row to update.");
            }

        }

        private void ClearPosBtn_Click(object sender, RoutedEventArgs e)
        {
            PosNameBox.Text = "";
            PosSalaryBox.Text = "";
            PosRespBox.Text = "";
            PosReqBox.Text = "";
            FindPosComboBox.SelectedItem = null;
            DisplayData(dataGridPosition, ds.Tables["Positions"].DefaultView);

        }

        private void FindPosBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ds.Tables["Positions"];
            var filterRows = table.AsEnumerable();
            if (FindPosComboBox.Text != null)
                filterRows = filterRows.Where(row => row.Field<string>("PositionName") == FindPosComboBox.Text);

            DisplayData(dataGridPosition, filterRows.AsDataView());

        }

        private void DelServBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (dataGridCompanyServices.SelectedItem != null)
            {
                int selectedId = (int)((DataRowView)dataGridCompanyServices.SelectedItem)["CompanyServicesId"];
                try
                {
                    dataAdapter = new SqlDataAdapter();
                    SqlCommand deleteCommand = new SqlCommand("DELETE FROM CompanyServices WHERE CompanyServicesId = @CompanyServicesId", connection);
                    deleteCommand.Parameters.AddWithValue("@CompanyServicesId", selectedId);
                    dataAdapter.DeleteCommand = deleteCommand;

                    DataTable table = ds.Tables["CompanyServices"];
                    DataRow[] deletedRows = table.Select($"CompanyServicesId = {selectedId}");
                    foreach (DataRow row in deletedRows)
                    {
                        row.Delete();
                    }

                    dataAdapter.Update(table);
                    table.AcceptChanges();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }

            }
        }

        private void AddServBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание подключения
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Создать команду на добавление с параметрами
                    SqlCommand insertCommand = new SqlCommand
                    {
                        CommandText = " insert into CompanyServices(CompanyServicesData, CompanyServicesTime, " +
                        "CompanyServicesPhNumber, DeparturePoint, DestinationPoint, RateId, OperatorId, AutomobileId)" +
                        " values(@CompanyServicesData, @CompanyServicesTime, @CompanyServicesPhNumber, @DeparturePoint," +
                        " @DestinationPoint, @RateId, @OperatorId, @AutomobileId)" +
                        " SELECT * FROM CompanyServices WHERE CompanyServicesId = SCOPE_IDENTITY();",
                        Connection = conn
                    };
                    insertCommand.Parameters.AddWithValue("@CompanyServicesData", ServDatePick.SelectedDate);
                    insertCommand.Parameters.AddWithValue("@CompanyServicesTime", ServTime.Text);
                    insertCommand.Parameters.AddWithValue("@CompanyServicesPhNumber", ServNumber.Text);
                    insertCommand.Parameters.AddWithValue("@DeparturePoint", ServPointOne.Text);
                    insertCommand.Parameters.AddWithValue("@DestinationPoint", ServPointTwo.Text);
                    insertCommand.Parameters.AddWithValue("@RateId", ServRateComboBox.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@OperatorId", ServEmplComboBox.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@AutomobileId", ServAutoComboBox.SelectedValue);
                    insertCommand.UpdatedRowSource = UpdateRowSource.Both;

                    dataAdapter.InsertCommand = insertCommand;
                    MessageBox.Show("Запись добавлена");

                    DataTable table = ds.Tables["CompanyServices"];
                    DataRow newRow = table.NewRow();
                    table.Rows.Add(newRow);

                    newRow["CompanyServicesData"] = ServDatePick.Text;
                    newRow["CompanyServicesTime"] = ServTime.Text;
                    newRow["CompanyServicesPhNumber"] = ServNumber.Text;
                    newRow["DeparturePoint"] = ServPointOne.Text;

                    newRow["DestinationPoint"] = ServPointTwo.Text;
                    newRow["RateName"] = ServRateComboBox.Text;
                    newRow["EmplSurname"] = ServEmplComboBox.Text;
                    newRow["AutomobileId"] = ServAutoComboBox.SelectedValue;

                    dataAdapter.Update(ds, "CompanyServices");
                    table.AcceptChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void UpdtServBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (dataGridCompanyServices.SelectedItem != null)
            {
                try
                {
                    int selectedId = (int)((DataRowView)dataGridCompanyServices.SelectedItem)["CompanyServicesId"];
                    dataAdapter = new SqlDataAdapter();
                    SqlCommand updateCommand = new SqlCommand();
                    updateCommand.CommandText = "UPDATE CompanyServices " +
                        "SET CompanyServicesData=@CompanyServicesData, CompanyServicesTime=@CompanyServicesTime," +
                        "CompanyServicesPhNumber=@CompanyServicesPhNumber,DeparturePoint=@DeparturePoint" +
                         "DestinationPoint=@DestinationPoint,RateId=@RateId, OperatorId=@OperatorId, AutomobileId=@AutomobileId" +
                        " WHERE CompanyServicesId=@CompanyServicesId";

                    updateCommand.Connection = connection;
                    updateCommand.Parameters.AddWithValue("@CompanyServicesData", ServDatePick.SelectedDate);
                    updateCommand.Parameters.AddWithValue("@CompanyServicesTime", ServTime.Text);
                    updateCommand.Parameters.AddWithValue("@CompanyServicesPhNumber", ServNumber.Text);
                    updateCommand.Parameters.AddWithValue("@DeparturePoint", ServPointOne.Text);
                    updateCommand.Parameters.AddWithValue("@DestinationPoint", ServPointTwo.Text);
                    updateCommand.Parameters.AddWithValue("@RateId", ServRateComboBox.SelectedValue);
                    updateCommand.Parameters.AddWithValue("@OperatorId", ServEmplComboBox.SelectedValue);
                    updateCommand.Parameters.AddWithValue("@AutomobileId", ServAutoComboBox.SelectedValue);

                    dataAdapter.UpdateCommand = updateCommand;
                    DataTable table = ds.Tables["CompanyServices"];

                    DataRow updatedRow = table.Select($" CompanyServicesId = {selectedId}").FirstOrDefault();
                    updatedRow["CompanyServicesData"] = ServDatePick.Text;
                    updatedRow["CompanyServicesTime"] = ServTime.Text;
                    updatedRow["CompanyServicesPhNumber"] = ServNumber.Text;
                    updatedRow["DeparturePoint"] = ServPointOne.Text;
                    updatedRow["DestinationPoint"] = ServPointTwo.Text;
                    updatedRow["RateName"] = ServRateComboBox.Text;
                    updatedRow["EmplSurname"] = ServEmplComboBox.Text;
                    updatedRow["AutomobileId"] = ServAutoComboBox.SelectedValue;
                    dataAdapter.Update(ds, "Positions");
                    table.AcceptChanges();

                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }
            }
            else
            {
                MessageBox.Show("Select row to update.");
            }

        }

        private void FindService_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ds.Tables["CompanyServices"];
            var filterRows = table.AsEnumerable();
            if (FindDatePicker.Text != null)
                filterRows = filterRows.Where(row => row.Field<System.DateTime>("CompanyServicesData").Equals(FindDatePicker.SelectedDate));

            DisplayData(dataGridCompanyServices, filterRows.AsDataView());
        }


        private void ClearServ_Click(object sender, RoutedEventArgs e)
        {
            ServDatePick.Text = "";
            ServTime.Text = "";
            ServNumber.Text = "";
            ServPointOne.Text = "";
            ServPointTwo.Text = "";
            ServRateComboBox.Text = "";
            ServEmplComboBox.Text = "";
            ServAutoComboBox.SelectedValue = null;
            DisplayData(dataGridCompanyServices, ds.Tables["CompanyServices"].DefaultView);
        }
    }
}
