

namespace Capstone_Xavier.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    /// <summary>
    /// Used for storing all needed data for gameplay. Character, Mosters, and current event data.
    /// </summary>
    public class GameModel
    {
        public CharacterModel character { get; set; }
        //Monster
        public List<RegionModel> regions { get; set; }
        public List<ItemModel> inventory { get; set; }

        public MonsterModel monster { get; set; }

        public int[] equiptedArmor { get; set; }

        public int[] equiptedWeapon { get; set; }

        public int[] usableWeapons { get; set; }

        public bool initiave { get; set; }

    }
}