using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class Day<T>
    {
        public virtual int Main(string input)
        {
            T data;
            string errorMessage;

            if (!this.TryParse(input, out data, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }



            return 0;
        }

        protected abstract bool TryParse(string input, out T parsed, out string errorMessage);
        public abstract void Solve(T data);

        protected abstract string Example { get; }
        protected abstract string Input { get; }
    }
}
