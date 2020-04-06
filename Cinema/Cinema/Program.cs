using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Cinema
{
    class Program
    {
        //public static Tuple<string, string, string> movies = new Tuple<string, string, string>("Sonic The Hedgehog", "Comedy", "12-02-2020");
        public static List<Films> myFilms = new List<Films>
            {
                new Films{ Name = "Sonic", Genre = "Comedy", Runtime = "120 min", Synopsis = "Blue hedgehog collects rings.", ReleaseDate = "12-02-2020" },
                new Films{ Name = "Birds", Genre = "Comedy", Runtime = "100 min", Synopsis = "Clown girl does funny stuff.", ReleaseDate = "18-02-2020" },
                new Films{ Name = "Bloodshot", Genre = "Action", Runtime = "110 min", Synopsis = "Vin Diesel shoots enemies.", ReleaseDate = "21-02-2020" },
            };

        

        static void Main(string[] args)
        {
            //this line takes the file location for the JSON files, reads the entire file, and passes it to the initializer
            //Room roomone = new Room(File.ReadAllText(@"./rooms/room1.json"));
            //Room roomtwo = new Room(File.ReadAllText(@"./rooms/room2.json"));
            //Room roomthree = new Room(File.ReadAllText(@"./rooms/room3.json"));

            //roomthree.updateCreateRoom(@"./rooms/room3.json")

            myFilms.Add(new Films { Name = "Invisible Man", Genre = "Horror", Runtime = "130 min", Synopsis = "Invisible Man stalks his ex.", ReleaseDate = "24-02-2020" });

            Search search = new Search();
        }
    }

    class Room
    {
        char[,] layout;
        int chairs;
        public Room(string l)
        {
            //the actual initialization function is its own method so that it can be called manually
            Initialize(l);
        }
        public void deleteRoom()
        {
            //TODO: D31373 the room
        }
        public void Initialize(string l)
        {
            //this line takes the string and turns it into a special object that contains the attributes of the JSON
            JObject input = JObject.Parse(l);
            //this line assigns the chairs value stored in the file in the object's chair variable 
            chairs = (int)input["chairs"];
            //there's also an array of strings in the file: this file takes it and turns it from a massive string to a special array
            JArray inputJArray = JArray.Parse(input["layout"].ToString());
            //create an empty 2D character array the same size as the string array
            char[,] inputMatrix = new char[inputJArray.First.ToString().Length, inputJArray.Count];
            // go through each position in the character array and fill it with the proper character
            for (int i = 0; i < inputMatrix.GetLength(0); i++)
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    inputMatrix[i, j] = inputJArray[j].ToString()[i];
                }
            //store the array in the object
            layout = inputMatrix;
        }

        public void updateCreateRoom(string room)
        {
            //read the file as one big string and turn it into a special object
            JObject fullObject = JObject.Parse(File.ReadAllText(room));
            //read the array in the object and store it
            JArray layoutArray = (JArray)fullObject["layout"];
            //go through each row in the layout
            for (int i = 0; i < layoutArray.Count; i++)
            {
                Console.WriteLine("Replace the " + (i + 1) + " line? If yes give new line.");
                string newLine = Console.ReadLine();
                if (newLine != "")
                {
                    //TODO: does making changes to layoutarray change the array inside fullObject?
                    layoutArray[i] = newLine;
                }
            }

            Console.WriteLine("How many chairs should the room have?");
            //alter the special object
            fullObject["chairs"] = Int32.Parse(Console.ReadLine());
            //turn the object into a string
            string updatedString = fullObject.ToString();
            //update the object
            Initialize(updatedString);
            //update the file
            File.WriteAllText(room, updatedString);
        }
    }
}