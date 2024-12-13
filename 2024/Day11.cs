using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode
{
    public class Day11
    {
        public static int Main11(string[] args)
        {
            string input = Day11.Input75;
            int blinks;
            IList<Stone> stones;
            string errorMessage;


            if (!Day11.TryParse(input, out blinks, out stones, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            // count cache indexed on stone value after number of blinks
            var knownResults = new Dictionary<long, IDictionary<int, long>>();
            Console.WriteLine("stones: " + stones.Sum(s => s.Count(blinks, knownResults)));
            return 0;
        }

        private static bool TryParse(string input, out int blinks, out IList<Stone> stones, out string errorMessage)
        {
            errorMessage = null;
            blinks = 0;
            stones = null;

            var pieces = input.Split(": ");
            if (pieces.Length != 2)
            {
                errorMessage = "Input format: '{blinks}: {stone1} {stone1} ...'.";
                return false;
            }

            var stonesTemp = new List<Stone>();
            int blinksTemp;
            if (!int.TryParse(pieces[0], out blinksTemp))
            {
                errorMessage = $"Failed to parse int '{pieces[0]}'.";
                return false;
            }

            foreach (var piece in pieces[1].Split(" "))
            {
                long n;
                if (!long.TryParse(piece, out n))
                {
                    errorMessage = $"Failed to parse int '{piece}'.";
                    return false;
                }
                stonesTemp.Add(new Stone(n));
            }

            blinks = blinksTemp;
            stones = stonesTemp;
            return true;
        }

        private class Stone
        {
            public long val;

            public Stone(long val)
            {
                this.val = val;
            }

            public long Count(int blinks, IDictionary<long, IDictionary<int, long>> knownResults)
            {
                if (blinks == 0)
                {
                    return 1;
                }

                // count already known
                long cachedCount = -1;
                if (knownResults.ContainsKey(this.val)
                 && knownResults[this.val].ContainsKey(blinks))
                {
                    return  knownResults[this.val][blinks];
                }

                Stone newStone;
                long valOriginal = this.val;
                bool split = this.Blink(out newStone);
                long count = this.Count(blinks - 1, knownResults);
                if (split)
                {
                    count += newStone.Count(blinks - 1, knownResults);
                }

                // cache the result for duplicate counts
                IDictionary<int, long> countByBlinks;
                if (!knownResults.TryGetValue(valOriginal, out countByBlinks))
                {
                    countByBlinks = new Dictionary<int, long>();
                    knownResults[valOriginal] = countByBlinks;
                }
                countByBlinks[blinks] = count;
                return count;
            }

            // returns true if split
            private bool Blink(out Stone newStone)
            {
                newStone = null;

                if (this.val == 0)
                {
                    this.val = 1;
                    return false;
                }

                var digits = this.val.ToString();
                if (digits.Length % 2 == 0)
                {
                    this.val = 0;
                    newStone = new Stone(0);

                    int pow = 1;
                    int iLeft = digits.Length / 2 - 1;
                    int iRight = digits.Length - 1;

                    while (iLeft >= 0)
                    {
                        this.val += int.Parse(digits[iLeft].ToString()) * pow;
                        newStone.val += int.Parse(digits[iRight].ToString()) * pow;
                        pow *= 10;

                        --iLeft;
                        --iRight;
                    }

                    return true;
                }

                this.val *= 2024;
                return false;
            }
        }

        private const string Debug = "6: 4048 4048";
        private const string Example = "25: 125 17";
        private const string Example75 = "75: 125 17";
        private const string Input = "25: 8435 234 928434 14 0 7 92446 8992692";
        private const string Input75 = "75: 8435 234 928434 14 0 7 92446 8992692";
    }
}
