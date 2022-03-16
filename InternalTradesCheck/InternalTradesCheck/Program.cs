using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace InternalTradesCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("D:\\InternalTradesCheck\\InternalTradesCheck\\FO 6_16_2021.txt");
            var first = new[] { "uniqueseqno, mkttype, trdno, trdtm, token, trdqty, trdprc, bsflg, ordno, brncd, usrid, procli, cliactno, cpcd, remarks, acttype, tcd, ordtm, booktype, opptmcd, ctclid, status, tmcd, sym, ser, inst, expdt, strprc, opttype"};
            File.WriteAllLines(@"D:\\InternalTradesCheck\\InternalTradesCheck\\file.txt", first);
            File.AppendAllLines(@"D:\\InternalTradesCheck\\InternalTradesCheck\\file.txt", lines);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[2]
                select x[2] + ", " + (x[1] + " " + x[0]);

            File.WriteAllLines(@"D:\\InternalTradesCheck\\InternalTradesCheck\\file.txt", query.ToArray());
        }
    }
}
