using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{

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
                       
                        Program.rooms[0].printRoom(true);
                        Program.rooms[0].printRoom(false);
                        caseSwitch = 0;
                        break;

                    case 5:
                        Console.Clear();
                        Order.orderMenu();
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
                        //Add movie
                        Program.myFilms.Add(new Films { Name = "Invisible Man", Genre = "Horror", Runtime = "130 min", Synopsis = "Invisible Man stalks his ex.", ReleaseDate = "24-02-2020" });
                        caseSwitch = 0;
                        break;

                    case 13:
                        //Add movie Jitske
                        Movies movies = new Movies(@"./movies/movie.json");
                        movies.updateCreateMovie();
                        caseSwitch = 0;
                        break;
                    case 14:
                        //Add a scheduleElement
                        Program.createShedule();
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
            string menu = "1:Login \n2:Print schedule\n3:Search  \n4:Print Maasvlakte 1 \n5:Order Tickets \n8:FAQ \n9:Contact\n";
            //text being displayed in menu
            Console.WriteLine("Welcome to the Deltascope!\n");
            Console.WriteLine("What action do you want to do?");

            if (!login) { Console.WriteLine(menu); }

            //text being displayed in menu Admin version
            if (login) { Console.WriteLine(menu + "10:Edit room \n11:Create room \n12:Create movie\n13:Create movie Jitske\n14:Add to schedule"); }
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
                Console.WriteLine("\n1: Is de bioscoop geschikt voor mensen in een rolstoel? \n2: Heeft de bioscoop zoete popcorn?\n3: Wat zijn de openingstijden van de bioscoop?\n4: Quit");

                //Ask for case input and quit when input is invalid
                try { question = int.Parse(Console.ReadLine()); } catch { }
                Console.WriteLine();
                Console.Clear();

                //Switch case
                switch (question)
                {
                    case 1:
                        Console.WriteLine("De bioscoop is zeker geschikt voor mensen in een rolstoel.\n" +
                            "Er is beschikking tot een lift voor de 2de verdieping en alle\n" +
                            "gangpaden zijn breed genoeg voor rolstoelen.\n");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 2:
                        Console.WriteLine("De bioscoop heeft 3 soorten popcorn.\n" +
                            "Zowel zoet, zout als karamel.\n");
                        Console.WriteLine("Press enter to continue"); Console.ReadLine(); break;
                    case 3:
                        Console.WriteLine("Wegens het corona virus zijn wij momenteel gesloten!\n" +
                            "wel hebben wij een thuisbioscoop klaarstaan! zie hiervoor de website\n");
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
                Console.WriteLine("Welkom bij de Deltascope contactpagina!\n" +
                    "Deltascope is gevestigd in een modern gebouw, gelegen aan de jachthaven in het centrum van Rotterdam.\n" +
                    "Vanuit een gastvrije insteek biedt het burgers, bedrijfsleven en verenigingen een accommodatie voor vele uiteenlopende activiteiten.\n" +
                    "Van feesten en vergaderen tot dansen, sporten en musiceren.\n" +
                    "\n" +
                    "If you would like to contact us, you can do so by making a choice");

                //Show menu
                Console.WriteLine("\n1: Phone number\n2: E-mail\n3: Location\n4: Quit");

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
    }
}

