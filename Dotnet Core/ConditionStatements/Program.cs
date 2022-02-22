using System;

namespace ConditionStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            //IF Statements
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            string result;

            Console.Write("How many apples do you have?");
            num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("How many oranges do you have?");
            num2 = Convert.ToInt32(Console.ReadLine());

            if(num1 < num2)
            {
                Console.WriteLine("You have more oranges");
            }
            else if(num2 < num1)
            {
                Console.WriteLine("You have more apples");
            }
            else if(num1 == num2)
            {
                Console.WriteLine("Same number of apples and oranges");
            }
            else
            {
                Console.WriteLine("Invalid Parameters");
            }

            Console.Write("Enter number");
            num3 = Convert.ToInt32(Console.ReadLine());

            //Switch Statements
            switch (num3)
            {
                case 1:
                    Console.WriteLine("Value is 1");
                    break;
                case 2:
                    Console.WriteLine("Value is 2");
                    break;
                default:
                    Console.WriteLine("Invalid value");
                    break;
            }


            //Ternery Statements
            result = (num2 > num1) ? "you have more oranges" : "you have more apples";

        }
    }
}
