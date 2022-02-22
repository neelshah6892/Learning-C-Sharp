using System;
using System.Collections.Generic;

namespace ArraysAndCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declare Fixed size array
            int[] grades = new int[5]; //Initilization
            // 5 spaces mean you have addresses 0, 1, 2, 3, 4
            //If n is the size of array, than your array address are 0 to n-1

            //Assign values to fixed array
            grades[0] = 1;
            grades[1] = 12;
            grades[2] = 13;
            grades[3] = 90;
            grades[4] = 54;

            grades = new int[] { 10, 20, 30, 40, 50 };

            Console.WriteLine("Enter Student Grades");
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine("Enter Grade: ");
                grades[i] = int.Parse(Console.ReadLine());
            }

            //Print values in fixed array
            Console.WriteLine("Grades Enterede are as folloows:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Grade {i}: {grades[i]}");
            }




            //Declare variable size array
            int[] grades1;


            //Assign values to variable array
            grades1 = new int[] { 10, 20, 30, 40, 50, 60 };


            //Print values in variable array
            Console.WriteLine("Grades Enterede are as folloows:");
            for (int i = 0; i < grades1.Length; i++)
            {
                Console.WriteLine($"Grade {i}: {grades1[i]}");
            }

            //Declare a list
            List<string> names = new List<string>();
            string name = "";

            //Add values to list
            //names.Add("Neel");
            Console.WriteLine("Enter names:");
            while (!name.Equals("-1"))
            {
                name = Console.ReadLine();
                if (!name.Equals("-1"))
                {
                    names.Add(name);
                }
            }

            //Print Values
            Console.WriteLine("Names Enterede are as folloows (for loop):");
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine(names[i]);
            }

            Console.WriteLine("Names Enterede are as folloows (foreach loop):");
            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
        }
    }
}
