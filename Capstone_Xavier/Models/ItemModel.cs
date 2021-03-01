

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Item class used for manipulation of inventory and items inside
    /// </summary>
    public class ItemModel
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        public int itemType { get; set; }
        public int armorType { get; set; }
        public int weaponType { get; set; }
        public int healthMod { get; set; }
        public int staminaMod { get; set; }
        public int magicaMod { get; set; }
        public int armorMod { get; set; }
        public int damageMod { get; set; }
        public int isEquipted { get; set; }
        public int inventoryID { get; set; }
        public int goldPrice { get; set; }
    }
}