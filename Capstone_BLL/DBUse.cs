

namespace Capstone_BLL
{
    using Capstone_BLL.BusinessObjects;
    using Capstone_DAL;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Used for calling database methods.
    /// </summary>
    public class DBUse
    {
        //------------------------User Methods---------------------

        //Registers a new user and adds the to the database for future use.
        public void AddUser(UsersBO user) {
            ModifyUsers _user = new ModifyUsers();
            Mapper mapper = new Mapper();
            UsersDO userDO = mapper.UserBO_To_DO(user);

            _user.AddUser(userDO);
        }

        public void RemoveUser(int userID) {
            ModifyUsers _user = new ModifyUsers();

            _user.RemoveUser(userID);
        }

        //checks the database to see if a user already exist with the given username
        public bool FindUsername(string username) {
            bool _returnBool = false;

            ModifyUsers _user = new ModifyUsers();

           _returnBool =  _user.FindUsername(username);

            return _returnBool;
        }

        //checks to see if a user exist with the given username and password.
        public UsersBO FindUser(UsersBO user) {
            ModifyUsers _user = new ModifyUsers();
            Mapper mapper = new Mapper();

            UsersDO userDO = _user.GetUser(user.Username, user.Password);
            if (userDO == null)
            {
                return null;
            }
            else {
                UsersBO _returnUser = mapper.UserDO_To_BO(userDO);
                return _returnUser;
            }

        }

        public void UpdateUserInfo(UsersBO user) {
            Mapper map = new Mapper();
            ModifyUsers _user = new ModifyUsers();

            _user.UpdateUser(user.UserID,map.UserBO_To_DO(user));
        }

        public List<UsersBO> GetAllUsers() {
            List<UsersBO> _returnList = new List<UsersBO>();
            ModifyUsers users = new ModifyUsers();
            Mapper map = new Mapper();

            _returnList = map.UserDO_To_List(users.GetAllUsers());

            return _returnList;
        }

        //---------------------Class Methods-------------------------------------

        //Gets a list of existing class types
        public List<ClassBO> GetClassList()
        {
            List<ClassBO> _returnList = new List<ClassBO>();
            Mapper map = new Mapper();
            ModifyClass _class = new ModifyClass();

            _returnList = map.ClassDO_To_BOList(_class.GetClassList());

            return _returnList;
        }

        //Gets user class information for use.
        public ClassBO GetClassInfo(int classID)
        {
            ModifyClass _class = new ModifyClass();
            Mapper map = new Mapper();
            ClassBO _returnClass = new ClassBO();

            _returnClass = map.ClassDO_To_BO(_class.GetClassInfo(classID));

            return _returnClass;
        }

        //----------------------Character Methods------------------------------

        //Gets the current users characters
        public List<CharacterBO> GetCharacters(int userID) {
            List<CharacterBO> _returnList = new List<CharacterBO>();
            Mapper mapper = new Mapper();
            ModifyCharacters character = new ModifyCharacters();

             _returnList = mapper.CharacterDO_To_BOList(character.GetCharacterList(userID));


            return _returnList;

        }

        //Gets a single characters values for use
        public CharacterBO GetCharacter(int characterID) {
            CharacterBO _returnCharacter = new CharacterBO();
            Mapper map = new Mapper();
            ModifyCharacters character = new ModifyCharacters();

            _returnCharacter = map.CharacterDO_To_BO(character.GetCharacter(characterID));

            return _returnCharacter;
        }

        //Updates a characters old data replacing it with user inputted
        public void UpdateUserCharacter(CharacterBO character) {
            Mapper map = new Mapper();
            ModifyCharacters _character = new ModifyCharacters();

            _character.UpdateCharacterData(map.CharacterBO_To_DO(character));
        }

        //Removes a character by user and character id
        public void RemoveCharacter(int characterID, int userID) {
            ModifyCharacters _character = new ModifyCharacters();

            _character.RemoveCharacter(userID, characterID);
        }

        //Used to create a new character 
        public void CreateCharacter(CharacterBO character) {
            Mapper map = new Mapper();
            ModifyCharacters _character = new ModifyCharacters();
            CharacterDO temp = map.CharacterBO_To_DO(character);
            _character.AddCharacter(temp, temp.userID);

        }


        //----------------Character Inventory-----------------------

        //Used to get an existing characters full inventory of items. 
        public List<ItemBO> GetCharacterInventory(int characterID)
        {
            List<ItemBO> _returnList = new List<ItemBO>();
            Mapper map = new Mapper();
            ModifyInventories inven = new ModifyInventories();

            _returnList = map.ItemDO_To_List(inven.GetCharacterInventory(characterID));

            return _returnList;
        }

        //Used only for merchant events
        public List<ItemBO> GetItemList()
        {
            List<ItemBO> _returnList = new List<ItemBO>();
            Mapper map = new Mapper();
            ModifyInventories inven = new ModifyInventories();

            _returnList = map.ItemDO_To_List(inven.GetItemsList());

            return _returnList;
        }

        public void AddToCharacterInventory(int itemID, int characterID)
        {
            ModifyInventories inventory = new ModifyInventories();

            inventory.AddToInventory(characterID, itemID);
        }

        public void RemoveItemFromInventory(int inventoryID)
        {
            ModifyInventories inventory = new ModifyInventories();
            inventory.RemoveItemFromInventory(inventoryID);
        }

        public void EquiptItem(int inventoryID, int isEquipted) {
            ModifyInventories inven = new ModifyInventories();

            inven.EquiptItem(inventoryID, isEquipted);
        }


        //------------------------Regions Methods---------------------------
        //Used for getting a list of the existing regions
        public List<RegionBO> GetRegion() {
            List<RegionBO> _returnList = new List<RegionBO>();
            ModifyRegions region = new ModifyRegions();
            Mapper map = new Mapper();

            _returnList = map.RegionDO_To_List(region.GetRegions());

            return _returnList;
        }

        public RegionBO GetRegionInfo(int regionID) {
            RegionBO _returnRegion = new RegionBO();
            ModifyRegions _region = new ModifyRegions();
            Mapper map = new Mapper();

            _returnRegion = map.RegionDO_To_BO(_region.GetRegionInfo(regionID));

            return _returnRegion;
        }

        //---------------------Monster Methods-------------------------------------
        
        //used to get a full list of possible monsters based on min and max danger
        public List<MonsterBO> GetMonstersByDanger(int max, int min) {
            List<MonsterBO> _returnList = new List<MonsterBO>();
            ModifyMonsters monster = new ModifyMonsters();
            DBUse data = new DBUse();
            Mapper map = new Mapper();

            _returnList = map.MonsterDO_To_List(monster.GetMonstersByDanger(min, max));

            return _returnList;
        }

        public List<MonsterBO> GetAllMonsters() {
            List<MonsterBO> _returnList = new List<MonsterBO>();
            Mapper map = new Mapper();
            ModifyMonsters monsters = new ModifyMonsters();

            _returnList = map.MonsterDO_To_List(monsters.GetMonsterList());

            return _returnList;
        }

        public void UpdateMonster(MonsterBO monster) {
            ModifyMonsters _monster = new ModifyMonsters();
            Mapper map = new Mapper();

            _monster.UpdateMonster(map.MonsterBO_To_DO(monster));
        }

        public void RemoveMonster(int monsterID) {
            ModifyMonsters _monster = new ModifyMonsters();

            _monster.RemoveMonster(monsterID);
        }

        public void CreateMonster(MonsterBO monster) {
            Mapper map = new Mapper();
            ModifyMonsters _monster = new ModifyMonsters();

            _monster.AddMonster(map.MonsterBO_To_DO(monster));
        }

        //----------------Role Methods-----------------
        public List<RoleBO> GetRoles() {
            List<RoleBO> _returnList = new List<RoleBO>();
            ModifyRoles roles = new ModifyRoles();
            Mapper map = new Mapper();

            _returnList = map.RoleDO_To_List(roles.GetRoles());

            return _returnList;
        }
        public void ChangeUserRole(int userID, int roleID) {
            ModifyUsers user = new ModifyUsers();

            user.ChangeUserRole(userID, roleID);
        }

    }
}
