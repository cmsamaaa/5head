using MySql.Data.MySqlClient;
using System;
using System.Configuration;

/// <summary>
/// Summary description for dbConn
/// </summary>
public class dbConn
{
    public dbConn()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public MySqlConnection GetConnection()
    {
        MySqlConnection dbConn;
        String connString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

        dbConn = new MySqlConnection(connString);

        return dbConn;
    }
}