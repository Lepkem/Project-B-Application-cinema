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
            readRooms();
            //Test update room
            //rooms[2].updateRoom(@".\rooms\room3.json");
            //Test create room
            //createRoom();
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
    }
}