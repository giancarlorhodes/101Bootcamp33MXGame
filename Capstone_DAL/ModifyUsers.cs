

namespace Capstone_DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;

    /// <summary>
    /// This class is used to do the CRUD functions to the Users table in the DB
    /// </summary>
    public class ModifyUsers
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        //Create: Used to add new users to the db. Takes in a username, password 
        public bool AddUser(UsersDO user) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddUsers", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 15;

                        //table parameters: _username(NVARCHAR(500)), _password(NVARCHAR(200))
                        command.Parameters.AddWithValue("@parm_username", SqlDbType.NVarChar).Value = user.Username;
                        command.Parameters.AddWithValue("@parm_password", SqlDbType.NVarChar).Value = user.Password;
                        command.Parameters.AddWithValue("@parm_email", SqlDbType.NVarChar).Value = user.Email;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return true;
            }
            catch(Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return false;
            }
        }

        //Read: Used to get only a single user from the database. Takes a username and password
        public UsersDO GetUser(string username,string password) {
            UsersDO _returnUser = new UsersDO();
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUsers",connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _username(NVARCHAR(200)), _password(NVARCHAR(200))
                        command.Parameters.AddWithValue("@parm_username", SqlDbType.NVarChar).Value = username;
                        command.Parameters.AddWithValue("@parm_password", SqlDbType.NVarChar).Value = password;

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                _returnUser.Username = reader["userName"].ToString();
                                _returnUser.UserID = (int)reader["user_ID"];
                                _returnUser.UserRole = (int)reader["role_FK"];
                                _returnUser.Email = reader["Email"].ToString();
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _returnUser;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return null;
            }
            
        }

        public bool FindUsername(string username) {
            string _username = "";
            bool _returnBool = false;
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_CheckForUsername", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _userName(NVARCHAR)
                        command.Parameters.AddWithValue("@parm_userName", SqlDbType.NVarChar).Value = username;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                _username = reader["userName"].ToString();
                                if (_username == username)
                                {
                                    _returnBool = true;
                                    break;
                                }
                                else {
                                    _returnBool = false;
                                    continue;
                                }
                                
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _returnBool;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return true;
            }
        }

        public List<UsersDO> GetAllUsers() {
            List<UsersDO> _returnList = new List<UsersDO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetUsersList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsersDO user = new UsersDO();
                                user.UserID = (int)reader["user_ID"];
                                user.UserRole = (int)reader["role_FK"];
                                user.Username = reader["userName"].ToString();
                                user.Email = reader["Email"].ToString();
                                _returnList.Add(user);

                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _returnList;
            }
            catch(Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return null;
            }
        }

        //Update: Updates data of a existing user. Takes both values.
        public bool UpdateUser(int userID, UsersDO user) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateUser", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _userID(INT), _newUsername(NVARCHAR(200)), _newPassword(NVARCHAR(200)
                        command.Parameters.AddWithValue("@parm_userID", SqlDbType.Int).Value = userID;
                        command.Parameters.AddWithValue("@parm_newUsername", SqlDbType.NVarChar).Value = user.Username;
                        command.Parameters.AddWithValue("@parm_newPassword", SqlDbType.NVarChar).Value = user.Password;
                        command.Parameters.AddWithValue("@parm_newEmail", SqlDbType.NVarChar).Value = user.Email;
                        command.ExecuteNonQuery(); 
                    }
                    connection.Close();
                    connection.Dispose();
                    return true;
                }
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return false;
            }
        }

        public bool ChangeUserRole(int userID, int roleID) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_ChangeUserRole", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _userID(INT), _newRole(INT)
                        command.Parameters.AddWithValue("@parm_userID", SqlDbType.Int).Value = userID;
                        command.Parameters.AddWithValue("@parm_newRole", SqlDbType.Int).Value = roleID;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return true;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return false;
            }
        }

        //Delete: Removes only a single user from the database. Takes in a userId only
        public bool RemoveUser(int userID) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_RemoveUser",connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Paramters: _userID(NVARCHAR(200))
                        command.Parameters.AddWithValue("@parm_userID", SqlDbType.Int).Value = userID;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return true;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return false;
            }
        }
    }
}
