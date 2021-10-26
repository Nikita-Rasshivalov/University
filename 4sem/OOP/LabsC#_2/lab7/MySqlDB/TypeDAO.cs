using System.Collections.Generic;
using DAO;
using Organizations;
using MySql.Data.MySqlClient;
using System.Threading;

namespace MySQLDB
{
    public class TypeDAO : IDAO<OrgType>
    {
        public int Create(OrgType element)
        {
            var id = 0;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"INSERT Types (TypeName) VALUES (@TypeName)";
                query.Parameters.AddWithValue($"@TypeName", element.TypeName);
                query.ExecuteNonQuery();
                id = (int)query.LastInsertedId;
            }
            catch
            {

            }
            return id;
        }
        public bool Update(OrgType element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"UPDATE Types SET TypeName=@TypeName WHERE Id={element.TypeId} ";
                query.Parameters.AddWithValue($"@TypeName", element.TypeId);// возможен казус
                query.ExecuteNonQuery();
            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }

        public bool Delete(OrgType element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"DELETE FROM Types WHERE TypeId='{element.TypeId}'";
                query.ExecuteNonQuery();

            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }
        public OrgType GetById(int id)
        {
            OrgType type = null;
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT TypeId,TypeName FROM Types WHERE Id='{id}'";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    var values = new List<object>();
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                    type = new OrgType { TypeId = (int)values[0], TypeName = (string)values[1] };
                }
            }
            finally
            {
                reader.Close();
            }
            return type;
        }
        public List<OrgType> GetAll()
        {
            var types = new List<OrgType>();
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT TypeId,TypeName FROM Types";
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
                        var type = new OrgType { TypeId = (int)values[0], TypeName = (string)values[1] };
                        if (type != null)
                        {
                            types.Add(type);
                        }
                    }

                }
            }
            finally
            {
                reader?.Close();
            }
            return types;
        }
    }
}
