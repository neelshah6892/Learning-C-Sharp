using System;

namespace StringManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declare a string variable
            string fullName = "Hi, My name is Neel Shah";
            string firstName = "Neel";
            string lastName = "Shah";

            //Print to screen
            Console.WriteLine(fullName);

            //Concatenation
            Console.WriteLine("My full name is " + firstName + " " + lastName);
            Console.WriteLine("My full name is {0} {1}", firstName, lastName);
            Console.WriteLine($"My full name is {firstName} {lastName}"); //Interpolation

            //Count length of string
            Console.WriteLine(firstName.Length);
            Console.WriteLine(fullName.Length); //Space is counted in length

            //Replace parts of string
            string newName = firstName.Replace('e', 'i');
            Console.WriteLine(newName);

            //Append to existing string


            //String splits


            //Coompare strings
            if (firstName == lastName)
                Console.WriteLine("Same name");
            else
                Console.WriteLine("Different name");

            Console.WriteLine();

            int result = string.Compare(firstName, lastName);
            if (result == 0)
                Console.WriteLine("Same");
            else
                Console.WriteLine("Different");

            //Convert to string
            result = 1234567890;
            Console.WriteLine(result.ToString());
            int total = 1 + result;

        }
    }
}
