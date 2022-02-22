using System;

namespace InputOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declare Variables
            string name;

            //Getting and storing valude from input
            Console.WriteLine("Enter your name:");
            name = Console.ReadLine();

            //Printing values to console
            Console.Write("Your name is: ");
            Console.WriteLine(name);
        }
    }
}
