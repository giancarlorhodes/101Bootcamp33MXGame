

namespace Capstone_Xavier
{
    using Capstone_BLL.BusinessObjects;
    using Capstone_Xavier.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Used for mapping info from UI models to Business objects
    /// </summary>
    public class Mapper
    {
        //----------------------UI Model Mapping--------------------------

        //Maps the values from RegisterModel to the new Business object
        public UsersBO UIRegister_To_BO(RegisterModel register) {
            UsersBO _returnUser = new UsersBO();

            _returnUser.Username = register.username;
            _returnUser.Password = register.password;
            _returnUser.Email = register.email;

            return _returnUser;
        }

        //Maps the values from the LoginModel to the new Business object
        public UsersBO UILogin_To_BO(LoginModel login)
        {
            UsersBO _returnuser = new UsersBO();

            _returnuser.Username = login.username;
            _returnuser.Password = login.password;

            return _returnuser;
        }

        //------------------------User Mapping---------------------

        public UserModel UserBO_To_Model(UsersBO user) {
            UserModel _returnUser = new UserModel();

            _returnUser.userID = user.UserID;
            _returnUser.roleID = user.UserRole;
            _returnUser.username = user.Username;
            _returnUser.password = user.Password;
            _returnUser.email = user.Email;

            return _returnUser;
        }

        public UsersBO UserModel_To_BO(UserModel user) {
            UsersBO _returnUser = new UsersBO();

            _returnUser.UserID = user.userID;
            _returnUser.UserRole = user.roleID;
            _returnUser.Username = user.username;
            _returnUser.Password = user.password;
            _returnUser.Email = user.email;

            return _returnUser;
        }

        public List<UserModel> UserBO_To_List(List<UsersBO> list) {
            List<UserModel> _returnList = new List<UserModel>();

            foreach (UsersBO user in list) {
                UserModel _user = new UserModel();
                _user = UserBO_To_Model(user);
                _returnList.Add(_user);

            }

            return _returnList;
        }

        //---------------------------Character Mapping------------------------------

        //Maps the values from a business object to a RegisterModel
        public CharacterModel CharacterBO_To_Model(CharacterBO character) {
            CharacterModel _returnCharacter = new CharacterModel();

            _returnCharacter.id = character.ID;
            _returnCharacter.userID = character.userID;
            _returnCharacter.name = character.Name;
            _returnCharacter.classID = character.Class;
            _returnCharacter.armor = character.Armor;
            _returnCharacter.location = character.Location;
            _returnCharacter.gold = character.Gold;
            _returnCharacter.level = character.Lvl;
            _returnCharacter.xp = character.Xp;
            _returnCharacter.health = character.Health;
            _returnCharacter.maxHP = character.maxHP;
            _returnCharacter.maxXp = character.maxXp;
            _returnCharacter.classArmor = character.ClassArmor;
            _returnCharacter.damage = character.Damage;
            _returnCharacter.stamina = character.Stamina;
            _returnCharacter.magica = character.Magica;
            _returnCharacter.className = character.ClassName;

            return _returnCharacter;
        }

        //Adds character models to a list from existing models
        public List<CharacterModel> CharacterModel_To_List(List<CharacterBO> characters) {
            List<CharacterModel> _returnList = new List<CharacterModel>();

            foreach (CharacterBO _character in characters) {
                _returnList.Add(CharacterBO_To_Model(_character));
            }

            return _returnList;
        }

        //Maps values from a existing character model to a business object
        public CharacterBO CharacterModel_To_BO(CharacterModel character) {
            CharacterBO _returnCharacter = new CharacterBO();

            _returnCharacter.ID = character.id;
            _returnCharacter.userID = character.userID;
            _returnCharacter.ClassName = character.className;
            _returnCharacter.Name = character.name;
            _returnCharacter.Class = character.classID;
            _returnCharacter.Armor = character.armor;
            _returnCharacter.Location = character.location;
            _returnCharacter.Gold = character.gold;
            _returnCharacter.Lvl = character.level;
            _returnCharacter.Xp = character.xp;
            _returnCharacter.Health = character.health;
            _returnCharacter.maxHP = character.maxHP;
            _returnCharacter.maxXp = character.maxXp;
            _returnCharacter.ClassArmor = character.classArmor;
            _returnCharacter.Damage = character.damage;
            _returnCharacter.Stamina = character.stamina;
            _returnCharacter.Magica = character.magica;

            return _returnCharacter;
        }

        //--------------------Class Mapping--------------------------

        //Maps values from a class business to a ClassModel
        public ClassModel ClassBO_To_Model(ClassBO Class) {
            ClassModel _returnClass = new ClassModel();

            _returnClass.className = Class.className;
            _returnClass.classID = Class.classID;
            _returnClass.classArmor = Class.classArmor;
            _returnClass.baseHP = Class.baseHP;
            _returnClass.classStamina = Class.classStamina;
            _returnClass.classMagica = Class.classMagica;
            _returnClass.classDamage = Class.classDamage;

            return _returnClass;
        }

        //Maps values from class business to a classmaodel list
        public List<ClassModel> ClassBO_To_ModelList(List<ClassBO> classes) {
            List<ClassModel> _returnList = new List<ClassModel>();

            foreach (ClassBO _class in classes) {
                ClassModel temp = new ClassModel();
                temp = ClassBO_To_Model(_class);

                _returnList.Add(temp);
            }

            return _returnList;
        }


        //-----------------------------Region Mapping------------------------------

        //Used to map values from Region business to Region model
        public RegionModel RegionBO_To_RegionModel(RegionBO region) {
            RegionModel _returnRegion = new RegionModel();

            _returnRegion.regionName = region.regionName;
            _returnRegion.danger = region.danger;
            _returnRegion.hasShop = region.hasShop;
            _returnRegion.regionID = region.regionID;
            _returnRegion.regionDesc = region.regionDesc;

            return _returnRegion;
        }

        //used only for returning a list of RegionModels
        public List<RegionModel> RegionBO_To_List(List<RegionBO> regions) {
            List<RegionModel> _returnList = new List<RegionModel>();

            foreach (RegionBO region in regions) {
                RegionModel _region = new RegionModel();
                _region = RegionBO_To_RegionModel(region);
                _returnList.Add(_region);
            }

            return _returnList;
        }

        //-------------------------Monster Mapping-------------------------------

        //Used for mapping monster Business to a usable model
        public MonsterModel MonsterBO_To_Model(MonsterBO monster) {
            MonsterModel _returnMonster = new MonsterModel();

            _returnMonster.monsterName = monster.monsterName;
            _returnMonster.health = monster.Health;
            _returnMonster.armor = monster.Armor;
            _returnMonster.damage = monster.Damage;
            _returnMonster.danger = monster.Danger;
            _returnMonster.behaviour = monster.Behaviour;
            _returnMonster.monsterID = monster.MonsterID;

            return _returnMonster;
        }

        public List<MonsterModel> MonsterBO_To_List(List<MonsterBO> list) {
            List<MonsterModel> _returnList = new List<MonsterModel>();

            foreach (MonsterBO monster in list) {
                MonsterModel _monster = new MonsterModel();
                _monster = MonsterBO_To_Model(monster);
                _returnList.Add(_monster);
            }

            return _returnList;
        }

        public MonsterBO MonsterModel_To_BO(MonsterModel monster) {
            MonsterBO _returnMonster = new MonsterBO();

            _returnMonster.monsterName = monster.monsterName;
            _returnMonster.Health = monster.health;
            _returnMonster.Armor = monster.armor;
            _returnMonster.Danger = monster.danger;
            _returnMonster.Damage = monster.damage;
            _returnMonster.Behaviour = monster.behaviour;
            _returnMonster.MonsterID = monster.monsterID;

            return _returnMonster;
        }

        //-------------------Item Mapping-------------------------------
        //used for mappin values from item business to model
        public ItemModel ItemBO_To_Model(ItemBO item) {
            ItemModel _returnItem = new ItemModel();

            _returnItem.itemID = item.itemID;
            _returnItem.inventoryID = item.inventoryID;
            _returnItem.itemType = item.itemType;
            _returnItem.armorType = item.armorType;
            _returnItem.weaponType = item.weaponType;
            _returnItem.itemName = item.itemName;
            _returnItem.isEquipted = item.isEquipted;
            _returnItem.healthMod = item.healthMod;
            _returnItem.staminaMod = item.staminaMod;
            _returnItem.magicaMod = item.magicaMod;
            _returnItem.armorMod = item.armorMod;
            _returnItem.damageMod = item.damageMod;
            _returnItem.goldPrice = item.goldPrice;

            return _returnItem;
        }

        public List<ItemModel> ItemBO_To_List(List<ItemBO> list) {
            List<ItemModel> _returnList = new List<ItemModel>();

            foreach (ItemBO item in list) {
                ItemModel _item = new ItemModel();
                _item = ItemBO_To_Model(item);
                _returnList.Add(_item);
            }

            return _returnList;
        }

        //---------------Role Mapping------------------
        public RoleModel RoleBO_To_Model(RoleBO role) {
            RoleModel _returnRole = new RoleModel();

            _returnRole.roleID = role.roleID;
            _returnRole.roleName = role.roleName;

            return _returnRole;
        }

        public List<RoleModel> RoleBO_To_List(List<RoleBO> list) {
            List<RoleModel> _returnList = new List<RoleModel>();

            foreach (RoleBO role in list) {
                RoleModel _role = new RoleModel();
                _role = RoleBO_To_Model(role);
                _returnList.Add(_role);

            }

            return _returnList;
        }
    }
}
