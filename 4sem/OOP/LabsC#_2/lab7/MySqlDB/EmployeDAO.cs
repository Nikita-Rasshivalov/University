
using System.Collections.Generic;
using DAO;
using Organizations;
using MySql.Data.MySqlClient;

namespace MySQLDB
{
    public class EmployeDAO : IDAO<Employe>
    {
        public int Create(Employe element)
        {
            var id = 0;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"INSERT Employes (Name,Age,Surname,Salary,OrganizationId) VALUES (@Name,@Age,@Surname,@Salary,@OrganizationId)";
                query.Parameters.AddWithValue($"@Name", element.Name);
                query.Parameters.AddWithValue($"@Age", element.Age);
                query.Parameters.AddWithValue($"@Surname", element.SecondName);
                query.Parameters.AddWithValue($"@Salary", element.Salary);
                query.Parameters.AddWithValue($"@OrganizationId", element.OrganizationId);
                query.ExecuteNonQuery();
                id = (int)query.LastInsertedId;
            }
            catch
            {

            }
            return id;
        }
        public bool Update(Employe element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"UPDATE Employes SET Salary=@Salary,Age=@Age,OrganizationId=@OrganizationId WHERE Id={element.Id} ";
                query.Parameters.AddWithValue($"@Name", element.Name);
                query.Parameters.AddWithValue($"@Salary", element.Salary);
                query.Parameters.AddWithValue($"@OrganizationId", element.OrganizationId);
                query.ExecuteNonQuery();
            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }

        public bool Delete(Employe element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"DELETE FROM Employes WHERE Id='{element.Id}'";
                query.ExecuteNonQuery();

            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }
        public Employe GetById(int id)
        {
            Employe employe = null;
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Name,Age,Surname,Salary,OrganizationId FROM Employes WHERE Id='{id}'";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    var values = new List<object>();
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                    employe = new Employe { Id = (int)values[0], Name = (string)values[1], SecondName = (string)values[2], Age = (int)values[3], Salary = (decimal)values[4], OrganizationId = (int)values[5] };
                }
            }
            finally
            {
                reader.Close();
            }
            return employe;
        }
        public List<Employe> GetAll()
        {
            var employes = new List<Employe>();
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Id,Name,Age,Surname,Salary,OrganizationId FROM Employes";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var values = new List<object>();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            values.Add(reader.GetValue(i));
                        }
                        var employe = new Employe { Id = (int)values[0], Name = (string)values[1],Age = (int)values[2],SecondName = (string)values[3],
                            Salary = (decimal)values[4],OrganizationId=(int)values[5]
                        };
                       
                        if (employe != null)
                        {
                            employes.Add(employe);
                        }
                    }

                }
            }
            catch
            {

            }
            finally
            {
                reader?.Close();
            }
            return employes;
        }
    }
}
