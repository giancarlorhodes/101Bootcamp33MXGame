
namespace Capstone_DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This is a object class for characters being created or used.
    /// </summary>
    public class CharacterDO
    {
        private int id;
        public int userID;
        private string name;
        private int characterClass;
        private string className;
        private int location;
        private int gold;
        private int level;
        private int xp;
        private int health;
        public int maxHP;
        public int maxXp;
        private int armor;
        private int damage;
        private int stamina;
        private int magica;

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public int Class {
            get { return characterClass; }
            set { characterClass = value; }
        }

        public int Health {
            get { return health; }
            set { health = value; }
        }

        public string ClassName {
            get { return className; }
            set { className = value; }
        }

        public int Location {
            get { return location; }
            set { location = value; }
        }

        public int Gold {
            get { return gold; }
            set { gold = value; }
        }

        public int Lvl {
            get { return level; }
            set { level = value; }
        }

        public int Xp {
            get { return xp; }
            set { xp = value; }
        }

        public int MaxXp {
            get { return maxXp; }
            set { maxXp = value; }
        }

        public int Armor {
            get { return armor; }
            set { armor = value; }
        }

        public int Damage {
            get { return damage; }
            set { damage = value; }
        }

        public int Stamina {
            get { return stamina; }
            set { stamina = value; }
        }

        public int Magica {
            get { return magica; }
            set { magica = value; }
        }

    }
}
