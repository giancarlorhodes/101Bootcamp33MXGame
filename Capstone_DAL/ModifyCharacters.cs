

namespace Capstone_DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// This class is used to do CRUD functions to the characters table.
    /// </summary>
    public class ModifyCharacters
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //Create: Adds a created character to the database
        public bool AddCharacter(CharacterDO character, int userID) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddCharacter", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterName(NVARCHAR(200), _characterClass(INT), _characterUser(INT)
                        command.Parameters.AddWithValue("@parm_characterName", SqlDbType.NVarChar).Value = character.Name;
                        command.Parameters.AddWithValue("@parm_characterClass", SqlDbType.Int).Value = character.Class;
                        command.Parameters.AddWithValue("@parm_characterUser", SqlDbType.Int).Value = userID;
                        command.Parameters.AddWithValue("@parm_characterHealth", SqlDbType.Int).Value = character.Health;
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

        //Read: Grabs characters from the database based on either userID or characterId
        public CharacterDO GetCharacter(int characterID) {
            try {

                CharacterDO character = new CharacterDO();
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetCharacter", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        //Parameters: _characterID(INT)
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = characterID;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                 character = new CharacterDO();
                                ModifyClass _class = new ModifyClass();
                                character.Name = reader["characterName"].ToString();
                                character.Class = (int)reader["characterClass_FK"];
                                character.Location = (int)reader["characterLocation_FK"];
                                character.Gold = (int)reader["characterGold"];
                                character.Lvl = (int)reader["characterLvl"];
                                character.Xp = (int)reader["characterXP"];
                                character.Health = (int)reader["characterHealth"];
                                character.maxHP = (int)reader["MaxHP"];
                                character.Armor = (int)reader["Armor"];
                                _class.GetClassInfo( character.Class);
                            }
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return character;
            } catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return null;
            }
        }

        public List<CharacterDO> GetCharacterList(int userID) {
            List<CharacterDO> _returnList = new List<CharacterDO>();
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetCharacterList", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _userID(INT)
                        command.Parameters.AddWithValue("@parm_userID", SqlDbType.Int).Value = userID;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                CharacterDO _character = new CharacterDO();
                                _character.Name = reader["characterName"].ToString();
                                _character.Lvl = (int)reader["characterLvl"];
                                _character.ClassName = reader["className"].ToString();
                                _character.ID = (int)reader["character_ID"];
                                _character.Health = (int)reader["characterHealth"];
                                _character.maxHP = (int)reader["MaxHP"];
                                _character.Xp = (int)reader["characterXP"];
                                _character.Class = (int)reader["class_ID"];

                                _returnList.Add(_character);
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

        //Update:  Changes the character values.
        public bool UpdateCharacterData(CharacterDO character) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateCharacter", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterLocation(INT), _characterGold(INT), _characterLvl(INT), _characterXp(INT), _characterHealth(INT), _characterID(INT)
                        command.Parameters.AddWithValue("@parm_characterLocation", SqlDbType.Int).Value = character.Location;
                        command.Parameters.AddWithValue("@parm_characterGold", SqlDbType.Int).Value = character.Gold;
                        command.Parameters.AddWithValue("@parm_characterLvl", SqlDbType.Int).Value = character.Lvl;
                        command.Parameters.AddWithValue("@parm_characterXp", SqlDbType.Int).Value = character.Xp;
                        command.Parameters.AddWithValue("@parm_characterHealth", SqlDbType.Int).Value = character.Health;
                        command.Parameters.AddWithValue("@parm_maxHealth", SqlDbType.Int).Value = character.maxHP;
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = character.ID;
                        command.Parameters.AddWithValue("@parm_classID", SqlDbType.Int).Value = character.Class;
                        command.Parameters.AddWithValue("@parm_charcterName", SqlDbType.NVarChar).Value = character.Name;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return true;
            }catch (Exception ex) {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message,ex.Source.ToString());
                return false;
            }
        }

        public bool updateUserCharacter(CharacterDO character) {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateUserCharacter", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterLocation(INT), _characterGold(INT), _characterLvl(INT), _characterXp(INT), _characterHealth(INT), _characterID(INT)
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = character.ID;
                        command.Parameters.AddWithValue("@parm_characterName", SqlDbType.NVarChar).Value = character.Name;
                        command.Parameters.AddWithValue("@parm_classID", SqlDbType.Int).Value = character.Class;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                return false;
            }
        }
        //Delete: Removes a single character from the database. Takes a userID and a characterID
        public bool RemoveCharacter(int userID, int characterID) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_RemoveCharacter", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterID(INT), _userID(INT)
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = characterID;
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