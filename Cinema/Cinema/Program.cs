using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

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
            StandardMessages.WelcomeMessage();
            readRooms();
            readMovies();
            schedule.Add(new ScheduleElement("12-00", myFilms[0], createScheduleRoom(0, "12-00", "20-04"), "20-04"));
            schedule.Add(new ScheduleElement("15-30", myFilms[1], createScheduleRoom(1, "15-30", "09-05"), "09-05"));
            schedule.Add(new ScheduleElement("18-00", myFilms[2], createScheduleRoom(2, "18-00", "30-02"), "30-02"));
            schedule.Add(new ScheduleElement("23-55", myFilms[4], createScheduleRoom(1, "23-55", "05-05"), "05-05"));
            schedule.Add(new ScheduleElement("12-00", myFilms[8], createScheduleRoom(0, "12-00", "30-05"), "30-05"));
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

        /// <summary>
        /// Creates a base/template room
        /// </summary>
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
                        Console.WriteLine("Exited successfully.\n");
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
                StandardMessages.EnterNumber();
            }

            //Set room type
            Console.WriteLine("What type of room is it? \n[1] = normal, [2] = 3D, [3] = IMAX");
            string roomType = "";
            try
            {
                roomType = Console.ReadLine();
            }
            catch
            {
                StandardMessages.GivenOptions();
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

        public static Room createScheduleRoom(int roomNumber, string time, string inputdate)
        {
            Room template = rooms[roomNumber];
            string[] vacancyArr = new string[template.layout.GetLength(0)];
            for (int i = 0; i < vacancyArr.Length; i++)
            {
                vacancyArr[i] = string.Concat(Enumerable.Repeat("0", template.layout.GetLength(1)));
            }

            string[] priceArr = new string[template.layout.GetLength(0)];
            for (int i = 0; i < priceArr.Length; i++)
            {
                string currentRow = "";
                for (int j = 0; j < template.layout.GetLength(1); j++)
                {

                    currentRow += (int)template.layout[i, j].priceMod;
                }
                priceArr[i] = currentRow;
            }


            JObject output = new JObject();
            output["layout"] = JArray.FromObject(priceArr);
            output["chairs"] = template.chairs.ToString();
            output["roomType"] = template.roomType;
            output["roomNumber"] = template.roomNumber;
            output["vacancy"] = JArray.FromObject(vacancyArr);


            //Set new file name in x location
            string fileName = $"{inputdate}--{time}-{rooms.IndexOf(template)}";
            string filePath = @$".\rooms\ScheduledRooms\{fileName}.json";

            //Create the new file
            File.WriteAllText(filePath, output.ToString());

            //Reads new file and makes it a room object
            return new Room(File.ReadAllText(filePath));

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Select Movie > Add To Schedule");
            Console.ResetColor();
            Console.WriteLine("Current Schedule: ");
            printSchedule();

            //input time
            Console.WriteLine($"What time will the movie start?\nFormat: HH-MM (13-50 for instance)" );
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Select Movie > Add To Schedule > Add Movie");
            Console.ResetColor();
            Console.WriteLine("Time: " + time);
            Console.WriteLine("\n\nWhat movie do you want to add? select a number\n");
            int i = 0;
            foreach (Films f in myFilms)
            {
                string x = f.printFilms();
                Console.WriteLine("["+i+"]" + " " + x + "\n");
                i++;
            }

            int inputFilm = 0;
            try
            {
                inputFilm = int.Parse(Console.ReadLine());
            }
            catch
            {
                StandardMessages.EnterNumber();
            }
            Console.Clear();

            //input room
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Select Movie > Add To Schedule > Add Movie > Assign Room");
            Console.ResetColor();
            Console.WriteLine("Time: " + time + "\nMovie: " + myFilms[inputFilm].Name);
            Console.WriteLine("\n\nWhat room do you want to assign? Select a number\n");
            int j = 0;
            foreach (Room r in rooms)
            {
                string y = r.printInfo();
                Console.WriteLine("["+j+"]" + " " + y + "\n");
                j++;
            }

            int inputRoom = 0;
            try { inputRoom = int.Parse(Console.ReadLine()); } catch { }
            Console.Clear();

            //input date
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Select Movie > Add To Schedule > Add Movie > Assign Room > Assign Date");
            Console.ResetColor();
            Console.WriteLine("Time: " + time + "\nMovie: " + myFilms[inputFilm].Name + "\nRoom: " + inputRoom);
            Console.WriteLine("\n\nWhat date do you want to assign? Example: dd-mm");
            string inputDate = "";
            try
            {
                inputDate = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine($"Please enter a date");
            }


            schedule.Add(new ScheduleElement(time, myFilms[inputFilm], createScheduleRoom(inputRoom,time,inputDate), inputDate));
        }

        public List<ScheduleElement> schedules
        {
            get { return schedule; }
        }

        public static void ShowComingSoon( int monthsUntilRelease = 2)
        {
            List<Films> filmslist = new List<Films>();
            DateTime releaseDate = DateTime.MaxValue;
            //V1
            //var x = myFilms.Where(film =>  Convert.ToDateTime(film.ReleaseDate) <= DateTime.Now.AddMonths(monthsUntilRelease)).ToList();
            //StandardMessages.ResultsCount(x.Count);
            // x.ForEach(film => printFilmprops(film.Name, film.Genre, film.Runtime, film.Synopsis, film.ReleaseDate, film.Age));


            Random r = new Random();
            int counter = 0;
            (from m in myFilms
                    select new {   Name = m.Name, 
                                   Genre = m.Genre, 
                                   Runtime = m.Runtime, 
                                   Synopsis = m.Synopsis,  Age = m.Age,
                                   ReleaseDate = DateTime.TryParse(m.ReleaseDate, out releaseDate) ? releaseDate : new DateTime(r.Next(2020, 2021), r.Next(5, 12), r.Next(1, 28))
                               })
                .Where(t => 
                    t.ReleaseDate < DateTime.Now.AddMonths(monthsUntilRelease) && t.ReleaseDate >= DateTime.Now.AddDays(-1))
                .ToList()
                .ForEach(
                    mv =>
                    {
                        counter++;
                        printFilmprops(mv.Name, mv.Genre, mv.Runtime, mv.Synopsis, mv.ReleaseDate.ToString("D"), mv.Age);
                    });
                    StandardMessages.ResultsCount(counter);


        }

        private static void printFilmprops(string t, string g, string rt, string s, string rd, string age)
        {
            Console.WriteLine($"Title:\t\t\t {t}");
            Console.WriteLine($"Genre:\t\t\t {g}");
            Console.WriteLine($"Runtime:\t\t {rt}");
            Console.WriteLine($"Synopsis:\t\t {s}");
            Console.WriteLine($"RELEASE DATE:\t\t {rd}");
            Console.WriteLine($"Age restriction:\t {age}\n\n");
        }
    }
}
