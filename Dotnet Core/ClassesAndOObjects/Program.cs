using System;

namespace ClassesAndOObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create object of class type box
            Box box = new Box();
            Box box2 = new Box();

            Console.WriteLine("Enter Length 1:");
            double length = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Breadth 1");
            double breath = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Height 1");
            double height = Double.Parse(Console.ReadLine());

            //Setting values in object properties
            box.Length = length;
            box.Breadth = breath;
            box.Height = length;
            double volume = box.getVolume();


            Console.WriteLine("Enter Length 2:");
            length = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Breadth 2");
            breath = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Height 2");
            height = Double.Parse(Console.ReadLine());

            //Setting values in object properties
            box2.Length = length;
            box2.Breadth = breath;
            box2.Height = length;
            volume = box2.getVolume();


            //Getting values from object properties
            Console.WriteLine($"Box 1 dimensions are: {box.Length}, {box.Breadth}, {box.Height}. Volume is: {volume}. Area is: {box.getArea()}");
            Console.WriteLine($"Box 2 dimensions are: {box2.Length}, {box2.Breadth}, {box2.Height}. Volume is: {volume}. Area is: {box2.getArea()}");

        }
    }
}
