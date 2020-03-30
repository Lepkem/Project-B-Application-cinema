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
            while(running){
                switch (caseSwitch)
                {   //functions
                    case 0:
                        //menu
                        caseSwitch = Menu(login);
                        break;

                    case 1:
                        //Login
                        if (!login){ login = Login(); }
                        else { Console.WriteLine("\n\n"); login = false; }
                        caseSwitch = 0;
                        break;

                    case 2:
                        //read rooms
                        readRooms();
                        //Test update room
                        //rooms[2].updateRoom(@".\rooms\room3.json");
                        //Test create room
                        //createRoom();
                        caseSwitch = 0;
                        break;

                    case 3:

                        break;
                    //Admin functions
                    case 10:
                       

                        break;

                    case 11:


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
                Console.Write("Enter a username: (admin) ");
                username = Console.ReadLine();
                if (username == "b") 
                {
                    Console.WriteLine("\n\n");
                    return false;
                }
                Console.Write("Enter a password: (admin) ");
                password = Console.ReadLine();

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
            Console.WriteLine("What action do you want to do?");
            if (!login) { Console.WriteLine("1:Login \n" + "2:read rooms \n" + "3:function 3 \n" + "4:function 4 \n"); }
            if (login) { Console.WriteLine("1:Logout \n" + "2:read rooms \n" + "3:function 3 \n" + "4:function 4 \n" + "10:function 1 admin \n" + "11:function 2 admin \n"); }
            while (true) 
            {
                string function = Console.ReadLine();
                bool isParsable = Int32.TryParse(function, out parsable);
                if (isParsable)
                {
                    if (!login) 
                    {
                        if(0 < parsable && parsable < 4) { return parsable; } //number equal to possible functions +1
                        else { Console.WriteLine("please select only action given");}
                    }
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