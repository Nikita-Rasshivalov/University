using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Guides;
using MySql.Data.MySqlClient;

namespace MySQLData
{
    public class OrganizationDAO : IDAO<Organization>
    {
        public int Create(Organization element)
        {
            var id = 0;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"INSERT Organizations (Name) VALUES (@Name)";
                query.Parameters.AddWithValue($"@Name", element.Name);
                query.ExecuteNonQuery();
                id = (int)query.LastInsertedId;
            }
            catch
            {

            }
            return id;
        }
        public bool Update(Organization element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"UPDATE Organizations SET Name=@Name WHERE Id={element.Id} ";
                query.Parameters.AddWithValue($"@Name", element.Name);
                query.ExecuteNonQuery();
            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }

        public bool Delete(Organization element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"DELETE FROM Organizations WHERE Id='{element.Id}'";
                query.ExecuteNonQuery();

            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }
        public Organization GetById(int id)
        {
            Organization organization = null;
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Id,Name FROM Organizations WHERE Id='{id}'";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    var values = new List<object>();
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                    organization = new Organization { Id = (int)values[0], Name=(string)values[1] };
                }
            }
            finally
            {
                reader.Close();
            }
            return organization;
        }
        public IList<Organization> GetAll()
        {
            var organizations = new List<Organization>();
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Id,Name FROM Organizations";
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
                        var organization = new Organization { Id = (int)values[0], Name = (string)values[1] };
                        if (organization != null)
                        {
                            organizations.Add(organization);
                        }
                    }

                }
            }
            finally
            {
                reader.Close();
            }
            return organizations;
        }
    }
}
