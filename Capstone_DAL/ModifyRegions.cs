

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
    /// Used for doing CRUD funtions to the Regions table in the database. Read funtions only.
    /// </summary>
    public class ModifyRegions
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //Read: Gets all the possible regions from the database.
        public List<RegionDO> GetRegions() {
            List<RegionDO> _returnList = new List<RegionDO>();
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetRegions",connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                RegionDO _region = new RegionDO();

                                _region.RegionName = reader["regionName"].ToString();
                                _region.RegionDanger = (int)reader["regionDanger"];
                                _region.HasShop = (int)reader["hasShop"];
                                _region.RegionID = (int)reader["region_ID"];
                                _region.RegionDesc = reader["regionDesc"].ToString();
                                _returnList.Add(_region);
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

        public RegionDO GetRegionInfo(int regionID) {

            RegionDO _region = new RegionDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetRegionInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        command.Parameters.AddWithValue("@parm_regionID", SqlDbType.Int).Value = regionID;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                _region.RegionDanger = (int)reader["regionDanger"];
                                _region.HasShop = (int)reader["hasShop"];
                                _region.RegionID = (int)reader["region_ID"];
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return _region;
            }
            catch(Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return _region;
            }
        }
    }
}
