using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Cinema
{
    class Room
    {
        char[,] layout;
        int chairs;
        public Room(string l)
        {
            //the actual initialization function is its own method so that it can be called manually
            Initialize(l);
        }
        public void deleteRoom(string room)
        {
            File.Delete(room);
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

        public void updateRoom(string room)
        {
            //read the file as one big string and turn it into a special object
            JObject fullObject = JObject.Parse(File.ReadAllText(room));
            //read the array in the object and store it
            JArray layoutArray = (JArray)fullObject["layout"];
            //go through each row in the layout
            int defaultLength = layoutArray.First.ToString().Length;
            for (int i = 0; i < layoutArray.Count; i++)
            {
                Console.WriteLine("Replace the " + (i + 1) + " line? If yes give new line.");
                string newLine = Console.ReadLine();
                while (true)
                {
                    if (newLine != "")
                    {
                        if (newLine.Length == defaultLength)
                        {
                            layoutArray[i] = newLine;
                            break;
                        }
                        else
                        {
                            Console.WriteLine(string.Format("The length of each row must be exactly {0}. Try inputting a line of the proper length.", defaultLength));
                            newLine = Console.ReadLine();
                        }
                    }
                    else break;
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
