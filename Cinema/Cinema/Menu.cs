using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    using System.Globalization;
    using System.Net;
    using System.Security.Cryptography;

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
                        Console.WriteLine("Main menu > Print Schedule\n");
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
                        printFAQ();
                        caseSwitch = 0;
                        break;

                    case 9:
                        //FAQ submit question
                        submitQuestion();
                        caseSwitch = 0;
                        break;

                    case 10:
                        //Contact
                        contact();
                        caseSwitch = 0;
                        break;

                    //Admin functions
                    case 11:
                        //Test update room
                        Console.WriteLine("Main menu > Edit room");
                        Console.WriteLine("Which room do you want to change?");
                        Program.rooms[2].updateRoom(string.Format(@".\rooms\room{0}.json", int.Parse(Console.ReadLine())));
                        caseSwitch = 0;
                        break;

                    case 12:
                        //Test create room
                        Console.WriteLine("Main menu > Edit room");
                        Program.createRoom();
                        caseSwitch = 0;
                        break;

                    case 13:
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

                    case 14:
                        //Add a scheduleElement
                        Program.createShedule();
                        caseSwitch = 0;
                        break;

                    case 15:
                        //Search Order by ID
                        IDBank.searchOrderByID();
                        caseSwitch = 0;
                        break;

                    case 16:
                        //Search Order by emailAddress
                        IDBank.SearchOrderByEmail();
                        caseSwitch = 0;
                        break;

                    case 17:
                        //Search Order by emailAddress
                        printSubmittedQuestions();
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

            string menu = "[1]Login \n[2]Print schedule\n[3]Search  \n[4]Show room \n[5]Order Tickets \n[6]Show coming movies \n[8]FAQ \n[9]Submit a question \n[10]Contact\n";

            //text being displayed in menu
            
            if (!login) { 
                
                Console.WriteLine("Main menu");
                Console.WriteLine(menu);
               
            }
            //text being displayed in menu Admin version


            Console.WriteLine("Main menu");
            if (login) { Console.WriteLine("[1]Logout \n[2]Print schedule\n[3]Search  \n[4]Show Room \n[5]Order Tickets \n[6]Show coming movies \n[8]FAQ \n[9]Print the submitted questions\n10]Contact \n[11]Edit room \n[12]Create room \n[13]Create movie \n[14]Add to schedule \n[15]Search order by ID \n[16]Search order by email\n[17]Print submitted questions"); }



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
                        if (0 < parsable && parsable < 11) { return parsable; } //number equal to possible functions +1
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
            Console.WriteLine("Main menu > Login");
            while (true)
            {
                string username, password = string.Empty;

                
                username = StandardMessages.GetInputForParam("username: (admin)");

                //Exit login screen
                if (username == "b")
                {
                    Console.WriteLine("\n\n");
                    return false;
                }

                //ask user input password
                password = StandardMessages.GetInputForParam("password: (admin)");
                

                //checks if user input correct.
                if (username == "admin" && password == "admin")
                {
                    Console.Clear();
                    Console.WriteLine("Welcome admin");
                    return true;
                }
                else { Console.WriteLine("Wrong input, please try again \n" + "if you want to return to the menu write b as username"); }

            }
        }
        
        /// <summary>
        /// prints the questions in the file faq.txt
        /// </summary>
        static void printFAQ() 
        {
            Console.WriteLine("Main menu > FAQ");
            string FileContentString = ""; 
            FileContentString = System.IO.File.ReadAllText("faq.txt"); 
            Console.Clear();
            Console.WriteLine($"{FileContentString}\n"); 
            StandardMessages.PressAnyKey();
            StandardMessages.PressKeyToContinue();
        }

        /// <summary>
        /// prints the questions in the file submittedQuestions.txt
        /// </summary>
        static void printSubmittedQuestions()
        {
            
            string FileContentString = System.IO.File.ReadAllText("submitQuestion.txt");
            Console.WriteLine($"{FileContentString}\n");
            StandardMessages.PressAnyKey();
            StandardMessages.PressKeyToContinue();
            Console.Clear();
        }

    

            
            //while (looping)
            //{
                

        /// <summary>
        /// lets a user add a question after solving a riddle
        /// </summary>
        static void submitQuestion()
        {
            if (riddle())
            {
                string[] question = new string[2] { StandardMessages.GetInputForParam("question of your choice"), "" };
                System.IO.File.AppendAllLines("submitQuestion.txt", question);
                StandardMessages.PressAnyKey();
                StandardMessages.PressKeyToContinue();
                Console.Clear();
            }


            else
            {
                StandardMessages.TryAgain();
            }
        }
        
        

        static bool riddle()
        {
            Console.WriteLine($"Prove that you are a human\n Don't use capitals");

            int RandomNum = RandomNumberGenerator.GetInt32(1, 11);
            switch (RandomNum)
            {
                case 1:
                    Console.WriteLine("How many holes does a normal t-shirt have?");
                    if (StandardMessages.GetInputForParam("number") == "4")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 2:
                    Console.WriteLine("Finish the saying: Curiosity killed the cat");
                    if (StandardMessages.GetInputForParam("proper ending") == "but satisfaction brought him back")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 3:
                    Console.WriteLine($"What animal smaller than a centimeter kills most humans a year?");
                    if (StandardMessages.GetInputForParam("animal name") == "mosquito")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 4:
                    Console.WriteLine($"Add the 5th flavour: sour, salty, bitter, umami and");
                    if (StandardMessages.GetInputForParam("flavour") == "sweet")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 5:
                    Console.WriteLine($"What animal smaller than a centimeter kills most humans a year?");
                    if (StandardMessages.GetInputForParam("animal name") == "mosquito")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 6:
                    Console.WriteLine($"What animal is the mascot of Australia?");
                    if (StandardMessages.GetInputForParam("animal name") == "kangaroo")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 7:
                    Console.WriteLine($"Who painted the night's watch?");
                    string ans = StandardMessages.GetInputForParam("painter's name");
                    if (ans == "Rembrandt" || ans == "rembrandt")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 8:
                    Console.WriteLine($"What is the name of the most famous sunken boat? \n Don't type the 'the'");
                    string answ = StandardMessages.GetInputForParam("boat's name");
                    if (answ == "Titanic" || answ == "titanic")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 9:
                    Console.WriteLine($"What superhero was bitten by a spider?");
                    string ansr = StandardMessages.GetInputForParam("hero name");
                    if (ansr == "Spiderman" || ansr == "spiderman")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 10:
                    Console.WriteLine($"Which cat loves lasagna?");
                    string anns = StandardMessages.GetInputForParam("cat name");
                    if (anns == "Garfield" || anns == "garfield")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                default:
                    Console.WriteLine($"What superhero was bitten by a spider?");
                    string ansrr = StandardMessages.GetInputForParam("hero name");
                    if (ansrr == "Spiderman" || ansrr == "spiderman")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                Console.WriteLine("Main menu > Contact");
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
                        Console.WriteLine("Main menu > Contact > Phone Number");
                        Console.WriteLine("Welcome to our phone number dialog. Call our phone number and follow the menu as told!\n" +
                            "Mobile number: 06-12345678\n" +
                            "Cinema number: 010-234567\n" +
                            "\n" +
                            "If you would like to ask questions about partnership etc. call our business number" +
                            "Buisiness number: 010-123456");

                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 2:
                        Console.WriteLine("Main menu > Contact > E-mail");
                        Console.WriteLine("Welcome to our e-mail service. Send us an e-mail to one of the following e-mails depending on your question\n" +
                            "E-mail: deltascope@gmail.com" +
                            "Business E-mail: b.deltascope@gmail.com");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 3:
                        Console.WriteLine("Main menu > Contact > Location");
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
            Console.WriteLine("Main menu > Show Room");
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
            Console.WriteLine("Main menu > Coming Movies");
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
