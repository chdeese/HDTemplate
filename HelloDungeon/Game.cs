﻿using System;
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
        Player PlayerCharacter;
        Character enemy;
        Character[] enemies = new Character[3];
        Item[] items = new Item[5];
        Item[] playerInv = new Item[2];
        Effect none;


        //battle functions
        void Battle()
        {
            while (PlayerCharacter.GetHealth() > 0 && enemy.GetHealth() > 0)
            {
                PlayerCharacter.PrintBattleStats(enemy);
                
                //prints enemy's move and gives player a chance to react appropriately
                int enemyDecision = EnemyChoice();
                Console.WriteLine("------------------------------------------");

                int playerChoice = PlayerCharacter.GetInput("What will you do?", "Heavy Attack", "Light Attack", "Shield", "Dodge", "Use Item");
                float playerDamageDone = 0;
                float enemyDamageDone = 0;

                if (playerChoice == 1)
                {
                    if (enemyDecision == 1)
                    {
                        playerDamageDone = strongAttack(PlayerCharacter, ref enemy);
                        enemyDamageDone = strongAttack(enemy, ref PlayerCharacter);
                    }
                    else if (enemyDecision == 2)
                    {
                        enemyDamageDone = quickAttack(enemy, ref PlayerCharacter);
                    }
                    else if (enemyDecision == 3)
                    {
                        if (PlayerCharacter.Shield(enemy, ref PlayerCharacter))
                        {
                            playerDamageDone = (strongAttack(PlayerCharacter, ref enemy) / 2);
                            Console.WriteLine(enemy.GetName() + " blocked the attack!! They took " + playerDamageDone + " damage!");
                        }
                        else
                        {
                            playerDamageDone = strongAttack(PlayerCharacter, ref enemy);
                            Console.WriteLine(enemy.GetName() + " failed to block the attack!! They took " + playerDamageDone + " damage!");
                        }
                    }
                }
                else if (playerChoice == 2)
                {
                    if (enemyDecision == 1)
                    {
                        playerDamageDone = quickAttack(PlayerCharacter, ref enemy);
                        if (playerDamageDone > 0)
                        {
                            Console.WriteLine(PlayerCharacter.name + " hit " + enemy.name + " for " + playerDamageDone + " damage!!");
                        }
                    }
                    else if (enemyDecision == 2)
                    {
                        enemyDamageDone = quickAttack(enemy, ref PlayerCharacter);
                        if (enemyDamageDone > 0)
                        {
                            Console.WriteLine(enemy.name + " hit " + PlayerCharacter.name + " first for " + enemyDamageDone + " damage!!");
                        }
                    
                    }
                    else if (enemyDecision == 3)
                    {
                        if (shield(enemy, ref PlayerCharacter))
                        {

                            playerDamageDone = quickAttack(PlayerCharacter, ref enemy) / 2;
                            if (playerDamageDone > 0)
                            {
                                Console.WriteLine(enemy.name + " blocked " + PlayerCharacter.name + "'s attack! They took " + playerDamageDone + " damage!");
                            }
                        }
                        else
                        {
                            playerDamageDone = quickAttack(PlayerCharacter, ref enemy);
                            if (playerDamageDone > 0)
                            {
                                Console.WriteLine(enemy.name + " failed to block " + PlayerCharacter.name + "'s attack! They took " + playerDamageDone + " damage!");
                            }
                        }
                    }
                }
                else if (playerChoice == 3)
                {
                    if (enemyDecision == 1)
                    {
                        if (shield(PlayerCharacter, ref enemy))
                        {
                            enemyDamageDone = strongAttack(enemy, ref PlayerCharacter) / 2;

                            Console.WriteLine(PlayerCharacter.name + " blocked " + enemy.name + "'s attack! They took " + enemyDamageDone + " damage!");
                        }
                        else
                        {
                            enemyDamageDone = strongAttack(enemy, ref PlayerCharacter);
                            Console.WriteLine(PlayerCharacter.name + " failed to block " + enemy.name + "'s attack! They took " + enemyDamageDone + " damage!");
                        }
                    }
                    else if (enemyDecision == 2)
                    {
                        if (shield(enemy, ref PlayerCharacter))
                        {
                            Console.WriteLine(PlayerCharacter.name + " blocked " + enemy.name + "'s attack!");
                        }
                        else
                        {
                            enemyDamageDone = quickAttack(enemy, ref PlayerCharacter);
                            Console.WriteLine(PlayerCharacter.name + "'s block failed!! " + enemy.name + " hit " + PlayerCharacter.name + " for " + enemyDamageDone + "damage!");
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
                            Console.WriteLine(PlayerCharacter.name + " was able to evade " + enemy.name + "'s attack!!");
                        }
                        else if (enemyDecision == 1)
                        {
                            enemyDamageDone = strongAttack(enemy, ref PlayerCharacter);
                            Console.WriteLine(PlayerCharacter.name + " failed to dodge " + enemy.name + "'s attack!! " + PlayerCharacter.name + " took " + enemyDamageDone + " damage!");
                        }
                        else
                        {
                            enemyDamageDone = quickAttack(enemy, ref PlayerCharacter);
                            Console.WriteLine(PlayerCharacter.name + " failed to dodge " + enemy.name + "'s attack!! " + PlayerCharacter.name + " took " + enemyDamageDone + " damage!");
                        }
                    }
                }
                else
                {
                    useItem();
                }
                //then do damage
                enemy.TakeDamage(playerDamageDone);
                PlayerCharacter.TakeDamage(enemyDamageDone);
                PlayerCharacter.BonusDamage();
            }
            WinResult();
        }

        void WinResult()
        {
            if (PlayerCharacter.GetHealth() > 0 && enemy.GetHealth() <= 0)
            {
                Console.Clear();
                Console.WriteLine("VICTORY!");
                Console.WriteLine(PlayerCharacter.GetHealth() + " health left...");
            }
            else if (PlayerCharacter.GetHealth() <= 0 && enemy.GetHealth() > 0)
            {
                Console.Clear();
                Console.WriteLine("DEFEAT.");
                Console.WriteLine(enemy.GetName() + " has " + enemy.GetHealth() + " health remaining");
                playerAlive = false;
            }
        }

        int EnemyChoice()
        {
            float halfHealthAmount = enemy.GetHealth() / 2;
            if (Chance(PlayerCharacter, -3) && (enemy.GetHealth() > halfHealthAmount))
            {
                return 1;
            }
            else
            {
                if (Chance(PlayerCharacter, 3) && enemy.GetHealth() <= halfHealthAmount)
                {
                    Console.WriteLine("The " + enemy.GetName() + " is charging up an attack!!");
                    return 1;
                }
                else
                {
                    if (Chance(PlayerCharacter, -3))
                    {
                        Console.WriteLine("The " + enemy.GetName() + " is attacking!!");
                        return 2;
                    }
                    else
                    {
                        if (Chance(PlayerCharacter, 2))
                        {
                            Console.WriteLine("The " + enemy.GetName() + " is preparing for you to strike!!");
                            return 3;
                        }
                        else
                        {
                            Console.WriteLine("The " + enemy.GetName() + " is about to jump out of the way!!");
                            return 4;
                        }
                    }
                }
            }
        }


        //support functions
        bool Chance(Character enemy, int mod)
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

        void AssignStats(int selection)
        {
            if (selection == 1) //basketball player
            {
                Console.WriteLine("You picked Basketball Player");
                PlayerCharacter = new Player("", 50, 0.5f, 1, 2, GetWeapon(-1), none);
                className = "BasketBall Player";

            }
            else if (selection == 2) //businessman
            {
                Console.WriteLine("You picked Businessman");
                PlayerCharacter = new Player("", 30, 0, 1, 0, GetWeapon(-2), none);
                className = "Businessman";
            }
            else //selection 3 //hobo
            {
                Console.WriteLine("You picked Hobo");
                PlayerCharacter = new Player("", 20, 0, 1, 1, GetWeapon(-3), none);
                className = "Hobo";
            }
        }

        void CharacterCreation()
        {
            bool selectedClass = false;
            while (!selectedClass)
            {
                int x = PlayerCharacter.GetInput("Select your class.", "Basketball Player", "Business man", "Hobo", "", "");
                AssignStats(x);

                //have a starting weapon and choose to drop it

                PlayerCharacter.SetName(PlayerCharacter.GetText("Enter character name"));
                Console.Clear();
                Proceed();

                PlayerCharacter.PrintStats();

                if (PlayerCharacter.GetInput("Would you like to keep this selection?", "Yes", "Nahh", "", "", "") == 1)
                {
                    selectedClass = true;
                }
            }
        }

        Weapon GetWeapon(int selection)
        {
            if (selection == -1)
            {
                Weapon ball = new Weapon();
                ball._name = "Ball";
                ball._damage = 1;
                ball._effect._name = "Blunt";
                ball._effect._damage = 0;
                ball._effect._duration = 0;
                return ball;
            }
            else if (selection == -2)
            {
                Weapon briefcase = new Weapon();
                briefcase._name = "Briefcase";
                briefcase._damage = 2;
                briefcase._effect._name = "Blunt";
                briefcase._effect._damage = 0;
                briefcase._effect._duration = 0;
                return briefcase;
            }
            else if (selection == -3)
            {
                Weapon unarmed = new Weapon();
                unarmed._name = "Unarmed";
                unarmed._damage = 0.5f;
                unarmed._effect._name = "Blunt";
                unarmed._effect._damage = 0;
                unarmed._effect._duration = 0;
                return unarmed;
            }
            else if (selection == 1)
            {
                Weapon sword = new Weapon();
                sword._name = "Sword";
                sword._damage = 1.5f;
                sword._effect._name = "Bleeding";
                sword._effect._damage = 1.5f;
                sword._effect._duration = 3;
                return sword;
            }
            else if (selection == 2)
            {
                Weapon needle = new Weapon();
                needle._name = "Needle";
                needle._damage = 0.4f;
                needle._effect._name = "Poison";
                needle._effect._damage = 1;
                needle._effect._duration = 5;
                return needle;
            }
            else //selection 3
            {
                Weapon long_stick = new Weapon();
                long_stick._name = "Long Stick";
                long_stick._damage = 6;
                long_stick._effect._name = "Finesse";
                long_stick._effect._damage = 0;
                long_stick._effect._duration = 0;
                return long_stick;
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
                QuitGame();
            }

        }

        void CreateEnemies(ref Character[] enemy)
        {
            enemy[0] = new Character("Ghoul", 30, 1, 3, 0.5f, new Weapon(), new Effect());

            enemy[1] = new Character("Bear", 75, 4, 4, 3, new Weapon(), new Effect());

            enemy[2] = new Character("Dragon", 200, 10, 10, 5, new Weapon(), new Effect());
        }



        //base functions
        void Proceed()
        {
            Console.WriteLine("Press any key to proceed.");
            Console.ReadKey(true);
            Console.Clear();
        }

        bool QuitGame()
        {
            int response = 0;
            response = GetInput("Do you want to play again?", "Yes", "No", "", "", "");

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

            CharacterCreation();
            CreateEnemies(ref enemies);
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
                Proceed();
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
