using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HelloDungeon
{
    struct Item
    {
        public string _name;
        public Effect _effects;
    }

    internal class Player : Character
    {
        public Player(string name, float health, float strength, float damage, float dexterity, Weapon weapon, Effect ailment) : base(name, health, strength, damage, dexterity, weapon, ailment)
        {

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

        public void PrintBattleStats(Character printEnemy)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Name: " + _name + "    Health: " + _health + "\nStrength: " + _strength + "    Dexterity: " + _dexterity);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Enemy: " + printEnemy.GetName() + " HP: " + printEnemy.GetHealth());
        }
    }
}
