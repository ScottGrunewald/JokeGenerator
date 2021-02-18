/** JokeGenerator main class - Program
 * 
 * This is the main class of the JokeGenerator application, which connects to a
 * Chuck Norris joke site by way of an API and retrieves jokes to print to the console.
 * 
 * The application provides some console input options to choose to replace Chuck Norris
 * with a random name, retrieve random jokes from a specific category, and choose a number
 * of jokes between 1 and 9. All options have on-screen instructions.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace JokeGenerator
{
    class Program
    {
        //Instance Variables
        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> names;

        // Text Elements
        static readonly string appIntroductionText = "Welcome to the Random Chuck Norris Joke Application!";
        static readonly string howManyJokesString = "How many jokes do you want? (1-9)";
        static readonly string descriptionText = "Follow the prompts to read Chuck Norris jokes.";
        static readonly string categoryText = "Press c to see a list of joke categories";
        static readonly string randomJokeText = "Press r to get random jokes";
        static readonly string randomNameQuestionText = "Want to use a random name? y/n";
        static readonly string categoryQuestionText = "Want to specify a category? y/n";
        static readonly string numberOneToNineText = "Please enter a number between 1 and 9";
        static readonly string quitText = "Press q to quit";
        static readonly string returnToMenuText = "Press any key to return to the menu.";
        static readonly string thankYouText = "Thank you for using the Random Chuck Norris Joke Application!";
        static readonly string yesOrNoText = "Please press y or n.";
        static readonly string pleaseWaitText = "Please wait...";
        static readonly string enterCategoryText = "Enter a category:";
        static readonly string pleaseChooseText = "Please choose c, r, or q! Press any key to continue.";
        static readonly string categoryHeadingText = "The current categories are as follows:";

        // Dictionary to hold keyboard input values
        static Dictionary<ConsoleKey, char> ourKeyDictionary;


        static void Main(string[] args)
        {
            Boolean whileBreakFlag = false;
            LoadKeyDictionary();
            while (whileBreakFlag == false)
                {
                Console.Clear();
                Console.WriteLine(appIntroductionText);
                Console.WriteLine(descriptionText);
                Console.WriteLine(categoryText);
                Console.WriteLine(randomJokeText);
                Console.WriteLine(quitText);
                GetEnteredKey(Console.ReadKey());
                Console.WriteLine("");
                if (key == 'c')
                {
                    GetCategories();
                    PrintCategories();
                    Console.WriteLine(returnToMenuText);
                    GetEnteredKey(Console.ReadKey());
                }
                else if (key == 'r')
                {
                    Console.WriteLine(randomNameQuestionText);
                    Boolean randNameBreakFlag = false;
                    while (randNameBreakFlag == false)
                    {
                        GetEnteredKey(Console.ReadKey());
                        Console.WriteLine("");
                        if (key == 'y')
                        {
                            GetNames();
                            randNameBreakFlag = true;
                        }
                        else if (key == 'n')
                        {
                            randNameBreakFlag = true;
                        }
                        else
                        {
                            Console.WriteLine(yesOrNoText);
                        }
                    }
                    Console.WriteLine(categoryQuestionText);
                    Boolean catBreakFlag = false;
                    while (catBreakFlag == false)
                    {
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y' || key == 'n')
                        {
                            catBreakFlag = true;
                        }
                        else
                        {
                            Console.WriteLine(yesOrNoText);
                        }
                    }
                    Console.WriteLine("");
                    if (key == 'y')
                    {
                        Console.WriteLine(enterCategoryText);
                        String ourCategory = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine(howManyJokesString);
                        Boolean keyBreakFlag = false;
                        int n = 0;
                        while (keyBreakFlag == false)
                        {
                            GetEnteredKey(Console.ReadKey());
                            n = Convert.ToInt32(char.GetNumericValue(key));
                            if (n >= 1 && n <= 9)
                            {
                                keyBreakFlag = true;
                            }
                            else
                            {
                                Console.WriteLine("\n" + numberOneToNineText);
                            }
                        }
                        Console.WriteLine("");
                        GetRandomJokes(ourCategory, n);
                        PrintResults();
                        Console.WriteLine(returnToMenuText);
                        GetEnteredKey(Console.ReadKey());
                    }
                    else if (key == 'n')
                    {
                        Console.WriteLine(howManyJokesString);
                        Boolean keyBreakFlag = false;
                        int n = 0;
                        while (keyBreakFlag == false)
                        {
                            GetEnteredKey(Console.ReadKey());
                            n = Convert.ToInt32(char.GetNumericValue(key));
                            if (n >= 1 && n <= 9)
                            {
                                keyBreakFlag = true;
                            }
                            else
                            {
                                Console.WriteLine("\n" + numberOneToNineText);
                            }
                        }
                        Console.WriteLine("");
                        Console.WriteLine(pleaseWaitText + "\n");
                        GetRandomJokes(null, n);
                        PrintResults();
                        Console.WriteLine(returnToMenuText);
                        GetEnteredKey(Console.ReadKey());
                    }
                    else
                    {
                        Console.WriteLine(yesOrNoText);
                    }
                }
                else if (key == 'q')
                {
                    // Be polite to the user :)
                    Console.WriteLine(thankYouText);
                    // Break out of the loop and end the program
                    whileBreakFlag = true;
                }
                // If the user presses any other key, give some basic instruction and go back to the
                // control loop
                else
                {
                    Console.WriteLine(pleaseChooseText);
                    GetEnteredKey(Console.ReadKey());
                }
                names = null;
            }
        }

        private static void PrintResults()
        {
            for (int i = 0; i < results.Count(); i++)
            {
                if (results[i] != null)
                {
                    Console.WriteLine((i + 1) + ". " + string.Join(",", results[i]));
                    Console.WriteLine("");
                }
            }
        }

        private static void LoadKeyDictionary()
        {
            ourKeyDictionary = new Dictionary<ConsoleKey, char>
            {
                { ConsoleKey.D0, '0' },
                { ConsoleKey.D1, '1' },
                { ConsoleKey.D2, '2' },
                { ConsoleKey.D3, '3' },
                { ConsoleKey.D4, '4' },
                { ConsoleKey.D5, '5' },
                { ConsoleKey.D6, '6' },
                { ConsoleKey.D7, '7' },
                { ConsoleKey.D8, '8' },
                { ConsoleKey.D9, '9' },
                { ConsoleKey.C, 'c' },
                { ConsoleKey.N, 'n' },
                { ConsoleKey.Q, 'q' },
                { ConsoleKey.R, 'r' },
                { ConsoleKey.Y, 'y' }
            };
        }

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            key = ourKeyDictionary.GetValueOrDefault(consoleKeyInfo.Key);
        }

        private static void GetRandomJokes(string category, int number)
        {
            new JsonJokeFeed(number);
            results = JsonJokeFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        }

        private static void GetCategories()
        {
            new JsonJokeFeed(0);
            results = (JsonJokeFeed.GetCategories());
        }

        private static void PrintCategories()
        {
            Boolean breakFlag = false;
            int currentIndex = 0;
            int nextIndexDifference;
            Console.WriteLine("\n" + categoryHeadingText);
            while (breakFlag == false)
            {
                currentIndex = results[0].IndexOf('"', currentIndex + 1);
                if (currentIndex < 0)
                {
                    breakFlag = true;
                }
                else
                {
                    nextIndexDifference = results[0].IndexOf('"', currentIndex + 1) - (currentIndex+1);
                    if(nextIndexDifference > 1)
                    {
                        // Print the next category
                        Console.WriteLine("- " + results[0].Substring(currentIndex + 1, nextIndexDifference));
                        currentIndex += nextIndexDifference;
                    }
                    else if (nextIndexDifference < 1)
                    {
                        // Exit the loop
                        breakFlag = true;
                    }
                    // Notionally, else if nextIndexDifference = 1 then this is a comma so don't print it
                }
            }
            Console.WriteLine("");
        }

        private static void GetNames()
        {
            new JsonNameFeed();
            dynamic result = JsonNameFeed.GetNames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
