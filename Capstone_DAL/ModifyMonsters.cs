

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
    /// Used for doing CRUD functions with the monsters table.
    /// </summary>
    public class ModifyMonsters
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //Create: Adds new monsters to the database.
        public bool AddMonster(MonsterDO monster) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddMonster", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _monsterName(NVARCHAR), _monsterHealth(INT), _monsterArmor(INT), _monsterDamage(INT), _monsterDanger(INT), _monsterBehaviour(INT)
                        command.Parameters.AddWithValue("@parm_monsterName", SqlDbType.NVarChar).Value = monster.monsterName;
                        command.Parameters.AddWithValue("@parm_monsterHealth", SqlDbType.Int).Value = monster.Health;
                        command.Parameters.AddWithValue("@parm_monsterArmor", SqlDbType.Int).Value = monster.Armor;
                        command.Parameters.AddWithValue("@parm_monsterDamage", SqlDbType.Int).Value = monster.Damage;
                        command.Parameters.AddWithValue("@parm_monsterDanger", SqlDbType.Int).Value = monster.Danger;
                        command.Parameters.AddWithValue("@parm_monsterBehaviour", SqlDbType.Int).Value = monster.Behaviour;
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

        //Update: Updates a monsters data.
        public bool UpdateMonster(MonsterDO monster) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateMonster", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _monsterID(INT),_monsterName(NVARCHAR), _monsterHealth(INT), _monsterArmor(INT), _monsterDamage(INT)
                        //_monsterDanger(INT), _monsterBehaviour(INT)
                        command.Parameters.AddWithValue("@parm_monsterID", SqlDbType.Int).Value = monster.MonsterID;
                        command.Parameters.AddWithValue("@parm_monsterName", SqlDbType.Int).Value = monster.monsterName;
                        command.Parameters.AddWithValue("@parm_monsterHealth", SqlDbType.Int).Value = monster.Health;
                        command.Parameters.AddWithValue("@parm_monsterArmor", SqlDbType.Int).Value = monster.Armor;
                        command.Parameters.AddWithValue("@parm_monsterDamage", SqlDbType.Int).Value = monster.Damage;
                        command.Parameters.AddWithValue("@parm_monsterDanger", SqlDbType.Int).Value = monster.Danger;
                        command.Parameters.AddWithValue("@parm_monsterBehaviour", SqlDbType.Int).Value = monster.Behaviour;
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

        //Read: Gets the monster(s) from the database.
        public List<MonsterDO> GetMonsterList() {
            List<MonsterDO> _returnList = new List<MonsterDO>();
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetMonsterList", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                MonsterDO _monster = new MonsterDO();
                                _monster.monsterName = reader["monsterName"].ToString();
                                _monster.Behaviour = (int)reader["monsterBehaviour"];
                                _monster.Health = (int)reader["monsterHealth"];
                                _monster.Armor = (int)reader["monsterArmor"];
                                _monster.Danger = (int)reader["monsterDanger"];
                                _monster.Damage = (int)reader["monsterDamage"];
                                _monster.MonsterID = (int)reader["monster_ID"];
                                _returnList.Add(_monster);
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

        public List<MonsterDO> GetMonstersByDanger(int minDanger, int maxDanger) {
            List<MonsterDO> _returnList = new List<MonsterDO>();

            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetMonstersByDanger", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _minDanger(INT), _maxDanger(INT)
                        command.Parameters.AddWithValue("@parm_minDanger", SqlDbType.Int).Value = minDanger;
                        command.Parameters.AddWithValue("@parm_maxDanger", SqlDbType.Int).Value = maxDanger;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                MonsterDO _monster = new MonsterDO();
                                _monster.MonsterID = (int)reader["monster_ID"];
                                _monster.monsterName = reader["monsterName"].ToString();
                                _monster.Health = (int)reader["monsterHealth"];
                                _monster.Armor = (int)reader["monsterArmor"];
                                _monster.Damage = (int)reader["monsterDamage"];
                                _monster.Danger = (int)reader["monsterDanger"];
                                _monster.Behaviour = (int)reader["monsterBehaviour"];

                                _returnList.Add(_monster);
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

        //Delete: Removes a monster from the datebase. Takes in a monster id;
        public bool RemoveMonster(int monsterID)
        {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_RemoveMonster", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _monsterID
                        command.Parameters.AddWithValue("@parm_monsterID", SqlDbType.Int).Value = monsterID;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return true;
            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}
