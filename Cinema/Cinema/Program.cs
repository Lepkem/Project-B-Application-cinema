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
            //console program		AmountOfRows	0	uint

            //readRooms();

            string[] testarr = new string[] {    "123123123123123123",
                                                "011111222222111110",
                                                "011112222222211110",
                                                "011112222222211110",
                                                "011122222222221110",
                                                "011122223322221110",
                                                "111222233332222111",
                                                "111222333333222111",
                                                "112222333333222211",
                                                "112222333333222211",
                                                "112222333333222211",
                                                "011222233332222110",
                                                "011122223322221110",
                                                "011112222222211110",
                                                "001111222222111100",
                                                "001111222222111100",
                                                "001111111111111100",
                                                "000111111111111000",
                                                "000111111111111000" };
           
            RoomV2 room = new RoomV2();
            room.CreateFromLayout(testarr, "pathe");
            room.PrintAvailableSeats();
            Console.ReadLine();
            room.PrintRoom();

            //RoomParser.LoadRooms().First().SaveRoom("test1");

            Console.ReadLine();

            schedule.Add(new ScheduleElement("12:00", myFilms[0], rooms[0], "20 april"));
            schedule.Add(new ScheduleElement("15:30", myFilms[1], rooms[2], "9 may"));
            schedule.Add(new ScheduleElement("18:00", myFilms[2], rooms[1], "30 february"));
            schedule.Add(new ScheduleElement("23:55", myFilms[0], rooms[2], "5 may"));

            Menu menu = new Menu();
            menu.switchCase();
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

        public static void createRoom()
        {
            //Set row amount
            Console.WriteLine("How many rows does this room have?");
            int rows = 0;

            try
            {
                rows = int.Parse(Console.ReadLine());
            }
            catch
            {
                //display error to user
            }
            string[] roomRows = new string[rows];

            //Fill rows
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("Set row " + (i + 1) + ".");
                Console.WriteLine("Chair type 0: Blocked chair (cant be purchased)");
                Console.WriteLine("Chair type 1: Cheap chair");
                Console.WriteLine("Chair type 2: Normal chair");
                Console.WriteLine("Chair type 3: Vip chair");
                Console.WriteLine("Type Exit to exit creating a room.");
                roomRows[i] = "";
                try
                {
                    string newLine = Console.ReadLine();
                    if (newLine == "Exit")
                    {
                        Console.Clear();
                        Console.WriteLine("Exitted succesfully.\n");
                        return;
                    }
                    roomRows[i] = newLine;
                }
                catch
                {
                    Console.WriteLine($"Please input only a single number for the row");
                }
            }

            //Set chair amount
            Console.WriteLine("Set chair amount.");
            int chairAmount = 0;
            try
            {
                chairAmount = Convert.ToInt32((Console.ReadLine()));
            }
            catch
            {
                Console.WriteLine($"Please only enter a number.");
            }

            //Set room type
            Console.WriteLine("What type of room is it? \n1 = normal, 2 = 3D, 3 = IMAX");
            string roomType = "";
            try
            {
                roomType = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine($"Only choose between the given options please.");
            }

            //fill the vacancy
            string[] vacancyArr = new string[rows];
            for (int i = 0; i < rows; i++)
            {
                vacancyArr[i] = string.Concat(Enumerable.Repeat("0", roomRows[0].Length));
            }

            //Convert roomRows and chairAmount
            JObject output = new JObject();
            output["layout"] = JArray.FromObject(roomRows);
            output["chairs"] = chairAmount.ToString();
            output["roomType"] = roomType;
            output["vacancy"] = JArray.FromObject(vacancyArr);

            //Set new file name in x location
            string filePath = string.Format(@".\rooms\room{0}.json", rooms.Count + 1);

            //Create the new file
            File.WriteAllText(filePath, output.ToString());

            //Reads new file and makes it a room object
            rooms.Add(new Room(File.ReadAllText(filePath)));
        }

        public static void printSchedule()
        {
            foreach (ScheduleElement se in schedule)
            {
                se.printScheduleElement();
            }
            Console.WriteLine("\n");
        }

        public static void createShedule()
        {
            Console.Clear();
            //print current schedule
            Console.WriteLine("Current Schedule: ");
            printSchedule();

            //input time
            Console.WriteLine($"What time wil the movie start?\nFormat: dd-mm-yyyy" );
            string time = "";
            try
            {
                time = (Console.ReadLine());
            }
            catch
            {
                Console.WriteLine($"Please enter a date according to the format only");
            }
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

            int inputFilm = 0;
            try
            {
                inputFilm = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine($"Please enter a number only");
            }
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

            int inputRoom = 0;
            try{ inputRoom = int.Parse(Console.ReadLine());} catch { }
            Console.Clear();

            //input date
            Console.WriteLine("Time: " + time + "\nMovie: " + myFilms[inputFilm].Name + "\nRoom: " + inputRoom);
            Console.WriteLine("\n\nWhat date do you want assign? Example: 1 march");
            string inputDate = "";
            try
            {
                inputDate = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine($"Please enter a date");
            }


            schedule.Add(new ScheduleElement(time, myFilms[inputFilm], rooms[inputRoom], inputDate));
        }

        public List<ScheduleElement> schedules
        {
            get { return schedule; }
        }
    }
}
