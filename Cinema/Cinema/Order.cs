using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Cinema
{
    class Order
    {
        public static void orderMenu()
        {
            Console.Clear();
            bool quit = false;
            int inputFilm = 0;
            while (quit == false)
            {
                Console.WriteLine("Which movie do you want to watch? enter number\n\n");

                int i = 0;
                foreach (Films f in Program.myFilms)
                {
                    string x = f.printFilms();
                    Console.WriteLine(i + " " + x + "\n");
                    i++;
                }

                try
                {
                    inputFilm = int.Parse(Console.ReadLine());
                    if (inputFilm > i - 1)
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
            OrderMovie(inputFilm);
        }

        static void OrderMovie(int input)
        {
            Console.Clear();
            bool quit = false;
            int x = 0;
            List<ScheduleElement> possibleMovies = new List<ScheduleElement>();
            while (quit == false)
            {
                Console.WriteLine("Choose your preference: select number \n\n");
                
                IEnumerable<ScheduleElement> query = Program.schedule.Where(schedule => schedule.movie == Program.myFilms[input]);
                int i = 0;
                foreach (ScheduleElement schedule in query)
                {
                    possibleMovies.Add(schedule);
                    Console.WriteLine(i);
                    schedule.printScheduleElement();
                    i++;
                }
                try
                {
                    x = int.Parse(Console.ReadLine());
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

                    Console.WriteLine("How many tickets do you want? enter number");
                    seats = int.Parse(Console.ReadLine());
                    quit = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Fill in integers only!");
                }
            }


            int cord_x = 0;
            int cord_y = 0;
            bool seatLoop = true;
            string file = string.Format(@".\rooms\room{0}.json", (x + 1));

            for (int s = seats; s >= 1; s--)
            {
                seatLoop = true;
                while (seatLoop)
                {
                    Console.Clear();
                    Console.WriteLine("Please pick a seat. You can select " + s + " more seats\n the upper left corner is 0,0");
                    possibleMovies[x].room.printRoom(true);

                    Console.WriteLine("select the X coordinate: ");
                    cord_x = int.Parse(Console.ReadLine());
                    Console.WriteLine("select the Y coordinate: ");
                    cord_y = int.Parse(Console.ReadLine());

                    if (possibleMovies[x].room.layout[cord_x, cord_y].vacant == true)//if spot is open
                    {
                        //possibleMovies[x].room.layout[cord_x, cord_y].vacant. == "1";
                        possibleMovies[x].room.updateVacancy(cord_x, cord_y, file);
                        possibleMovies[x].room.Initialize(File.ReadAllText(file));
                        seatLoop = false;
                    }
                    else { Console.WriteLine("Seats are already taken."); }
                }
            }
        }
    }
}
