

namespace Capstone_BLL.BusinessObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Used for holding monster values across object types.
    /// </summary>
    public class MonsterBO
    {
        private int monsterID;
        public string monsterName;
        private int health;
        private int armor;
        private int damage;
        private int danger;
        private int behaviour;

        public int Behaviour {
            get { return behaviour; }
            set { behaviour = value; }
        }

        public int Danger {
            get { return danger; }
            set { danger = value; }
        }

        public int Damage {
            get { return damage; }
            set { damage = value; }
        }

        public int Armor {
            get { return armor; }
            set { armor = value; }
        }

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public string MonsterName {
            get { return monsterName; }
            set { monsterName = value; }
        }

        public int MonsterID {
            get { return monsterID; }
            set { monsterID = value; }
        }
    }
}
