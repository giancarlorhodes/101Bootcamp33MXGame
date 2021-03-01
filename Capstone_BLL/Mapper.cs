
namespace Capstone_BLL
{
    using Capstone_BLL.BusinessObjects;
    using Capstone_DAL;
    using Capstone_DAL.DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Used for mapping info from Business objects to Data objects
    /// </summary>
    class Mapper
    {
        //-------------------User Mapping----------------------

        //Maps the values from a Business object to an existing Data object.
        public UsersDO UserBO_To_DO(UsersBO user) {
            UsersDO _returnUser = new UsersDO();

            _returnUser.UserID = user.UserID;
            _returnUser.UserRole = user.UserRole;
            _returnUser.Username = user.Username;
            _returnUser.Password = user.Password;
            _returnUser.Email = user.Email;

            return _returnUser;
        }

        //Maps the values from a data object to a business object
        public UsersBO UserDO_To_BO(UsersDO user) {
            UsersBO _returnUser = new UsersBO();

            _returnUser.UserID = user.UserID;
            _returnUser.UserRole = user.UserRole;
            _returnUser.Username = user.Username;
            _returnUser.UserRole = user.UserRole;
            _returnUser.Email = user.Email;

            return _returnUser;
        }

        public List<UsersBO> UserDO_To_List(List<UsersDO> list) {
            List<UsersBO> _returnList = new List<UsersBO>();

            foreach (UsersDO user in list) {
                UsersBO _user = new UsersBO();
                _user = UserDO_To_BO(user);
                _returnList.Add(_user);
            }

            return _returnList;
        }

        //-------------------Character Mapping----------------------

        //Maps the values from a character data to character business objects
        public CharacterBO CharacterDO_To_BO(CharacterDO character) {
            CharacterBO _returnCharacter = new CharacterBO();
            
            _returnCharacter.ID = character.ID;
            _returnCharacter.userID = character.userID;
            _returnCharacter.ClassName = character.ClassName;
            _returnCharacter.Name = character.Name;
            _returnCharacter.Class = character.Class;
            _returnCharacter.Armor = character.Armor;
            _returnCharacter.Location = character.Location;
            _returnCharacter.Gold = character.Gold;
            _returnCharacter.Lvl = character.Lvl;
            _returnCharacter.Xp = character.Xp;
            _returnCharacter.Health = character.Health;
            _returnCharacter.maxHP = character.maxHP;
            _returnCharacter.maxXp = character.maxXp;
            _returnCharacter.Damage = character.Damage;
            _returnCharacter.Stamina = character.Stamina;
            _returnCharacter.Magica = character.Magica;

            return _returnCharacter;
        }

        //Used for mapping values from a character business to data object
        public CharacterDO CharacterBO_To_DO(CharacterBO character) {
            CharacterDO _returnCharacter = new CharacterDO();

            _returnCharacter.ID = character.ID;
            _returnCharacter.userID = character.userID;
            _returnCharacter.ClassName = character.ClassName;
            _returnCharacter.Name = character.Name;
            _returnCharacter.Class = character.Class;
            _returnCharacter.Armor = character.Armor;
            _returnCharacter.Location = character.Location;
            _returnCharacter.Gold = character.Gold;
            _returnCharacter.Lvl = character.Lvl;
            _returnCharacter.Xp = character.Xp;
            _returnCharacter.Health = character.Health;
            _returnCharacter.maxHP = character.maxHP;
            _returnCharacter.maxXp = character.maxXp;
            _returnCharacter.Damage = character.Damage;
            _returnCharacter.Stamina = character.Stamina;
            _returnCharacter.Magica = character.Magica;

            return _returnCharacter;
        }

        //Maps character data objects to a list of business objects 
        public List<CharacterBO> CharacterDO_To_BOList(List<CharacterDO> characters) {
            List<CharacterBO> _returnList = new List<CharacterBO>();

            foreach (CharacterDO _character in characters) {
                _returnList.Add(CharacterDO_To_BO(_character)); 
            }

            return _returnList;
        }

        //-----------------Class Mapping------------------------

        //Maps values from a class data to a class business object
        public ClassBO ClassDO_To_BO(ClassDO Class) {
            ClassBO _returnClass = new ClassBO();

            _returnClass.className = Class.className;
            _returnClass.classID = Class.classID;
            _returnClass.classArmor = Class.classArmor;
            _returnClass.baseHP = Class.baseHP;
            _returnClass.classStamina = Class.classStamina;
            _returnClass.classMagica = Class.classMagica;
            _returnClass.classDamage = Class.classDamage;

            return _returnClass;
        }

        //Maps class data to business object list
        public List<ClassBO> ClassDO_To_BOList(List<ClassDO> classes) {
            List<ClassBO> _returnList = new List<ClassBO>();

            foreach (ClassDO _class in classes) {
                ClassBO temp = new ClassBO();
                 temp = ClassDO_To_BO(_class);
                _returnList.Add(temp);
            }

            return _returnList;
        }

        //--------------------------Region Mapping-----------------------------

        //Mpas data from Region DO to ragion BO
        public RegionBO RegionDO_To_BO(RegionDO region) {
            RegionBO _returnRegion = new RegionBO();

            _returnRegion.regionName = region.RegionName;
            _returnRegion.danger = region.RegionDanger;
            _returnRegion.hasShop = region.HasShop;
            _returnRegion.regionID = region.RegionID;
            _returnRegion.regionDesc = region.RegionDesc;

            return _returnRegion;
        }

        //only adds regionDO to a list for return use.
        public List<RegionBO> RegionDO_To_List(List<RegionDO> regions) {
            List<RegionBO> _returnList = new List<RegionBO>();

            foreach (RegionDO region in regions) {
                RegionBO _region = RegionDO_To_BO(region);
                _returnList.Add(_region);
            }

            return _returnList;
        }

        //-------------------------Monster Mapping------------------------------

        //for mapping values from monster data to business objects
        public MonsterBO MonsterDO_To_BO(MonsterDO monster) {
            MonsterBO _returnMonster = new MonsterBO();

            _returnMonster.MonsterID = monster.MonsterID;
            _returnMonster.monsterName = monster.monsterName;
            _returnMonster.Health = monster.Health;
            _returnMonster.Armor = monster.Armor;
            _returnMonster.Damage = monster.Damage;
            _returnMonster.Danger = monster.Danger;
            _returnMonster.Behaviour = monster.Behaviour;

            return _returnMonster;
        }
        
        public List<MonsterBO> MonsterDO_To_List(List<MonsterDO> monsters) {
            List<MonsterBO> _returnList = new List<MonsterBO>();

            foreach(MonsterDO monster in monsters){
                MonsterBO _monster = new MonsterBO();
                _monster = MonsterDO_To_BO(monster);

                _returnList.Add(_monster);
            }

            return _returnList;
        }

        public MonsterDO MonsterBO_To_DO(MonsterBO monster) {
            MonsterDO _returnMonster = new MonsterDO();

            _returnMonster.MonsterID = monster.MonsterID;
            _returnMonster.monsterName = monster.monsterName;
            _returnMonster.Health = monster.Health;
            _returnMonster.Armor = monster.Armor;
            _returnMonster.Damage = monster.Damage;
            _returnMonster.Danger = monster.Danger;
            _returnMonster.Behaviour = monster.Behaviour;

            return _returnMonster;
        }

        
        //------------------Item Mapping--------------------------

        //for mapping values from a item data to business object
        public ItemBO ItemDO_To_BO(ItemDO item) {
            ItemBO _returnItem = new ItemBO();

            _returnItem.itemID = item.ItemID;
            _returnItem.itemName = item.ItemName;
            _returnItem.itemType = item.itemType;
            _returnItem.armorType = item.armorType;
            _returnItem.weaponType = item.weaponType;
            _returnItem.healthMod = item.HealthMod;
            _returnItem.staminaMod = item.staminaMod;
            _returnItem.magicaMod = item.MagicaMod;
            _returnItem.armorMod = item.ArmorMod;
            _returnItem.damageMod = item.DamageMod;
            _returnItem.isEquipted = item.isEquipted;
            _returnItem.inventoryID = item.InventoryID;
            _returnItem.goldPrice = item.goldPrice;

            return _returnItem;
        }

        public List<ItemBO> ItemDO_To_List(List<ItemDO> list) {
            List<ItemBO> _returnList = new List<ItemBO>();

            foreach (ItemDO item in list) {
                ItemBO _item = new ItemBO();
                _item = ItemDO_To_BO(item);
                _returnList.Add(_item);

            }

            return _returnList;
        }

        //-------------------Role Mapping-----------------
        public RoleBO RoleDO_To_BO(RoleDO role) {
            RoleBO _returnRole = new RoleBO();

            _returnRole.roleID = role.roleID;
            _returnRole.roleName = role.roleName;

            return _returnRole;
        }

        public List<RoleBO> RoleDO_To_List(List<RoleDO> list) {
            List<RoleBO> _returnList = new List<RoleBO>();

            foreach (RoleDO role in list) {
                RoleBO _role = new RoleBO();
                _role = RoleDO_To_BO(role);
                _returnList.Add(_role);
            }

            return _returnList;
        }
    }
}
