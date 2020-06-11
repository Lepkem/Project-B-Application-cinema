﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cinema
{
    using System.ComponentModel.DataAnnotations;

    class Order
    {
        
        public static void orderMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Order Tickets");
            Console.ResetColor();
            bool quit = false;
            int inputFilm = 0;
            int userAge = 0;

            userAge = askAge();

            while (quit == false)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main menu > Order Tickets > Select Movie");
                Console.ResetColor();
                Console.WriteLine("Which movie do you want to watch? Enter a number\n");

                int i = 0;
                foreach (Films f in Program.myFilms)
                {
                    string x = f.printOrderFilms();
                    Console.WriteLine("[" + i + "] " + x);
                    i++;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nNotice! If you want to quit out of ordering a ticket anywhere, just type exit.\n");
                Console.ResetColor();

                try
                {
                    string exit = Console.ReadLine();
                    if (exit == "exit" || exit == "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    inputFilm = int.Parse(exit);
                    if (inputFilm > i - 1)
                    {
                        Console.Clear();
                        StandardMessages.EnterNumber();
                    }
                    else
                    {
                        if (checkAge(inputFilm, userAge)) { quit = true; }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Unfortunately {0} is too young for the official viewing guide, please select another movie.\n", userAge);
                        }
                    }
                }
                catch
                {
                    Console.Clear();
                    StandardMessages.EnterNumber();
                }
            }
            OrderMovie(inputFilm);
        }

        static void OrderMovie(int input)
        {
            //prints all available movies
            double price = 0.0;
            Console.Clear();
            bool quit = false;
            int x = 0;
            List<ScheduleElement> possibleMovies = new List<ScheduleElement>();
            while (quit == false)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main menu > Order Tickets > Select Movie");
                Console.ResetColor();
                Console.WriteLine("Choose your preference: select number \n");
                //finds all schedule elements that contain that movie
                IEnumerable<ScheduleElement> query = Program.schedule.Where(schedule => schedule.movie == Program.myFilms[input]);
                int i = 0;
                
                foreach (ScheduleElement schedule in query)
                {
                    possibleMovies.Add(schedule);
                    Console.WriteLine("[" + i + "]");
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
                        StandardMessages.EnterNumber();
                    }
                    else
                    {
                        quit = true;
                    }
                }
                catch
                {
                    Console.Clear();
                    StandardMessages.EnterNumber();
                }
            }
            quit = false;
            int seats = 0;
            Console.Clear();
            while (quit == false)
            {
                try
                {

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Select Movie > Select Tickets");
                    Console.ResetColor();
                    Console.WriteLine("How many tickets do you want? Please enter a number below 11.\n(Or type 'exit' to go back to the main menu.)");

                    string exit = Console.ReadLine();
                    if (exit == "exit" || exit == "Exit")
                    {
                        Console.Clear();
                        return;
                    }
                    seats = int.Parse(exit);
                    quit = seats <= 10;
                    
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please fill in numbers below 10 only!");
                }
            }

            ScheduleElement ticket = possibleMovies[x];
            //takes the index number of the room in the room list, adds one to that number, and that **should** always be the number in the room's name
            string filename = $"{ticket.date}--{ticket.time}-{ticket.room.roomNumber}";
            string file = @$".\rooms\ScheduledRooms\{filename}.json";
            List<Tuple<Seat, Tuple<int, int>>> selectedSeats = new List<Tuple<Seat, Tuple<int, int>>>();
            bool running = true;

            while (running)
            {
                bool print = false;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main menu > Order Tickets > Select Tickets > Select Seats");
                Console.ResetColor();
                Console.WriteLine("\nPlease pick a row of seats. The row has to be " + seats + " seats long.\nThe upper left corner is 1,1.");
                ticket.room.printRoom();

                Console.Write("Input the coordinates of the leftmost seat; coordinates must be in the format ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("chair,row");
                Console.ResetColor();
                Console.Write(".\n");

                bool loop = true;
                string coordInput = "";
                string[] try0 = coordInput.Split();
                while (loop)
                {
                    coordInput = Console.ReadLine();
                    if (coordInput == "exit" || coordInput == "Exit")
                    {
                        return;
                    }
                    if (coordInput != "")
                    {
                        try
                        {
                            try0 = coordInput.Split(',');
                            Tuple<int, int> try1 = new Tuple<int, int>(int.Parse(try0[0]), int.Parse(try0[1]));
                            loop = false;
                        }
                        catch
                        {
                            Console.WriteLine("Please fill in integers only including the ','!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Fill in something");
                    }
                }

                string[] splitInput = coordInput.Split(',');
                Tuple<int, int> coords = new Tuple<int, int>(int.Parse(splitInput[0]), int.Parse(splitInput[1]));
                if (coords.Item1 < 1 || coords.Item2 < 1 || coords.Item2 > ticket.room.layout.GetLength(0) || coords.Item1 > (ticket.room.layout.GetLength(1) - seats))
                {
                    Console.WriteLine("Please write the coordinates as instructed, and ensure there is enough space for the other seats in the row.\n\nPress Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
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
                            print = true;
                        }
                        else
                        {
                            Console.WriteLine("That seat is already taken.\n\nPress enter to continue...");
                            selectedSeats = new List<Tuple<Seat, Tuple<int, int>>>();
                            Console.ReadLine();
                            break;
                        }
                    }
                //Print confirmation of order
                if (print)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("\nYou are about to purchase the folowing chairs: ");
                    Console.ResetColor();             
                    foreach (var b in selectedSeats)
                    {
                        Console.WriteLine("Row: {0} Chair: {1}", b.Item2.Item2.ToString(), b.Item2.Item1.ToString());
                    }
                    bool choise = StandardMessages.AreYouSure();
                    if (choise)
                    {
                        running = false;
                    }
                    else
                    //reverse vacancy and emty selected seats
                    {
                        Console.Clear();
                        for (int i = 0; i < seats; i++)
                        {
                            Tuple<int, int> tempcoords = new Tuple<int, int>(coords.Item1 + i, coords.Item2);
                            if (!ticket.room.layout[tempcoords.Item2 - 1, tempcoords.Item1 - 1].vacant)
                            {
                                ticket.room.layout[tempcoords.Item2 - 1, tempcoords.Item1 - 1].vacant = true;
                                selectedSeats.Remove(new Tuple<Seat, Tuple<int, int>>(ticket.room.layout[tempcoords.Item2, tempcoords.Item1], tempcoords));
                            }
                        }
                    }

                }
            }
            foreach (var c in selectedSeats)
            {
                ticket.room.updateVacancy(c.Item2.Item1, c.Item2.Item2, file);
                price += (1+1/(c.Item1.priceMod-1))*(double)ticket.room.roomType;
            }

            IDBank.storeOrder(ticket, selectedSeats, price);
            ticket.room.Initialize(File.ReadAllText(file));
        }

        static int askAge()
        {
            int age = 0;
            bool quit = false;

            while (quit == false)
            {
                Console.WriteLine("To make sure that you wil enjoy the movie you are about to select,\nWe have to request the age of the youngest member you are buying ticket(s) for.");
                Console.WriteLine("Deltascope personal can ask for identification at entry, to make sure the viewing guide is uphold.\n ");

                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Age: ");
                    Console.ResetColor();
                    age = int.Parse(Console.ReadLine());

                    if (age < 1) { Console.Clear(); Console.WriteLine("Please fill in a positve integer\n"); }
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
                    StandardMessages.NoSearchResults();
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
