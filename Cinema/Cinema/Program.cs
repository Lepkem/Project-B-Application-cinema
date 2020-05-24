﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Cinema
{
    class Program
    {
        static public List<Room> rooms = new List<Room>();
        static public List<ScheduleElement> schedule = new List<ScheduleElement>();
        public static List<Films> myFilms = new List<Films>();

        static void Main(string[] args)
        {
            //console program
            readRooms();
            readMovies();
            schedule.Add(new ScheduleElement("12:00", myFilms[0], rooms[0], "20 april"));
            schedule.Add(new ScheduleElement("15:30", myFilms[1], rooms[2], "9 may"));
            schedule.Add(new ScheduleElement("18:00", myFilms[2], rooms[1], "30 february"));
            schedule.Add(new ScheduleElement("23:55", myFilms[4], rooms[2], "5 may"));

            Menu menu = new Menu();
            menu.switchCase();
        }

        public static void readMovies()
        {
            bool add = true;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            Movies movies = new Movies(Path.Combine(projectDirectory, @"movies/movie.json"));

            List<Movie> movieList = Movies.GetList();
            JArray movie = new JArray();

            foreach (Movie m in movieList)
            {
                add = true;
                foreach (Films f in myFilms)
                {
                    if (f.Name == m.name)
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    movie.Add(JObject.FromObject(m));
                    myFilms.Add(new Films { Name = m.name, Genre = m.genre, Runtime = m.runtime, Synopsis = m.synopsis, ReleaseDate = m.releaseDate, Age = m.age });
                }

            }
        }

        static void readRooms()
        {
            try
            {
                string[] files = Directory.GetFiles(@"./rooms", "*.json");
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
                Console.WriteLine("Chair type 0: Blocked chair (can not be purchased)");
                Console.WriteLine("Chair type 1: Cheap chair");
                Console.WriteLine("Chair type 2: Normal chair");
                Console.WriteLine("Chair type 3: Vip chair");
                Console.WriteLine("Type 'Exit' to exit creating a room.");
                roomRows[i] = "";
                try
                {
                    string newLine = Console.ReadLine();
                    if (newLine == "Exit")
                    {
                        Console.Clear();
                        Console.WriteLine("Exitted successfully.\n");
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
            Console.WriteLine($"What time will the movie start?\nFormat: HH:MM (13:50 for instance)" );
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
                Console.WriteLine(i + " " + x + "\n");
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
            Console.WriteLine("Time: " + time + "\nMovie: " + myFilms[inputFilm].Name);
            Console.WriteLine("\n\nWhat room do you want to assign? Select a number\n");
            int j = 0;
            foreach (Room r in rooms)
            {
                string y = r.printInfo();
                Console.WriteLine(j + " " + y + "\n");
                j++;
            }

            int inputRoom = 0;
            try { inputRoom = int.Parse(Console.ReadLine()); } catch { }
            Console.Clear();

            //input date
            Console.WriteLine("Time: " + time + "\nMovie: " + myFilms[inputFilm].Name + "\nRoom: " + inputRoom);
            Console.WriteLine("\n\nWhat date do you want to assign? Example: 1 march");
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
