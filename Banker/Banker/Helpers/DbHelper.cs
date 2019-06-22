using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Banker.Helpers
{
    public static class DbHelper
    {
        public static int ExecuteNonQuery(string connectionString, string commandText, Dictionary<string, object> parameters)
        {
            int ret = 0;

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = connectionString;
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = commandText;

                    foreach (var item in parameters)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = item.Key;
                        parameter.Value = item.Value;
                        command.Parameters.Add(parameter);
                    }

                    try
                    {
                        ret = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }                   
                }
            }

            return ret;
        }

        public static SqlDataReader ExecuteReader(SqlConnection sqlConnection, string connectionString, string commandText, Dictionary<string, object> parameters)
        {
            SqlDataReader ret = null;

            sqlConnection.ConnectionString = connectionString;
            sqlConnection.Open();
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = commandText;

                foreach (var item in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = item.Key;
                    parameter.Value = item.Value;
                    command.Parameters.Add(parameter);
                }

                try
                {
                    ret = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            return ret;
        }
    }
}