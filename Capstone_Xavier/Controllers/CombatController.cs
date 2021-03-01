

namespace Capstone_Xavier.Controllers
{
    using Capstone_Xavier.Models;
    using Capstone_Xavier.MonsterAI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Used only during combat phases in the game. Holding all the different actions for both player and monster.
    /// </summary>
    public class CombatController : Controller
    {
        [HttpGet]
        public ActionResult CombatAction(int monsterHealth, int playerHealth, int magica, int stamina,int actionID) {
            var _return = new { };
            //Player attacks
            if (actionID == 0) {
                return AttackAction(monsterHealth, playerHealth, magica, stamina);
            }

            if (actionID == 1) {
                return DefendAction(monsterHealth, playerHealth);
            }

            if (actionID == 2) {
                return FleeAction();
            }

            return Json(_return, JsonRequestBehavior.AllowGet);

        }

        //called only if the player attacks. Checks for initiative and decided who takes damage or not.
        private ActionResult AttackAction(int monsterHealth, int playerHealth, int magica, int stamina) {
            GameModel game = (GameModel)Session["Game"];
            bool initiative = game.initiave;
            CharacterModel player = game.character;
            MonsterModel monster = game.monster;
            string story = "";
            int endID = 1;

            //weapon details. [weaponType, damageMod, healthMod, magicaMod, staminaMod ]
            int[] weapon = game.equiptedWeapon;

            //Setting the monster and player information
            int health = playerHealth;
            int mHealth = monsterHealth;
            int monsterSpeed = (monster.danger + monster.armor) / 2;

            int magicaMod = 0;
            int staminaMod = 0;
            int _stamina = stamina + staminaMod;
            int _magica = 0;

            //Setting damage values
            double baseDamage = player.damage;
            int damage = (int)((baseDamage + (baseDamage * (player.level / 10))) - (baseDamage * (monster.armor / 100)));
            int monsterDamage = monster.damage - (monster.damage * (player.armor / 100));
            


            //-----------------End of values-----------------
            //For if the character isnt a stamina user/runs out of stamina
            if (stamina == 0 || _stamina <= 0) {
                damage = 0;
                _stamina = stamina;
                story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> You have no stamina! Weakly attacking your foe you do no damage making a fool of yourself.</div><br>";
            }

            //For if they have a weapon equipted
            if (weapon != null) {
                magicaMod = weapon[3];
                staminaMod = weapon[4];
            }
            
            //For if the character isnt a magic user. Magic cost health if cast.
            if (magica == 0 && weapon != null)
            {
                health = health + magicaMod;
                story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> Due to your lack of magical experience you to use your own life energy to cast the spell costing you, " + magicaMod.ToString() + " Damge</div><br>";
            }
            else {
                 _magica = magica + magicaMod;
                if (_magica < 0)
                {
                    //For if the caster runs out of magica.
                    health = health + _magica;
                    story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> You run out of magica causing you to use your own life energy to cast the spell costing you, " + _magica.ToString() + " Damge</div><br>";
                    _magica = 0;
                }
            }
            
            
            //For calculating the energy/magica use of a weapon if your wielding one.
            if (weapon != null) {

                for (int i = 0; i < game.usableWeapons.Length; i++) {
                    //For checking if its a weapons usable by the character class
                    if (weapon[0] == game.usableWeapons[i])
                    {
                        
                        _stamina = stamina + staminaMod;
                        break;
                    }
                    else {
                         staminaMod = weapon[4] * 2;
                        _stamina = stamina + staminaMod;
                    }
                }
                
                _magica = magica + magicaMod;
                if (_magica < 0) {
                    _magica = 0;
                }
            }

            //For if the damage is less that 0 set a basic damage.
            if (damage < 0) {
                damage = 1;
            }

            //checks the initiative role.
            if (initiative == true)
            {
                //if true and the monster is stronger
                if (monster.danger > player.level)
                {
                    damage += (int)(damage*(.5));
                    monsterDamage--;
                }
                //if true and the monster is equal to or weaker
                else {
                    damage += damage;
                }
                game.initiave = false;
            }
            else {
                if (monster.danger > player.level) {
                    damage--;
                    monsterDamage += (int)(monsterDamage * (.2));
                }
            }

            int actionID = MonsterAI(mHealth, health, monster, player, initiative);

            //Called after the player action. Decides how the combat goes.

            //Monster flees the attack
            if (actionID == 0) {//Monster flee event.
                Random rand = new Random();
                int chance = rand.Next(20) +monsterSpeed;
                //Successful flee
                if (chance > 15)
                {
                    endID = 1;
                    story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '> You scared the monster into quick submisstion. Scared or not interesting in fighting the mosnter flees from you.</div><br>";
                    
                }
                else {//flee unsucessful
                    actionID = -1;
                    monsterDamage = monsterDamage - 5;
                }
            }

            //monster attacks 
            if (actionID == 1 || actionID == -1) {
                
                    health = health - monsterDamage;
                
                
                mHealth = mHealth - damage;
                if (actionID == 1) {
                    story = story + "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'>You manage to strike the mosnter doing " + damage.ToString() + " damage." +
                              "but the " + monster.monsterName + "was ready and attacks you doing " + monsterDamage.ToString() + "damage.</div><br>";
                } else if (actionID == -1) {
                    story = story + "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> The mosnter attempted to flee but you easily keep up. With this advantage you strike for " + damage.ToString() + "damage."+
                            "The monster wasnt ready and weakly attempt to strike only doing " + monsterDamage.ToString() + "damage. </div><br>";
                }
                

            }

            //monster defends
            if (actionID == 2) {
                mHealth = (int)(mHealth - (damage - (monster.armor * 1.5)));
                mHealth += 10;
                story = story + "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> The beast tries to get defensive protecting its vital spots. Not attacking it takes " + 
                        damage.ToString() + " damage but manages to take this time to heal a small amount of health.</div><br>";
            }

            var _return = new { action = endID, print = story, monsterH = mHealth, playerH = health, Magica = _magica, Stamina = _stamina };

            return Json(_return, JsonRequestBehavior.AllowGet);
        }

        private ActionResult DefendAction(int monsterHealth, int playerHealth) {
            GameModel game = (GameModel)Session["Game"];
            bool initiative = game.initiave;
            CharacterModel player = game.character;
            MonsterModel monster = game.monster;

            int monsterDamage = (int)(monster.damage - (monster.damage * (player.armor * 1.5)));
            if (monsterDamage < 0) {
                monsterDamage = 1;
            }
            int monsterSpeed = (monster.danger + monster.armor) / 2;

            int health = playerHealth;
            int mHealth = monsterHealth;

            string story = "";
            int endID = 1;


            //checks the initiative role.
            if (initiative == true)
            {
                //if true and the monster is stronger
                if (monster.danger > player.level)
                {
                    monsterDamage--;
                }
            }
            else
            {
                if (monster.danger > player.level)
                {
                    monsterDamage += (int)(monsterDamage * (.2));
                }
            }

            //TODO- Make a seperate method for this.
            int actionID = MonsterAI(mHealth, health, monster, player, initiative);
            //Called after the player action. Decides how the combat goes.

            //Monster flees the attack
            if (actionID == 0)
            {//Monster flee event.
                Random rand = new Random();
                int chance = rand.Next(20) + monsterSpeed;
                //Successful flee
                if (chance > 15)
                {
                    endID = 1;
                    story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px; '> You scared the monster into quick submisstion. Scared or not interesting in fighting the mosnter flees from you.</div><br>";

                }
                else
                {//flee unsucessful
                    actionID = -1;
                    monsterDamage = monsterDamage - 5;
                }
            }

            //monster attacks 
            if (actionID == 1 || actionID == -1)
            {
                health = (int)(health - (monsterDamage - (player.armor/100)));
                if (actionID == 1)
                {
                    story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'>You Raise your defenses in hopes to defelect damage." +
                              " but the " + monster.monsterName + " in unrelenting and attacks you for " + monsterDamage.ToString() + "damage.</div><br>";
                }
                else if (actionID == -1)
                {
                    story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> The mosnter attempted to flee but you easily keep up. Not willing to lower your guard you pursue." +
                            " </div><br>";
                }
            }

            //monster defends
            if (actionID == 2)
            {
                mHealth += 10;
                story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> The beast tries to get defensive protecting its vital spots. Not attacking it takes "+
                        "damage but manages to take this time to heal a small amount of health.</div><br>";
            }

            var _return = new { action = endID, print = story, monsterH = mHealth, playerH = health };

            return Json(_return, JsonRequestBehavior.AllowGet);
        }

        //For when the player decides to flee a monster event
        private ActionResult FleeAction() {
            GameModel game = (GameModel)Session["Game"];
            game.monster = null;
            string story = "<br><div style=' width: 10 %; height: auto; display: block; float: left; margin: 3px; padding: 3px;'> You turn around and run from the beast as fast as you can. Not wanting a fight you flee. " +
                        "</div><br>";


            var _return = new {  print = story, action = 0 };
            return Json(_return, JsonRequestBehavior.AllowGet);
        }

        //Used to decide which monsterAI to use and what the monster will do. 
        private int MonsterAI(int monsterHealth,int playerHealth, MonsterModel monster, CharacterModel player, bool inititiave) {
            int _returnInt = 0;
            int monsterBehaviour = monster.behaviour;
            MonsterAIPassive aiPassive = new MonsterAIPassive();
            MosnterAINeutral aiNeutral = new MosnterAINeutral();
            MonsterAIAressive aiAgressive = new MonsterAIAressive();

            if (monsterBehaviour == 0)
            {
                _returnInt = aiPassive.AI(monsterHealth, playerHealth, inititiave, monster, player);
            }
            else if (monsterBehaviour == 1)
            {
                _returnInt = aiNeutral.AI(monsterHealth, playerHealth, inititiave, monster, player);
            }
            else {
                _returnInt = aiAgressive.AI(monsterHealth, playerHealth, inititiave, monster, player);
            }

            return _returnInt;
        }
    }
}