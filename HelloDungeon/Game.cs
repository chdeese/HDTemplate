using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{
    class Game
    {
        //game variables
        struct Character
        {
            public string name;
            public float health;
            public float strength;
            public float baseDamage;
            public float dexterity;
            public Weapon weapon;
        }
        struct Weapon
        {
            public string name;
            public float damage;
            public Skill effect;
        }
        struct Skill
        {
            public string name;
            public int damage;
            public int duration;
        }
        bool gameOver;
        bool playerAlive;
        int stageNumber;
        Character player;
        Character enemy;


        //unfinished functions

        void assignStats(int selection)
        {
            if(selection == 1) //basketball player
            {
                Console.WriteLine("You picked Basketball Player");
                player.health = 50;
                player.strength = 0.5f;
                player.dexterity = 2;
                
            }
            else if(selection == 2) //businessman
            {
                Console.WriteLine("You picked Businessman");
                player.health = 30;
                player.strength = 0;
                player.dexterity = 0;
            }
            else //selection 3 //hobo
            {
                Console.WriteLine("You picked Hobo");
                player.health = 20;
                player.strength = 0;
                player.dexterity = 1;
            }
            player.baseDamage = 2;
        }

        void characterCreation()
        {
            bool selectedClass = false;
            player.name = getText("Enter player name");
            while (!selectedClass)
            {
                assignStats(getInput("Select your class.", "Basketball Player", "Business man", "Hobo", "", ""));

                printStats(player);

                //have a starting weapon and choose to drop it



            }
            //change 
        }

        void getWeapon(ref Character equipee, int selection)
        {
            if(selection == -1)
            {
                equipee.weapon.name = "Ball";
                equipee.weapon.damage = 2;
                equipee.weapon.effect.name = "Blunt";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
            else if(selection == -2)
            {
                equipee.weapon.name = "Briefcase";
                equipee.weapon.damage = 4;
                equipee.weapon.effect.name = "Blunt";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
            else if(selection == -3)
            {
                equipee.weapon.name = "Unarmed";
                equipee.weapon.damage = 2;
                equipee.weapon.effect.name = "Blunt";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
            else if(selection == 1)
            {
                equipee.weapon.name = "Sword";
                equipee.weapon.damage = 5;
                equipee.weapon.effect.name = "Bleeding";
                equipee.weapon.effect.damage = 3;
                equipee.weapon.effect.duration = 3;
            }
            else if(selection == 2)
            {
                equipee.weapon.name = "Needle";
                equipee.weapon.damage = 3;
                equipee.weapon.effect.name = "Poison";
                equipee.weapon.effect.damage = 2;
                equipee.weapon.effect.duration = 5;
            }
            else //selection 3
            {
                equipee.weapon.name = "Long Stick";
                equipee.weapon.damage = 6;
                equipee.weapon.effect.name = "Finesse";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
        }

        void Stage(int stageNumber)
        {
            if (stageNumber == 1)
            {
                createEnemy("ghoul");
                Battle();
            }
            else if (stageNumber == 2)
            {
                createEnemy("bear");
                Battle();
            }
            else if (stageNumber == 3)
            {
                createEnemy("dragon");
                Battle();
            }
            else
            {
                quitGame();
            }

        }

        void quickAttack(Character initiator, ref Character receiver)
        {
            receiver.health -= initiator.baseDamage * (initiator.strength + 1);
        }

        void Battle()
        {
            printStats(player);

            if(player.health <= 0 || enemy.health <= 0)
            {
                WinResult();
            }
        }

        void WinResult()
        {
            if(player.health > 0 && enemy.health <= 0)
            {
                Console.WriteLine("VICTORY!");
            }
            else if(player.health <= 0 && enemy.health > 0)
            {
                Console.WriteLine("DEFEAT.");
                playerAlive = false;
            }
        }
        void weaponDamage()
        {
            if (player.weapon.name == "Sword")
            {
                //Bleeding
            }
            else if (player.weapon.name == "Needle")
            {
                //Poison
            }
            else if (player.weapon.name == "Long Stick")
            {
                //Blunt, no extra damage
            }
            else
            {
                //unarmed
            }
            return;
        }
        void proceed()
        {
            Console.WriteLine("Press any key to proceed.");
            Console.ReadKey(true);
            Console.Clear();
        }

        //   void lvlUP(string character)


        //game functions

        void printStats(Character print)
        {
            Console.WriteLine("Name: " + print.name + "       Health: " + print.health + "\nStrength: " + print.strength + "     Dexterity: " + print.dexterity);
        }

        void heal(ref Character op, float health)
        {
            op.health += health;
        }

        //option 3-5 are optional and can toggled out by entering "" into the parameter slots for 3-5 when calling the function
        int getInput(string prompt, string option1, string option2, string option3, string option4, string option5)
        {
            string userInput = "";
            int result = 0;
            while (result != 1 && result != 2 && !(result == 3 && option3 != "") && !(result == 4 && option4 != "") && !(result == 5 && option5 != ""))
            {
                Console.WriteLine(prompt);
                Console.WriteLine("1. " + option1 + "\n2. " + option2);
                if (option3 != "")
                {
                    Console.WriteLine("3. " + option3);
                    if (option4 != "")
                    {
                        Console.WriteLine("4. " + option4);
                        if (option5 != "")
                        {
                            Console.WriteLine("5. " + option5);
                        }
                    }
                }
                Console.Write("> ");

                userInput = Console.ReadLine();
                if (userInput != "1" && userInput != "2" && !(userInput == "3" && option3 != "") && !(userInput == "4" && option4 != "") && !(userInput == "5" && option5 != ""))
                {
                    Console.WriteLine("Invalid Input, to try again, press any key.");
                    userInput = "";
                    Console.ReadKey(true);
                    continue;
                }
                else if (userInput == "1")
                {
                    result = 1;
                }
                else if (userInput == "2")
                {
                    result = 2;
                }
                else if (option3 != "")
                {
                    if (userInput == "3")
                    {
                        result = 3;
                    }
                    if (option4 != "")
                    {
                        if (userInput == "4")
                        {
                            result = 4;
                        }
                        if (option5 != "")
                        {
                            if (userInput == "5")
                            {
                                result = 5;
                            }
                        }
                    }
                }

            }
            Console.Clear();
            return result;

        }

        void createEnemy(string enemyName)
        {
            if (enemyName == "ghoul")
            {
                enemy.name = "Ghoul";
                enemy.health = 30;
                enemy.strength = 1;
                enemy.baseDamage = 5;
                enemy.dexterity = 0.5f;
            }
            else if (enemyName == "bear")
            {
                enemy.name = "Bear";
                enemy.health = 75;
                enemy.strength = 4;
                enemy.baseDamage = 4;
                enemy.dexterity = 3;
            }
            else if (enemyName == "dragon")
            {
                enemy.name = "Dragon";
                enemy.health = 200;
                enemy.strength = 10;
                enemy.baseDamage = 10;
                enemy.dexterity = 5;
            }
            else
            {
                Console.WriteLine("Error, failed enemy creation!");
                //self destruct
            }
            return;
        }

        string getText(string prompt)
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

        bool quitGame()
        {
            int response;
            if(playerAlive == false)
            {
                response = getInput("Do you want to play again?", "Restart", "Quit Game", "", "", "");
                response++;
            }
            else
            {
                response = getInput("Do you want to continue?", "Continue", "Restart", "Quit Game", "", "");
            }
            
            if (response == 1)
            {
                return false;
            }
            else if (response == 2)
            {
                playerAlive = false;
                return false;
            }
            else
            {
                return true;
            }
        }



        //base functions

        //start - called before the first loop and initalizes variables
        void Start()
        {
            gameOver = false;
            playerAlive = true;
            stageNumber = 1;

            characterCreation();
        }


        //update - called every time the game loops, like player input, character postions, game logic
        void Update()
        {
            while (!gameOver)
            {

                if (!playerAlive)
                {
                    Console.WriteLine("You Died!");
                    gameOver = quitGame();
                    continue;
                }
                Stage(stageNumber);
                proceed();
            }
        }


        //end - called after game loop exits
        //Used to clean up memory or display end game messages
        void End()
        {
            Console.WriteLine("Thanks for playing!");
        }


        public void Run()
        {
            Start();
            while (!gameOver)
            {
                Update();
            }
            End();
        }


    }
}
