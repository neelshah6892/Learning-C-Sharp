using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public class Movie
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public string Genre { get; set; }
    }

    public static class Book
    {
        public static string Title { get; set; }

        public static string Genre { get; set;}

        public static string Description { get; set; }

        public static string Print()
        {
            return "Good Day";
        }
    }
}
