using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace ClassLibrary
{
    public class Def
    {
        public DataSet GetDataSet()
        {
            var connection = ConnectionSingleton.Instance.GetDBConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from organizations", connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Org");
            return dataSet;
        }

        public EnumerableRowCollection OrgLoad()
        {
            DataSet dataSet = GetDataSet();
            EnumerableRowCollection result = from org in dataSet.Tables["Org"].AsEnumerable()
                         select new
                         {
                             OrganizationId = (int)org[0],
                             OrganizationName = (string)org[1],
                             EmployesNumber = (int)org[2],
                             Adress = (string)org[3],
                             PhoneNumber = (long)org[4],
                             TypeId = (int)org[5]
                         };
            return result;
        }
    }
}
