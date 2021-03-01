
namespace Capstone_DAL.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Used for mapping values between Item objects 
    /// </summary>
    public class ItemDO
    {
        private int itemID;
        private int inventoryID;
        private string itemName;
        public int itemType;
        public int armorType;
        public int weaponType;
        private int healthMod;
        public int staminaMod;
        private int magicaMod;
        private int sellPrice;
        private int armorMod;
        private int damageMod;
        public int isEquipted;
        public int goldPrice;


        public int ItemID {
            get { return itemID; }
            set { itemID = value; }
        }

        public int ArmorMod {
            get { return armorMod; }
            set { armorMod = value; }
        }

        public int DamageMod {
            get { return damageMod; }
            set { damageMod = value; }
        }

        public string ItemName {
            get { return itemName; }
            set { itemName = value; }
        }

        

        public int HealthMod {
            get { return healthMod; }
            set { healthMod = value; }
        }

        public int MagicaMod {
            get { return magicaMod; }
            set { magicaMod = value; }
        }

        public int SellPrice {
            get { return sellPrice; }
            set { sellPrice = value; }
        }

        public int InventoryID {
            get { return inventoryID; }
            set { inventoryID = value; }
        }

    }
}
