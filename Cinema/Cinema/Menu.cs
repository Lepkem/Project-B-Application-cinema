using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    using System.Globalization;

    class Menu
    {
        Boolean running = true;
        int caseSwitch = 0;
        Boolean login = false;

        public void switchCase()
        {
            while (running)
            {
                switch (caseSwitch)
                {   //functions
                    case 0:
                        //menu
                        caseSwitch = MenuHandler(login);
                        break;

                    case 1:
                        Console.Clear();
                        //Login OR logout
                        if (!login) { login = Login(); }
                        else { Console.WriteLine("\n\n"); login = false; }
                        caseSwitch = 0;
                        break;

                    case 2:
                        Console.Clear();
                        //Print a Shedule
                        Program.printSchedule();
                        caseSwitch = 0;
                        break;

                    case 3:
                        Console.Clear();
                        //Search
                        Search search = new Search();
                        caseSwitch = 0;
                        break;


                    case 4:
                        //print a room
                        ShowRoom();
                        caseSwitch = 0;
                        break;

                    case 5:
                        //Order
                        Console.Clear();
                        Order.orderMenu();
                        caseSwitch = 0;
                        break;

                    case 6:
                        ExpectedMovies();
                        StandardMessages.PressAnyKey();
                        StandardMessages.PressKeyToContinue();
                        Console.Clear();
                        caseSwitch = 0;
                        break;

                    case 8:
                        //FAQ
                        faq();
                        caseSwitch = 0;
                        break;

                    case 9:
                        //Contact
                        contact();
                        caseSwitch = 0;
                        break;

                    //Admin functions
                    case 10:
                        //Test update room
                        Console.WriteLine("Which room do you want to change?");
                        Program.rooms[2].updateRoom(string.Format(@".\rooms\room{0}.json", int.Parse(Console.ReadLine())));
                        caseSwitch = 0;
                        break;

                    case 11:
                        //Test create room
                        Program.createRoom();
                        caseSwitch = 0;
                        break;

                    case 12:
                        //Add movie Jitske
                        // This will get the current WORKING directory (i.e. \bin\Debug)
                        string workingDirectory = Environment.CurrentDirectory;
                        // or: Directory.GetCurrentDirectory() gives the same result

                        // This will get the current PROJECT directory
                        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                        Movies movies = new Movies(Path.Combine(projectDirectory, @"movies/movie.json"));

                        movies.updateCreateMovie();
                        Program.readMovies();
                        caseSwitch = 0;
                        break;

                    case 13:
                        //Add a scheduleElement
                        Program.createShedule();
                        caseSwitch = 0;
                        break;

                     case 14:
                        //Search Order by ID
                        IDBank.searchOrderByID();
                        caseSwitch = 0;
                        break;

                    case 15:
                        //Search Order by emailAddress
                        IDBank.SearchOrderByEmail();
                        caseSwitch = 0;
                        break;

                    default:
                        Console.WriteLine("That's not an option, please choose again");
                        caseSwitch = 0;
                        break;
                }
            }
        }

        static int MenuHandler(Boolean login)
        {
            int parsable = 0;
            string menu = "[1]Login \n[2]Print schedule\n[3]Search  \n[4]Show room \n[5]Order Tickets \n[6]Show coming movies \n[8]FAQ \n[9]Contact\n";

            //text being displayed in menu
            if (!login) { Console.WriteLine(menu); }
            //text being displayed in menu Admin version


            if (login) { Console.WriteLine($"[1]Logout \n[2]Print schedule\n[3]Search  \n[4]Show Room \n[5]Order Tickets \n[6]Show coming movies \n[8]FAQ \n[9]Contact \n[10]Edit room \n[11]Create room \n[12]Create movie \n[13]Add to schedule \n[14]Search order by ID \n[15]Search order by Email address customer"); }

            while (true)
            {

                //gets user input converts it to numbers
                string function = Console.ReadLine();
                bool isParsable = Int32.TryParse(function, out parsable);
                if (isParsable)
                {
                    //checks if number is the same as a user fucntion
                    if (!login)
                    {
                        if (0 < parsable && parsable < 10) { return parsable; } //number equal to possible functions +1
                        else { Console.WriteLine("Please only select from the options given."); }
                    }
                    //checks if number is the same as a user OR admin fucntion
                    if (login)
                    {
                        if (0 < parsable && parsable < 151) { return parsable; } //number equal to possible functions +1
                        else { Console.WriteLine("Please only select from the options given."); }
                    }
                }
                else { Console.WriteLine("Please only select from the options given."); }
            }
        }

        static Boolean Login()
        {
            while (true)
            {
                string username, password = string.Empty;

                //asks user input username
                Console.Write("Enter a username: (admin) ");
                username = Console.ReadLine();

                //Exit login screen
                if (username == "b")
                {
                    Console.WriteLine("\n\n");
                    return false;
                }

                //ask user input password
                Console.Write("Enter a password: (admin) ");
                password = Console.ReadLine();

                //checks if user input correct.
                if (username == "admin" && password == "admin")
                {
                    Console.WriteLine("\n \nWelcome admin");
                    return true;
                }
                else { Console.WriteLine("Wrong input, please try again \n" + "if you want to return to the menu write b as username"); }

            }
        }

        static void faq()
        {
            //Set variables
            bool looping = true;

            //While loop
            Console.Clear();
            while (looping)
            {
                int question = 0;
                Console.WriteLine("\n[1]Is the cinema suitable for people in a wheelchair? \n[2]Does the cinema have sweet popcorn? \n[3]What are the opening hours of the cinema \n[4]Quit");

                //Ask for case input and quit when input is invalid
                try { question = int.Parse(Console.ReadLine()); } catch { }
                Console.WriteLine();
                Console.Clear();

                //Switch case
                switch (question)
                {
                    case 1:
                        Console.WriteLine("The cinema is certainly suitable for people in a wheelchair. \n" +
                            "There is a lift for the 2nd floor and all \n" +
                            "aisles are wide enough for wheelchairs. \n");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 2:
                        Console.WriteLine("The cinema has 3 types of popcorn.\n" +
                            "Sweet, salty and caramel.\n");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 3:
                        Console.WriteLine("We are currently closed due to the corona virus! \n" +
                            "we have a home cinema ready! see the website. \n");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Going back"); looping = false; break;
                    default:
                        Console.WriteLine("Enter an existing value"); break;
                }

                //static void printFAQ()
                //{
                //    string FileContentString = "";
                //   FileContentString = System.IO.File.ReadAllText("faq.txt");
                //    Console.WriteLine(FileContentString);
                //    Console.WriteLine("\n");
                //}

            }
        }

        static void contact()
        {
            //Set variables
            bool looping = true; int question = 0;

            //While looping is true
            Console.Clear();
            while (looping)
            {
                //Intro text
                Console.WriteLine("Welcome to the Deltascope contact page! \n" +
                    "Deltascope is located in a modern building, located at the marina in the center of Rotterdam. \n" +
                    "VFrom a hospitable approach, it offers citizens, business and associations accommodation for many diverse activities. \n" +
                    "From parties and meetings to dancing, sports and making music.\n" +
                    "\n" +
                    "If you would like to contact us, you can do so by making a choice");

                //Show menu
                Console.WriteLine("\n[1]Phone number\n[2]E-mail\n[3]Location\n[4]Quit");

                //Ask for case input and quit when input is invalid
                try { question = int.Parse(Console.ReadLine()); } catch { }
                Console.WriteLine();
                Console.Clear();

                //Switch case
                switch (question)
                {
                    case 1:
                        Console.WriteLine("Welcome to our phone number dialog. Call our phone number and follow the menu as told!\n" +
                            "Mobile number: 06-12345678\n" +
                            "Cinema number: 010-234567\n" +
                            "\n" +
                            "If you would like to ask questions about partnership etc. call our business number" +
                            "Buisiness number: 010-123456");

                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 2:
                        Console.WriteLine("Welcome to our e-mail service. Send us an e-mail to one of the following e-mails depending on your question\n" +
                            "E-mail: deltascope@gmail.com" +
                            "Business E-mail: b.deltascope@gmail.com");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 3:
                        Console.WriteLine("If you would like to visit our headquarters you can by making an appointment and coming to the following adress\n" +
                            "Street address: Monopolystraat 124\n" +
                            "Postal code: 2777 ID\n" +
                            "City: Rotterdam\n" +
                            "Country: Netherlands\n" +
                            "Province: Zuid-Holland\n");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Going back"); looping = false; break;
                    default:
                        Console.WriteLine("Enter an existing value"); break;
                }
            }
        }


        public static void ShowRoom()
        {
            Console.Clear();
            Console.WriteLine("Wich room do you want to look at?");
            int i = 0;
            int inputRoom= 0;

            foreach (Room r in Program.rooms)
            {
                string  y = r.printInfo();
                Console.WriteLine("[" + i + "] Maasvlakte:"+ i +" "+  y + "\n");
                i++;
            }
            while (true)
            {
                try
                {
                    inputRoom = int.Parse(Console.ReadLine());
                    if (inputRoom <= (i-1) && inputRoom > -1)
                    {
                        break;
                    }
                    else {Console.WriteLine($"Please enter valid number only"); }
                }
                catch
                {
                    Console.WriteLine($"Please enter a number only");
                }
            }

            Program.rooms[inputRoom].printRoom();
        }
        public static void ExpectedMovies()
        {
            Console.Clear();
            Console.WriteLine($"See the expected movies for the coming X months.\n If you do not make a (valid) choice, the value will be 2 months.");
            
            int inputRoom;
            bool works = int.TryParse(StandardMessages.GetInputForParam("number for the amount of months."), out inputRoom);
            StandardMessages.PressAnyKey();
            if (works)
            {
                try
                {
                    Program.ShowComingSoon(inputRoom);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            else
            {
                Program.ShowComingSoon();
            }

        }
    }

    public static class StandardMessages
    {
        /// <summary>
        /// TryAgain displays a try again message 
        /// </summary>
        public static void TryAgain()
        {
            Console.WriteLine($"Please Try again.");
        }

        /// <summary>
        /// NoSearchResults displays that there were no results
        /// </summary>
        public static void NoSearchResults()
        {
            Console.WriteLine($"Sorry, no search results were found with this input.");
        }

        /// <summary>
        /// AreYouSure asks for yes or no and returns bool
        /// </summary>
        /// <returns></returns>
        public static bool AreYouSure()
        {
            Console.WriteLine("Are you sure?\nPlease enter yes or no.");
            string yesorno = Console.ReadLine();
            if (yesorno.ToLower().Equals("yes"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ResultsCount displays the amount of search results
        /// </summary>
        /// <param name="input"></param>
        public static void ResultsCount(int input)
        {
            if (input > 1)
            {
                Console.WriteLine($"There were {input} results.");
            }
            else
            {
                Console.WriteLine($"There was {input} result.");
            }
        }


        /// <summary>
        /// GetInputForParam displanys a "please enter a "{}"
        /// </summary>
        /// <param name="forParameter"></param>
        /// <returns></returns>
        public static string GetInputForParam(string forParameter)
        {
            Console.WriteLine($"Please enter a {forParameter}.");
            return Console.ReadLine();
        }

        /// <summary>
        /// WriteInputBelow prints a request of input
        /// </summary>
        public static void WriteInputBelow()
        {
            Console.WriteLine($"Please write your input below.\n \n");
        }
        /// <summary>
        /// EnterNumber prints a request of input of number
        /// </summary>
        public static void EnterNumber()
        {
            Console.WriteLine($"Please only enter a number!");
        }

        /// <summary>
        /// GivenOptions prints a request of input of given options
        /// </summary>
        public static void GivenOptions()
        {
            Console.WriteLine($"Only choose from the given options.");
        }

        /// <summary>
        /// FilePathError shows path error message and with input of filepath can be specified
        /// </summary>
        /// <param name="filePath"></param>
        public static void FilePathError(string filePath = "")
        {
            Console.WriteLine($"Oops! Something went wrong. {filePath} was not found.");
        }

        /// <summary>
        /// PressAnyKey prints to press any key
        /// </summary>
        public static void PressAnyKey()
        {
            Console.WriteLine($"Press any key to continue.");
        }

        /// <summary>
        /// PressKeyToContinue requires you to press any key and after that clears the screen.
        /// </summary>
        public static void PressKeyToContinue()
        {
            Console.Write($">");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
