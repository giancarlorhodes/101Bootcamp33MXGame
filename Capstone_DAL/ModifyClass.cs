

namespace Capstone_DAL
{
    using Capstone_DAL.DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// Used for doing CRUD functions with the Classes table.
    /// </summary>
    public class ModifyClass
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //Read: Gets the class information
        public List<ClassDO> GetClassList() {
            List<ClassDO> _returnList = new List<ClassDO>();

            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetClassList", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                ClassDO _class = new ClassDO();
                                _class.classID = (int)reader["class_ID"];
                                _class.className = reader["className"].ToString();
                                _returnList.Add(_class);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _returnList;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return _returnList;
            }
        }

        public ClassDO GetClassInfo( int classID) {
            try {
                ClassDO _class = new ClassDO();
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetClassInfo", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _classID(INT)
                        command.Parameters.AddWithValue("@parm_classID", SqlDbType.Int).Value = classID;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                _class.classArmor = (int)reader["classArmor"];
                                _class.classStamina = (int)reader["classStamina"];
                                _class.classMagica = (int)reader["classMagica"];
                                _class.classDamage = (int)reader["classDamage"];
                                _class.baseHP = (int)reader["baseHP"];
                                _class.className = reader["className"].ToString();
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _class;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return null;
            }
        }

    }
}
