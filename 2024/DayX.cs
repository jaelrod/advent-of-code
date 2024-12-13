using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DayX
    {
        public static int MainX(string[] args)
        {
            string input = DayX.Example;

            string errorMessage;

            if (!DayX.TryParse(input, out errorMessage))
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
