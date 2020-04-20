using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace Cinema
{
    class Movie
    {
        public string name;
        public string genre;
        public int runtime;
        public string synopsis;
        public DateTime releaseDate;


        public Movie(string name, string genre, int runtime, string synopsis, DateTime releaseDate)
        {
            this.name = name;
            this.genre = genre;
            this.runtime = runtime;
            this.synopsis = synopsis;
            this.releaseDate = releaseDate;
        }

        //constructor overloading: redefine a constructor in more than one form
        public Movie(string name, string genre, int runtime, string synopsis, string releaseDate)
        {
            this.name = name;
            this.genre = genre;
            this.runtime = runtime;
            this.synopsis = synopsis;
            CultureInfo provider = CultureInfo.InvariantCulture;
            this.releaseDate = DateTime.ParseExact(releaseDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }

    class Movies
    {
        List<Movie> movieList;


        string jsonFileLocation;




        public Movies(string jsonFileLocation)
        {
            //the actual initialization function is its own method so that it can be called manually
            this.jsonFileLocation = jsonFileLocation;
            initialize();
        }


        public void deleteMovie(Movie movie)
        {
            Console.WriteLine("Delete a movie\nEnter the title:");
            string deleteTitle = Console.ReadLine();
            bool check = false;
            for (int i = 0; i < movieList.Count - 1; i++)
            {
                if (movieList[i].name == deleteTitle)
                {
                    movieList.RemoveAt(i);
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                Console.WriteLine("That movie does not exist.");
            }

            JArray delete = new JArray();
            foreach (Movie m in movieList)
            {
                delete.Add(JObject.FromObject(m));
            }
            File.WriteAllText(@"./movies/movie.json", delete.ToString());
        }

        public void initialize()
        {
            this.movieList = new List<Movie>();
            JArray movieArray = JArray.Parse(File.ReadAllText(this.jsonFileLocation));
            foreach (JObject obj in movieArray)
            {
                this.movieList.Add(new Movie((string)obj["name"], (string)obj["genre"], (int)obj["runtime"], (string)obj["synopsis"], (string)obj["releaseDate"]));
            }
        }

        public void updateCreateMovie()
        {
            Console.WriteLine("Enter the name of the movie: ");
            string movieName = Console.ReadLine();
            foreach (Movie movie in this.movieList)
            {
                if (movie.name == movieName)
                {
                    Console.WriteLine("Do you want to remove or edit the movie?");
                    string addOrDelete = Console.ReadLine();
                    if (addOrDelete == "edit")
                    {
                        this.updateMovie(movie);
                        return;
                    }
                    if (addOrDelete == "remove")
                    {
                        this.deleteMovie(movie);
                        return;
                    }
                }
                else
                {
                    this.createMovie(movieName);
                    return;
                }
            }
        }
        private void updateMovie(Movie movie)
        {
            Console.WriteLine("What do you want to edit?");
            string update = Console.ReadLine();
            switch (update)
            {
                case "genre":
                    Console.WriteLine("new genre:");
                    movie.genre = Console.ReadLine();
                    break;
                case "synopsis":
                    Console.WriteLine("new synopsis:");
                    movie.synopsis = Console.ReadLine();
                    break;
                case "name":
                    Console.WriteLine("new name:");
                    movie.name = Console.ReadLine();
                    break;
                case "runtime":
                    Console.WriteLine("new runtime:");
                    movie.runtime = int.Parse(Console.ReadLine());
                    break;
                case "releaseDate":
                    Console.WriteLine("new releaseDate:");
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    movie.releaseDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            this.createJson();
        }

        private void createMovie(string movieName)
        {
            Console.WriteLine("Add a movie\nTitle:");
            string addMovie = Console.ReadLine();
            Console.WriteLine("Genre:");
            string genre = Console.ReadLine();
            Console.WriteLine("runtime:");
            string runtime = Console.ReadLine();
            Console.WriteLine("synopsis:");
            string synopsis = Console.ReadLine();
            Console.WriteLine("releaseDate:");
            string releaseDate = Console.ReadLine();


            this.movieList.Add(new Movie(movieName, genre, int.Parse(runtime), synopsis, releaseDate));
            this.createJson();
        }
        private void createJson()
        {
            string location = this.jsonFileLocation;

            //checking if file exists
            if (File.Exists(location))
            {
                File.Delete(location);
            }
            JArray movieArray = new JArray();
            foreach (Movie movie in this.movieList)
            {
                movieArray.Add(new JObject(
                    new JProperty("name", movie.name),
                    new JProperty("genre", movie.genre),
                    new JProperty("runtime", movie.runtime),
                    new JProperty("synopsis", movie.synopsis),
                    new JProperty("releaseDate", movie.releaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture))));
            }
            File.WriteAllText(location, movieArray.ToString());
        }

    }
}

