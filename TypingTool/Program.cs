using System;


class TypingTool
{
    public static void Main(string[] args)
    {
        int match = 0;
        int mismatch = 0;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Black;
        for (int i=30; i>0; i--)
        {
            Random rand = new Random();
            double randNumber = NextDouble(rand, 10, 50000, 2); // Round to 2 decimal places
            Console.WriteLine(randNumber);
            string typedNumber = Console.ReadLine();
            if (typedNumber == randNumber.ToString())
            {
                Console.WriteLine("Match");
                Console.WriteLine();
                match += 1;
            }
            else
            {
                Console.WriteLine("OOps");
                Console.WriteLine();
                mismatch += 1;
            }
        }
        Console.WriteLine($"Match: {match} & Mismatch: {mismatch}");

    }

    public static double NextDouble(Random rand, double minValue, double maxValue, int decimalPlaces)
    {
        double randNumber = rand.NextDouble() * (maxValue - minValue) + minValue;
        return Convert.ToDouble(randNumber.ToString("f" + decimalPlaces));
    }

}



