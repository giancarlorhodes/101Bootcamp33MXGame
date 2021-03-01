

namespace Capstone_DAL
{
    using Capstone_DAL.DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    //For getting roles only. 
    public class ModifyRoles
    {
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        //Read: Gets all the available roles for the project
        public List<RoleDO> GetRoles() {
            List<RoleDO> _returnList = new List<RoleDO>();
            try
            {
                ClassDO _class = new ClassDO();
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetRoles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoleDO role = new RoleDO();
                                role.roleID = (int)reader["role_ID"];
                                role.roleName = reader["roleName"].ToString();
                                _returnList.Add(role);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _returnList;
            }
            catch (Exception ex)
            {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return null;
            }

            
        }
            

    }
}
