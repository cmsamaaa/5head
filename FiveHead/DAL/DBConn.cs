﻿using FiveHead.Scripts.Libraries;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace FiveHead.DAL
{
    /// <summary>
    /// MySQL database connector
    /// </summary>
    public class DBConn
    {
        MySQL_Utils mySQL = new MySQL_Utils();

        public MySqlConnection GetConnection()
        {
            MySqlConnection dbConn;
            String connString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            dbConn = mySQL.conn_open(connString);

            return dbConn;
        }

        public void CloseConnection(MySqlConnection conn)
        {
            conn.Close();
        }
    }
}