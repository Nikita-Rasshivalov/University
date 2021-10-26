using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQLData
{
    class ConnectionSingleton
    {
        private static readonly Lazy<ConnectionSingleton> instance = new Lazy<ConnectionSingleton>(() => new ConnectionSingleton());
        private readonly MySqlConnection connection = new MySqlConnection("Database=lab7db;Datasource=localhost;User=root;Password=mysql");
        public static ConnectionSingleton Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public MySqlConnection GetDBConnection()
        {
            return connection;
        }
    }
}
