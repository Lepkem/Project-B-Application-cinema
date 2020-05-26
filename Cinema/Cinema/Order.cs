using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cinema
{
    class Order
    {
        public static void orderMenu()
        {
            Console.Clear();
            bool quit = false;
            bool ageCheck = true;
            int inputFilm = 0;
            int userAge = 0;

            userAge = askAge();

            while (quit == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Notice! If you want to quit out of ordering a ticket anywhere, just type exit.\n");
                Console.ResetColor();

                Console.WriteLine("Which movie do you want to watch? Enter number\n\n");

                int i = 0;
                foreach (Films f in Program.myFilms)
                {
                    string x = f.printFilms();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[" + i + "]");
                    Console.ResetColor();
                    Console.WriteLine("\n" + x + "\n");
                    i++;
                }

                try
                {
                    string exit = Console.ReadLine();
                    if (exit == "exit" || exit == "Exit") {
                        Console.Clear();
                        return;
                    }
                    inputFilm = int.Parse(exit);
                    if (inputFilm > i - 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Please fill in existing integers only!");
                    }
                    else
                    {
                        if (checkAge(inputFilm, userAge)) { quit = true; }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Unfortunaly {0} is too young for the offical viewing guide, please select another movie.\n", userAge);
                        }          
                    }
                }
                catch
                { 
                        Console.Clear();
                        Console.WriteLine("Please fill in an integer only!\n\n");
                }
            }
            OrderMovie(inputFilm);
        }

        static void OrderMovie(int input)
        {
            //prints all available movies
            Console.Clear();
            bool quit = false;
            int x = 0;
            List<ScheduleElement> possibleMovies = new List<ScheduleElement>();
            while (quit == false)
            {
                Console.WriteLine("Choose your preference: select number \n\n");
                //finds all schedule elements that contain that movie
                IEnumerable<ScheduleElement> query = Program.schedule.Where(schedule => schedule.movie == Program.myFilms[input]);
                int i = 0;
                foreach (ScheduleElement schedule in query)
                {
                    possibleMovies.Add(schedule);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[" + i + "]");
                    Console.ResetColor();
                    schedule.printScheduleElement();
                    i++;
                }
                try
                {
                    string exit = Console.ReadLine();
                    if (exit == "exit" || exit == "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    x = int.Parse(exit);
                    if (x > i - 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Please fill in existing integers only!");
                    }
                    else
                    {
                        quit = true;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please fill in an integer only!\n\n");
                }
            }
            quit = false;
            int seats = 0;
            Console.Clear();
            while (quit == false)
            {
                try
                {

                    Console.WriteLine("How many tickets do you want? Please enter a number.");
                    string exit = Console.ReadLine();
                    if (exit == "exit" || exit == "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    seats = int.Parse(exit);
                    quit = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please fill in integers only!");
                }
            }

            ScheduleElement ticket = possibleMovies[x];
            //takes the index number of the room in the room list, adds one to that number, and that **should** always be the number in the room's name
            string file = string.Format(@".\rooms\room{0}.json", (Array.IndexOf(Program.rooms.ToArray(), ticket.room) + 1));
            List<Tuple<Seat, Tuple<int, int>>> selectedSeats = new List<Tuple<Seat, Tuple<int, int>>>();
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nPlease pick a row of seats. The row has to be " + seats + " seats long.\nThe upper left corner is 1,1.");
                ticket.room.printRoom();

                Console.Write("Input the coordinates of the leftmost seat; coordinates must be in the format ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("x,y");
                Console.ResetColor();
                Console.Write(".\n");

                string coordInput = Console.ReadLine();
                string[] splitInput = coordInput.Split(',');
                Tuple<int, int> coords = new Tuple<int, int>(int.Parse(splitInput[0]), int.Parse(splitInput[1]));
                if (coords.Item1 < 1 || coords.Item2 < 1 || coords.Item2 > ticket.room.layout.GetLength(0) || coords.Item1 > (ticket.room.layout.GetLength(1) - seats))
                {
                    Console.WriteLine("Please write the coordinates as instructed, and ensure there is enough space for the other seats in the row.\n\nPress Enter to continue...");
                    Console.ReadLine();
                }
                else
                    //takes every seat to the left of the given chair up to a certain point and checks if they're free
                    for (int i = 0; i < seats; i++)
                    {
                        Tuple<int, int> tempcoords = new Tuple<int, int>(coords.Item1 + i, coords.Item2);
                        if (ticket.room.layout[tempcoords.Item2 - 1, tempcoords.Item1 - 1].vacant)
                        {
                            ticket.room.layout[tempcoords.Item2 - 1, tempcoords.Item1 - 1].vacant = false;
                            selectedSeats.Add(new Tuple<Seat, Tuple<int, int>>(ticket.room.layout[tempcoords.Item2, tempcoords.Item1], tempcoords));
                            running = false;
                        }
                        else
                        {
                            Console.WriteLine("That seat is already taken.\n\nPress enter to continue...");
                            selectedSeats = new List<Tuple<Seat, Tuple<int, int>>>();
                            Console.ReadLine();
                            break;
                        }
                    }
            }
            foreach (var c in selectedSeats)
            {
                ticket.room.updateVacancy(c.Item2.Item1, c.Item2.Item2, file);
            }
            ticket.room.printRoom();

            IDBank.storeOrder(ticket, selectedSeats);
            ticket.room.Initialize(File.ReadAllText(file));
        }

        static int askAge()
        {
            int age = 0;
            bool quit = false;
            
            while (quit == false)
            {
                Console.WriteLine("To make sure that u wil enjoy the movie you are about to select.\nWe have to request the age of the youngest member for whom your buying ticket(s) for.");
                Console.WriteLine("Deltascope personal can ask for identification at entry. To make sure the viewing guide is uphold.\n ");

                try
                {
                    Console.Write("Age: ");
                    age = int.Parse(Console.ReadLine());
                    if (age < 1) { Console.Clear(); Console.WriteLine("Please fill in a positve intiger\n"); }
                    else
                    {
                        Console.Clear();
                        quit = true;
                        return age;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please fill in a intiger only!\n\n");
                }
            }
            return age;
        }

        static bool checkAge(int input, int userAge)
        {
            if (Program.myFilms[input].Age == "all" || Program.myFilms[input].Age == "All")
            {
                return true;
            }
            else
            {
                if (int.Parse(Program.myFilms[input].Age) <= userAge)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
