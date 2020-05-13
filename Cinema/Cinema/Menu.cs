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
                        ShowRoom();
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
                        Console.WriteLine("That's not an option you knucklehead");
                        caseSwitch = 0;
                        break;
                }
            }
        }

        static int MenuHandler(Boolean login)
        {
            int parsable = 0;

            string menu = "[1]Login \n[2]Print schedule\n[3]Search  \n[4]Print Maasvlakte 1 \n[5]Order Tickets \n[8]FAQ \n[9]Contact\n";

            //text being displayed in menu
            Console.WriteLine("What action do you want to do?");

            if (!login) { Console.WriteLine(menu); }

            //text being displayed in menu Admin version
            if (login) { Console.WriteLine("[1]Logout \n[2]Print schedule\n[3]Search  \n[4]Print Maasvlakte 1 \n[5]Order Tickets \n[8]FAQ \n[9]Contact\n[10]Edit room \n[11]Create room \n[12]Create movie\n[13]Create movie Jitske\n[14]Add to schedule"); }
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

            Program.rooms[inputRoom].printRoom(true);
            Program.rooms[inputRoom].printRoom(false);
        }
    }
}

