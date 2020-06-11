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
        public string runtime;
        public string synopsis;
        public string releaseDate;
        public string age; 


        public Movie(string name, string genre, string runtime, string synopsis, string releaseDate, string age)
        {
            this.name = name;
            this.genre = genre;
            this.runtime = runtime;
            this.synopsis = synopsis;
            this.releaseDate = releaseDate;
            this.age = age;
        }
    }

    class Movies
    {
        static List<Movie> movieList;


        string jsonFileLocation;




        public Movies(string jsonFileLocation)
        {
            //the actual initialization function is its own method so that it can be called manually
            this.jsonFileLocation = jsonFileLocation;
            initialize();
        }


        public void deleteMovie(Movie movie)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Delete movie");
            Console.ResetColor();
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
                StandardMessages.NoSearchResults();
                StandardMessages.TryAgain();
            }

            JArray delete = new JArray();
            foreach (Movie m in movieList)
            {
                delete.Add(JObject.FromObject(m));
            }
            File.WriteAllText(jsonFileLocation, delete.ToString());
        }

        public void initialize()
        {
            if (!File.Exists(jsonFileLocation))
            {
                File.WriteAllText(jsonFileLocation, "[]");
            }

            movieList = new List<Movie>();
            JArray movieArray = JArray.Parse(File.ReadAllText(this.jsonFileLocation));
            foreach (JObject obj in movieArray)
            {
                movieList.Add(new Movie((string)obj["name"], (string)obj["genre"], (string)obj["runtime"], (string)obj["synopsis"], (string)obj["releaseDate"], (string)obj["age"]));
            }
        }

        public void updateCreateMovie()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Create movie");
            Console.ResetColor();
            Console.WriteLine("Enter the name of the movie.\nIf the name of the movie already exists, you will be automatically redirected to adding a movie. ");
            string movieName = Console.ReadLine();
            foreach (Movie movie in movieList)
            {
                if (movie.name.Equals(movieName))
                {
                    
                    Console.WriteLine("\nDo you want to remove or edit the movie? Choose from:\n[1] Edit\n[2] Remove");
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
            }
            this.createMovie(movieName);
            return;
        }
        private void updateMovie(Movie movie)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Edit Movie");
            Console.ResetColor();
            Console.WriteLine("What do you want to edit? Choose from:\n[1] Genre\n[2] Synopsis\n[3] Name\n[4] Runtime\n[5] Release Date\n[6] Age restriction\n");
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
                    Console.WriteLine("new runtime (in minutes):");
                    movie.runtime = Console.ReadLine();
                    break;
                case "5":
                    Console.WriteLine("new releaseDate (format: dd/mm/yyyy):");
                    movie.releaseDate = Console.ReadLine();
                    break;
                case "6":
                    Console.WriteLine("new age restriction:");
                    movie.age = Console.ReadLine();
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
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Main menu > Create Movie > Add New Movie");
            Console.ResetColor();
            Console.WriteLine("Add a movie\nTitle:");
            string addMovie = Console.ReadLine();
            Console.WriteLine("\nGenre:");
            string genre = Console.ReadLine();
            Console.WriteLine("\nruntime (in minutes):");
            string runtime = Console.ReadLine();
            Console.WriteLine("\nsynopsis:");
            string synopsis = Console.ReadLine();
            Console.WriteLine("\nreleaseDate (Format: MM/dd/yyyy):");
            string releaseDate = Console.ReadLine();
            Console.WriteLine("\nAge restriction:");
            string age = Console.ReadLine();


            movieList.Add(new Movie(movieName, genre, runtime, synopsis, releaseDate, age));
            this.createJson();
            Console.WriteLine("You succesfully added the movie:");
            Console.WriteLine( "Title: " + addMovie + "\n" + "Genre: " + genre + "\n" + "Runtime: " + runtime + "\n" + "Synopsis: " + synopsis + "\n" + "Release Date: " + releaseDate + "\n" + "Age: " + age + "\n");
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
            foreach (Movie movie in movieList)
            {
                movieArray.Add(new JObject(
                    new JProperty("name", movie.name),
                    new JProperty("genre", movie.genre),
                    new JProperty("runtime", movie.runtime),
                    new JProperty("synopsis", movie.synopsis),
                    new JProperty("releaseDate", movie.releaseDate),
                    new JProperty("age", movie.age)));
            }

            File.WriteAllText(location, movieArray.ToString());
        }

        public static List<Movie> GetList()
        {
            return Movies.movieList;
        }
    }
}

