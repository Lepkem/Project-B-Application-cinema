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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main menu > Search");
                Console.ResetColor();
                Console.WriteLine($"Please select one of the following:");
                Console.WriteLine($"\n [1]Film \n [2]Genre \n [3]Datum \n [4]Exit \n");
                filmChoice = Convert.ToInt32(StandardMessages.GetInputForParam("number"));

                if (filmChoice == 1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Search > Film");
                    Console.ResetColor();
                    Console.WriteLine("Which movie are you looking for? \n Our system is case sensitive.");
                    filmSearch = StandardMessages.GetInputForParam("movie title");
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.Name == filmSearch);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Search > Film > Result");
                    Console.ResetColor();
                    StandardMessages.ResultsCount(query.Count());
                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                   
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                }

                else if (filmChoice == 2)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Search > Genre");
                    Console.ResetColor();
                    Console.WriteLine("Which genre do you want to view?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.Genre == filmSearch);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Search > Genre > Result");
                    Console.ResetColor();
                    StandardMessages.ResultsCount(query.Count());

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                    StandardMessages.ResultsCount(query.Count());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                }

                else if (filmChoice == 3)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Search > Date");
                    Console.ResetColor();
                    Console.WriteLine("Which release date are you looking for?:" );
                    
                    filmSearch = StandardMessages.GetInputForParam("date in the following format: DD-MM-YYYY");
                    Console.Clear();

                    IEnumerable<ScheduleElement> query = Program.schedule.Where(myFilms => myFilms.movie.ReleaseDate == filmSearch);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Main menu > Search > Date > Result");
                    Console.ResetColor();
                    StandardMessages.ResultsCount(query.Count());
                    

                    foreach (ScheduleElement schedule in query)
                    {
                        schedule.printScheduleElement();
                    }
                    StandardMessages.ResultsCount(query.Count());
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                }

                else if (filmChoice == 4)
                {
                    userWrong = false;
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                }

                else
                {
                    StandardMessages.SomethingWentWrong();
                    StandardMessages.TryAgain();
                    StandardMessages.PressAnyKey();
                    StandardMessages.PressKeyToContinue();
                }
            }
        }
    }
}
