﻿/*
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
            MySqlConnection conn = null;

            if(connStr != "")
            {
                conn = new MySqlConnection(connStr);
            }

            return conn;
        }

        public MySqlCommand cmd_set_connection(string cmd, MySqlConnection conn)
        {
            MySqlCommand mySqlCmd = null;

            if (conn != null)
            {
                mySqlCmd = new MySqlCommand(cmd, conn);
            }

            return mySqlCmd;
        }

        public MySqlDataAdapter adapter_set_query(string cmd, MySqlConnection conn)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(cmd, conn);
            return da;
        }

        public void conn_close(MySqlConnection conn)
        {
            conn.Close();
        }
    }
}