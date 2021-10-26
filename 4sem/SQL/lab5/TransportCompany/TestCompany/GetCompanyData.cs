using System;
using System.Data.SqlClient;
using System.Globalization;

namespace TestCompany
{
    public static class GetCompanyData
    {
        /// <summary>
        /// Получение сотрудников
        /// </summary>
        /// <param name="connectionString"></param>
        public static void GetEmploye(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT EmployeId,EmplName,EmplSurname,EmplMiddlename,EmplAge,EmplAdress,EmplPhoneNumber,PassportData,PositionName,Salary FROM Employes INNER JOIN Positions on Positions.PositionId = Employes.PositionId  ";
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"{reader.GetName(0)} {reader.GetName(1)} {reader.GetName(2)} {reader.GetName(3)} {reader.GetName(4)}" +
                  $" {reader.GetName(5)} {reader.GetName(6)} {reader.GetName(7)} {reader.GetName(8)} {reader.GetName(9)}");
                while (reader.Read())
                {
                    Console.WriteLine
                        (
                        reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " +
                        reader[3].ToString() + " " + reader[4].ToString() + " " + reader[5].ToString() + " " +
                        reader[6].ToString() + " " + reader[7].ToString() + " " + reader[8].ToString() + " " + reader[9].ToString()
                        );
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }
        /// <summary>
        /// Получение авто
        /// </summary>
        /// <param name="connectionString"></param>

        public static void GetAuto(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT AutomobileId, StampId,RegNumber,WIN,EngineNumber,Release, Mileage,DriverId,LastTI, EmplName,EmplSurname,PositionId FROM Automobiles INNER JOIN Employes on DriverId = Employes.EmployeId ";
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"Id\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}" +
    $"\t\t{reader.GetName(4)}\t\t{reader.GetName(5)}\t\t{reader.GetName(6)}\t\t{reader.GetName(7)}\t{reader.GetName(8)}\t\t{reader.GetName(9)}\t{reader.GetName(10)}\t{reader.GetName(11)}");
                while (reader.Read())
                {

                    Console.WriteLine(reader[0].ToString() + "\t" + reader[1].ToString() + "\t" + reader[2].ToString() + "\t\t" +
                        reader[3].ToString() + "\t" + reader[4].ToString() + "\t" + ((DateTime)reader[5]).ToString("d") + "\t" +
                        reader[6].ToString() + "\t\t" + reader[7].ToString() + "\t\t" + ((DateTime)reader[8]).ToString("d") + " _____ " + reader[9] + "\t\t" + reader[10] + "\t\t" + reader[11]
                        );
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }
        /// <summary>
        /// Получение сервисов водителей чья фамилия начинается на М
        /// </summary>
        /// <param name="connectionString"></param>
        public static void GetDriversServicesM(string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM dbo.DriversServicesM  ";
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"id\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}" +
                     $"\t{reader.GetName(4)}\t{reader.GetName(5)}\t{reader.GetName(6)}");
                while (reader.Read())
                {

                    Console.WriteLine(reader[0].ToString() + "\t" + ((DateTime)reader[1]).ToString("d") + "\t\t" + reader[2].ToString() + "\t\t"
                        + reader[3].ToString() + "\t" + reader[4].ToString() + "\t" + reader[5].ToString() + "\t\t" + reader[6].ToString());
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }
        /// <summary>
        /// Параметрический запрос для отображения информации о сотрудниках, работающих на определенной должности.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void GetEmplByParam(int id, string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                SqlCommand command = new SqlCommand("SELECT EmplSurname,EmplName,EmplMiddleName,EmplAge,EmplAdress,EmplPhoneNumber,PassportData,PositionName FROM Employes INNER JOIN Positions on Employes.PositionId = Positions.PositionId WHERE Employes.PositionId=@id;", conn);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}" +
                    $"\t{reader.GetName(4)}\t{reader.GetName(5)}\t{reader.GetName(6)}\t{reader.GetName(7)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}   {reader[1]}   {reader[2]}   {reader[3]}   {reader[4]}   {reader[5]}   {reader[6]}   {reader[7]}");
                }

                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Параметрический запрос для отображения информации об автомобилях определенного года выпуска.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connectionString"></param>
        public static void GetAutoByParam(DateTime desiredDate, string connectionString)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Automobiles WHERE YEAR(Release)=YEAR(@desiredDate);", conn);
                command.Parameters.AddWithValue("@desiredDate", desiredDate);

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"Id\t{reader.GetName(1)}\t{reader.GetName(2)}\t{reader.GetName(3)}" +
                    $"\t\t{reader.GetName(4)}\t\t{reader.GetName(5)}\t\t\t{reader.GetName(6)}\t\t{reader.GetName(7)}\t\t{reader.GetName(8)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t\t{reader[3]}\t{reader[4]}\t{reader[5]}\t{reader[6]}\t\t{reader[7]}\t\t{reader[8]}");
                }

                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Добавление авто
        /// </summary>
        /// <param name="сonnectionString"></param>
        public static void AddAuto(int stampId, int driverId, int mileage, string сonnectionString)
        {
            Random randObj = new Random();
            DateTime today = DateTime.Now.Date;
            SqlConnection connection = new SqlConnection(сonnectionString);
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand("dbo.InsertAutomobile", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter StampId = new SqlParameter
                {
                    ParameterName = "@StampId",
                    Value = stampId
                };
                command.Parameters.Add(StampId);
                SqlParameter RegNumber = new SqlParameter
                {
                    ParameterName = "@RegNumber",
                    Value = randObj.Next(1000, 9999) + " BY"
                };
                command.Parameters.Add(RegNumber);
                SqlParameter WIN = new SqlParameter
                {
                    ParameterName = "@WIN",
                    Value = "ВИН " + randObj.Next(1000, 9999)
                };
                command.Parameters.Add(WIN);

                SqlParameter EngineNumber = new SqlParameter
                {
                    ParameterName = "@EngineNumber",
                    Value = "Номер двигателя " + randObj.Next(100, 9999)
                };
                command.Parameters.Add(EngineNumber);

                SqlParameter Release = new SqlParameter
                {
                    ParameterName = "@Release",
                    Value = today.AddDays(randObj.Next(-1500, -200))
                };
                command.Parameters.Add(Release);

                SqlParameter Mileage = new SqlParameter
                {
                    ParameterName = "@Mileage",
                    Value = mileage
                };
                command.Parameters.Add(Mileage);

                SqlParameter DriverId = new SqlParameter
                {
                    ParameterName = "@DriverId",
                    Value = driverId
                };
                command.Parameters.Add(DriverId);

                SqlParameter LastTI = new SqlParameter
                {
                    ParameterName = "@LastTI",
                    Value = today.AddDays(randObj.Next(1, 50))
                };
                command.Parameters.Add(LastTI);
                var result = command.ExecuteScalar();
                //var result = command.ExecuteNonQuery();
                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
        }
        /// <summary>
        /// Удаление авто
        /// </summary>
        /// <param name="automobileId"></param>
        /// <param name="connectionString"></param>
        public static void DelAuto(int automobileId, string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                SqlCommand command = new SqlCommand(" DELETE FROM Automobiles WHERE AutomobileId=@automobileId;", conn);
                command.Parameters.AddWithValue("@automobileId", automobileId);
                Console.WriteLine($"Автомобиль с ID {automobileId} удален.");
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// обновление авто 
        /// </summary>
        /// <param name="automobileId"></param>
        /// <param name="lastTI"></param>
        /// <param name="regNumber"></param>
        /// <param name="mileage"></param>
        /// <param name="connectionString"></param>
        public static void UpdateAuto(int automobileId, DateTime lastTI, string regNumber, int mileage, string connectionString)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                SqlCommand command = new SqlCommand(" UPDATE Automobiles SET RegNumber=@regNumber,Mileage = @mileage,LastTI=@lastTI WHERE AutomobileId=@automobileId;", conn);
                command.Parameters.AddWithValue("@automobileId", automobileId);
                command.Parameters.AddWithValue("@regNumber", regNumber);
                command.Parameters.AddWithValue("@mileage", mileage);
                command.Parameters.AddWithValue("@lastTI", lastTI);
                command.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine($"Автомобиль с Id {automobileId} обновлен.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Средняя стоимость вызова для каждого клиента
        /// </summary>
        /// <param name="connectionString"></param>
        public static void GetAvgCost(string connectionString)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                SqlCommand command = new SqlCommand(
                    "select AVG(Rates.Cost) as AverageCost,EmplName," +
                    "EmplSurname from CompanyServices INNER JOIN Rates ON CompanyServices.RateId = Rates.RateId" +
                    " INNER JOIN Employes ON CompanyServices.OperatorId = Employes.EmployeId " +
                    "Group by OperatorId, EmplName, EmplSurname; ", conn);

                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(1)}\t{reader.GetName(2)}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}\t{reader[1]}\t\t{reader[2]}");
                }

                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Перекрестный запрос
        /// </summary>
        /// <param name="сonnectionString"></param>
        public static void GetDate(string сonnectionString)
        {
            SqlConnection connection = new SqlConnection(сonnectionString);
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand("dbo.CrossVisits", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                ///возврат имени столбцов
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(9)}\t{reader.GetName(10)}\t{reader.GetName(11)}");
                while (reader.Read())
                {

                    Console.WriteLine($"{((DateTime)reader[0]).ToString("d")}\t\t\t{reader[9]}\t\t\t\t{reader[10]}\t\t\t{reader[11]}");
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();

            }

        }
    }
}
