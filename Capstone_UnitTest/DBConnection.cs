

//namespace Capstone_UnitTest
//{
//    using Capstone_DAL;
//    using Capstone_DAL.DataObjects;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using System;
//    using System.Collections.Generic;
//    using System.Data;
//    using System.Data.SqlClient;

//    [TestClass]
//    public class DBConnection
//    {
//        //string _connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Initial Catalog=Capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
//        string _connection = "Server=LAPTOP-401;Database=Library;Trusted_Connection=True; providerName=System.Data.SqlClient";

//        /// <summary>
//        /// ErrorLogging Table testing
//        /// </summary>

//        //Create test: Testing the ability to log new errors into the ErrorLogging table
//        [TestMethod]
//        public void CanLogsErrors()
//        {
//            string testErrorName = "Error";
//            string testErrorMessage = "Message";
//            string testErrorSrc = "Source";
//            LoggingError error = new LoggingError();

//            bool success = error.LogError(testErrorName, testErrorMessage, testErrorSrc);

//            Assert.IsTrue(success);

//            DeleteTestError(testErrorName);

//        }

//        //Testing purposes only. Used to delete the entered value from test. 
//        private void DeleteTestError(string errorName) {
//            try {
//                using (SqlConnection connection = new SqlConnection(_connection)) {
//                    connection.Open();

//                    using (SqlCommand command = new SqlCommand("SP_RemoveError", connection)) {
//                        command.CommandType = CommandType.StoredProcedure;
//                        command.CommandTimeout = 10;

//                        //Parameters: _errorName(NVARCHAR(200)
//                        command.Parameters.AddWithValue("@parm_errorName", SqlDbType.NVarChar).Value = errorName;
//                        command.ExecuteNonQuery();
//                    }

//                    connection.Close();
//                    connection.Dispose();
//                }
//            } catch (Exception ex) {
//                LoggingError error = new LoggingError();
//                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
//            }
//        }

//        //ModifyUsers class connections testing.

//            //Create test: Testing the ability to add new users. Should always pass.
//        [TestMethod]
//        public void CanAddUser() {
//            string testUsername = "User";
//            string testPassword = "Password";
//            string testEmail = "Email";
//            ModifyUsers _user = new ModifyUsers();
//            UsersDO _test = new UsersDO();
//            _test.Username = testUsername;
//            _test.Password = testPassword;
//            _test.Email = testEmail;
//            string result;

//            bool success = _user.AddUser(_test);
//            _test = _user.GetUser(testUsername, testPassword);
//            result = _test.Username;

//            Assert.IsTrue(success);

//            //Used only to remove the testing value from the database
//            _user.RemoveUser(_test.UserID);
//        }

//            //Read test: Testing the ability to pull and read data from the Users table.
//        [TestMethod]
//        public void CandReadUser() {
//            ModifyUsers _user = new ModifyUsers();
//            UsersDO _test = new UsersDO() ;
//            string test;
//            _test.Username = "User1";
//            _test.Password = "Password";
//            _test.Email = "None";
//            _user.AddUser(_test);
//            _test = _user.GetUser("User1", "Password");
//            test = _test.Username;

//            Assert.AreEqual("User1", test);

//            _user.RemoveUser(_test.UserID);
//        }

//        [TestMethod]
//        public void CanFindUsername() {
//            ModifyUsers _user = new ModifyUsers();

//            UsersDO _test = new UsersDO();
//            _test.Username = "User1";
//            _test.Password = "Password";
//            _test.Email = "None";
//            _user.AddUser(_test);
//            _test = _user.GetUser("User1", "Password");

//            bool success = _user.FindUsername(_test.Username);

//            Assert.IsTrue(success);

//            _user.RemoveUser(_test.UserID);
//        }

//            //Update test: Testing the ability to update User info 
//        [TestMethod]
//        public void CanUpdateUser() {
//            string testName = "Test";
//            string testPass = "Pass";
//            string testEmail = "Email";
//            ModifyUsers user = new ModifyUsers();
//            UsersDO _user = new UsersDO();
//            _user.Username = testName;
//            _user.Password = testPass;

//            user.AddUser(_user);
//            _user = user.GetUser("User", "Test");

//            bool success = user.UpdateUser(_user.UserID, _user);
//            _user = user.GetUser(testName, testPass);

//            Assert.IsTrue(success);

//            user.RemoveUser(_user.UserID);
//        }

//        [TestMethod]
//        public void CanChangeRole() {
//            ModifyUsers user = new ModifyUsers();
//            UsersDO _user = new UsersDO();
//            _user.Username = "Test";
//            _user.Password = "Pass";
//            user.AddUser(_user);
//            _user = user.GetUser("Test", "Pass");
//            bool success = user.ChangeUserRole(_user.UserID, 1);

//            Assert.IsTrue(success);

//            user.RemoveUser(_user.UserID);
//        }

//            //Delete test: Testing the ability to remove a existing user from the Users table.
//        [TestMethod]
//        public void CanRemoveUser() {
//            ModifyUsers _user = new ModifyUsers();
//            UsersDO _test = new UsersDO();
//            _test.Username = "Test";
//            _test.Password = "Pass";
//            //creates a new users for testing purposes
//            _user.AddUser(_test);
//            _test = _user.GetUser("Test","Pass");

//            bool success = _user.RemoveUser(_test.UserID);

//            Assert.IsTrue(success);
//        }

//        //ModifyCharacters class connection testing

//            //Create test: Testing the ability to create a new character and add it to the database.
//        [TestMethod]
//        public void CanAddCharacter() {
//            CharacterDO _character = new CharacterDO();
//            ModifyCharacters _test = new ModifyCharacters();
//            _character.Name = "Test";
//            _character.Class = 1;
            
//            bool success = _test.AddCharacter(_character, 2);

//            Assert.IsTrue(success);
//            RemoveTestCharacter(_character.Name);
//        }

//            //Read test: Testing the ability to grab one or more characters from the database.
//        [TestMethod]
//        public void CanReadCharacter() {
//            ModifyCharacters _character = new ModifyCharacters();
//            CharacterDO _test = new CharacterDO();
//            int id;
//            _test.Name = "Test";
//            _test.Class = 0;
//            _character.AddCharacter(_test, 0);
//            id = GetTestCharacterID(_test.Name);

//            CharacterDO success = _character.GetCharacter(id);

//            Assert.AreNotEqual(null, success);
//            RemoveTestCharacter(_test.Name);
//        }

//        [TestMethod]
//        public void CanReadCharacterList() {
//            ModifyCharacters _character = new ModifyCharacters();
//            CharacterDO _test = new CharacterDO();
//            _test.Name = "Test";
//            _test.Class = 0;
//            bool success;

//            _character.AddCharacter(_test, 0);
//            List<CharacterDO> test = _character.GetCharacterList(2);

//            if (test.Count >= 1)
//            {
//                success = true;
//            }
//            else {
//                success = false;
//            }
//            Assert.IsTrue(success);

//            RemoveTestCharacter(_test.Name);
//        }

//            //Update test: testing the ability to updatea character info in the datebase
//        [TestMethod]
//        public void CanUpdateCharacter() {
//            ModifyCharacters _character = new ModifyCharacters();
//            CharacterDO _test = new CharacterDO();
//            _test.Name = "Test";
//            _test.Class = 0;

//            _character.AddCharacter(_test,0);
//            bool success = _character.UpdateCharacterData(_test);

//            Assert.IsTrue(success);
//            RemoveTestCharacter(_test.Name);
//        }

//            //Delete test: Testing the ability to delete characters.
//        [TestMethod]
//        public void CanRemoveCharacter() {
//            ModifyCharacters _character = new ModifyCharacters();
//            CharacterDO _test = new CharacterDO();
//            _test.Name = "Test";
//            _test.Class = 0;

//            _character.AddCharacter(_test, 0);
//            int id = GetTestCharacterID(_test.Name);

//            bool success = _character.RemoveCharacter(0, id);

//            Assert.IsTrue(success);
//        }

//            //Testing purposes only. Gets the character ID.
//        private int GetTestCharacterID(string characterName) {
//            int _returnInt = 0;
//            try {
//                using (SqlConnection connection = new SqlConnection(_connection)) {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand("SP_GetTestCharacterID", connection)) {
//                        command.CommandType = CommandType.StoredProcedure;
//                        command.CommandTimeout = 10;

//                        //Parameters: _characterName(NVARCHAR(200)
//                        command.Parameters.AddWithValue("@parm_characterName", SqlDbType.NVarChar).Value = characterName;
//                        using (SqlDataReader reader = command.ExecuteReader()) {
//                            while (reader.Read()) {
//                                _returnInt = (int)reader["character_ID"];
//                            }
//                        }
//                    }
//                    connection.Close();
//                    connection.Dispose();
//                }
//                return _returnInt;
//            } catch (Exception ex) {
//                LoggingError error = new LoggingError();
//                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
//                return _returnInt;
//            }

//        }

//            //Testing purposes only. Removes the test value character(s) from the database.
//        private void RemoveTestCharacter(string characterName) {
//            try {
//                using (SqlConnection connection = new SqlConnection(_connection)) {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand("SP_RemoveTestCharacters", connection)) {
//                        command.CommandType = CommandType.StoredProcedure;
//                        command.CommandTimeout = 10;

//                        //Parameters: _characterName(NVARCHAR(200)
//                        command.Parameters.AddWithValue("@parm_characterName", SqlDbType.NVarChar).Value = characterName;
//                        command.ExecuteNonQuery();
//                    }
//                    connection.Close();
//                    connection.Dispose();
//                }
//            } catch (Exception ex) {
//                LoggingError error = new LoggingError();
//                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
//            }
//        }

//        //ModifyInventories class connection testing

//            //Create test: Testing the ability to add new inventory
//        [TestMethod]
//        public void CanAddToInventory() {
//            ModifyInventories _inventory = new ModifyInventories();
//            bool success = _inventory.AddToInventory(2,0);

//            Assert.IsTrue(success);

//            _inventory.ClearCharacterInventory(24);
//        }

//            //Read test: Testing the ability to get the character inventory.
//        [TestMethod]
//        public void CanGetInventory() {
//            ModifyInventories _inventory = new ModifyInventories();
//            bool success;

//            _inventory.AddToInventory(2, 0);
//            List<ItemDO> _list = _inventory.GetCharacterInventory(2);

//            if (_list.Count >= 1)
//            {
//                success = true;
//            }
//            else {
//                success = false;
//            }
//            Assert.IsTrue(success);

//            _inventory.ClearCharacterInventory(2);
//        }

//            //Delete test: Testing the ability remove item(s) from character inventory
//        [TestMethod]
//        public void CanClearInventory() {
//            ModifyInventories _inventory = new ModifyInventories();

//            bool success = _inventory.ClearCharacterInventory(24);

//            Assert.IsTrue(success);
//        }

//        [TestMethod]
//        public void CanRemoveItem() {
//            ModifyInventories _inventory = new ModifyInventories();

//            _inventory.AddToInventory(24, 0);
//            int ID = GetInventoryID(24, 0);
//            bool success = _inventory.RemoveItemFromInventory(ID);

//            Assert.IsTrue(success);
//        }

//        //Testing purposes only. Gets a specific inventory ID
//        private int GetInventoryID(int characterID, int itemID) {
//            int _returnInt = 0;

//            try {
//                using (SqlConnection connection = new SqlConnection(_connection)) {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand("SP_GetInventoryID", connection)) {
//                        command.CommandType = CommandType.StoredProcedure;
//                        command.CommandTimeout = 10;

//                        //Parameters: _characterID(INT), _itemID(INT)
//                        command.Parameters.AddWithValue("@parm_characterID", SqlDbType.Int).Value = characterID;
//                        command.Parameters.AddWithValue("@parm_itemID", SqlDbType.Int).Value = itemID;
//                        using (SqlDataReader reader = command.ExecuteReader()) {
//                            while (reader.Read()) {
//                                _returnInt = (int)reader["Inventory_ID"];
//                            }
//                        }
//                    }
//                    connection.Close();
//                    connection.Dispose();
//                }
//                return _returnInt;
//            } catch (Exception ex) {
//                LoggingError error = new LoggingError();
//                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
//                return 0;
//            }
//        }

//        //ModifyRegions class connection testing

//        //Read test: Testing the ability to grab regions.
//        [TestMethod]
//        public void CanGetRegionsList() {
//            ModifyRegions _region = new ModifyRegions();
//            bool success;

//            List<RegionDO> _test = _region.GetRegions();

//            if (_test.Count >= 1)
//            {
//                success = true;
//            }
//            else {
//                success = false;
//            }

//            Assert.IsTrue(success);
//        }

//        //ModifyClass class connection testing.

//                //Read test: Testing the ability to get class(es) from the database.
//        [TestMethod]
//        public void CanGetClassList() {
//            ModifyClass _class = new ModifyClass();
//            bool success;

//            List<ClassDO> _test = _class.GetClassList();
//            if (_test.Count >= 1)
//            {
//                success = true;
//            }
//            else {
//                success = false;
//            }

//            Assert.IsTrue(success);
//        }

      
//        //ModifyMonsters class connection testing

//            //Create test: Testing the ability to add new monsters to the database.
//        [TestMethod]
//        public void CanAddMonster() {
//            ModifyMonsters _monster = new ModifyMonsters();
//            MonsterDO _test = new MonsterDO();
//            _test.monsterName = "Test";
//            _test.Health = 0;
//            _test.Armor = 0;
//            _test.Damage = 0;
//            _test.Danger = 0;
//            _test.Behaviour = 0;

//            bool success = _monster.AddMonster(_test);

//            Assert.IsTrue(success);
//            RemoveTestMonster(_test.monsterName);
//        }

//        //Read test: Testing the ability to get monster(s) from the database.
//        [TestMethod]
//        public void CanGetMonsters() {
//            ModifyMonsters _monster = new ModifyMonsters();
//            MonsterDO _test = new MonsterDO();
//            bool success;
//            _test.monsterName = "Test";
//            _test.Health = 0;
//            _test.Armor = 0;
//            _test.Damage = 0;
//            _test.Danger = 0;
//            _test.Behaviour = 0;

//            _monster.AddMonster(_test);
//            List<MonsterDO> _list = _monster.GetMonsterList();
//            if (_list.Count >= 1)
//            {
//                success = true;
//            }
//            else {
//                success = false;
//            }

//            Assert.IsTrue(success);

//            RemoveTestMonster(_test.monsterName);

//        }

//        [TestMethod]
//        public void CanGetMonstersByDanger() {
//            ModifyMonsters _monster = new ModifyMonsters();
//            bool success;

//            List<MonsterDO> _test = _monster.GetMonstersByDanger(0,1);

//            if (_test.Count >= 1)
//            {
//                success = true;
//            }
//            else {
//                success = false;
//            }

//            Assert.IsTrue(success);
//        }


//            //Update test: Testing the ability to update monsters.
//        [TestMethod]
//        public void CanUpdateMonster() {
//            ModifyMonsters _monster = new ModifyMonsters();
//            MonsterDO _test = new MonsterDO();
//            _test.monsterName = "Test";
//            _test.Health = 0;
//            _test.Armor = 0;
//            _test.Damage = 0;
//            _test.Danger = 0;
//            _test.Behaviour = 0;

//            _monster.AddMonster(_test);
//            _test.monsterName = "Update";
//            bool success = _monster.UpdateMonster(_test);

//            Assert.IsTrue(success);

//            RemoveTestMonster(_test.monsterName);
//        }

//        //Delete test: Testing the ability to remove monster(s) from the database
//        [TestMethod]
//        public void CanRemoveMonster() {
//            ModifyMonsters _monster = new ModifyMonsters();
//            MonsterDO _test = new MonsterDO();
//            _test.monsterName = "Test";
//            _test.Health = 0;
//            _test.Armor = 0;
//            _test.Damage = 0;
//            _test.Danger = 0;
//            _test.Behaviour = 0;

//            _monster.AddMonster(_test);
//            int ID = GetTestMonsterID(_test.monsterName);
//            bool success = _monster.RemoveMonster(ID);

//            Assert.IsTrue(success);
//        }

//        //Testing purposes only. Gets the test monsters id.
//        private int GetTestMonsterID(string monsterName) {
//            int _returnInt = 0;

//            try {
//                using (SqlConnection connection = new SqlConnection(_connection)) {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand("SP_GetTestMonsterID", connection)) {
//                        command.CommandType = CommandType.StoredProcedure;
//                        command.CommandTimeout = 10;

//                        //Parameters: _monsterName(NVARCHAR)
//                        command.Parameters.AddWithValue("@parm_monsterName", SqlDbType.NVarChar).Value = monsterName;
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                _returnInt = (int)reader["monster_ID"];
//                            }
//                        }
//                    }
//                    connection.Close();
//                    connection.Dispose();
//                }
//                return _returnInt;
//            } catch (Exception ex) {
//                LoggingError error = new LoggingError();
//                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
//                return _returnInt;
//            }
//        }

//        //Testing purposes only. Removes all testing monsters from the database.
//        private void RemoveTestMonster(string monsterName) {
//            try {
//                using (SqlConnection connection = new SqlConnection(_connection)) {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand("SP_RemoveTestMonster", connection)) {
//                        command.CommandType = CommandType.StoredProcedure;
//                        command.CommandTimeout = 10;

//                        //Parameters: _monsterName(NVARCHAR)
//                        command.Parameters.AddWithValue("@parm_monsterName", SqlDbType.NVarChar).Value = monsterName;
//                        command.ExecuteNonQuery();
//                    }
//                    connection.Close();
//                    connection.Dispose();
//                }
//            } catch (Exception ex) {
//                LoggingError error = new LoggingError();
//                error.LogError(ex.ToString(), ex.Message, ex.Source.ToString());
//            }
//        }
//    }


//}
