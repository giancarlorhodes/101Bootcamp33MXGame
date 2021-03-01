
namespace Capstone_Xavier.Controllers
{
    using Capstone_BLL;
    using Capstone_Xavier.Filters;
    using Capstone_Xavier.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Mvc;
    using Capstone_Xavier.Common;

    /// <summary>
    /// used for user interactions between views
    /// </summary>
    public class UserController : Controller
    {
        [MustBeLoggedIn]
        [HttpGet]
        public ActionResult UserInfo() {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            UserModel user = (UserModel)Session["User"];

            return View(user);
        }

        [HttpPost]
        public ActionResult UserInfo(UserModel user) {
            Mapper map = new Mapper();
            DBUse data = new DBUse();
            UserModel _user = (UserModel)Session["User"];

            if (ModelState.IsValid) {
                user.userID = (int)Session["UserID"];

                if (user.username == null) {
                    user.username = _user.username;
                }

                if (user.password == null)
                {
                    user.password = _user.password;
                }
                else {
                    if (user.password != user.confirmPassword) {
                        //TODO- return warning
                        user.password = _user.password;
                    }
                }

                data.UpdateUserInfo(map.UserModel_To_BO(user));
                Session["User"] = user;
                //TODO- update user info
            }
            return View((UserModel)Session["User"]);
        }
       
        //---------------------Character Manipulation----------------------------------

        [MustBeLoggedIn]
        [HttpGet]
        public ActionResult UpdateCharacter(CharacterModel character) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();

            character.classes = map.ClassBO_To_ModelList(data.GetClassList());
            //Session["Character"] = character.id.ToString();

            return View(character);
        }

        [HttpPost]
        //For updating the selected character from the updateCharacter UI
        public ActionResult UpdateCharacterInfo(CharacterModel character) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            ClassModel _class = new ClassModel();
            if (ModelState.IsValid)
            {
                _class = map.ClassBO_To_Model(data.GetClassInfo(character.classID));

                character.id = character.id;
                character.level = 1;
                character.health = _class.baseHP;
                character.maxHP = _class.baseHP;
                character.stamina = _class.classStamina;
                character.magica = _class.classMagica;
                

                data.UpdateUserCharacter(map.CharacterModel_To_BO(character));

                Session["Character"] = null;

                return RedirectToAction("Users", "Home");
            }
            else {
                return RedirectToAction("Users", "Home");
            }
            
        }

        public ActionResult RemoveCharacter(CharacterModel character) {
            DBUse data = new DBUse();
            int id = character.id;
            int userID = int.Parse(Session["UserID"].ToString());

            data.RemoveCharacter(id, userID);

            return RedirectToAction("Users", "Home");
        }

        [MustBeLoggedIn]
        [HttpGet]
        public ActionResult CreateCharacter() {

            DBUse data = new DBUse();
            Mapper map = new Mapper();
            CharacterModel _character = new CharacterModel();

            _character.classes = map.ClassBO_To_ModelList(data.GetClassList());

            return View(_character);
        }

        [HttpPost]
        public ActionResult CreateNewCharacter(CharacterModel character) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            if (ModelState.IsValid)
            {
                ClassModel _class = map.ClassBO_To_Model(data.GetClassInfo(character.classID));

                character.health = _class.baseHP;
                character.userID = (int)Session["UserID"];

                data.CreateCharacter(map.CharacterModel_To_BO(character));

                return RedirectToAction("Users", "Home");
            }
            else {
                return RedirectToAction("Users", "Home");
            }
            
        }


        
    //----------------Game Data: Set due to action type--------------------
             
             //for getting all the different class types on character creation.
        [HttpPost]
        public string GetClasses(string classID) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            int id = int.Parse(classID);

            ClassModel _class = map.ClassBO_To_Model(data.GetClassInfo(id));

            string _classWeapons = GetClassWeapons(id); ;

            string _return = "<h6>" + _class.className + "</h6>"
                + "<div class='seperator'><label> Health </label>" +
                " <p> " + _class.baseHP.ToString() + "</p> "+ 
                "</div> " + "<div class='seperator'> "+
                "<label> Magica </label>"+
                " <p>"+ _class.classMagica.ToString() +" </p>"+
                "</div>" + "<div class='seperator'> " +
                "<label> Stamina </label>" +
                " <p>" + _class.classStamina.ToString() + " </p>" +
                "</div>" + "<hr>" + "<div class='class'>" +
                "<label style='display: block'>Weapons</label>" +
                "<p> Weapons available to this class are: " + _classWeapons +"</p>" +
                "</div>";



            return _return;
        }

        //For finding what weapoins are available the the character class
        private string GetClassWeapons(int classID) {
            string _returnString = "";
            string[] weaponTypes = new string[] { "Daggers","Short Swords", "Swords","Hammers/Maces", "Axes", "Staves","Scrolls/Books","Bows/Thrown" };
            int[] classWeapons = new int[3];
            //TODO enums
            switch (classID) {
                case (int)Classes.Hunter:
                    classWeapons = new int[]{ 1, 2, 8};
                    break;
                case (int)Classes.Warrior:
                    classWeapons = new int[] {3,4,5 };
                    break;
                case (int)Classes.Thief:
                    classWeapons = new int[] {1, 7, 8 };
                    break;
                case (int)Classes.Mage:
                    classWeapons = new int[] {6,7,1 };
                    break;
                case (int)Classes.Paladin:
                    classWeapons = new int[] {3,4,7 };
                    break;
                case (int)Classes.Ranger:
                    classWeapons = new int[] { 1,3,8};
                    break;
            }

            _returnString = weaponTypes[classWeapons[0]-1].ToString() + ", " + weaponTypes[classWeapons[1]-1].ToString() + " and " + weaponTypes[classWeapons[2]-1].ToString();

            return _returnString;
        }

        //For getting the full inventory of the current character. Used in a ajax function
        [HttpPost]
        public string GetCharacterInventory(CharacterModel character) {
            //return "Test string" + character.name;
            string _returnString = "";
            Mapper map = new Mapper();
            DBUse data = new DBUse();

            List<ItemModel> list = map.ItemBO_To_List(data.GetCharacterInventory(character.id));
            for (int i = 0; i < list.Count; i++) {
                ItemModel item = list[i];
                string stats = GetStatsString(item);
                string temp = "";
                if (item.itemType == (int)ItemTypes.Armor || item.itemType == (int)ItemTypes.Weapons)//If the item is armor or a weapon
                {
                    if (item.isEquipted == 1)//If the item is equipted. 0: Not , 1: Equipted
                    {
                        temp = "<br><div class='shop-item' style='height: 5vw'> <h6>" + item.itemName + "</h6> <div class='item-stats'> Gold: " + item.goldPrice.ToString() + stats
                   + "</div><button class='btn-user'style='display: inline-block; float: left;' onclick='UseNonCosumable(" + item.inventoryID + ","
                   + item.itemType.ToString() + ")'>Unequipt</button></div><br>";
                    }
                    else
                    {
                        temp = "<br><div class='shop-item' style='height: 5vw'> <h6>" + item.itemName + "</h6> <div class='item-stats'> Gold: " + item.goldPrice.ToString() + stats
                   + "</div><button class='btn-user'style='display: inline-block; float: left;' onclick='UseNonCosumable(" + item.inventoryID + ","
                   + item.itemType.ToString() + ")'>Equipt</button></div><br>";
                    }
                }
                else {
                    temp = "<br><div class='shop-item' style='height: 5vw'> <h6>" + item.itemName + "</h6> <div class='item-stats'> Gold: " + item.goldPrice.ToString() + stats
                    + "</div><button class='btn-user'style='display: inline-block; float: left;' onclick='UseItem(" + item.inventoryID + ","
                    + item.itemType.ToString() + ")'>Use</button></div><br>";
                }

                //return string HTML. Used for list of items
                 
                _returnString = _returnString + temp;
            }

            return _returnString;
        }

        //For explaining items on the UI. Checks item type and returns the appropriate values.
        private string GetStatsString(ItemModel item)
        {
            string stats = "";

            switch (item.itemType)
            {
                case (int)ItemTypes.Health:
                    stats = " Health: +" + item.healthMod.ToString();
                    break;
                case (int)ItemTypes.Stamina:
                    stats = " Stamina +" + item.staminaMod.ToString();
                    break;
                case (int)ItemTypes.Magica:
                    stats = " Magica +" + item.magicaMod.ToString();
                    break;
                case (int)ItemTypes.Armor:
                    stats = " Armor +" + item.armorMod.ToString();
                    switch (item.armorType)
                    {
                        case (int)ArmorTypes.Cloth:
                            stats = stats + " Magica: +" + item.magicaMod.ToString();
                            break;
                        case (int)ArmorTypes.Light_Armor:
                            stats = stats + " Stamina: +" + item.staminaMod.ToString();
                            break;
                        case (int)ArmorTypes.Heavy_Armor:
                            stats = stats + " Health +" + item.healthMod.ToString();
                            break;
                    }
                    break;
                case (int)ItemTypes.Weapons:
                    stats = " Damage: +" + item.damageMod.ToString();
                    if (item.weaponType != (int)WeaponTypes.Scrolls_Books && item.weaponType != (int)WeaponTypes.Staves)
                    {
                        stats = stats + " Stamina: " + item.staminaMod.ToString();
                    }
                    else
                    {
                        stats = stats + " Magica: " + item.magicaMod.ToString();
                    }
                    break;
            }

            return stats;
        }

        [HttpGet]
        public ActionResult UseItem(int inventoryID, int itemType, int bar) {
            int barID = 0;
            int modifier = 0;
            int increment;
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            GameModel game = (GameModel)Session["Game"];
            List<ItemModel> list =  map.ItemBO_To_List(data.GetCharacterInventory(game.character.id));
            ItemModel item = new ItemModel();

            foreach (ItemModel _item in list) {
                if (_item.inventoryID != inventoryID)
                {
                    continue;
                }
                else {
                    item = _item;
                    break;
                }
            }

            switch (itemType) {
                case 0:
                    modifier = item.healthMod;
                    game.character.health = game.character.health + modifier;
                    barID = 0;
                    break;
                case 1:
                    modifier = item.staminaMod;
                    game.character.stamina = game.character.stamina + modifier;
                    barID = 1;
                    break;
                case 2:
                    modifier = item.magicaMod;
                    game.character.magica = game.character.magica + modifier;
                    barID = 2;
                    break;
            }

            increment = (bar + modifier) - bar;

            var _return = new {bar = barID, value =  increment};
            data.RemoveItemFromInventory(inventoryID);
            data.UpdateUserCharacter(map.CharacterModel_To_BO(game.character));

            return Json(_return, JsonRequestBehavior.AllowGet);
        }
    }
}