using System;
using System.Linq;

namespace ConString.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine("Please enter a comma seperated list of strings to filter:");
                string input = System.Console.ReadLine();
                var listToFilter = input.Split(',');

                System.Console.WriteLine("Result: {0}", String.Join(",", listToFilter.FilterConcatenatedStrings().Select(x => x.ToString()).ToArray()));
                System.Console.WriteLine("");
                System.Console.WriteLine("##########################################################");
            }
        }
    }
}
