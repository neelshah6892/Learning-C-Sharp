using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestApp
{
    class Program
    {
        static readonly string textFile = @"D:\Trade Files\FO 6_16_2021.txt";
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(textFile);
            string temp;
            foreach (string line in lines)
            {
                temp = line;
                Console.WriteLine(line);
            }
            //USing StreamReader
            using(StreamReader file = new StreamReader(textFile)) {  
            int counter = 0;  
            string ln;  
  
            while ((ln = file.ReadLine()) != null) {  
            Console.WriteLine(ln);  
            counter++;  
            }  
            file.Close();  
            Console.WriteLine(counter);
            } 
        }
    }
}
