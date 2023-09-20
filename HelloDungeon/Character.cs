using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{        
        struct Weapon
        {
            public string _name;
            public float _damage;
            public Effect _effect;
        }
        struct Effect
        {
            public string _name;
            public float _damage;
            public float _duration;
        }
        struct Item
        {
            public string _name;
            public Effect _effects;
            public void DoStuff()
        {
            Console.WriteLine();
        }
        }

    internal class Character
    {
        public Character(string name, float health, float strength, float damage, float dexterity, Weapon weapon, Effect ailment)
        {
            //returns new Character
            _name = name;
            _health = health;
            _strength = strength;
            _baseDamage = damage;
            _dexterity = dexterity;
            _weapon = weapon;
            _ailment = ailment;
        }


        private string _name;
        private float _health;
        private float _strength;
        private float _baseDamage;
        private float _dexterity;
        private Weapon _weapon;
        private Effect _ailment;

        public string GetName()
        {
         return _name;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public float GetHealth()
        {
            return _health;
        }
        public float GetStrength()
        {
            return _strength;
        }
        public float GetDexterity()
        {
            return _dexterity;
        }
        public float GetDamage()
        {
            return _baseDamage + _weapon._damage;
        }
        public Effect GetAilment()
        {
            return _ailment;
        }
        public Weapon GetWeapon()
        {
            return _weapon;
        }


        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
        //float quickAttack(ref Character receiver)
        //{
        //    if (chance(ref receiver, -3))
        //    {
        //        float damage = (_weapon.damage) * (_dexterity + 1);

        //        //chance to add ailment
        //        if (_weapon.effect.damage > 0 && chance(ref receiver, 2))
        //        {
        //            receiver._ailment = _weapon.effect;
        //        }
        //        return damage;
        //    }
        //    else
        //    {
        //        Console.WriteLine("You missed!!");
        //    }
        //    return 0;
        //}

        //float strongAttack(Character initiator, ref Character receiver)
        //{
        //    if (chance(ref receiver, 2))
        //    {
        //        float damage = (_weapon.damage + _baseDamage) * (_strength + 1);

        //        //chance to add ailment
        //        if (_weapon.effect.duration > 0 && chance(ref receiver, 5))
        //        {
        //            receiver._ailment = _weapon.effect;
        //        }
        //        return damage;
        //    }
        //    else
        //    {
        //        Console.WriteLine("You missed!!");
        //    }
        //    return 0;
        //}

        //requires an invincible bool inside of the calling function to check if dodge was successful
        void UseItem()
        {
            int selection = 0;
            if (playerInv[0].name == null)
            {
                Console.WriteLine("You have no items");
                selection = 0;
            }
            else
            {
                if (playerInv[1].name == null)
                {
                    int x;
                    x = getInput("Use " + playerInv[0].name + "?", "Yes", "No", "", "", "");
                    if (x == 1)
                    {
                        selection = 1;
                    }
                    else
                    {
                        selection = 0;
                    }
                }
                else
                {
                    selection = getInput("Choose an Item", playerInv[0].name, playerInv[1].name, "Nevermind", "", "");
                }
            }

            if (selection == 1)
            {
                int x = 0;
                x = getInput("Use " + playerInv[0].name + "?", "Yes", "No", "", "", "");
                if (x == 1)
                {
                    player.ailment = playerInv[0].effects;
                }
                else
                {
                    useItem();
                }
            }
            else if (selection == 2)
            {
                int x = 0;
                x = getInput("Use " + playerInv[1].name + "?", "Yes", "No", "", "", "");
                if (x == 1)
                {
                    player.ailment = playerInv[1].effects;
                }
                else
                {
                    useItem();
                }
            }
            else
            {
                Console.WriteLine("You did nothing");
                proceed();
            }
        }
        public void Heal(float health)
        {
            _health += health;
        }



        //WIP




        bool Dodge(ref Character receiver)
        {

            //if dodge is successful, pass a true bool to the invincible bool 
            if (chance(ref receiver, 2))
            {
                return true;
            }
            return false;
        }
        bool Shield(Character initiator, ref Character receiver)
        {
            if (chance(ref receiver, 2))
            {
                return true;
            }
            else
            {
                Console.WriteLine(initiator.name + " failed to block!!");
            }
            return false;
        }
        public void PrintStats()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Name: " + _name + "    Health: " + _health + "\nStrength: " + _strength + "    Dexterity: " + _dexterity);
        }
        public void PrintBattleStats(Character printEnemy)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Name: " + _name + "    Health: " + _health + "\nStrength: " + _strength + "    Dexterity: " + _dexterity);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Enemy: " + printEnemy.GetName() + " HP: " + printEnemy.GetHealth());
        }
        public void AddToInventory(Item item)
        {
            if (playerInv[0].name == null)
            {
                playerInv[0] = item;
            }
            else if (playerInv[1].name == null)
            {
                playerInv[1] = item;
            }
            else
            {
                int x = 0;
                int y = 0;
                x = getInput("Inventory is full, would you like to overwrite an item?", "Yes", "No", "", "", "");
                if (x == 1)
                {
                    y = getInput("Which Item do you want to replace with " + item.name + "?", playerInv[0].name, playerInv[1].name, "", "", "");
                    if (y == 1)
                    {
                        playerInv[0] = item;
                    }
                    else
                    {
                        playerInv[1] = item;
                    }
                }
                else
                {
                    Console.WriteLine("You put back " + item.name);
                }
            }

        }
    }
}
