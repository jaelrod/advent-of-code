using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day10
    {
        public static int Main10(string[] args)
        {
            string input = Day10.Input;
            string errorMessage;

            if (!Day10.TryParse(input, out int[][] map, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            // cache unique summits reachable from each cell
            HashSet<Summit>[][] canSummit = new HashSet<Summit>[map.Length][];
            int[][] ratings = new int[map.Length][];
            for (int row = 0; row < map.Length; ++row)
            {
                canSummit[row] = new HashSet<Summit>[map[row].Length];
                ratings[row] = new int[map[row].Length];
            }

            // count summitable 9's
            int summits = 0;
            int rating = 0;
            for (int row = 0; row < map.Length; ++row)
            {
                for (int col = 0; col < map[row].Length; ++col)
                {
                    if (map[row][col] == 0)
                    {
                        CanSummit(ref map, row, col, ref canSummit, ref ratings);
                        summits += canSummit[row][col].Count;
                        rating += ratings[row][col];
                    }
                }
            }

            Console.WriteLine($"summits: {summits}");
            Console.WriteLine($"rating: {rating}");
            return 0;
        }

        private static void CanSummit(ref int[][] map, int row, int col, ref HashSet<Summit>[][] canSummit, ref int[][] ratings)
        {
            // already evaluated this cell
            if (canSummit[row][col] != default)
            {
                return;
            }

            // only can reach this summit if at height 9
            if (map[row][col] == 9)
            {
                canSummit[row][col] = new HashSet<Summit>() { new Summit(row, col) };
                ratings[row][col] = 1;
                return;
            }

            // count summits reachable in all +1 uphill directions
            canSummit[row][col] = new HashSet<Summit>();
            ratings[row][col] = 0;
            int nextHeight = map[row][col] + 1;
            if (row > 0 && map[row - 1][col] == nextHeight)
            {
                CanSummit(ref map, row - 1, col, ref canSummit, ref ratings);
                ratings[row][col] += ratings[row - 1][col];
                foreach (var summit in canSummit[row - 1][col])
                {
                    canSummit[row][col].Add(summit);
                }
            }
            if (row < map.Length - 1 && map[row + 1][col] == nextHeight)
            {
                CanSummit(ref map, row + 1, col, ref canSummit, ref ratings);
                ratings[row][col] += ratings[row + 1][col];
                foreach (var summit in canSummit[row + 1][col])
                {
                    canSummit[row][col].Add(summit);
                }
            }
            if (col > 0 && map[row][col - 1] == nextHeight)
            {
                CanSummit(ref map, row, col - 1, ref canSummit, ref ratings);
                ratings[row][col] += ratings[row][col - 1];
                foreach (var summit in canSummit[row][col - 1])
                {
                    canSummit[row][col].Add(summit);
                }
            }
            if (col < map[row].Length - 1 && map[row][col + 1] == nextHeight)
            {
                CanSummit(ref map, row, col + 1, ref canSummit, ref ratings);
                ratings[row][col] += ratings[row][col + 1];
                foreach (var summit in canSummit[row][col + 1])
                {
                    canSummit[row][col].Add(summit);
                }
            }
        }

        private class Summit
        {
            public int row;
            public int col;

            public Summit(int row, int col)
            {
                this.row = row;
                this.col = col;
            }

            // overrides for hashset
            public override int GetHashCode()
            {
                return this.row ^ (100000 * this.col);
            }
            public override bool Equals(object obj)
            {
                return this.Equals(obj as Summit);
            }

            public bool Equals(Summit other)
            {
                return other != null
                    && this.row == other.row
                    && this.col == other.col;
            }
        }

        private static bool TryParse(string input, out int[][] map, out string errorMessage)
        {
            map = null;
            errorMessage = null;

            string[] lines = input.Split("\r\n");
            int[][] mapTemp = new int[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {
                mapTemp[i] = new int[lines.Length];
                for (int j = 0; j < lines[i].Length; ++j)
                {
                    if (lines[i][j] == '.') // used for testing
                    {
                        mapTemp[i][j] = -1;
                        continue;
                    }

                    int n;
                    if (!int.TryParse(lines[i][j].ToString(), out n))
                    {
                        errorMessage = $"Failed to parse int '{lines[i][j]}'";
                        return false;
                    }
                    if (n < 0 || n > 9)
                    {
                        errorMessage = $"Map height '{n}' not in [0-9].";
                        return false;
                    }
                    mapTemp[i][j] = n;
                }
            }

            map = mapTemp;
            return true;
        }

        private const string Example = "89010123\r\n78121874\r\n87430965\r\n96549874\r\n45678903\r\n32019012\r\n01329801\r\n10456732";
        private const string ExampleRating = ".....0.\r\n..4321.\r\n..5..2.\r\n..6543.\r\n..7..4.\r\n..8765.\r\n..9....";
        private const string ExampleRating2 = "..90..9\r\n...1.98\r\n...2..7\r\n6543456\r\n765.987\r\n876....\r\n987....";
        private const string Input = "981050112210121034565432321032103321234567567\r\n872341201014434921878891434549012310432198678\r\n765434318123567890989760023678543986501021549\r\n545985329854356792105654116707621677893430932\r\n456576416763447883498743209811030543082567801\r\n327676509823430976541050124322345892121078998\r\n218489678016521050132011267401276734289010867\r\n109345908987210763249121278569889825678123459\r\n543237817876345854358230989078778910107645678\r\n654106726541096981267345678123498723216530501\r\n761235430432387870301076521014567234345321432\r\n890144321001456765432189410019010121237876501\r\n781055697878921212345674321928723210345989432\r\n218966788965430101254789345839654234326710101\r\n109875692100123270163231236743010165019823321\r\n018754103018786789872100109852123278956754430\r\n325673214109695698987212121961094109849869543\r\n234981005234534321210103030878785603732778672\r\n189012346943413210321234549879676712011001981\r\n076321057892100110450321676566543893425652760\r\n325401069787012026565410789457012798534743854\r\n410512178976543237656998898308932687649887923\r\n567893467689854148997867891219801456308996310\r\n234554992501763056788950750304762343210105409\r\n147667881432612234563441065403451012308712358\r\n018589670109803189612532674312332305419654347\r\n189478521210789078703675689100123498569323256\r\n789767432345650876589986789210096567878012100\r\n679854343217821987676521654323487458901223321\r\n565943034506934910505430598714510329874341234\r\n450012129645445823412014567609621210965210585\r\n321101518789336798703123455418760145670195696\r\n234965400689221209654328954303210234987784787\r\n105874321678100118765017763214301001056653610\r\n876701234549010329870126894505212892347542123\r\n965891098238321234561235214676703083498230036\r\n874782347107630145490344305987894176580121545\r\n923690456016549854987432234589765237891234694\r\n012541219897896768876501105676894396790346787\r\n001432108766541089004321212988787781687656698\r\n123498789655432376113410145679695670521044567\r\n876567610141045645223567034034544321430123698\r\n965458901232038764344038123129632487654310432\r\n012307676540129854965129876038701898943216581\r\n103212789432100123876434565345612781012107890";
    }
}
