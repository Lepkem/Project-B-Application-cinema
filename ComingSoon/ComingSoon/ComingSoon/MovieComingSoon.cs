using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace ComingSoon
{
    public class MovieComingSoon : IComingSoon //here I implemented the interface and 
    {
        public string ShowComingSoon(string pathOfFile)
        {
            string FileContentString = "";

            List<Movie> DeserializedS; 
            try
            {
                FileContentString = System.IO.File.ReadAllText(pathOfFile);
                DeserializedS = JsonConvert.DeserializeObject<List<Movie>>(FileContentString); // here I made instances of classes from a JSON string in a list of class movie

            }
            catch (Exception e)
            {
                return e.Message + "something went wrong";
            }


            List<Movie> theComingMovies = new List<Movie>();
            uint counter = 0;
            foreach (var item in DeserializedS)
            {
                if (item.releasedate <= DateTime.Now.AddMonths(2))
                {
                    theComingMovies.Add(item);
                    counter++;

                }
                
            }
            return JsonConvert.SerializeObject(theComingMovies);
                
            
            
        }


        //here I made an overload method in case I wanted to change the coming soon method to show a different timeframe
        public string ShowComingSoon(string pathOfFile, int monthsUntilRelease)
        {

            List<Movie> DeserializedS;
            try
            {
                string fileContentString = "";
                fileContentString = System.IO.File.ReadAllText(pathOfFile);
                DeserializedS = JsonConvert.DeserializeObject<List<Movie>>(fileContentString); // here I made instances of classes from a JSON string in a list of class movie

            }
            catch (Exception e)
            {
                return e.Message + "something went wrong";
            }

            List<Movie> theComingMovies = new List<Movie>();
            uint counter = 0;
            foreach (var item in DeserializedS)
            {
                if (item.releasedate <= DateTime.Now.AddMonths(monthsUntilRelease))
                {
                    theComingMovies.Add(item);
                    counter++;

                }

            }

            return JsonConvert.SerializeObject(theComingMovies);
        }
        
    }
}
