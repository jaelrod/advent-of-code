using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day8
    {
        public static int Main8(string[] args)
        {
            string input = Day8.Input;
            bool[][] map;
            IDictionary<char, IList<FrequencyCell>> antennae;
            string errorMessage;

            if (!Day8.TryParse(input, out map, out antennae, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            bool useAntinodeLines = true;
            var antinodes = new HashSet<Cell>(); // note: frequency agnostic
            foreach (var kv in antennae)
            {
                for (int i = 0; i < kv.Value.Count; ++i)
                {
                    for (int j = i+1; j < kv.Value.Count; ++j)
                    {
                        // add the antinodes if they are within the map
                        var antinodeCells = useAntinodeLines ? kv.Value[i].AntinodeCellLine(kv.Value[j], map.Length, map[0].Length)
                                                             : kv.Value[i].AntinodeCellPair(kv.Value[j]);
                        foreach (var antinode in antinodeCells)
                        {
                            if (antinode.row >= 0 && antinode.row < map.Length
                             && antinode.col >= 0 && antinode.col < map[antinode.row].Length)
                            {
                                antinodes.Add(antinode);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"antinodes: {antinodes.Count}");
            Console.ReadKey();
            return 0;
        }

        private static bool TryParse(string input, out bool[][] map, out IDictionary<char, IList<FrequencyCell>> antennae, out string errorMessage)
        {
            antennae = null;
            errorMessage = null;
            map = null;

            var antennaeTemp = new Dictionary<char, IList<FrequencyCell>>();
            var lines = input.Split("\r\n");
            var mapTemp = new bool[lines.Length][];
            for (int row = 0; row < lines.Length; ++row)
            {
                mapTemp[row] = new bool[lines[row].Length];
                for (int col = 0; col < lines[row].Length; ++col)
                {
                    if (char.IsLetterOrDigit(lines[row][col])) // antennae represented only by letter/digit
                    {
                        IList<FrequencyCell> antennaeFreq;
                        if (!antennaeTemp.TryGetValue(lines[row][col], out antennaeFreq))
                        {
                            antennaeFreq = new List<FrequencyCell>();
                            antennaeTemp[lines[row][col]] = antennaeFreq;
                        }
                        antennaeFreq.Add(new FrequencyCell(lines[row][col], row, col));
                    }
                }
            }

            map = mapTemp;
            antennae = antennaeTemp;
            return true;
        }

        private class FrequencyCell : Cell
        {
            public char frequency;

            public FrequencyCell(char frequency, int row, int col)
                : base(row, col)
            {
                this.frequency = frequency;
            }
            
            // override for use with hash table/set
            public override int GetHashCode()
            {
                return this.frequency ^ base.GetHashCode();
            }

            // override for use with hash table/set
            public override bool Equals(object? obj)
            {
                FrequencyCell cell = obj as FrequencyCell;
                return this.Equals(cell);
            }

            public bool Equals(FrequencyCell other)
            {
                return other != null
                    && other.frequency == this.frequency
                    && base.Equals(other);
            }
        }

        private class Cell
        {
            public int row;
            public int col;

            public Cell(int row, int col)
            {
                this.row = row;
                this.col = col;
            }

            public Cell Clone()
            {
                return new Cell(this.row, this.col);
            }

            public IList<Cell> AntinodeCellPair(Cell other)
            {
                if (other == null)
                {
                    throw new InvalidOperationException("'other' Cell cannot be null.");
                }

                // |a> - |b> = vector from |b> to |a>
                int rowDiff = other.row - this.row;
                int colDiff = other.col - this.col;

                // adding |a> - |b> to the destination yields one antinode
                // subtracting |a> - |b> from the source yields the other
                return new List<Cell>() { new Cell(other.row + rowDiff, other.col + colDiff),
                                          new Cell(this.row - rowDiff, this.col - colDiff) };
            }

            public IList<Cell> AntinodeCellLine(Cell other, int rows, int cols)
            {
                if (other == null)
                {
                    throw new InvalidOperationException("'other' Cell cannot be null.");
                }

                // |a> - |b> = vector from |b> to |a>
                int rowDiff = other.row - this.row;
                int colDiff = other.col - this.col;

                // starting from either antinode
                var antinodes = new List<Cell>() { this.Clone() };

                // iterate in one direction to the edge of the map
                int row = this.row;
                int col = this.col;
                while (row >= 0 && row < rows && col >= 0 && col < cols)
                {
                    row -= rowDiff;
                    col -= colDiff;
                    antinodes.Add(new Cell(row, col));
                }

                // iterate in the other direction to the edge of the map
                row = this.row;
                col = this.col;
                while (row >= 0 && row < rows && col >= 0 && col < cols)
                {
                    row += rowDiff;
                    col += colDiff;
                    antinodes.Add(new Cell(row, col));
                }

                return antinodes;
            }


            // override for use with hash table/set
            // a clever/convenient way to deduplicate different frequency antinodes at the same location
            public override int GetHashCode()
            {
                // might break down for maps > 10k side length (could parametrize by map size)
                return this.row ^ (10000 * this.col);
            }

            // override for use with hash table/set
            public override bool Equals(object? obj)
            {
                Cell cell = obj as Cell;
                return this.Equals(cell);
            }

            public bool Equals(Cell other)
            {
                return other != null
                    && this.row == other.row
                    && this.col == other.col;
            }
        }

        private const string Example = "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............";
        private const string Input = "...O.....0...............................p..k.....\r\nO.........o....w..T.........................p.....\r\n..................w..........oM...................\r\n.............................................Y....\r\no.............T...........................z.....pk\r\n.....................................z..Y....t.F..\r\n...........T..........................F.......Y...\r\n...................A............z...k..M..........\r\n....O.........j....w.....................M........\r\n..........w....T..................M..k............\r\n.............5.............................F.....t\r\n......................A.............F....E........\r\n.....................S.........A..................\r\n.P................................................\r\n........................A...E.............x...t...\r\n............j.........................t.........x.\r\n.......................j.........................x\r\n....................................E........c....\r\n.............P.......E............................\r\n...............j..5...............q...............\r\n..............P..............................Qc...\r\n..........C..........s................c........x..\r\n.............C...r................................\r\n.....C......V..........q...................Q......\r\n...........yX.........q...................Q.......\r\n.....X....................e.............m.........\r\n.2.................e..7....m.......c..............\r\n......i..........e...K..............f....U...WQ...\r\n...X....................e....................V...Y\r\n...............5..X.....0.........................\r\n..C..i......5..3...sK......J.........f..B.........\r\n2............3.............0I...a.........BNb.....\r\n.........................K............m...........\r\n.r........3...............s....7...m.v...f.......b\r\n........3........7.....Iy..........q...b.N........\r\n.....R.1.......................n.....a.B.......VN.\r\n......R.........9...................a...W.........\r\n..........7.6................S....................\r\n..............r.......s...0........nb....W..f..B..\r\n...2...........I......K...........................\r\n..............................u...n............U..\r\n............r......y.............O............W...\r\n.......R..........v..u................N...V.......\r\n..........R.8..4.9..y........u....................\r\n...8...............v................J.............\r\n.....8..............4.........Z.........n.....J.U.\r\n...........4i....................Z..S.............\r\n..............9...........1.u.S................J..\r\n8...6....9..4......a........Z.1...................\r\n....................v..i.............Z............";
    }
}
