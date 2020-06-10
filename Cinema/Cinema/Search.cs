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
                Console.WriteLine("Main menu > Search");
                Console.WriteLine("What do you want to do?\nEnter the number \n [1]Film \n [2]Genre \n [3]Datum \n [4]Exit \n");
                var val = Console.ReadLine();
                filmChoice = Convert.ToInt32(val);

                if (filmChoice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Main menu > Search > Film");
                    Console.WriteLine("Which movie are you looking for? \n Our system is case sensitive.");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.Name == filmSearch);

                    Console.WriteLine("Main menu > Search > Film > Result");
                    Console.WriteLine("Result:");
                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                    Console.ReadLine();
                    Console.Clear();
                }

                else if (filmChoice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Main menu > Search > Genre");
                    Console.WriteLine("Which genre do you want to view?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.Genre == filmSearch);

                    Console.WriteLine("Main menu > Search > Genre > Result");
                    Console.WriteLine("Result:");

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                    Console.ReadLine();
                    Console.Clear();
                }

                else if (filmChoice == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Main menu > Search > Date");
                    Console.WriteLine("Which release date are you looking for?: \nEnter a date in the following format: DD-MM-YYYY");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.ReleaseDate == filmSearch);

                    Console.WriteLine("Main menu > Search > Date > Result");
                    Console.WriteLine("Result:");
                    

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                    Console.ReadLine();
                    Console.Clear();
                }

                else if (filmChoice == 4)
                {
                    userWrong = false;
                    Console.Clear();
                }

                else
                {
                    Console.WriteLine("Invalid choice.");
                    Console.ReadLine();
                }
            }
        }
    }
}
