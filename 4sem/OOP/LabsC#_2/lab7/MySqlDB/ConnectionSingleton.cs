using System;
using MySql.Data.MySqlClient;

namespace MySQLDB
{
    class ConnectionSingleton
    {
        private static readonly Lazy<ConnectionSingleton> instance = new Lazy<ConnectionSingleton>(() => new ConnectionSingleton());
        private readonly MySqlConnection connection = new MySqlConnection("Database=directions;Datasource=localhost;User=root;Password=zHell323q");
        public static ConnectionSingleton Instance
        {
            get
            {
                return instance.Value;
            }
        }
        public MySqlConnection GetDBConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            return connection;
        }
    }
}
