
using System.Collections.Generic;
using DAO;
using Organizations;
using MySql.Data.MySqlClient;

namespace MySQLDB
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
                query.CommandText = $"INSERT Organizations (OrganizationName,EmployesNumber,Adress,PhoneNumber,TypeId) VALUES (@OrganizationName,@EmployesNumber,@Adress,@PhoneNumber,@TypeId)";
                query.Parameters.AddWithValue($"@OrganizationName", element.OrganizationName);
                query.Parameters.AddWithValue($"@EmployesNumber", element.EmployesNumber);
                query.Parameters.AddWithValue($"@Adress", element.Adress);
                query.Parameters.AddWithValue($"@PhoneNumber", element.PhoneNumber);
                query.Parameters.AddWithValue($"@TypeId", element.TypeId);
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
                query.CommandText = $"UPDATE Organizations SET Name=@OrganizationName WHERE Id={element.OrganizationId} ";
                query.Parameters.AddWithValue($"@OrganizationName", element.OrganizationName);
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
                query.CommandText = $"DELETE FROM Organizations WHERE OrganizationId='{element.OrganizationId}'";
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
                query.CommandText = $"SELECT OrganizationId,OrganizationName FROM Organizations WHERE Id='{id}'";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    var values = new List<object>();
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                    organization = new Organization { OrganizationId = (int)values[0], OrganizationName = (string)values[1] };
                }
            }
            finally
            {
                reader.Close();
            }
            return organization;
        }
        public List<Organization> GetAll()
        {
            var organizations = new List<Organization>();
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT OrganizationId,OrganizationName,EmployesNumber,Adress,PhoneNumber,TypeId FROM Organizations";
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
                        var organization = new Organization { OrganizationId = (int)values[0], OrganizationName = (string)values[1],
                        EmployesNumber= (int)values[2],Adress= (string)values[3],PhoneNumber= (long)values[4],TypeId= (int)values[5]};
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
