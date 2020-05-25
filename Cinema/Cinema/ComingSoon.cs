using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cinema
{
    //public interface IComingSoon
    //{
    //    string ShowComingSoon(string filename, int monthsUntilRelease); 
    //}

    //public static class ComingSoon 
    //{
        

        //public static string ShowComingSoon(string filename, int monthsUntilRelease = 2)
        //{
        //    string FileContentString = "";

        //    List<Films> DeserializedS;
        //    try
        //    {
        //        FileContentString = System.IO.File.ReadAllText(filename);
        //        DeserializedS = JsonConvert.DeserializeObject<List<Films>>(FileContentString); // here I made instances of classes from a JSON string in a list of class movie

        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message + "something went wrong";
        //    }


        //    List<Films> theComingMovies = new List<Films>(); //The list of the coming movies filtered by 2 months
        //    uint counter = 0;
        //    foreach (var item in DeserializedS)
        //    {
        //        // Use DateTime.TryParse when input is bad. 
        //        //https://www.dotnetperls.com/datetime-tryparse

        //        DateTime movieReleaseTime;

        //        // Try parsing datetime string and returning result with out param 
        //        if (DateTime.TryParse(item.ReleaseDate, out movieReleaseTime)) 
        //        {
        //            // its fine  we continue
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid"); // <-- Control flow goes here
        //            Environment.ExitCode = 1; // I wanted the console app to stop with invalid values https://stackoverflow.com/questions/155610/how-do-i-specify-the-exit-code-of-a-console-application-in-net
        //        }



        //        if (movieReleaseTime <= DateTime.Now.AddMonths(monthsUntilRelease))
        //        {
        //            theComingMovies.Add(item);
        //            counter++;

        //        }
        //    }
        //    return JsonConvert.SerializeObject(theComingMovies);
        //}
    
}
