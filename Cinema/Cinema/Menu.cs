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
                        else { Console.WriteLine(""); login = false; }
                        caseSwitch = 0;
                        break;

                    case 2:
                        Console.Clear();
                        //Print a Shedule
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Main menu > Print Schedule\n");
                        Console.ResetColor();
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

                    case 7:
                        //FAQ
                        printFAQ();
                        caseSwitch = 0;
                        break;

                    case 8:
                        //FAQ submit question
                        submitQuestion();
                        caseSwitch = 0;
                        break;

                    case 9:
                        //Contact
                        Console.Clear();
                        contact();
                        caseSwitch = 0;
                        break;

                    //Admin functions
                    case 11:
                        //Test update room
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Main menu > Edit room");
                        Console.ResetColor();
                        Console.WriteLine("Which room do you want to change?");
                        Program.rooms[2].updateRoom(string.Format(@".\rooms\room{0}.json", int.Parse(Console.ReadLine())));
                        caseSwitch = 0;
                        break;

                    case 12:
                        //Test create room
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Main menu > Edit room");
                        Console.ResetColor();
                        Program.createRoom();
                        caseSwitch = 0;
                        break;

                    case 13:
                        //Add movie Jitske
                        Console.Clear();
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
                        Console.Clear();
                        Program.createShedule();
                        caseSwitch = 0;
                        break;

                    case 15:
                        //Search Order by ID
                        Console.Clear();
                        IDBank.searchOrderByID();
                        caseSwitch = 0;
                        break;

                    case 16:
                        //Search Order by emailAddress
                        Console.Clear();
                        IDBank.SearchOrderByEmail();
                        caseSwitch = 0;
                        break;

                    case 17:
                        //Search Order by emailAddress
                        printSubmittedQuestions();
                        caseSwitch = 0;
                        break;
                    default:
                        StandardMessages.GivenOptions();
                        caseSwitch = 0;
                        break;
                }
            }
        }

        static int MenuHandler(Boolean login)
        {
            int parsable = 0;

            string menu = "[1]Login \n[2]Print schedule\n[3]Search  \n[4]Show room \n[5]Order Tickets \n[6]Show coming movies \n[7]FAQ \n[8]Submit a question \n[9]Contact\n";

            //text being displayed in menu

            if (!login) {

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Main menu");
                Console.ResetColor();

                Console.WriteLine(menu);
            }
            //text being displayed in menu Admin version

            if (login)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Main menu");
                Console.ResetColor();
                Console.WriteLine("[1]Logout \n[2]Print schedule\n[3]Search  \n[4]Show Room \n[5]Order Tickets \n[6]Show coming movies \n[7]FAQ \n[8]Submit a question\n[9]Contact \n[11]Edit room \n[12]Create room \n[13]Create movie \n[14]Add to schedule \n[15]Search order by ID \n[16]Search order by email\n[17]Print submitted questions");
            }

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Login");
            Console.ResetColor();
            while (true)
            {
                string username, password = string.Empty;


                username = StandardMessages.GetInputForParam("username: (admin)");

                //Exit login screen
                if (username == "b")
                {
                    Console.WriteLine("");
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > FAQ");
            Console.ResetColor();
            string FileContentString = "";
            FileContentString = System.IO.File.ReadAllText("faq.txt");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > FAQ");
            Console.ResetColor();
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






        /// <summary>
        /// lets a user add a question after solving a riddle
        /// </summary>
        static void submitQuestion()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Submit Question");
            Console.ResetColor();
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
                    break;
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main menu > Contact");
                Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Main menu > Contact > Phone number");
                        Console.ResetColor();
                        Console.WriteLine("Welcome to our phone number dialog. Call our phone number and follow the menu as told!\n" +
                            "Mobile number: 06-12345678\n" +
                            "Cinema number: 010-234567\n" +
                            "\n" +
                            "If you would like to ask questions about partnership etc. Call our business number:\n" +
                            "Buisiness number: 010-123456");

                        StandardMessages.PressAnyKey();
                        StandardMessages.PressKeyToContinue();
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Main menu > Contact > E-mail");
                        Console.ResetColor();
                        Console.WriteLine("Welcome to our e-mail service. Send us an e-mail to one of the following e-mail addresses depending on your question.\n\n" +
                            "E-mail: deltascope@gmail.com\n" +
                            "Business E-mail: b.deltascope@gmail.com");
                        StandardMessages.PressAnyKey();
                        StandardMessages.PressKeyToContinue();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Main menu > Contact > Location");
                        Console.ResetColor();
                        Console.WriteLine("If you would like to visit our headquarters you can make an appointment and visit the following adress\n\n" +
                            "Street address: Monopolystraat 124\n" +
                            "Postal code: 2777 ID\n" +
                            "City: Rotterdam\n" +
                            "Country: Netherlands\n" +
                            "Province: Zuid-Holland\n");
                        StandardMessages.PressAnyKey();
                        StandardMessages.PressKeyToContinue();
                        break;
                    case 4:
                        Console.Clear();
                        looping = false; break;
                    default:
                        Console.WriteLine("Enter an existing value"); break;
                }
            }
        }


        public static void ShowRoom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Show room");
            Console.ResetColor();
            Console.WriteLine("Wich room do you want to look at?");
            int i = 0;
            int inputRoom= 0;

            foreach (ScheduleElement r in Program.schedule)
            {
                string  y = r.printInfo();
                Console.WriteLine("[" + i + "] "+ y + " In maasvlakte: " +r.room.roomNumber +"\n");
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
                    else {StandardMessages.GivenOptions(); }
                }
                catch
                {
                    StandardMessages.EnterNumber();
                }
            }

            Program.schedule[inputRoom].room.printRoom();
            StandardMessages.PressAnyKey();
            StandardMessages.PressKeyToContinue();
        }
        public static void ExpectedMovies()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Coming movies");
            Console.ResetColor();
            Console.WriteLine($"See the expected movies for the coming X months.\n If you do not make a (valid) choice, the value will be 2 months.");

            int inputRoom;
            bool works = int.TryParse(StandardMessages.GetInputForParam("number for the amount of months."), out inputRoom);
            StandardMessages.PressAnyKey();
            if (works)
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Coming movies");
                    Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Are you sure?\nPlease enter yes or no.");
            Console.ResetColor();
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
            if (input > 1 || input == 0)
            {
                Console.WriteLine($"There were {input} results.");
            }
            else
            {
                Console.WriteLine($"There was {input} result.");
            }
        }

        /// <summary>
        /// Prints the message that something went wrong
        /// </summary>
        /// <returns></returns>
        public static void SomethingWentWrong()
        {
            Console.WriteLine($"Oops! Something went wrong.");
        }

        /// <summary>
        /// Prints the welcome message
        /// </summary>
        public static void WelcomeMessage()
        {
            //http://patorjk.com/software/taag/#p=display&f=ANSI%20Shadow&t=Welcome%20to%20the%20deltascope
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"         ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗    ████████╗ ██████╗");
            Console.WriteLine($"         ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝    ╚══██╔══╝██╔═══██╗");
            Console.WriteLine($"         ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗         ██║   ██║   ██║");
            Console.WriteLine($"         ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝         ██║   ██║   ██║");
            Console.WriteLine($"         ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗       ██║   ╚██████╔╝");
            Console.WriteLine($"          ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝       ╚═╝    ╚═════╝");
            Console.WriteLine($"████████╗██╗  ██╗███████╗    ██████╗ ███████╗██╗  ████████╗ █████╗ ███████╗ ██████╗ ██████╗ ██████╗ ███████╗");
            Console.WriteLine($"╚══██╔══╝██║  ██║██╔════╝    ██╔══██╗██╔════╝██║  ╚══██╔══╝██╔══██╗██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔════╝");
            Console.WriteLine($"   ██║   ███████║█████╗      ██║  ██║█████╗  ██║     ██║   ███████║███████╗██║     ██║   ██║██████╔╝█████╗  ");
            Console.WriteLine($"   ██║   ██╔══██║██╔══╝      ██║  ██║██╔══╝  ██║     ██║   ██╔══██║╚════██║██║     ██║   ██║██╔═══╝ ██╔══╝  ");
            Console.WriteLine($"   ██║   ██║  ██║███████╗    ██████╔╝███████╗███████╗██║   ██║  ██║███████║╚██████╗╚██████╔╝██║     ███████╗");
            Console.WriteLine($"   ╚═╝   ╚═╝  ╚═╝╚══════╝    ╚═════╝ ╚══════╝╚══════╝╚═╝   ╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝");
            Console.ResetColor();
            Console.WriteLine("                        WWWWWW:*@WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWW+......+@WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WW*............+#WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        =..................:#WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        *......................:#WWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        W+.........................-=WWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WW-.....*=:....................-=WWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WW@.....-WWWW#:....................-*@WWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWW=.....@WWWWWWW#+.....................*@WWWWWWWWWWW");
            Console.WriteLine("                        WWWW*....*WWWWWWWWWWW@+.....................+@WWWWWWW");
            Console.WriteLine("                        WWWWW:...:WWWWWWWWWWWWWWW@*.....................+#WWW");
            Console.WriteLine("                        WWWWWW-...@WWWWWWWWWWWWWWWWWW@*-....................:");
            Console.WriteLine("                        WWWWWW@...=WWWWWWWWWWWWWWWWWWWWWWW=-.................");
            Console.WriteLine("                        WWWWWWW=..+WWWWWWWWWWWWWWWWWWWWWWWWWWW=-.............");
            Console.WriteLine("                        WWWWWWWW+..WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW#:.........");
            Console.WriteLine("                        WWWWWWWWW:.#WWWWWWWWWWWWWWWWWWWWWWWWWWWWW=...........");
            Console.WriteLine("                        WWWWWWWWWW.+WWWWWWWWWWWWWWWWWWWWWWWWWW=-.............");
            Console.WriteLine("                        WWWWWWWWWW#-WWWWWWWWWWWWWWWWWWWWWWW#-..............+@");
            Console.WriteLine("                        WWWWWWWWWWW=#WWWWWWWWWWWWWWWWWWW@:.............*@WWWW");
            Console.WriteLine("                        WWWWWWWWWWWW@WWWWWWWWWWWWWWWW@+............*@WWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWWWWWWWWWWWWWWWWWWW*..........-*@WWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWWWWWWWWWWWWWWWW=.........-*WWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWWWWWWWWWWWWW#-.......-*WWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWWWWWWWWWW@-......-=WWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWWWWWWW@:.....-=WWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWWWWW+....-=WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWWWWW*...-=WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWWWWW=-.:=WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        WWWW#-:#WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("                        W@*#WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Press enter to continue");
            Console.ResetColor();
            StandardMessages.PressKeyToContinue();
            Console.Clear();
        }

       


        /// <summary>
        /// GetInputForParam displanys a "please enter a "{}"
        /// </summary>
        /// <param name="forParameter"></param>
        /// <returns></returns>
        public static string GetInputForParam(string forParameter)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Please enter a {forParameter}.");
            Console.ResetColor();
            return Console.ReadLine();
        }

        /// <summary>
        /// WriteInputBelow prints a request of input
        /// </summary>
        public static void WriteInputBelow()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Please write your input below.\n \n");
            Console.ResetColor();
        }
        /// <summary>
        /// EnterNumber prints a request of input of number
        /// </summary>
        public static void EnterNumber()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Please only enter a number!");
            Console.ResetColor();
        }

        /// <summary>
        /// GivenOptions prints a request of input of given options
        /// </summary>
        public static void GivenOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Only choose from the given options.");
            Console.ResetColor();
        }

        /// <summary>
        /// FilePathError shows path error message and with input of filepath can be specified
        /// </summary>
        /// <param name="filePath"></param>
        public static void FilePathError(string filePath = "")
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Oops! Something went wrong. {filePath} was not found.");
            Console.ResetColor();
        }

        /// <summary>
        /// PressAnyKey prints to press any key
        /// </summary>
        public static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Press any key to continue.");
            Console.ResetColor();
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
