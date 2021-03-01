

namespace Capstone_BLL.BusinessObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Used only for holding values across tiers for item objects
    /// </summary>
    public class ItemBO
    {
        public int itemID;
        public string itemName;
        public int itemType;
        public int armorType;
        public int weaponType;
        public int healthMod;
        public int staminaMod;
        public int magicaMod;
        public int inventoryID;
        public int isEquipted;
        public int armorMod;
        public int damageMod;
        public int goldPrice;
    }
}
