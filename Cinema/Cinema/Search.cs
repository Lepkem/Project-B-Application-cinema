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


                    IEnumerable<Films> query = Program.myFilms.Where(myFilms => myFilms.Name == filmSearch);

                    foreach (Films films in query)
                    {
                        Console.WriteLine($"Movie: {films.Name}\n{films.Genre}\n{films.Runtime}\n{films.Synopsis}\n{films.ReleaseDate}\n");
                    }
                }

                else if (filmChoice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Welke genre zoek je?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<Films> query = Program.myFilms.Where(myFilms => myFilms.Genre == filmSearch);

                    foreach (Films films in query)
                    {
                        Console.WriteLine($"Movie: {films.Name}\n{films.Genre}\n{films.Runtime}\n{films.Synopsis}\n{films.ReleaseDate}\n");
                    }
                }

                else if (filmChoice == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Welke release datum zoek je?: ");
                    filmSearch = Console.ReadLine();
                    Console.Clear();

                    IEnumerable<Films> query = Program.myFilms.Where(myFilms => myFilms.ReleaseDate == filmSearch);

                    foreach (Films films in query)
                    {
                        Console.WriteLine($"Movie: {films.Name}\n{films.Genre}\n{films.Runtime}\n{films.Synopsis}\n{films.ReleaseDate}\n");
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
