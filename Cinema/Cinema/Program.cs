using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cinema
{
    class Program
    {
        static public List<Room> rooms = new List<Room>();
        static public List<ScheduleElement> schedule = new List<ScheduleElement>();
        
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
                        printSchedule();
                        caseSwitch = 0;
                        break;

                    case 3:
                        caseSwitch = 0;
                        break;

                    //Admin functions
                    case 10:
                        //Test update room
                        Console.WriteLine("Which room do you want to change?");
                        rooms[2].updateRoom(string.Format(@".\rooms\room{0}.json", int.Parse(Console.ReadLine())));
                        //Test create room
                        createRoom();
                        caseSwitch = 0;
                        break;

                    case 11:
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

            //Convert roomRows and chairAmount
            JObject output = new JObject();
            output["layout"] = JArray.FromObject(roomRows);
            output["chairs"] = chairAmount;

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
            if (!login) { Console.WriteLine("1:Login \n" + "2:print schedule\n" + "3:function 3 \n" + "4:function 4 \n"); }

            //text being displayed in menu Admin version
            if (login) { Console.WriteLine("1:Logout \n" + "2:print schedule\n" + "3:function 3 \n" + "4:function 4 \n" + "10:edit and create rooms \n" + "11:function 2 admin \n"); }
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
                        if (0 < parsable && parsable < 12) { return parsable; } //number equal to possible functions +1
                        else { Console.WriteLine("please select only action given"); }
                    }
                    
                }
                else { Console.WriteLine("please select only action given"); }
            }
        }


    }
}