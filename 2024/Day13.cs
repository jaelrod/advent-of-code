using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day13
    {
        public static int Main(string[] args)
        {
            string input = Day13.Example;

            string errorMessage;

            if (!Day13.TryParse(input, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }



            return 0;
        }

        private static bool TryParse(string input, out string errorMessage)
        {
            errorMessage = null;


            return true;
        }

        private const string Example = "";
        private const string Input = "";
    }
}
