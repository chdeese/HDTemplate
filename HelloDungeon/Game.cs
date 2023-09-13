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
            public Weapon equippedWeapon;
        }
        struct Weapon
        {
            public string name;
            public float damage;
            public string effectName;
            public int effectDamage;
        }
        bool gameOver;
        bool playerAlive;
        int stageNumber;
        Character player;
        Character enemy;


        //unfinished functions

        void selectClass()
        {
            int selectedClass = 0;
            player.name = getText("Enter player name");
            while (selectedClass == 0)
            {
                selectedClass = getInput("Select your class.", "Basketball Player", "Business man", "Hobo", "", "");


                //give class with selectClass function

                printStats(player);

                //have a starting weapon and choose to drop it

            }
            //change stats here with if
            return;
        }

        void Stage(int stageNumber)
        {
            if (stageNumber == 1)
            {
                createEnemy("ghoul");
                Battle(ref enemy);
            }
            else if (stageNumber == 2)
            {
                createEnemy("bear");
                Battle(ref enemy);
            }
            else if (stageNumber == 3)
            {
                createEnemy("dragon");
                Battle(ref enemy);
            }
            else
            {
                quitGame();
            }

        }

        Character quickAttack(Character initiator, ref Character receiver)
        {
            receiver.health -= initiator.baseDamage * (initiator.strength + 1);
            return receiver;
        }

        void Battle(ref Character enemy)
        {
            printStats(player);
        }

        void weaponDamage()
        {
            if (player.equippedWeapon == "Sword")
            {
                //Bleeding
            }
            else if (player.equippedWeapon == "Needle")
            {
                //Poison
            }
            else if (player.equippedWeapon == "Long Stick")
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
                else
                {
                    //do nothing
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

            selectClass();
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
