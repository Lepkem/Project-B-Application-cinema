using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Numerics;

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
                Console.Clear();
                Console.WriteLine("Replace the " + (i + 1) + " line? If yes give a new line with a length of "+ defaultLength + " characters.");
                Console.WriteLine("Chair type 0: Blocked chair (cant be purchased)");
                Console.WriteLine("Chair type 1: Cheap chair");
                Console.WriteLine("Chair type 2: Normal chair");
                Console.WriteLine("Chair type 3: Vip chair");
                string newLine = Console.ReadLine();
                while (true)
                {
                    if (newLine != "")
                    {
                        if (newLine.Length == defaultLength)
                        {
                            try
                            {
                                var errorhandling = BigInteger.Parse(newLine);
                                layoutArray[i] = newLine;
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Fill in integers only!");
                                newLine = Console.ReadLine();
                            }
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
            
            //alter the special object
            bool quit = false;
            int seats = 0;
            while (quit == false)
            {
                try
                {
                    Console.WriteLine("How many chairs should the room have?");
                    seats = int.Parse(Console.ReadLine());
                    quit = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Fill in integers only!");
                }
            }
            fullObject["chairs"] = seats;
            
            //alter the special object
            quit = false;
            int type = 0;
            while (quit == false)
            {
                try
                {
                    
                    Console.WriteLine("What type of room is it? \n1 = normal, 2 = 3D, 3 = IMAX");
                    type = int.Parse(Console.ReadLine());
                    if (type < 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Integer must be higher than 0 and lower than 4!");
                    } else if (type > 3){
                        Console.Clear();
                        Console.WriteLine("Integer must be higher than 0 and lower than 4!");
                    } else
                    {
                        quit = true;
                    }                   
                }
                catch
                {
                    Console.WriteLine("Fill in integers only!");
                    Console.Clear();
                }
            }
            fullObject["roomType"] = type;

            //turn the object into a string
            string updatedString = fullObject.ToString();
            //update the object
            Initialize(updatedString);
            //update the file
            File.WriteAllText(room, updatedString);
        }

        public void printRoom(bool vacancy)
        {
            //Print legend with colors and prices
            PrintLegend();

            WriteInColor(ConsoleColor.Cyan, "O  ");

            //Show screen
            string screen = "";
            for (int y = 0; y < layout.GetLength(1); y++)
            {
                screen += "+++";
            }
            Console.WriteLine(screen + "  (Screen)\n");

            //Show x coordinates
            string xcostring = "";
            for (int y = 1; y <= layout.GetLength(1); y++)
            {
                if (y < 10)
                {
                    xcostring = xcostring + y.ToString() + "  ";
                }
                if (y > 9)
                {
                    xcostring = xcostring + y.ToString() + " ";
                }
            }           
            Console.WriteLine(xcostring + "\n");

            for (int x = 0; x < layout.GetLength(0); x++)
            {
                string printString = "";
                for (int y = 0; y < layout.GetLength(1); y++)
                {
                    if (vacancy)
                    {
                        if (layout[x, y].vacant)
                            printString += "O  ";
                        else printString += "X  ";
                    }
                    else printString += layout[x,y].priceMod;
                }


                Console.WriteLine(printString + " " + (x+1));
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
            vacancyArray[cord_y - 1] = temp;

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

        /// <summary>
        /// PrintLegend is a helper function and prints the legend suited for the PrintRoom
        /// </summary>
        public void PrintLegend()
        {
            WriteInColor(ConsoleColor.Green, "Price cat \t$\n");
            WriteInColor(ConsoleColor.Cyan, "Price cat \t$$\n");
            WriteInColor(ConsoleColor.Yellow, "Price cat \t$$$\n");
            WriteInColor(ConsoleColor.Red, "unavailable seat\n\n");
        }


        private void WriteInColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.Write($"{text}");
            Console.ResetColor();
        }
    }
}
