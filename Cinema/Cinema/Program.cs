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

            schedule.Add(new ScheduleElement("12:00", "sonic 2 electric boogaloo", rooms[0], "20 april"));
            schedule.Add(new ScheduleElement("15:30", "preys of bird", rooms[2], "9 may"));
            schedule.Add(new ScheduleElement("18:00", "test film", rooms[1], "30 february"));

            while (running){
                switch (caseSwitch)
                {   //functions
                    case 0:
                        //menu
                        caseSwitch = Menu(login);
                        break;

                    case 1:
                        //Login OR logout
                        if (!login){ login = Login(); }
                        else { Console.WriteLine("\n\n"); login = false; }
                        caseSwitch = 0;
                        break;

                    case 2:
                        //Print a Shedule
                        printSchedule();
                        caseSwitch = 0;
                        break;

                    case 3:
                        //Search
                        Search search = new Search();
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

                    default:
                        Console.WriteLine("That's not an option you knucklehead");
                        caseSwitch = 0;
                        break;
                }
            }
    }

    static void readRooms()
        {
            string[] files = Directory.GetFiles(@".\rooms", "*.json");
            for (int i = 0; i < files.Length; i++)
                //this line takes the file location for the JSON files, reads the entire file, and passes it to the initializer
                rooms.Add(new Room(File.ReadAllText(files[i])));
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

        static Boolean Login()
        {
            while (true) 
            {
                Boolean login = false;
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
                else { Console.WriteLine("Wrong input, please try again \n" + "if you want to return to the menu write b as username");}

            }
        }

        static int Menu(Boolean login) 
        {
            int parsable = 0;

            //text being displayed in menu
            Console.WriteLine("What action do you want to do?");
            if (!login) { Console.WriteLine("1:Login \n" + "2:print schedule\n" + "3:Search  \n" + "4:function 4 \n"); }

            //text being displayed in menu Admin version
            if (login) { Console.WriteLine("1:Logout \n" + "2:print schedule\n" + "3:Search \n" + "4:function 4 \n" + "10:edit room \n" + "11:create room \n" + "12:create movie"); }
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
                        if(0 < parsable && parsable < 5) { return parsable; } //number equal to possible functions +1
                        else { Console.WriteLine("please select only action given");}
                    }
                    //checks if number is the same as a user OR admin fucntion
                    if (login)
                    {
                        if (0 < parsable && parsable < 13) { return parsable; } //number equal to possible functions +1
                        else { Console.WriteLine("please select only action given"); }
                    }
                    
                }
                else { Console.WriteLine("please select only action given"); }
            }
        }


    }
}