using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cinema
{
    class Program
    {
        static public List<Room> rooms = new List<Room>();
        static public List<ScheduleElement> schedule = new List<ScheduleElement>();
        public static List<Films> myFilms = new List<Films>

            {
                new Films{ Name = "Sonic", Genre = "Comedy", Runtime = "120 min", Synopsis = "Blue hedgehog collects rings.", ReleaseDate = "12-02-2020" },
                new Films{ Name = "Birds", Genre = "Comedy", Runtime = "100 min", Synopsis = "Clown girl does funny stuff.", ReleaseDate = "18-02-2020" },
                new Films{ Name = "Bloodshot", Genre = "Action", Runtime = "110 min", Synopsis = "Vin Diesel shoots enemies.", ReleaseDate = "21-02-2020" },
            };

        static void Main(string[] args)
        {
            //console program
            Boolean running = true;
            int caseSwitch = 0;
            Boolean login = false;
            readRooms();

            schedule.Add(new ScheduleElement("12:00", myFilms[0], rooms[0], "20 april"));
            schedule.Add(new ScheduleElement("15:30", myFilms[1], rooms[2], "9 may"));
            schedule.Add(new ScheduleElement("18:00", myFilms[2], rooms[1], "30 february"));
            schedule.Add(new ScheduleElement("23:55", myFilms[0], rooms[2], "5 may"));

            while (running)
            {
                switch (caseSwitch)
                {   //functions
                    case 0:
                        //menu
                        caseSwitch = Menu(login);
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
                        printSchedule();
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
                        rooms[0].printRoom(true);
                        rooms[0].printRoom(false);
                        caseSwitch = 0;
                        break;

                    case 5:
                        Console.Clear();
                        orderMenu();

                        
                        //IDBank orders = new IDBank(@"./orders/orders.json");



                        /*
                        IDBank bank2 = new IDBank();
                        bank2.generateUniqueNumber();
                        
                        */
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
                        rooms[2].updateRoom(string.Format(@".\rooms\room{0}.json", int.Parse(Console.ReadLine())));

                        caseSwitch = 0;
                        break;

                    case 11:
                        //Test create room
                        createRoom();
                        caseSwitch = 0;
                        break;

                    case 12:
                        //Add movie
                        myFilms.Add(new Films { Name = "Invisible Man", Genre = "Horror", Runtime = "130 min", Synopsis = "Invisible Man stalks his ex.", ReleaseDate = "24-02-2020" });
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
                        createShedule();
                        caseSwitch = 0;
                        break;


                    default:
                        Console.WriteLine("That's not an option you knucklehead");
                        caseSwitch = 0;
                        break;
                }
            }
        }

        static void readRooms()
        {
            try
            {
                string[] files = Directory.GetFiles(@".\rooms", "*.json");
                for (int i = 0; i < files.Length; i++)
                    //this line takes the file location for the JSON files, reads the entire file, and passes it to the initializer
                    rooms.Add(new Room(File.ReadAllText(files[i])));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        static void createRoom()
        {
            //Set row amount
            Console.WriteLine("How many rows does this room have?");
            int rows = int.Parse(Console.ReadLine());
            string[] roomRows = new string[rows];

            //Fill rows
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("Set row " + (i + 1) + ".");
                roomRows[i] = Console.ReadLine();
            }
            
            //Set chair amount
            Console.WriteLine("Set chair amount.");
            string chairAmount = Console.ReadLine();

            //Set room type
            Console.WriteLine("What type of room is it? \n1 = normal, 2 = 3D, 3 = IMAX");
            String roomType = Console.ReadLine();

            //Convert roomRows and chairAmount
            JObject output = new JObject();
            output["layout"] = JArray.FromObject(roomRows);
            output["chairs"] = chairAmount;
            output["roomType"] = roomType;

            //Set new file name in x location
            string filePath = string.Format(@".\rooms\room{0}.json", rooms.Count + 1);

            //Create the new file
            File.WriteAllText(filePath, output.ToString());

            //Reads new file and makes it a room object
            rooms.Add(new Room(File.ReadAllText(filePath)));
        }

        static void printSchedule()
        {
            foreach (ScheduleElement se in schedule)
            {
                se.printScheduleElement();
            }
            Console.WriteLine("\n");
        }

        //static void printFAQ()
        //{
        //    string FileContentString = "";
        //   FileContentString = System.IO.File.ReadAllText("faq.txt");
        //    Console.WriteLine(FileContentString);
        //    Console.WriteLine("\n");
        //}

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

        static int Menu(Boolean login)
        {
            int parsable = 0;
            string menu = "1:Login \n2:Print schedule\n3:Search  \n4:Print Maasvlakte 1 \n5:Order Tickets";
            //text being displayed in menu
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

        static void orderMenu()
        {
            Console.Clear();
            Console.WriteLine("Which movie do you want to watch? enter number\n\n");
            Console.WriteLine($"Choose a number between 0 and {(myFilms.Count)-1}"); //als we de opties laten beginnen vanaf 1 moet dit ff veranderd worden
            int i = 0;
            foreach (Films f in myFilms)
            {
                string x = f.printFilms();
                Console.WriteLine(i + " " + x + "\n");
                
                i++;
            }

            try
            {
                int inputFilm = int.Parse(Console.ReadLine());
                searchMovie(inputFilm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                throw;
            }
            
        }

        static void createShedule()
        {
            Console.Clear();
            //print current schedule
            Console.WriteLine("Current Schedule: ");
            printSchedule();

            //input time
            Console.WriteLine("What time wil the movie start?");
            string time = (Console.ReadLine());
            Console.Clear();

            //input movie
            Console.WriteLine("Time: " + time);
            Console.WriteLine("\n\nWhat movie do you want to add? select a number\n");
            int i = 0;
            foreach (Films f in myFilms)
            {
               string x = f.printFilms();
                Console.WriteLine(i + " " + x +"\n");
                i++;
            }
            int inputFilm = int.Parse(Console.ReadLine());
            Console.Clear();

            //input room
            Console.WriteLine("Time: " + time +"\nMovie: " + myFilms[inputFilm].Name);
            Console.WriteLine("\n\nWhat room do you want assign? Select a number\n");
            int j = 0;
            foreach (Room r in rooms)
            {
                string y = r.printInfo();
                Console.WriteLine(j + " " + y +"\n");
                j++;
            }
            int inputRoom = int.Parse(Console.ReadLine());
            Console.Clear();

            //input date
            Console.WriteLine("Time: " + time + "\nMovie: " + myFilms[inputFilm].Name + "\nRoom: " + inputRoom);
            Console.WriteLine("\n\nWhat date do you want assign? Example: 1 march");
            string inputDate = Console.ReadLine();


            schedule.Add(new ScheduleElement(time, myFilms[inputFilm], rooms[inputRoom], inputDate)); 
        }

        static void searchMovie(int input)
        {
            Console.Clear();
            List<ScheduleElement> possibleMovies = new List<ScheduleElement>();
            Console.WriteLine("Choose your preference: select number \n\n");
            IEnumerable<ScheduleElement> query = schedule.Where(schedule => schedule.movie == myFilms[input]);
            int i = 0;
            foreach (ScheduleElement schedule in query)
            {
                possibleMovies.Add(schedule);
                Console.WriteLine(i);
                schedule.printScheduleElement();
                i++;
            }
            int x = int.Parse(Console.ReadLine());

            Console.WriteLine("How many tickets do you want? Enter number");
            int seats = int.Parse(Console.ReadLine());


            int cord_x = 0;
            int cord_y = 0;
            bool seatLoop = true;
            string file = string.Format(@".\rooms\room{0}.json", (x + 1));

            for (int s = seats; s >= 1; seats--)
            {
                seatLoop = true;
                while (seatLoop)
                {
                    Console.Clear();
                    Console.WriteLine("Please pick a seat. You can select " + s + " more seats\n the upper left corner is 0,0");
                    possibleMovies[x].room.printRoom(true);

                    Console.WriteLine("select the X coordinate: ");
                    cord_x = int.Parse(Console.ReadLine());
                    Console.WriteLine("select the Y coordinate: ");
                    cord_y = int.Parse(Console.ReadLine());

                    if(possibleMovies[x].room.layout[cord_x, cord_y].vacant == true)//if spot is open
                    {
                        Console.WriteLine("Your in the if");
                        //possibleMovies[x].room.layout[cord_x, cord_y].vacant. == "1";
                        possibleMovies[x].room.updateVacancy(cord_x, cord_y,file);
                        seatLoop = false;
                    }
                    else { Console.WriteLine("Seats are already taken."); }
                    
                }

            }
    
        }


        public List<ScheduleElement> schedules
        {
            get { return schedule; }
        }
    }
}