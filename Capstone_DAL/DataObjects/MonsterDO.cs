

namespace Capstone_DAL.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Uses for mapping values across Monster objects
    /// </summary>
    
    public class MonsterDO
    {
        private int monsterID;
        public string monsterName;
        private int health;
        private int armor;
        private int damage;
        private int danger;
        private int behaviour;

        public int MonsterID {
            get { return monsterID; }
            set { monsterID = value; }
        }

        public int Armor {
            get { return armor; }
            set { armor = value; }
        }

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public int Damage {
            get { return damage; }
            set { damage = value; }
        }

        public int Danger {
            get { return danger; }
            set { danger = value; }
        }

        public int Behaviour {
            get { return behaviour; }
            set { behaviour = value; }
        }

    }
}
