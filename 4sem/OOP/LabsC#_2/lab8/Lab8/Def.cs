using System;
using System.Collections.Generic;
using System.Data;
using MySQLDB;
using MySql.Data.MySqlClient;

namespace Default
{
  public  class Def
    {
        DataSet GetDataSet()
        {
            var connection = ConnectionSingleton.Instance.GetDBConnection();
            MySqlCommand query = connection.CreateCommand();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from organizations", connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet,"Org");
            return dataSet;
        }

        public void OrgLoad()
        {
            DataSet dataSet = GetDataSet();
            var result = from org in dataSet.Tables["Org"].AsEnumerable()
                         select new
                         {
                             OrgId = (int)org[0],
                             OrgName = (string)org[1],
                             OrgEmplNum=(int)org[2],
                             Adress = (string)org[3],
                             PhoneNumber=(long)org[4],
                             TypeId=(int)org[5]
                         };

        }
    }
}
