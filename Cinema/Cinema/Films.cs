using System;
namespace Cinema
{
    public class Films
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Runtime { get; set; }
        public string Synopsis { get; set; }
        public string ReleaseDate { get; set; }
        public string Age { get; set; }


        public string printFilms()
        {
            return string.Format("name: {0} Genre: {1} runtime: {2} \nSynopsis: {3}\nRELEASE DATE: {4}\nAge: {5}", Name, Genre, Runtime, Synopsis, ReleaseDate,Age);
        }

        public string printOrderFilms()
        {
            return string.Format("name: {0} | Genre: {1} | runtime: {2} | Age: {3}", Name, Genre, Runtime, Age);
        }
    }
}
 