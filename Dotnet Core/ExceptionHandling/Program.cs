using System;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first number: ");
            int n1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second number: ");
            int n2 = int.Parse(Console.ReadLine());

            try
            {
                int result = n1 / n2;
                Console.WriteLine($"Result: +{result}");
            }
            catch(DivideByZeroException e)
            {
                throw;
                Console.WriteLine($"Illegal Operation: {e.Message}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("END OF PROGRAM");
            }
        }
    }
}
