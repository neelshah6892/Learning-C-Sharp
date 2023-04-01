using System;

namespace Learning
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>()
            {
                "one",
                "two",
                "three"
            };

            //Classes
            Movie movie = new Movie();
            movie.Title = "one";

            Movie movie2 = new Movie();
            movie2.Title = "two";

            List<Movie> movies = new List<Movie>();

            movies.Add(movie);
            movies.Add(movie2);

            foreach (var item in movies) {
                Console.WriteLine(item.Title);
            }

            //Static Classes
            
            Console.WriteLine(Book.Print());

            //Switch and const
            const string input = "good day";

            switch (input)
            {
                case "one":
                    Console.WriteLine("Case 1");
                    break;
                case "good day":
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("DEfault");
                    break;
            }

            //While loop
            while (true)
            {
                Console.WriteLine("Enter your name");
                Console.WriteLine(Console.ReadLine());
            }
        }
    }
}