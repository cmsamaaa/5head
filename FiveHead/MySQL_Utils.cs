/*
 * A MySQL Utilities and Data Handler External Class for
 * C# ASP.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Text;
using System.Data;

// MySQL Libraries
using MySql.Data.MySqlClient; 

namespace FiveHead.Scripts.Libraries
{
    public class MySQL_Utils
    {
        public MySqlConnection conn_open(String connStr)
        {
            MySqlConnection conn = null;

            if(connStr != "")
            {
                conn = new MySqlConnection(connStr);
            }

            return conn;
        }

        public MySqlCommand cmd_set_connection(MySqlCommand cmd, MySqlConnection conn)
        {
            if (conn != null)
            {
                cmd.Connection = conn;
            }

            return cmd;
        }

        public MySqlDataAdapter adapter_set_query(MySqlCommand cmd)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            return da;
        }

        public void conn_close(MySqlConnection conn)
        {
            conn.Close();
        }
    }
}