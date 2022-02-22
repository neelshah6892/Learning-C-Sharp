using System;

namespace Methods
{
    class Program
    {
        //Void Functions
        static void PrintName()
        {
            Console.WriteLine("Neel");
        }

        static void PrintName(string name)
        {
            Console.WriteLine(name.ToUpper());
        }
        static void PrintNameLowerCase(string name)
        {
            Console.WriteLine(name.ToLower());
        }


        //Value Returning Functions
        static int LargestNumber(int num1,int  num2,int num3)
        {
            int largest = num1;
            if(num2 > largest)
            {
                largest = num2;
            }
            if(num3 > largest)
            {
                largest = num3;
            }
            return largest;
        }

        static void Main(string[] args)
        {
            //Void function call
            PrintName();
            Console.WriteLine("End function call");
            Console.WriteLine("Enter your name");
            string n = Console.ReadLine();
            PrintName(n);
            PrintNameLowerCase(n);

            //Value function call
            Console.WriteLine("Enter number 1:");
            int n1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number 2:");
            int n2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number 3:");
            int n3 = Convert.ToInt32(Console.ReadLine());
            //LargestNumber(n1, n2, n3);
            int result = LargestNumber(n1, n2, n3);
            Console.WriteLine($"Largest number: {result}");
        }
    }
}
