

namespace Capstone_Xavier.Controllers
{
    using Capstone_BLL;
    using Capstone_Xavier.Filters;
    using Capstone_Xavier.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Capstone_Xavier.Common;

    /// <summary>
    /// Used for all the game functions like combat and random events.
    /// </summary>
    public class GameController : Controller
    {
        [MustBeLoggedIn]
        [HttpGet]
        //For setting up the needed information in the game. UI info and other.
        public ActionResult Game(CharacterModel character) {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            GameModel game = new GameModel();
            CharacterModel player = map.CharacterBO_To_Model(data.GetCharacter(character.id));
            ClassModel _class = map.ClassBO_To_Model(data.GetClassInfo(character.classID));
            player.id = character.id;
            game.inventory = map.ItemBO_To_List(data.GetCharacterInventory(character.id));

            player.stamina = _class.classStamina;
            player.magica = _class.classMagica;
            player.damage = _class.classDamage;
            player.armor = _class.classArmor;

            game.character = player;
            game.regions = map.RegionBO_To_List(data.GetRegion());
            game.usableWeapons = GetUsableWeapons(character.classID);
            Session["Game"] = game;
            EquiptItems(game.inventory);

            return View(game);
        }

        //For getting the weapons that the character can use best. 
        private int[] GetUsableWeapons(int classID) {
                int[] classWeapons = new int[3];

                switch (classID)
                {
                    case (int)Classes.Hunter:
                        classWeapons = new int[] { 1, 2, 8 };
                        break;
                    case (int)Classes.Warrior:
                        classWeapons = new int[] { 3, 4, 5 };
                        break;
                    case (int)Classes.Thief:
                        classWeapons = new int[] { 1, 7, 8 };
                        break;
                    case (int)Classes.Mage:
                        classWeapons = new int[] { 6, 7, 1 };
                        break;
                    case (int)Classes.Paladin:
                        classWeapons = new int[] { 3, 4, 7 };
                        break;
                    case (int)Classes.Ranger:
                        classWeapons = new int[] { 1, 3, 8 };
                        break;
                }
            return classWeapons;
        }

        [HttpPost]
        //For generating events based on actions. Travel/Looking. Monster, Arrive/Look, and Thief events possible
        public string DoAction(string action, string regionID) {
            string _return = " ";
            Random rand = new Random();

            //Traveling from one location to another
            if (action == "1") {
                int _event = rand.Next(3);
                //Travel successful
                if (_event == 0)
                {
                    _return = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '>" + ArriveEvent(int.Parse(regionID)) + "</div> <br>";
                }
                else {//Met a monster during travel and failed to get to your location
                    _return = "1";
                }
            } else if (action == "2") {//Look around event
                int _event = rand.Next(2);
                if (_event == 0)
                {
                    //TODO- get shop
                    _return = "3";
                }
                else {
                    _return = "-1";
                }
            }

            return _return;
        }

        //---------------Events-------------------


        //Attack event: Used only when called for monster and thief events.
        private string MonsterEvent(int currentLoc, int destination) {
            string _returnString = "";
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            RegionModel currentRegion = map.RegionBO_To_RegionModel(data.GetRegionInfo(currentLoc));
            RegionModel destinationRegion = map.RegionBO_To_RegionModel(data.GetRegionInfo(destination));
            int maxDanger = (currentRegion.danger + destinationRegion.danger) / 2;
            int minDanger = maxDanger - 3;
            Random rand = new Random();
            Random initiative = new Random();
            MonsterModel _monster;
            GameModel game = (GameModel)Session["Game"];

            if (minDanger <= 0) {
                minDanger = 1;
            }

            List<MonsterModel> monster = map.MonsterBO_To_List(data.GetMonstersByDanger(maxDanger, minDanger));
            _monster = monster[rand.Next(monster.Count )];
            game.monster = _monster;

            if (initiative.Next(21) > 15)
            {
                _returnString = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '>You encountered a <strong>" + _monster.monsterName + "</strong> You seem to have caught the monster off guard. What will you do?</div><br>";
                game.initiave = true;
            }
            else {
                _returnString = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '>You encountered a <strong>" + _monster.monsterName + "</strong> Your clumsy loud noise gave your position away. As you and the beast face off you decide what to do.</div><br>";
            }

            return _returnString;
        }

        //used only if the user successfully arrives at his destination
        private string ArriveEvent(int regionID)
        {
            string _returnString = "";
            GameModel game = (GameModel)Session["Game"];
            List<RegionModel> list = game.regions;
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            CharacterModel character = game.character;

            foreach (RegionModel r in list)
            {
                if (r.regionID == regionID)
                {
                    _returnString = r.regionDesc;
                    break;
                }
                else
                {
                    continue;
                }
            }
            character.location = regionID;

            data.UpdateUserCharacter(map.CharacterModel_To_BO(character));

            return _returnString;
        }


        //Called if the monster dies. Updates the character data and continues.
        [HttpPost]
        public string MonsterDeathEvent(int playerHealth)
        {
            GameModel game = (GameModel)Session["Game"];
            MonsterModel monster = game.monster;
            CharacterModel player = game.character;
            player.health = playerHealth;
            Mapper map = new Mapper();
            DBUse data = new DBUse();
            string _returnString = "";
            int gold = monster.danger * 10;
            game.monster = null;

            player.gold = gold + player.gold;

            data.UpdateUserCharacter(map.CharacterModel_To_BO(player));

            _returnString = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '> Your combat was strong. After a good battle you land the final strike killing the monster. As the body turns to dust a small pile of gold can be seen. " +
                            "+" + gold.ToString() + "gold </div><br>";

            return _returnString;
        }

        [HttpPost]
        public string PlayerDeathEvent()
        {
            string _returnString = "";
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            GameModel game = (GameModel)Session["Game"];
            int userID = int.Parse(Session["UserID"].ToString());

            _returnString = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '> Althought you fought with all your strenght the monster gained the uppder hand. " +
                            "without mercy or pity the beast strikes you down. As you turn to dust with your final breath your save data is deleted forever and your story is forgotten.</div><br>";

            data.RemoveCharacter(game.character.id, userID);

            return _returnString;
        }

        //Used for the merchant event. Allows the user to purchase goods
        [HttpPost]
        public string MerchantEvent()
        {
            DBUse data = new DBUse();
            Mapper map = new Mapper();
            string _returnString = "";
            List<ItemModel> list = new List<ItemModel>();
            Random rand = new Random();
            int inven = rand.Next(6);
            if (inven <= 0) {
                inven = 3;
            }

            list = map.ItemBO_To_List(data.GetItemList());

            for (int i = 0; i < inven; i++)
            {
                int itemNum = rand.Next(list.Count);
                ItemModel item = list[itemNum];
                string stats = GetStatsString(item);

                //return string HTML. Used for list of items
                string temp = "<br><div class='shop-item' style='height: 5vw'> <h6>" + item.itemName + "</h6> <div class='item-stats'> Gold: " + item.goldPrice.ToString() + stats + "</div><button class='btn-user'style='display: inline-block; float: right;' onclick='buy(" + item.itemID.ToString() + "," + item.goldPrice.ToString() + ")'>Buy</button></div><br>";
                _returnString = _returnString + temp;
            }

            return _returnString;
        }

        [HttpPost]
        //For getting the sellable items in inventory. Equipted cannot be sold
        public string SellInventory() {
            GameModel game = (GameModel)Session["Game"];
            Mapper map = new Mapper();
            DBUse data = new DBUse();
            List<ItemModel> inven = map.ItemBO_To_List(data.GetCharacterInventory(game.character.id));
            string _returnString = "";

            foreach (ItemModel item in inven) {

                if (item.isEquipted == 1)
                {
                    continue;
                }
                else {
                    _returnString = _returnString + "<br><div class='shop-item' style='height: 5vw'> <h6>" + item.itemName + "</h6> <div class='item-stats'> Gold: " + item.goldPrice.ToString() +
                    "</div><button class='btn-user'style='display: inline-block; float: left;' onclick='SellItem(" + item.inventoryID + ","
                   + item.goldPrice + ")'>Sell</button></div><br>";
                }
                
            }

            return _returnString;
        }

        [HttpPost]
        public string BuyEvent(int itemID, int itemPrice)
        {
            string _returnstring = "";
            Mapper map = new Mapper();
            DBUse data = new DBUse();
            GameModel game = (GameModel)Session["Game"];
            if (game.character.gold - itemPrice < 0)
            {

                _returnstring = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '>Realizing you dont have the gold needed  you put the item back and continue searching.</div><br>";
            }
            else {

                data.AddToCharacterInventory(itemID, game.character.id);
                game.character.gold -= itemPrice;
                data.UpdateUserCharacter(map.CharacterModel_To_BO(game.character));
                game.inventory = map.ItemBO_To_List(data.GetCharacterInventory(game.character.id));
                _returnstring = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '>Holding the " + itemPrice.ToString() + "gold coins out you see the old man greedily grab the shiny coins cackling soflty. He allows you to grab the item and looks at you once again. Will that be all? Your purse: <strong>" + game.character.gold.ToString() + "gold</strong></div><br>";

            }
            return _returnstring;
        }

        [HttpPost]
        public string SellEvent(int itemID, int itemPrice) {
            string _returnstring = "";
            Mapper map = new Mapper();
            DBUse data = new DBUse();
            GameModel game = (GameModel)Session["Game"];

            data.RemoveItemFromInventory(itemID);
            game.character.gold  += itemPrice;
            data.UpdateUserCharacter(map.CharacterModel_To_BO(game.character));
            game.inventory = map.ItemBO_To_List(data.GetCharacterInventory(game.character.id));
            _returnstring = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '>The greedy man holds out the gold grudginly quickly swiping the item from you. Your purse <strong>"+game.character.gold+"gold</strong></div><br>";

            return _returnstring;
        }

        //----------------Misc---------------------

        //Used to get the current monster values for use.
        [HttpGet]
        public ActionResult GetMonsterValues() {
            GameModel game = (GameModel)Session["Game"];

            if (game.monster == null)
            {

                GameModel G = Session["Game"] as GameModel;

                CharacterModel charloc = G.character;

                MonsterEvent(charloc.location, charloc.location);

                game = (GameModel)Session["Game"];

            }


            MonsterModel monster = game.monster;
            var _monster = new {monstername = monster.monsterName, monsterHealth = monster.health.ToString() };

            return Json(_monster, JsonRequestBehavior.AllowGet);
        }

        //For returning string responses based on the event type. Monster, Arrive, or Thief
        [HttpPost]
        public string EventAction(string regionID, string eventID) {
            string _returnString = "attack! " + regionID + eventID;
            GameModel game = (GameModel)Session["Game"];

            if (eventID == "1" || eventID == "-1") {
                if (eventID == "1")//if traveling from place to place
                {
                    _returnString = MonsterEvent(game.character.location, int.Parse(regionID));
                }
                else {//If looking around in one area
                    _returnString = MonsterEvent(game.character.location, game.character.location);
                }
                
            }

            return _returnString;
        }

        private string GetRegions() {
            string _returnString = "";
            DBUse data = new DBUse();
            Mapper map = new Mapper();

            return _returnString;
        }

        //For getting the individual stats of a weapon/Armor
        private string GetStatsString(ItemModel item) {
            string stats = "";

            switch (item.itemType)
            {
                case 0:
                    stats = " Health: +" + item.healthMod.ToString();
                    break;
                case 1:
                    stats = " Stamina +" + item.staminaMod.ToString();
                    break;
                case 2:
                    stats = " Magica +" + item.magicaMod.ToString();
                    break;
                case 3:
                    stats = " Armor +" + item.armorMod.ToString();
                    switch (item.armorType)
                    {
                        case 1:
                            stats = stats + " Magica: +" + item.magicaMod.ToString();
                            break;
                        case 2:
                            stats = stats + " Stamina: +" + item.staminaMod.ToString();
                            break;
                        case 3:
                            stats = stats + " Health +" + item.healthMod.ToString();
                            break;
                    }
                    break;
                case 4:
                    stats = " Damage: +" + item.damageMod.ToString();
                    if (item.weaponType != 7 && item.weaponType != 6)
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

        //For equpting the armor and weapons that the character has in their inventory
        private void EquiptItems(List<ItemModel> inven) {
            GameModel game = (GameModel)Session["Game"];
            CharacterModel player = game.character;
            foreach (ItemModel item in inven) {
                if (item.itemType == (int)ItemTypes.Armor)
                {
                    if (item.isEquipted == 1) {
                        game.equiptedArmor = new int[] { item.armorType, item.armorMod, item.healthMod, item.magicaMod, item.staminaMod };
                        game.character.armor = item.armorMod;
                    }
                }
                else if(item.itemType == (int)ItemTypes.Weapons){
                    if (item.isEquipted == 1) {
                        int[] weapons = game.usableWeapons;
                        int damageMod = item.damageMod;

                        for (int i = 0; i < weapons.Length; i++) {
                            if (item.weaponType == weapons[i]) {
                                damageMod = (int)(damageMod * 1.5);
                            }
                        }

                        game.equiptedWeapon = new int[] { item.weaponType, item.damageMod, item.healthMod, item.magicaMod, item.staminaMod };
                        game.character.damage = game.character.damage + item.damageMod;
                    }
                    
                }
            }
        }

        [HttpPost]
        public string UseNonConsumable(int itemType, int inventoryID) {
            GameModel game = (GameModel)Session["Game"];
            CharacterModel player = game.character;
            List<ItemModel> inven = game.inventory;
            DBUse data = new DBUse();

            foreach (ItemModel item in inven) {
                if (item.inventoryID == inventoryID) {
                    if (item.isEquipted == 1)
                    {
                        item.isEquipted = 0;
                    }
                    else {
                        item.isEquipted = 1;
                    }
                    data.EquiptItem(inventoryID, item.isEquipted);
                }

                //For if the items is the same type and is equipted but not the same item
                if (item.isEquipted == 1 && item.inventoryID != inventoryID && item.itemType == itemType) {
                    item.isEquipted = 0;
                    data.EquiptItem(item.inventoryID, item.isEquipted);
                }

            }
            EquiptItems(inven);
            

            return "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '> You quickly use the chosen item and get back to what you were doing. </div><br>";
        }

    }
}