using System;

namespace CSharpOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            //Basic assignment opearator
            int num;
            num = 5;
            Console.WriteLine("Assigned value to variable: {0}", num);

            //Arithmetic operators
            int x = 11;
            int y = 3;
            Console.WriteLine($"Addition: {x+y}");
            Console.WriteLine($"Subtraction: {x - y}");
            Console.WriteLine($"Multiplication: {x*y}");
            Console.WriteLine($"Division: { x / y}");
            Console.WriteLine($"Modulus: {x % y}");

            x = x + 4;
            Console.WriteLine($"New value of x: {x}");
            Console.WriteLine($"Addition: {x + y}");
            Console.WriteLine($"Subtraction: {x - y}");
            Console.WriteLine($"Multiplication: {x * y}");
            Console.WriteLine($"Division: { x / y}");
            Console.WriteLine($"Modulus: {x % y}");

            //Compound assignment operators
            //x += 4; //x = x + 4;
            //x *= 5; //x = x * 5;
            x += 5;
            Console.WriteLine(x);
            x -= 3;
            Console.WriteLine(x);
            x *= 2;
            Console.WriteLine(x);
            x /= 3;
            Console.WriteLine(x);
            x %= 3;
            Console.WriteLine(x);
        }
    }
}
