

namespace Capstone_DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using System.Data;
    using Capstone_DAL.DataObjects;
    using System.Configuration;

    /// <summary>
    /// This table is for doing CRUD Functions to the Inventory table
    /// </summary>
    public class ModifyInventories
    {
        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string _connection = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //Create: Adds a new entry to a specific characters inventory. Takes  a character and item ID
        public bool AddToInventory(int characterID, int itemID) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddToInventory", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterID(INT), _itemID(INT)
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = characterID;
                        command.Parameters.AddWithValue("@parm_itemID", SqlDbType.Int).Value = itemID;
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

        //Read: Gets all items from a character inventory.
        public List<ItemDO> GetCharacterInventory(int characterID)
        {
            List<ItemDO> _returnList = new List<ItemDO>();
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetCharacterInventory", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterID(INT)
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = characterID;
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                ItemDO _item = new ItemDO();
                                _item.ItemID = (int)reader["item_FK"];
                                _item.ItemName = reader["itemName"].ToString();
                                _item.itemType = (int)reader["itemType"];
                                _item.HealthMod = (int)reader["healthModifier"];
                                _item.staminaMod = (int)reader["staminaModifier"];
                                _item.MagicaMod = (int)reader["magicaModifier"];
                                _item.goldPrice = (int)reader["goldPrice"];
                                _item.InventoryID = (int)reader["inventory_ID"];
                                _item.isEquipted = (int)reader["isEquipted"];
                                _item.ArmorMod = (int)reader["armorMod"];
                                _item.DamageMod = (int)reader["damageMod"];
                                _returnList.Add(_item);
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
        public List<ItemDO> GetItemsList()
        {
            List<ItemDO> _returnList = new List<ItemDO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetItemsList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemDO _item = new ItemDO();
                                _item.ItemName = reader["itemName"].ToString();
                                _item.ItemID = (int)reader["item_ID"];
                                _item.itemType = (int)reader["itemType"];
                                _item.armorType = (int)reader["armorType"];
                                _item.weaponType = (int)reader["weaponType"];
                                _item.HealthMod = (int)reader["healthModifier"];
                                _item.staminaMod = (int)reader["staminaModifier"];
                                _item.MagicaMod = (int)reader["magicaModifier"];
                                _item.goldPrice = (int)reader["goldPrice"];
                                _item.ArmorMod = (int)reader["armorMod"];
                                _item.DamageMod = (int)reader["damageMod"];
                                _returnList.Add(_item);
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
                return _returnList;
            }
        }

        //Update: Used only to update the equipted status of certaion items
        public void EquiptItem(int inventoryID, int isEquipted) {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateInventoryItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        command.Parameters.AddWithValue("@parm_inventoryID", SqlDbType.Int).Value = inventoryID;
                        command.Parameters.AddWithValue("@parm_isEquipted", SqlDbType.Int).Value = isEquipted;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    connection.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                LoggingError error = new LoggingError();
                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
                
            }
        }

        //Delete: Removes  entry(s) from the inventory table.
        public bool ClearCharacterInventory(int characterID) {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_ClearInventory", connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _characterID(INT)
                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = characterID;
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

        public bool RemoveItemFromInventory(int inventoryID)
        {
            try {
                using (SqlConnection connection = new SqlConnection(_connection)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_RemoveItemFromInventory",connection)) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;

                        //Parameters: _inventoryID(INT)
                        command.Parameters.AddWithValue("@parm_inventoryID", SqlDbType.Int).Value = inventoryID;
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
