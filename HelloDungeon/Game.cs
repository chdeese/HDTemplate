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
        struct Character
        {
           public string name;
           public float health;
           public float strength;
           public float baseDamage;
           public float dexterity;
        }

        //game variables


        //player stats


        //enemy stats

        void printStats(Character print)
        {
            Console.WriteLine("Name: " + print.name + "       Health: " + print.health + "\nStrength: " + print.strength + "     Dexterity: " + print.dexterity);
        }

        void heal(ref Character op, float health)
        {
            op.health += health;
        }

        //broke
        //option 3-5 are optional and can toggled out by entering "" into the parameter slots for 3-5 when calling the function
        int getInput(string prompt, string option1, string option2, string option3, string option4, string option5)
        {
            string userInput = "";
            int result = 0;
            while(result != 1 && result != 2 && !(result == 3 && option3 != "") && !(result == 4 && option4 != "") && !(result == 5 && option5 != ""))
            {
                Console.WriteLine(prompt);
                Console.WriteLine("1. " + option1 + "\n2. " + option2);
                if(option3 != "")
                {
                    Console.WriteLine("3. " + option3);
                    if(option4 != "")
                    {
                        Console.WriteLine("4. " + option4);
                        if(option5 != "")
                        {
                            Console.WriteLine("5. " + option5);
                        }
                    }
                }
                Console.Write("> ");

                userInput = Console.ReadLine();
                if(userInput == "1")
                {
                    result = 1;
                }
                else if (userInput == "2")
                {
                    result = 2;
                }
                else if (option3 != "")
                {
                    if(userInput == "3")
                    {
                        result = 3;
                    }
                    if(option4 != "")
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

        void selectClass(ref Character player)
        {
            string selectedClass = "";
            while(selectedClass == "")
            {
                //selectedClass = getInput("Select your class.", "Basketball Player", "Business man", "Hobo", "", "");
                printStats(player);
            }
            //change stats here with if

        }

        void selectClass(ref Character badguy)
        {
            
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
                if(text != "" && text != null)
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

        void createEnemy(ref Character badguy)
        {
            if (badguy.name == "Ghoul")
            {
                badguy.health = 30;
                badguy.strength = 1;
                badguy.baseDamage = 5;
                badguy.dexterity = 0.5f;
            }
            else if (badguy.name == "Bear")
            {
                badguy.health = 75;
                badguy.strength = 4;
                badguy.baseDamage = 4;
                badguy.dexterity = 3;
            }
            else if (badguy.name == "Dragon")
            {

            }
            else
            {
                //self destruct
            }
            return;
        }

        void quitGame()
        {

        }

        void Stage(int stageNumber)
        {
            if(stageNumber == 1)
            {
                Character ghoul;
                ghoul = selectClass(ghoul);
                ghoul = quickAttack(player, ghoul);
            }
            else if(stageNumber == 2)
            {
                Character bear;
            }
            else if(stageNumber == 3)
            {
                Character dragon;
            }
            
        }

        void lvlUP(string character)
        {

        }

        Character quickAttack(Character initiator, Character receiver)
        {
            receiver.health -= initiator.baseDamage * (initiator.strength + 1);
            return receiver;
        }
        
        void Battle(Character player, Character enemy)
        {
            printStats(player);
        }

        public void Run()
        {
            Character player = selectClass();
            player.name = getText("Enter player name");
            player.health = 0;
            heal(player, 50);
            Console.WriteLine(player.health);
            int n = 1;
            Stage(n);
        
        }

        
    }
}
