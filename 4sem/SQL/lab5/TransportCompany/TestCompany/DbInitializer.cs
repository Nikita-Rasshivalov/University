using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace TestCompany
{
    public static class DbInitializer
    {
        /// <summary>
        /// Инициализация Марок
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        /// <returns>result</returns>
        public static string InitializeStamps(string connectionString)
        {
            DateTime today = DateTime.Now.Date;
            Random randObj = new Random();
            int stampValue = 120;
            int stampId;
            string stampName;
            string specification;
            decimal cost;
            string specificity;
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие соединения
                connection.Open();
                string strSql;
                // Собственно заполнение базы данных записями
                // Открытие транзакции для выполнения команд вставки данных
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                try
                {
                    //Название марки
                    string[] stampNames = { "BMV", "Mercedes", "Volvo", "Bentley", "Rolls royce" };

                    //ТТХ
                    string[] specifications = { @"200км\ч", @"220км\ч", @"240км\ч", @"260км\ч", @"280км\ч", @"300км\ч" };
                    //Специфика
                    string[] specificities = { "Бизнес", "Люкс", "Обычныая", "Комфорт" };
                    strSql = "INSERT INTO Stamps (StampName, Specification, Cost, Specificity) VALUES ";
                    for (stampId = 1; stampId <= stampValue; stampId++)
                    {
                        var randomStampName = randObj.Next(0, stampNames.Length);
                        var randomSpecification = randObj.Next(0, specifications.Length);
                        cost = randObj.Next(100, 200);
                        var randomSpecificiti = randObj.Next(0, specificities.Length);

                        stampName = "N'" + stampNames[randomStampName] + "'";
                        specification = "N'" + specifications[randomSpecification] + "'";
                        cost = 50 * cost;
                        specificity = "N'" + specificities[randomSpecificiti] + "'";
                        strSql += "(" + stampName + ", " + specification + ", " + cost.ToString() + ", " + specificity + "), ";
                    }
                    command.CommandText = strSql.TrimEnd(new Char[] { ',', ' ' }) + ";";
                    //отправляет команду на вставку в базу данных
                    command.ExecuteNonQuery();

                    // подтверждаем транзакцию
                    transaction.Commit();
                    Console.WriteLine("Транзакция завершена успешно");
                }
                // Обработка ошибок внутри транзакции
                catch (Exception ex)
                {
                    result = ex.Message;
                    Console.WriteLine(result);
                    Console.WriteLine("Rollback"); ;
                    transaction.Rollback();
                }



            }
            return result;
        }

        /// <summary>
        /// Получение водителей
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<int> GetEmployes(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            List<int> drivers = new List<int>();
            string sql = "SELECT EmployeId FROM Employes INNER JOIN Positions on Positions.PositionId = Employes.PositionId WHERE PositionName = 'Водитель' ";
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    drivers.Add((int)reader[0]);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return drivers;
        }
        /// <summary>
        /// Инициализация автомобилей
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        /// <returns>result</returns>
        public static string InitializeAuto(string connectionString)
        {
            DateTime today = DateTime.Now.Date;
            Random randObj = new Random();
            int stampValue = 120;
            int stampId;
            int autoValue = 1000;
            int AutomobileId;
            string regNumber;
            string win;
            string engineNumber;
            DateTime release;
            int miliage;
            int driverId;
            DateTime lastTI;
            string result = "";
            List<int> drivers = GetEmployes(connectionString);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие соединения
                connection.Open();
                string strSql;
                // Собственно заполнение базы данных записями
                // Открытие транзакции для выполнения команд вставки данных
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                try
                {
                    strSql = "INSERT INTO Automobiles (StampId, RegNumber,WIN,EngineNumber,Release,Mileage,DriverId,LastTI) VALUES";
                    for (AutomobileId = 1; AutomobileId <= autoValue; AutomobileId++)
                    {
                        stampId = randObj.Next(1, stampValue - 1);
                        regNumber = "N'" + randObj.Next(1000, 9999) + " BY" + "'";
                        win = "N'" + "ВИН " + randObj.Next(1000, 9999) + "'";
                        engineNumber = "N'" + "Номер двигателя " + randObj.Next(100, 9999) + "'";
                        release = today.AddDays(-AutomobileId);
                        miliage = randObj.Next(20000, 200000);
                        driverId = drivers[randObj.Next(0, drivers.Count)];
                        lastTI = today.AddDays(-AutomobileId);
                        strSql += "(" + stampId.ToString() + ", " + regNumber + ", " + win + ", " + engineNumber + ", " + "'" + release.ToString() + "'" + ", " + miliage.ToString() + ", " + driverId.ToString() + ", " + "'" + lastTI.ToString() + "'" + "), ";
                    }
                    command.CommandText = strSql.TrimEnd(new Char[] { ',', ' ' }) + ";";
                    //отправляет команду на вставку в базу данных
                    command.ExecuteNonQuery();
                    // подтверждаем транзакцию
                    transaction.Commit();
                    Console.WriteLine("Транзакция завершена успешно");
                }
                // Обработка ошибок внутри транзакции
                catch (Exception ex)
                {
                    result = ex.Message;
                    Console.WriteLine(result);
                    transaction.Rollback();
                }


            }
            return result;
        }

        /// <summary>
        /// Инициализация Должностей
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string InitializePositions(string connectionString)
        {
            Random randObj = new Random();
            int posValue = 120;
            string positionName;
            decimal salary;
            string responsibility;
            string requirements;
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие соединения
                connection.Open();
                    string strSql;
                    // Собственно заполнение базы данных записями
                    // Открытие транзакции для выполнения команд вставки данных
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;
                    try
                    {
                        string[] positions = { "Главбух", "Бухгатлер", "Завбух", "Начальник отдела", "Водитель", "Зав отдела торговли", "Зам.Директора", "Менеджер", "Бизнес аналитик" };
                        string[] responsibilities = { "обязанность1", "обязанность2", "обязанность3", "обязанность4" };
                        string[] requirementss = { "требование1", "требование2", "требование3", "требование4" };
                        strSql = "INSERT INTO Positions (PositionName, Salary,Responsibility,Requirements) VALUES";
                        for (var PosId = 1; PosId <= posValue; PosId++)
                        {

                            positionName = "N'" + positions[randObj.Next(0, positions.Length)] + "'";
                            salary = randObj.Next(300, 1500);
                            responsibility = "N'" + responsibilities[randObj.Next(0, responsibilities.Length)] + "'";
                            requirements = "N'" + requirementss[randObj.Next(0, requirementss.Length)] + "'";

                            strSql += "(" + positionName + ", " + salary.ToString() + ", " + responsibility + ", " + requirements + "), ";
                        }
                        command.CommandText = strSql.TrimEnd(new Char[] { ',', ' ' }) + ";";
                        //отправляет команду на вставку в базу данных
                        command.ExecuteNonQuery();
                        // подтверждаем транзакцию
                        transaction.Commit();

                    }
                    // Обработка ошибок внутри транзакции
                    catch (Exception ex)
                    {
                        result = ex.Message;
                        Console.WriteLine(result);
                        Console.WriteLine("Rollback");
                        transaction.Rollback();
                    }

            }
            return result;
        }
        /// <summary>
        /// Инициализация тарифов
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string InitializeRates(string connectionString)
        {
            Random randObj = new Random();
            int rateValue = 120;
            string rateName;
            decimal cost;
            string specification;
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открытие соединения
                connection.Open();
                    string strSql;
                    // Собственно заполнение базы данных записями
                    // Открытие транзакции для выполнения команд вставки данных
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;
                    try
                    {
                        string[] rates = { "Бизнес", "Супер", "Эконом", "Детский", "ФастФри", "Uber X" };
                        string[] specifications = { "описание1", "описание2", "описание3" };

                        strSql = "INSERT INTO Rates (RateName, Specification,Cost) VALUES";
                        for (var RateId = 1; RateId <= rateValue; RateId++)
                        {
                            rateName = "N'" + rates[randObj.Next(0, rates.Length)] + "'";
                            specification = "N'" + specifications[randObj.Next(0, specifications.Length)] + "'";
                            cost = randObj.Next(300, 500);
                            strSql += "(" + rateName + ", " + specification + ", " + cost.ToString() + "), ";
                        }
                        command.CommandText = strSql.TrimEnd(new Char[] { ',', ' ' }) + ";";
                        //отправляет команду на вставку в базу данных
                        command.ExecuteNonQuery();
                        // подтверждаем транзакцию
                        transaction.Commit();
                        Console.WriteLine("Транзакция завершена успешно");
                    }
                    // Обработка ошибок внутри транзакции
                    catch (Exception ex)
                    {
                        result = ex.Message;
                        Console.WriteLine(result);
                        Console.WriteLine("Rollback");
                        transaction.Rollback();
                    } 
            }
            return result;
        }
    }
}
