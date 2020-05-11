using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Cinema
{
    class Room
    {
        public Seat[,] layout;
        int chairs;
        int roomType;
        string type;
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
            //this line assigns the roomType value stored in the file in the object's roomType variable
            roomType = (int)input["roomType"];
            //there's also an array of strings in the file: this file takes it and turns it from a massive string to a special array
            JArray inputJArray = JArray.Parse(input["layout"].ToString());
            //create an empty 2D character array the same size as the string array
            Seat[,] inputMatrix = new Seat[inputJArray.Count, inputJArray.First.ToString().Length];
            //this array goes over which seat is already taken
            JArray vacancyJArray = JArray.Parse(input["vacancy"].ToString());
            // go through each position in the character array and fill it with the proper seat object
            for (int rows = 0; rows < inputMatrix.GetLength(0); rows++)
            {
                string inputLine = inputJArray[rows].ToString();
                string vacancyLine = vacancyJArray[rows].ToString();
                for (int columns = 0; columns < inputMatrix.GetLength(1); columns++)
                {
                    if (inputLine[columns] == '0')
                        inputMatrix[rows, columns] = new Seat(false, 0.0f);
                    else
                        inputMatrix[rows, columns] = new Seat(!Convert.ToBoolean(Char.GetNumericValue(vacancyLine[columns])), (float)Char.GetNumericValue(inputLine[columns]));
                }
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
            Console.WriteLine("What type of room is it? \n1 = normal, 2 = 3D, 3 = IMAX");
            //alter the special object
            fullObject["roomType"] = Int32.Parse(Console.ReadLine());
            //turn the object into a string
            string updatedString = fullObject.ToString();
            //update the object
            Initialize(updatedString);
            //update the file
            File.WriteAllText(room, updatedString);
        }

        public void printRoom(bool vacancy)
        {
            for (int x = 0; x < layout.GetLength(0); x++)
            {
                string printString = "";
                for (int y = 0; y < layout.GetLength(1); y++)
                {
                    if (vacancy)
                    {
                        if (layout[x, y].vacant)
                            printString += "_";
                        else printString += "X";
                    }
                    else printString += layout[x,y].priceMod;
                }
                Console.WriteLine(printString);
            }
            Console.WriteLine("\n");
        }

        public void updateVacancy(int cord_x, int cord_y, string room)
        {
            JObject fullObject = JObject.Parse(File.ReadAllText(room));
            JArray vacancyArray = (JArray)fullObject["vacancy"];

            int defaultLength = vacancyArray.First.ToString().Length;


            string temp = vacancyArray[cord_y - 1].ToString();
            char[] temp2 = temp.ToCharArray();
            temp2[cord_x - 1]='1';
            temp = new string(temp2);

            vacancyArray[cord_y] = temp;

            string updatedString = fullObject.ToString();
            File.WriteAllText(room, updatedString);
        }
        public string printInfo() 
        {
            
            if (roomType == 1){ type = "normal";}
            if (roomType == 2) { type = "3D"; }
            if (roomType == 3) { type = "IMAX"; }
            return string.Format("Type:{0}  Chairs:{1} ", type, chairs);
        }
    }
}
