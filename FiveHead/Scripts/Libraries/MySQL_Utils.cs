/*
 * A MySQL Utilities and Data Handler External Class for
 * C# ASP.NET
 */
using System;

// MySQL Libraries
using MySql.Data.MySqlClient; 

namespace FiveHead.Scripts.Libraries
{
    public class MySQL_Utils
    {
        public MySqlConnection conn_open(string connStr)
        {
            if(!string.IsNullOrEmpty(connStr))
                return new MySqlConnection(connStr);

            return null;
        }

        public MySqlCommand cmd_set_connection(string cmd, MySqlConnection conn)
        {
            if (!conn.Equals(null))
                return new MySqlCommand(cmd, conn);

            return null;
        }

        public MySqlDataAdapter adapter_set_query(string cmd, MySqlConnection conn)
        {
            return new MySqlDataAdapter(cmd, conn);
        }

        public void conn_close(MySqlConnection conn)
        {
            conn.Close();
        }
    }
}