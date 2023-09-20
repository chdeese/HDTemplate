using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private int _playerChoice;
        public Player() : base()
        {
            _playerChoice = 0;
        }
        public Player(string name, float health, float strength, float damage, float dexterity, Weapon weapon, Effect ailment) : base(name, health, strength, damage, dexterity, weapon, ailment)
        {
            _playerChoice = 0;
        }

        public void BonusDamage(ref Character enemy)
        {
            if (GetAilment()._name != "")
            {
                Console.WriteLine(GetName() + " took " + GetAilment()._damage + " damage from " + GetAilment()._name + "!!");
                SetHealth(GetHealth() - GetAilment()._damage);
                SetAilmentDuration(GetAilment()._duration - 1);
            }
            if (enemy.GetAilment()._duration != 0)
            {
                Console.WriteLine("The " + enemy.GetName() + " took " + enemy.GetAilment()._damage + " damage from " + enemy.GetAilment()._name + "!!");
            }
        }

        public string GetText(string prompt)
        {
            string text = "";

            while (text == "")
            {
                //display prompt and recieve input
                Console.Write(prompt + "\n> ");
                text = Console.ReadLine();
                Console.WriteLine();

                //check input
                if (text != "" && text != null)
                {
                    Console.WriteLine(text);
                    return text;
                }

                //reset if input is incorrect
                Console.WriteLine("Please enter text" +
                    "\n--Press any key to continue--");
                Console.ReadKey(true);
                Console.Clear();
                text = "";

            }
            return "Bob";
        }

        public int GetInput(string prompt, string option1, string option2)
        {
            string userInput = "";

            while (userInput != "1" && userInput != "2")
            {

                Console.WriteLine(prompt);
                Console.WriteLine("1. " + option1 + "\n2. " + option2);

                Console.Write("> ");

                userInput = Console.ReadLine();
                Console.WriteLine();

                if(userInput == "1")
                {
                    _playerChoice = 1;
                    return 1;
                }
                else if (userInput == "2")
                {
                    _playerChoice = 2;
                    return 2;
                }
                else
                {
                    Console.WriteLine("Invalid Input, try again.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            return 0;
        }

        public int GetInput(string prompt, string option1, string option2, string option3)
        {
            string userInput = "";

            while (userInput != "1" && userInput != "2" && userInput != "3")
            {

                Console.WriteLine(prompt);
                Console.WriteLine("1. " + option1 + "\n2. " + option2 + "\n3. " + option3);

                Console.Write("> ");

                userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput == "1")
                {

                    _playerChoice = 1;
                    return 1;
                }
                else if (userInput == "2")
                {
                    _playerChoice = 2;
                    return 2;
                }
                else if (userInput == "3")
                {

                    _playerChoice = 3;
                    return 3;
                }
                else
                {
                    Console.WriteLine("Invalid Input, try again.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            return 0;
        }

        public int GetInput(string prompt, string option1, string option2, string option3, string option4)
        {
            string userInput = "";

            while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4")
            {

                Console.WriteLine(prompt);
                Console.WriteLine("1. " + option1 + "\n2. " + option2 + "\n3. " + option3 + "\n4. " + option4);

                Console.Write("> ");

                userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput == "1")
                {
                    _playerChoice = 1;
                    return 1;
                }
                else if (userInput == "2")
                {
                    _playerChoice = 2;
                    return 2;
                }
                else if (userInput == "3")
                {
                    _playerChoice = 3;
                    return 3;
                }
                else if (userInput == "4")
                {
                    _playerChoice = 4;
                    return 4;
                }
                else
                {
                    Console.WriteLine("Invalid Input, try again.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            return 0;
        }

        public int GetInput(string prompt, string option1, string option2, string option3, string option4, string option5)
        {
            string userInput = "";

            while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5")
            {

                Console.WriteLine(prompt);
                Console.WriteLine("1. " + option1 + "\n2. " + option2 + "\n3. " + option3 + "\n4. " + option4 + "\n5. " + option5);

                Console.Write("> ");

                userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput == "1")
                {
                    _playerChoice = 1;
                    return 1;
                }
                else if (userInput == "2")
                {
                    _playerChoice = 2;
                    return 2;
                }
                else if (userInput == "3")
                {
                    _playerChoice = 3;
                    return 3;
                }
                else if (userInput == "4")
                {
                    _playerChoice = 4;
                    return 4;
                }
                else if (userInput == "5")
                {
                    _playerChoice = 5;
                    return 5;
                }
                else
                {
                    Console.WriteLine("Invalid Input, try again.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            return 0;
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

        //override overrides higher higharchy function when called.
        public override void PrintStats()
        {
            base.PrintStats();
            //additional stats only for the player
        }
    }
}
