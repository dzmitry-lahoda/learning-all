using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

/// <summary>
/// Static class wich generates connections, commands. 
/// </summary>
public static class Factory
{
    private static string connectionString = WebConfigurationManager.ConnectionStrings["UCDatabaseConnectionString"].ConnectionString;
    //private static string factory = WebConfigurationManager.AppSettings["factory"];
    //private static DbProviderFactory providerFactory = DbProviderFactories.GetFactory(factory);

    static Factory(){}

    public static SqlCommand CreateCommand(String cmdText, CommandType type, SqlConnection connection)
    {
        var command = new SqlCommand(cmdText,connection) { CommandType = type};
        return command;
    }

    public static SqlCommand CreateCommandWithConnection(String cmdText, CommandType type)
    {
        var command = new SqlCommand(cmdText, CreateConnection()) {CommandType = type};
        return command;
    }

    public static SqlCommand CreateCommandWithConnection(String cmdText)
    {
        return CreateCommandWithConnection(cmdText, CommandType.Text);
    }

    /// <summary>
    /// Creates and retruns new connection.
    /// </summary>
    /// <returns></returns>
    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(connectionString);;
    }

}
