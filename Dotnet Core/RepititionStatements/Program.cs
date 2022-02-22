using System;

namespace RepititionStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            //For loop(Counter controlled)
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Counter: {i}");
            }
            Console.WriteLine();
            Console.WriteLine("End of for loop");


            //While loop(Condition controlled - pre check)
            int n = 10;
            while (n < 5)
            {
                //n++;
                Console.WriteLine("Input number: ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"In while loop {n}");
                Console.WriteLine();
                //n += 1;
            }
            Console.WriteLine("End of while loop");

            //Do...While loop(Condition controlled - post check)
            do
            {
                Console.WriteLine("Input number: ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"In while loop {n}");
                Console.WriteLine();
            }
            while (n < 5);
            Console.WriteLine("End of do...while loop");
        }
    }
}
