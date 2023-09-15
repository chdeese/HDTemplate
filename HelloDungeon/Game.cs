using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
            public Effect ailment;
        }
        struct Weapon
        {
            public string name;
            public float damage;
            public Effect effect;
        }
        struct Effect
        {
            public string name;
            public float damage;
            public float duration;
        }
        bool gameOver;
        bool playerAlive;
        int stageNumber;
        string className = "";
        Character player;
        Character enemy;

        Character[] chars = new Character[4];


        //unfinished functions
        int enemyChoice()
        {
            if(chance(ref player, -3) && (enemy.health > (enemy.health /= 2)))
            {
                return 1;
            }
            else
            {
                if(chance(ref player, 3) && enemy.health <= (enemy.health /= 2))
                {
                    return 1;
                }
                else
                {
                    if(chance(ref player, -3))
                    {
                        return 2;
                    }
                    else
                    {
                        if(chance(ref player, 2))
                        {
                            return 3;
                        }
                        else
                        {
                            return 4;
                        }
                    }
                }
            }
        }



        void shield(Character initiator, ref Character receiver)
        {
            
        }

        //actions

        void quickAttack(Character initiator, ref Character receiver)
        {
            if (chance(ref receiver, -3))
            {
                float damage = (initiator.weapon.damage) * (initiator.dexterity + 1);
                Console.WriteLine(initiator.name + " hit " + receiver.name + " for " + damage + " damage!!");
                receiver.health -= damage;

                //chance to add ailment
                if (initiator.weapon.effect.damage > 0 && chance(ref receiver, 2))
                {
                    receiver.ailment = initiator.weapon.effect;
                }
            }
            else
            {
                Console.WriteLine("You missed!!");
            }
        }

        void strongAttack(Character initiator, ref Character receiver)
        {
            if (chance(ref receiver, 2))
            {
                float damage = (initiator.weapon.damage + initiator.baseDamage) * (initiator.strength + 1);
                Console.WriteLine(initiator.name + " hit " + receiver.name + " for " + damage + " damage!!");
                receiver.health -= damage;

                //chance to add ailment
                if (initiator.weapon.effect.duration > 0 && chance(ref receiver, 5))
                {
                    receiver.ailment = initiator.weapon.effect;
                }
            }
            else
            {
                Console.WriteLine("You missed!!");
            }
        }

        //requires an invincible bool inside of the calling function to check if dodge was successful
        bool dodge(Character initiator, ref Character receiver)
        {

            //if dodge is successful, pass a true bool to the invincible bool 
            if(chance(ref receiver, 2))
            {
                Console.WriteLine(initiator.name + " dodged the attack!!");
                return true;
            }
            return false;
        }        

        //battle functions

        void Battle()
        {
            while (player.health > 0 && enemy.health > 0)
            {
                printStats(ref player);

                int playerChoice = getInput("What will you do?", "Heavy Attack", "Light Attack", "Shield", "Dodge", "Use Item");

                if (playerChoice == 1)
                {
                    strongAttack(player, ref enemy);
                }
                else if(playerChoice == 2)
                {
                    quickAttack(player, ref enemy);
                }
                else if(playerChoice == 3)
                {
                    shield(player, ref enemy);
                }
                else if(playerChoice == 4)
                {
                    dodge(player, ref enemy);
                }
                else
                {
                    getItem(player);
                }
                //then do enemy move (own function)

                bonusDamage();
            }
            WinResult();
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
        void bonusDamage()
        {
            if (player.ailment.duration != 0)
            {
                Console.WriteLine(player.name + " took " + player.ailment.damage + " from " + player.ailment.name + "!!");
                player.health -= player.ailment.damage;
                player.ailment.duration--;
            }
            if (enemy.ailment.duration != 0)
            {
                Console.WriteLine("The " + enemy.name + " took " + enemy.ailment.damage + " from " + enemy.ailment.name + "!!");
            }
        }


        //   void lvlUP(string character)


        //support functions

        //pass 1 for %100, 2 for %50, 3 for #30, 5 for %10, -3 for %70, and -5 for %90
        bool chance(ref Character enemy, int mod)
        {
            if(mod == 1)
            {
                return true;
            }
            else if(mod == 2)
            {
                if((enemy.health %= 2) == 0)
                {
                    return true;
                }
            }
            else if (mod == 3)
            {
                if ((enemy.health %= 3) == 0)
                {
                    return true;
                }
            }
            else if(mod == 5)
            {
                if((enemy.health %= 5) == 0)
                {
                    return true;
                }
            }
            else if(mod == -3)
            {
                if((enemy.health %= 3) != 0)
                {
                    return true;
                }
            }
            else // -5
            {
                if((enemy.health %= 5) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        void assignStats(int selection)
        {
            if(selection == 1) //basketball player
            {
                Console.WriteLine("You picked Basketball Player");
                player.health = 50;
                player.strength = 0.5f;
                player.dexterity = 2;
                className = "BasketBall Player";
                getWeapon(ref player, -1);

            }
            else if(selection == 2) //businessman
            {
                Console.WriteLine("You picked Businessman");
                player.health = 30;
                player.strength = 0;
                player.dexterity = 0;
                className = "Businessman";
                getWeapon(ref player, -2);
            }
            else //selection 3 //hobo
            {
                Console.WriteLine("You picked Hobo");
                player.health = 20;
                player.strength = 0;
                player.dexterity = 1;
                className = "Hobo";
                getWeapon(ref player, -3);
            }
            player.baseDamage = 2;
        }

        void characterCreation()
        {
            bool selectedClass = false;
            while (!selectedClass)
            {
                assignStats(getInput("Select your class.", "Basketball Player", "Business man", "Hobo", "", ""));

                //have a starting weapon and choose to drop it

                player.name = getText("Enter character name");
                Console.Clear();
                proceed();

                printStats(ref player);

                if(getInput("Would you like to keep this selection?", "Yes", "Nahh", "", "", "") == 1)
                {
                    selectedClass = true;
                }
            } 
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

        void proceed()
        {
            Console.WriteLine("Press any key to proceed.");
            Console.ReadKey(true);
            Console.Clear();
        }

        void printStats(ref Character print)
        {
            Console.WriteLine("Name: " + print.name + "       Class: " + className + "\nHealth: " + print.health + "\nStrength: " + print.strength + "\nDexterity: " + print.dexterity);
        }

        void printStats(ref Character printPlayer, ref Character printEnemy)
        {
            Console.WriteLine("Name: " + printPlayer.name + "      Class: " + className + "              Enemy: " + printEnemy.name + "       Health: " + printEnemy.health + "\nHealth: " + printPlayer.health + "\nStrength: " + printPlayer.strength + "\nDexterity: " + printPlayer.dexterity);
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
