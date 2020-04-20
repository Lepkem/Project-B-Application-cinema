using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema
{
    public class Search
    {
        public Search()
        {
            int filmChoice;
            string filmSearch;
            bool userWrong = true;

            

            while (userWrong)
            {
                Console.WriteLine("Waar zoek je naar?: \n [1]Film \n [2]Genre \n [3]Datum \n [4]Exit \n");
                var val = Console.ReadLine();
                filmChoice = Convert.ToInt32(val);

                if (filmChoice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Welke film zoek je?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.Name == filmSearch);

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                }

                else if (filmChoice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Welke genre zoek je?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.Genre == filmSearch);

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                }

                else if (filmChoice == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Welke release datum zoek je?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.ReleaseDate == filmSearch);

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                }

                else if (filmChoice == 4)
                {
                    userWrong = false;
                    Console.Clear();
                }

                else
                {
                    Console.WriteLine("Ongeldige keuze!");
                    Console.ReadLine();
                }
            }
        }
    }
}
