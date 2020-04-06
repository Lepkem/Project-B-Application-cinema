using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComingSoon;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Rafiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Movie movie1 = new Movie()
            {
                genre = "genre1",
                name = "name1",
                runtime = 90,
                synopsis = "Syn1",
                releasedate = DateTime.Parse("2020-04-01 00:00:01")
            };


            Movie movie2 = new Movie()
            {
                genre = "drama",
                name = "Rafal goes to the park",
                runtime = 120,
                synopsis = "Rafal finds a lost dog",
                releasedate = DateTime.Parse("2005-05-02 00:00:01")
            };

            Movie movie3 = new Movie()
            {
                genre = "genre1",
                name = "RskiAndJordan",
                runtime = 3,
                synopsis = "Syn1",
                releasedate = DateTime.Parse("2020-09-01 00:00:01")
            };

            Movie movie4 = new Movie()
            {
                genre = "slapstick",
                name = "R does what",
                runtime = 3,
                synopsis = "Jordan says What?",
                releasedate = DateTime.Parse("2020-10-01 00:00:01")
            };

            List<Movie> Movies = new List<Movie>();
            Movies.Add(movie1);
            Movies.Add(movie2);
            Movies.Add(movie3);
            Movies.Add(movie4);

            string jsonString;
            jsonString = JsonSerializer.Serialize(movie1);
            Console.WriteLine(jsonString);

            foreach (Movie item in Movies)
            {
//Console.WriteLine(item.name);
            }


            List<string> JstringList = new List<string>();


            foreach (var item in Movies)
            {
                //Console.WriteLine($"{item.name} will be shown at {item.releasedate}.");
                jsonString = JsonSerializer.Serialize(item);
                JstringList.Add(jsonString);
            }

            string json = JsonConvert.SerializeObject(Movies);
            Console.WriteLine(json+"JAAAAAAAAAAA");



            Console.WriteLine("******* Now reading file ***** ");
            string FileContentString = "";
            try
            {
                FileContentString = System.IO.File.ReadAllText(@"C:\j.json");
                Console.WriteLine(FileContentString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Program will now terminator");
                Console.ReadLine();

                Environment.ExitCode = 1; // From uncle google - to exit with error 
            }

            try
            {
                List<Movie> DeserializedS = JsonConvert.DeserializeObject<List<Movie>>(FileContentString);
                Console.WriteLine(DeserializedS);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            uint counter = 0;

            foreach (var item in Movies)
            {
                if (item.releasedate <= DateTime.Now.AddMonths(2))
                {
                    Console.WriteLine(item.releasedate);
                    counter++;

                }
            }

            if (counter == 0)
            {
                Console.WriteLine("There are no movies containing this");

                Console.Write("> Press Enter");

                Console.ReadLine();


            }
            Console.ReadLine();
            

            MovieComingSoon mcs = new MovieComingSoon();

            //mcs.ShowComingSoon(@"C:\j.json");
            Console.WriteLine(mcs.ShowComingSoon(@"C:\j.json"));
            Console.WriteLine("it works for the first one");
            Console.WriteLine(mcs.ShowComingSoon(@"C:\j.json",3));
            Console.WriteLine("it works for the second one");
            Console.ReadLine();
        }
    }

}
