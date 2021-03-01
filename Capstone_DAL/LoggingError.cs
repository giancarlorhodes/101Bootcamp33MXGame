
namespace Capstone_DAL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// This class is used to do the CRUD functions in the ErrorLogging Table of the DB.
    /// </summary>
    public class LoggingError
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        public bool LogError(string errorName, string errorMsg, string errorSrc) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_LogError", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //parameters: _errorName(NVARCHAR(200)), _errorMsg(NVARCHAR(MAX)), _errorSrc(NVARCHAR(200))
                        command.Parameters.AddWithValue("@parm_errorName", SqlDbType.NVarChar).Value = errorName;
                        command.Parameters.AddWithValue("@parm_errorMsg", SqlDbType.NVarChar).Value = errorMsg;
                        command.Parameters.AddWithValue("@parm_errorSrc", SqlDbType.NVarChar).Value = errorSrc;
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                    connection.Dispose();
                    return true;
                }
            } catch (Exception ex) {
                return false;
                throw ex;
            }
        }
    }
}
