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
    public class EquipmentDAO : IDAO<Equipment>
    {
        public int Create(Equipment element)
        {
            var id = 0;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"INSERT Equipments (Name,Price,ReleaseYear,GuideId) VALUES (@Name,@Price,@ReleaseYear,@GuideId)";
                query.Parameters.AddWithValue($"@Name", element.Name);
                query.Parameters.AddWithValue($"@Price", element.Price);
                query.Parameters.AddWithValue($"@ReleaseYear", element.ReleaseYear);
                query.Parameters.AddWithValue($"@GuideId", element.GuideId);
                query.ExecuteNonQuery();
                id = (int)query.LastInsertedId;
            }
            catch
            {

            }
            return id;
        }
        public bool Update(Equipment element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"UPDATE Equipments SET Name=@Name, Price=@Price,ReleaseYear=@ReleaseYear,GuideId=@GuideId WHERE Id={element.Id} ";
                query.Parameters.AddWithValue($"@Name", element.Name);
                query.Parameters.AddWithValue($"@Price", element.Price);
                query.Parameters.AddWithValue($"@ReleaseYear", element.ReleaseYear);
                query.Parameters.AddWithValue($"@GuideId", element.GuideId);
                query.ExecuteNonQuery();
            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }

        public bool Delete(Equipment element)
        {
            var operationResult = true;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"DELETE FROM Equipments WHERE Id='{element.Id}'";
                query.ExecuteNonQuery();

            }
            catch
            {
                operationResult = false;
            }
            return operationResult;
        }
        public Equipment GetById(int id)
        {
            Equipment equipment = null;
            MySqlDataReader reader = null;
            try
            {
                var connection = ConnectionSingleton.Instance.GetDBConnection();
                MySqlCommand query = connection.CreateCommand();
                query.CommandText = $"SELECT Name,Price,ReleaseYear,GuideId FROM Equipments WHERE Id='{id}'";
                reader = query.ExecuteReader();
                if (reader.HasRows)
                {
                    var values = new List<object>();
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        values.Add(reader.GetValue(i));
                    }
                    equipment = new Equipment { Id = (int)values[0], Name = (string)values[1],ReleaseYear=(int)values[2],Price=(double)values[3],GuideId=(int)values[4] };
                }
            }
            finally
            {
                reader.Close();
            }
            return equipment;
        }
        public IList<Equipment> GetAll()
        {
            var equipments = new List<Equipment>();
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
                        var equipment = new Equipment { Id = (int)values[0], Name = (string)values[1], ReleaseYear = (int)values[2], Price = (double)values[3], GuideId = (int)values[4] };
                        if (equipment != null)
                        {
                            equipments.Add(equipment);
                        }
                    }

                }
            }
            finally
            {
                reader.Close();
            }
            return equipments;
        }
    }
}
