using System;

namespace VariablesAndDataTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vriable Declarations and Types
            string name;
            int age;
            double salary;
            char gender;
            bool working;

            //Prompt users for input
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            
            Console.WriteLine("Enter your age: ");
            age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter your salary: ");
            salary = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter your Gender (M or F): ");
            gender = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("Are you working? (true or false): ");
            working = Convert.ToBoolean(Console.ReadLine());

            //Print to console
            Console.WriteLine("Your name is: " + name);
            Console.WriteLine("Your age is: {0}", age);
            Console.WriteLine($"Your salary is: {salary}");
            Console.WriteLine("Your gender is: " + gender);
            Console.WriteLine("You are working: " + working);
        }
    }
}
