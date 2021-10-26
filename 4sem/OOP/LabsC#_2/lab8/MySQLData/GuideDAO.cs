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
    public class GuideDAO : IDAO<Guide>
    {
        public int Create(Guide element)
        {
            var id = 0;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"INSERT Guides (OrganizationId) VALUES (@OrganizationId)";
                query.Parameters.AddWithValue($"@OrganizationId", element.OrganizationId);
                query.ExecuteNonQuery();
                id = (int)query.LastInsertedId;
            }
            catch
            {

            }
            return id;
        }
        public bool Update(Guide element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"UPDATE Guides SET OrganizationId=@OrganizationId WHERE Id={element.Id} ";
                query.Parameters.AddWithValue($"@OrganizationId", element.OrganizationId);
                query.ExecuteNonQuery();
            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }

        public bool Delete(Guide element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"DELETE FROM Guides WHERE Id='{element.Id}'";
                query.ExecuteNonQuery();

            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }
        public Guide GetById(int id)
        {
            Guide guide = null;
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Id,OrganizationId FROM Guides WHERE Id='{id}'";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    var values = new List<object>();
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                    guide = new Guide { Id = (int)values[0], OrganizationId = (int)values[1] };
                }
            }
            finally
            {
                reader.Close();
            }
            return guide;
        }
        public IList<Guide> GetAll()
        {
            var guides = new List<Guide>();
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Id,OrganizationId FROM Guides";
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
                        var guide = new Guide { Id = (int)values[0], OrganizationId = (int)values[1] };
                        if (guide != null)
                        {
                            guides.Add(guide);
                        }
                    }

                }
            }
            finally
            {
                reader.Close();
            }
            return guides;
        }
    }
}
