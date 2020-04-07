using Newtonsoft.Json.Linq;
using System;
using System.IO;

using System.Collections.Generic;
using System.Globalization;


namespace Cinema
{
    class Program
    {
        static void Main(string[] args)
        {
            //this line takes the file location for the JSON files, reads the entire file, and passes it to the initializer
            //Room roomone = new Room(File.ReadAllText(@".\rooms\room1.json"));
            //Room roomtwo = new Room(File.ReadAllText(@".\rooms\room2.json"));
            //Room roomthree = new Room(File.ReadAllText(@".\rooms\room3.json"));
            
            //roomthree.updateCreateRoom(@".\rooms\room3.json");

            Movies movies = new Movies(@"./movies/movie.json");
            //movieone.updateCreateMovie(@".\movies\movie.json");
            movies.updateCreateMovie();

        }
    }

    class Room
    {
        char[,] layout;
        int chairs;
        public Room(string l)
        {
            //the actual initialization function is its own method so that it can be called manually
            Initialize(l);
        }
        public void deleteRoom()
        {
            //TODO: D31373 the room
        }
        public void Initialize(string l)
        {
            //this line takes the string and turns it into a special object that contains the attributes of the JSON
            JObject input = JObject.Parse(l);
            //this line assigns the chairs value stored in the file in the object's chair variable 
            chairs = (int)input["chairs"];
            //there's also an array of strings in the file: this file takes it and turns it from a massive string to a special array
            JArray inputJArray = JArray.Parse(input["layout"].ToString());
            //create an empty 2D character array the same size as the string array
            char[,] inputMatrix = new char[inputJArray.First.ToString().Length, inputJArray.Count];
            // go through each position in the character array and fill it with the proper character
            for (int i = 0; i < inputMatrix.GetLength(0); i++)
                for (int j = 0; j < inputMatrix.GetLength(1); j++)
                {
                    inputMatrix[i, j] = inputJArray[j].ToString()[i];
                }
            //store the array in the object
            layout = inputMatrix;
        }

        public void updateCreateRoom(string room)
        {
            //read the file as one big string and turn it into a special object
            JObject fullObject = JObject.Parse(File.ReadAllText(room));
            //read the array in the object and store it
            JArray layoutArray = (JArray)fullObject["layout"];
            //go through each row in the layout
            for (int i = 0; i < layoutArray.Count; i++)
            {
                Console.WriteLine("Replace the " + (i + 1) + " line? If yes give new line.");
                string newLine = Console.ReadLine();
                if (newLine != "")
                {
                    //TODO: does making changes to layoutarray change the array inside fullObject?
                    layoutArray[i] = newLine;
                }
            }

            Console.WriteLine("How many chairs should the room have?");
            //alter the special object
            fullObject["chairs"] = Int32.Parse(Console.ReadLine());
            //turn the object into a string
            string updatedString = fullObject.ToString();
            //update the object
            Initialize(updatedString);
            //update the file
            File.WriteAllText(room, updatedString);
        }
    }
    
    //Add, edit and delete movies
    class Movie
    {
        public string name;
        public string genre;
        public int runtime;
        public string synopsis;
        public DateTime releaseDate; 


        public Movie(string name, string genre, int runtime, string synopsis, DateTime releaseDate) {
            this.name = name;
            this.genre = genre;
            this.runtime = runtime;
            this.synopsis = synopsis;
            this.releaseDate = releaseDate;
        }   

        //constructor overloading: redefine a constructor in more than one form
        public Movie(string name, string genre, int runtime, string synopsis, string releaseDate) {
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
            this.createJson();
        }

        public void initialize() 
        {
            this.movieList = new List<Movie>();
            JArray movieArray = JArray.Parse(File.ReadAllText(this.jsonFileLocation));
            foreach (JObject obj in movieArray) {
                this.movieList.Add(new Movie((string)obj["name"], (string)obj["genre"], (int)obj["runtime"], (string)obj["synopsis"], (string)obj["releaseDate"]));
            }
        }

        public void updateCreateMovie()
        {
            Console.WriteLine("Enter the name of the movie: ");
            string movieName = Console.ReadLine();
            foreach (Movie movie in this.movieList) {
                if (movie.name == movieName) {
                    Console.WriteLine("Do you want to remove or edit the movie?");
                    string addOrDelete = Console.ReadLine();
                    if (addOrDelete == "edit") {
                        this.updateMovie(movie);
                        return;
                    }
                    if (addOrDelete == "remove") { 
                        this.deleteMovie(movie);
                        return;
                    }
                }
                else {
                    this.createMovie(movieName);
                    return;
                }
            }
        }   
        private void updateMovie(Movie movie) {
            Console.WriteLine("What do you want to edit?");
            string update = Console.ReadLine();
            switch (update)
            {
                case "genre":
                    Console.WriteLine("genre:");
                    movie.genre = Console.ReadLine();
                    break;
                case "synopsis":
                    Console.WriteLine("synopsis:");
                    movie.synopsis = Console.ReadLine();
                    break;
                case "name":
                    Console.WriteLine("name:");
                    movie.name = Console.ReadLine();
                    break;
                case "runtime":
                    Console.WriteLine("runtime:");
                    movie.runtime = int.Parse(Console.ReadLine());
                    break;
                case "releaseDate":
                    Console.WriteLine("releaseDate:");
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    movie.releaseDate = DateTime.ParseExact(Console.ReadLine(),"MM/dd/yyyy", CultureInfo.InvariantCulture);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            this.createJson();
        }
   
        private void createMovie(string movieName) {
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
        private void createJson() {
            string location = this.jsonFileLocation;
            
            //checking if file exists
            if (File.Exists(location)) {
                File.Delete(location);
            }
            JArray movieArray = new JArray();
            foreach (Movie movie in this.movieList) {
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
