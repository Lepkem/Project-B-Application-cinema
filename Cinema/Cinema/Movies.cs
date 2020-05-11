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
        public string releaseDate;


        public Movie(string name, string genre, int runtime, string synopsis, string releaseDate)
        {
            this.name = name;
            this.genre = genre;
            this.runtime = runtime;
            this.synopsis = synopsis;
            this.releaseDate = releaseDate;
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
            Console.WriteLine("Enter the name of the movie.\nIf the name of the movie already exists, you will be automatically redirected to adding a movie. ");
            string movieName = Console.ReadLine();
            foreach (Movie movie in this.movieList)
            {
                if (movie.name == movieName)
                {
                    Console.WriteLine("Do you want to remove or edit the movie? Choose from:\n[1] Edit\n[2] Remove");
                    string addOrDelete = Console.ReadLine();
                    if (addOrDelete == "1")
                    {
                        this.updateMovie(movie);
                        return;
                    }
                    if (addOrDelete == "2")
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
            Console.WriteLine("What do you want to edit? Choose from:\n[1] Genre\n[2] Synopsis\n[3] Name\n[4] Runtime\n[5] Release Date");
            string update = Console.ReadLine();
            switch (update)
            {
                case "1":
                    Console.WriteLine("new genre:");
                    movie.genre = Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("new synopsis:");
                    movie.synopsis = Console.ReadLine();
                    break;
                case "3":
                    Console.WriteLine("new name:");
                    movie.name = Console.ReadLine();
                    break;
                case "4":
                    Console.WriteLine("new runtime:");
                    movie.runtime = int.Parse(Console.ReadLine());
                    break;
                case "5":
                    Console.WriteLine("new releaseDate:");
                    movie.releaseDate = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("\nThat is not an option, please choose again\n");
                    updateMovie(movie);
                    break;
            }
            this.createJson();
        }

        private void createMovie(string movieName)
        {
            Console.WriteLine("Add a movie\nTitle:");
            string addMovie = Console.ReadLine();
            Console.WriteLine("\nGenre:");
            string genre = Console.ReadLine();
            Console.WriteLine("\nruntime:");
            string runtime = Console.ReadLine();
            Console.WriteLine("\nsynopsis:");
            string synopsis = Console.ReadLine();
            Console.WriteLine("\nreleaseDate (Format: MM/dd/yyyy):");
            string releaseDate = Console.ReadLine();


            this.movieList.Add(new Movie(movieName, genre, int.Parse(runtime), synopsis, releaseDate));
            this.createJson();
            Console.WriteLine("You succesfully added a movie");        
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
                    new JProperty("releaseDate", movie.releaseDate)));
            }
            File.WriteAllText(location, movieArray.ToString());
        }
    }
}

