using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace SqlConn
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "computer_shop";
            string username = "root";
            string password = "12345678";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
