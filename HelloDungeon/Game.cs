using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
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
        bool gameOver;
        bool restart;
        bool playerAlive;
        int stageNumber;
        string className = "";
        Character player;
        Character enemy;
        Character[] enemies = new Character[3];
        Item[] items = new Item[5];
        Item[] playerInv = new Item[2];


        //battle functions
        void Battle()
        {
            while (player.GetHealth() > 0 && enemy.GetHealth() > 0)
            {
                player.PrintStats(player);

                int playerChoice = GetInput("What will you do?", "Heavy Attack", "Light Attack", "Shield", "Dodge", "Use Item");
                int enemyDecision = EnemyChoice();
                float playerDamageDone = 0;
                float enemyDamageDone = 0;

                if (playerChoice == 1)
                {
                    if (enemyDecision == 1)
                    {
                        playerDamageDone = strongAttack(player, ref enemy);
                        enemyDamageDone = strongAttack(enemy, ref player);
                    }
                    else if (enemyDecision == 2)
                    {
                        enemyDamageDone = quickAttack(enemy, ref player);
                    }
                    else if (enemyDecision == 3)
                    {
                        if (shield(enemy, ref player))
                        {
                            playerDamageDone = (strongAttack(player, ref enemy) / 2);
                            Console.WriteLine(enemy.name + " blocked the attack!! They took " + playerDamageDone + " damage!");
                        }
                        else
                        {
                            playerDamageDone = strongAttack(player, ref enemy);
                            Console.WriteLine(enemy.name + " failed to block the attack!! They took " + playerDamageDone + " damage!");
                        }
                    }
                }
                else if (playerChoice == 2)
                {
                    if (enemyDecision == 1)
                    {
                        playerDamageDone = quickAttack(player, ref enemy);
                        if (playerDamageDone > 0)
                        {
                            Console.WriteLine(player.name + " hit " + enemy.name + " for " + playerDamageDone + " damage!!");
                        }
                    }
                    else if (enemyDecision == 2)
                    {
                        enemyDamageDone = quickAttack(enemy, ref player);
                        if (enemyDamageDone > 0)
                        {
                            Console.WriteLine(enemy.name + " hit " + player.name + " first for " + enemyDamageDone + " damage!!");
                        }
                    
                    }
                    else if (enemyDecision == 3)
                    {
                        if (shield(enemy, ref player))
                        {

                            playerDamageDone = quickAttack(player, ref enemy) / 2;
                            if (playerDamageDone > 0)
                            {
                                Console.WriteLine(enemy.name + " blocked " + player.name + "'s attack! They took " + playerDamageDone + " damage!");
                            }
                        }
                        else
                        {
                            playerDamageDone = quickAttack(player, ref enemy);
                            if (playerDamageDone > 0)
                            {
                                Console.WriteLine(enemy.name + " failed to block " + player.name + "'s attack! They took " + playerDamageDone + " damage!");
                            }
                        }
                    }
                }
                else if (playerChoice == 3)
                {
                    if (enemyDecision == 1)
                    {
                        if (shield(player, ref enemy))
                        {
                            enemyDamageDone = strongAttack(enemy, ref player) / 2;

                            Console.WriteLine(player.name + " blocked " + enemy.name + "'s attack! They took " + enemyDamageDone + " damage!");
                        }
                        else
                        {
                            enemyDamageDone = strongAttack(enemy, ref player);
                            Console.WriteLine(player.name + " failed to block " + enemy.name + "'s attack! They took " + enemyDamageDone + " damage!");
                        }
                    }
                    else if (enemyDecision == 2)
                    {
                        if (shield(enemy, ref player))
                        {
                            Console.WriteLine(player.name + " blocked " + enemy.name + "'s attack!");
                        }
                        else
                        {
                            enemyDamageDone = quickAttack(enemy, ref player);
                            Console.WriteLine(player.name + "'s block failed!! " + enemy.name + " hit " + player.name + " for " + enemyDamageDone + "damage!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You both take defensive positions but nothing happens.");
                    }
                }
                else if (playerChoice == 4)
                {
                    if (enemyDecision == 3 || enemyDecision == 4)
                    {
                        Console.WriteLine("You both take defensive positions but nothing happens.");
                    }
                    else
                    {
                        if (dodge(ref enemy))
                        {
                            Console.WriteLine(player.name + " was able to evade " + enemy.name + "'s attack!!");
                        }
                        else if (enemyDecision == 1)
                        {
                            enemyDamageDone = strongAttack(enemy, ref player);
                            Console.WriteLine(player.name + " failed to dodge " + enemy.name + "'s attack!! " + player.name + " took " + enemyDamageDone + " damage!");
                        }
                        else
                        {
                            enemyDamageDone = quickAttack(enemy, ref player);
                            Console.WriteLine(player.name + " failed to dodge " + enemy.name + "'s attack!! " + player.name + " took " + enemyDamageDone + " damage!");
                        }
                    }
                }
                else
                {
                    useItem();
                }
                //then do damage
                enemy.TakeDamage(playerDamageDone);
                player.TakeDamage(enemyDamageDone);
                BonusDamage();
            }
            WinResult();
        }

        void WinResult()
        {
            if (player.GetHealth() > 0 && enemy.GetHealth() <= 0)
            {
                Console.Clear();
                Console.WriteLine("VICTORY!");
                Console.WriteLine(player.GetHealth() + " health left...");
            }
            else if (player.GetHealth() <= 0 && enemy.GetHealth() > 0)
            {
                Console.Clear();
                Console.WriteLine("DEFEAT.");
                Console.WriteLine(enemy.GetName() + " has " + enemy.GetHealth() + " health remaining");
                playerAlive = false;
            }
        }

        void BonusDamage()
        {
            if (player.GetAilment().duration != 0)
            {
                Console.WriteLine(player.name + " took " + player.ailment.damage + " from " + player.ailment.name + "!!");
                player.health -= player.ailment.damage;
                player.ailment.duration--;
            }
            if (enemy.GetAilment().duration != 0)
            {
                Console.WriteLine("The " + enemy.GetName() + " took " + enemy.GetAilment().damage + " from " + enemy.GetAilment().name + "!!");
            }
        }

        int EnemyChoice()
        {
            float halfHealthAmount = enemy.GetHealth() / 2;
            if (Chance(ref player, -3) && (enemy.GetHealth() > halfHealthAmount))
            {
                return 1;
            }
            else
            {
                if (Chance(ref player, 3) && enemy.GetHealth() <= halfHealthAmount)
                {
                    return 1;
                }
                else
                {
                    if (Chance(ref player, -3))
                    {
                        return 2;
                    }
                    else
                    {
                        if (Chance(ref player, 2))
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


        //support functions
        bool Chance(ref Character enemy, int mod)
        {  
            //pass 1 for %100, 2 for %50, 3 for #30, 5 for %10, -3 for %70, and -5 for %90
            if (mod == 1)
            {
                return true;
            }
            else if (mod == 2)
            {
                if ((enemy.GetHealth() % 2) == 0)
                {
                    return true;
                }
            }
            else if (mod == 3)
            {
                if ((enemy.GetHealth() % 3) == 0)
                {
                    return true;
                }
            }
            else if (mod == 5)
            {
                if ((enemy.GetHealth() % 5) == 0)
                {
                    return true;
                }
            }
            else if (mod == -3)
            {
                if ((enemy.GetHealth() % 3) != 0)
                {
                    return true;
                }
            }
            else // -5
            {
                if ((enemy.GetHealth() % 5) != 0)
                {
                    return true;
                }
            }
            return false;
        }


        void CharacterCreation()
        {
            bool selectedClass = false;
            while (!selectedClass)
            {
                int x = GetInput("Select your class.", "Basketball Player", "Business man", "Hobo", "", "");
                player.AssignStats(x);

                //have a starting weapon and choose to drop it

                player.SetName(GetText("Enter character name"));
                Console.Clear();
                Proceed();

                player.PrintStats(player);

                if (GetInput("Would you like to keep this selection?", "Yes", "Nahh", "", "", "") == 1)
                {
                    selectedClass = true;
                }
            }
        }

        void GetWeapon(ref Character equipee, int selection)
        {
            if (selection == -1)
            {
                equipee.weapon.name = "Ball";
                equipee.weapon.damage = 1;
                equipee.weapon.effect.name = "Blunt";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
            else if (selection == -2)
            {
                equipee.weapon.name = "Briefcase";
                equipee.weapon.damage = 2;
                equipee.weapon.effect.name = "Blunt";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
            else if (selection == -3)
            {
                equipee.weapon.name = "Unarmed";
                equipee.weapon.damage = 0.5f;
                equipee.weapon.effect.name = "Blunt";
                equipee.weapon.effect.damage = 0;
                equipee.weapon.effect.duration = 0;
            }
            else if (selection == 1)
            {
                equipee.weapon.name = "Sword";
                equipee.weapon.damage = 1.5f;
                equipee.weapon.effect.name = "Bleeding";
                equipee.weapon.effect.damage = 1.5f;
                equipee.weapon.effect.duration = 3;
            }
            else if (selection == 2)
            {
                equipee.weapon.name = "Needle";
                equipee.weapon.damage = 0.4f;
                equipee.weapon.effect.name = "Poison";
                equipee.weapon.effect.damage = 1;
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
                enemy = enemies[0];
                Battle();
            }
            else if (stageNumber == 2)
            {
                enemy = enemies[1];
                Battle();
            }
            else if (stageNumber == 3)
            {
                enemy = enemies[2];
                Battle();
            }
            else
            {
                quitGame();
            }

        }

        void CreateEnemies(ref Character[] enemy)
        {
            enemy[0] = new Character("Ghoul", 30, 1, 3, 0.5f, new Weapon(), new Effect());

            enemy[1] = new Character("Bear", 75, 4, 4, 3, new Weapon(), new Effect());

            enemy[2] = new Character("Dragon", 200, 10, 10, 5, new Weapon(), new Effect());
        }


        //base functions
        int GetInput(string prompt, string option1, string option2, string option3, string option4, string option5)
        {  
            //option 3-5 are optional and can toggled out by entering "" into the parameter slots for 3-5 when calling the function
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

        string GetText(string prompt)
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

        void Proceed()
        {
            Console.WriteLine("Press any key to proceed.");
            Console.ReadKey(true);
            Console.Clear();
        }

        bool QuitGame()
        {
            int response = 0;
            response = getInput("Do you want to play again?", "Yes", "No", "", "", "");

            if (response == 2)
            {
                restart = false;
            }
            return true;
        }



        //start - called before the first loop and initalizes variables
        void Start()
        {
            gameOver = false;
            playerAlive = true;
            stageNumber = 1;

            characterCreation();
            createEnemies(ref enemies);
        }

        //update - called every time the game loops, like player input, character postions, game logic
        void Update()
        {
            while (!gameOver)
            {

                if (!playerAlive)
                {
                    Console.WriteLine("You Died!");
                    gameOver = true;
                    stageNumber = 1;
                    continue;
                }
                playerAlive = true;
                Stage(stageNumber);
                stageNumber++;
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
            restart = true;
            while (restart)
            {
                Start();
                while (!gameOver)
                {
                    Update();
                }
            }
            End();
        }


    }
}
