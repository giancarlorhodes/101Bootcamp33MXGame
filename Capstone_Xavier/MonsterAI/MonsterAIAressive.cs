

namespace Capstone_Xavier.MonsterAI
{
    using Capstone_Xavier.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    /// <summary>
    /// Aggressive ai. will only attack or defend.
    /// </summary>
    public class MonsterAIAressive
    {
        public int AI(int monsterHealth, int playerHealth, bool initiative, MonsterModel monster, CharacterModel player)
        {
            int _returnInt = 0;

            int actionID = _returnInt + Initiative(initiative) + Danger(monster, player) + Health(monsterHealth, playerHealth);

            if (actionID > 0)
            {
                _returnInt = 1;
            }
            else {
                _returnInt = 2;
            }

            return _returnInt;
        }

        //If the monster has the initiative +1, else -1
        private int Initiative(bool initiative)
        {
            int _returnInt = 0;

            if (initiative == true)
            {
                _returnInt++;
            }
            else
            {
                _returnInt = 0;
            }

            return _returnInt;
        }

        //If the monsters danger > playerlevel +1 lower -1 same +1
        private int Danger(MonsterModel monster, CharacterModel player)
        {
            int _returnInt = 0;

            if (monster.danger > player.level)
            {
                _returnInt++;
            }
            else if (player.level > monster.danger)
            {
                _returnInt = 0;
            }
            else
            {
                _returnInt = 1;
            }

            return _returnInt;
        }

        //If mosnter health >= player +1, lower -1
        private int Health(int monster, int player)
        {
            int _returnInt = 0;

            if (monster >= player)
            {
                _returnInt++;
            }
            else
            {
                _returnInt = 0;
            }

            return _returnInt;
        }
    }
}