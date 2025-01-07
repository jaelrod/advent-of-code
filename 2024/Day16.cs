using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day16
    {
        public static int Main(string[] args)
        {
            string input = Day16.Example;
            Maze maze;
            string errorMessage;

            if (!Maze.TryParse(input, out maze, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            Console.WriteLine($"shortest path: {maze.ShortestPath(true)}");
            return 0;
        }

        private class Maze
        {
            public class Space
            {
                public enum Occupied
                {
                    Free,
                    Wall,
                    None
                }

                public Occupied occupied;
                public int cost;

                public Space(Occupied occupied, int cost = 0)
                {
                    this.occupied = occupied;
                    this.cost = cost;
                }
            }

            Space[][] spaces;
            int[] start;
            int[] end;

            private Maze(Space[][] spaces, int[] start, int[] end)
            {
                this.spaces = spaces;
                this.start = start;
                this.end = end;
            }

            public int ShortestPath(bool debug = false)
            {
                ShortestPath(1, this.end[0], this.end[1] + 1, Direction.Right);
                ShortestPath(1, this.end[0] - 1, this.end[1], Direction.Up);
                ShortestPath(1, this.end[0], this.end[1] - 1, Direction.Left);
                ShortestPath(1, this.end[0] + 1, this.end[1], Direction.Down);

                if (debug)
                {
                    this.Print();
                }

                return this.spaces[this.start[0]][this.start[1]].cost;
            }

            private enum Direction
            {
                Right,
                Up,
                Left,
                Down,
                None
            }

            private void ShortestPath(int newCost, int row, int col, Direction direction)
            {
                // ...SO..?
                if (row < 0 || col < 0 || row >= this.spaces.Length || col >= this.spaces[0].Length
                 || ((row == this.end[0] && col == this.end[1]) || this.spaces[row][col].cost <= newCost))
                {
                    return;
                }

                this.spaces[row][col].cost = newCost;

                ShortestPath(this.spaces[row][col].cost +
                                direction == Direction.Right ? 1
                                    : direction == Direction.Left ? 2001 : 1001,
                             row, col + 1, Direction.Right);

                ShortestPath(this.spaces[row][col].cost +
                                direction == Direction.Up ? 1
                                    : direction == Direction.Down ? 2001 : 1001,
                             row - 1, col, Direction.Up);

                ShortestPath(this.spaces[row][col].cost +
                                direction == Direction.Left ? 1
                                    : direction == Direction.Right ? 2001 : 1001,
                             row, col - 1, Direction.Left);

                ShortestPath(this.spaces[row][col].cost +
                                direction == Direction.Down ? 1
                                    : direction == Direction.Up ? 2001 : 1001,
                             row + 1, col, Direction.Down);
            }

            public void Print()
            {
                var sb = new StringBuilder(this.spaces.Length * this.spaces.Length);
                for (int row = 0; row < this.spaces.Length; row++)
                {
                    for (int col = 0;  col < this.spaces[row].Length; col++)
                    {
                        sb.Append(this.spaces[row][col].occupied == Space.Occupied.Free ? this.spaces[row][col].cost.ToString() + " " : "# ");
                    }
                    sb.Append(Environment.NewLine);
                }
                Console.WriteLine(sb.ToString());
            }

            public static bool TryParse(string input, out Maze maze, out string errorMessage)
            {
                maze = null;
                errorMessage = null;

                var lines = input.Split(Environment.NewLine);
                if (lines.Length == 0)
                {
                    return false;
                }

                Space[][] spaces = new Space[lines.Length][];
                int[] start = new int[2];
                int[] end = new int[2];

                for (int row = 0; row < lines.Length; ++row)
                {
                    spaces[row] = new Space[lines[row].Length];
                    for (int col = 0; col < lines[row].Length; ++col)
                    {
                        switch (lines[row][col])
                        {
                            case '#':
                                spaces[row][col] = new Space(Space.Occupied.Wall, int.MaxValue);
                                break;
                            case '.':
                                spaces[row][col] = new Space(Space.Occupied.Free, int.MaxValue);
                                break;
                            case 'S':
                                spaces[row][col] = new Space(Space.Occupied.Free, int.MaxValue);
                                start[0] = row;
                                start[1] = col;
                                break;
                            case 'E':
                                spaces[row][col] = new Space(Space.Occupied.Free, int.MaxValue);
                                end[0] = row;
                                end[1] = col;
                                break;
                            default:
                                return false;
                        }
                    }
                }
                spaces[end[0]][end[1]].cost = 0;

                maze = new Maze(spaces, start, end);
                return true;
            }
        }

        private const string Example = "###############\r\n#.......#....E#\r\n#.#.###.#.###.#\r\n#.....#.#...#.#\r\n#.###.#####.#.#\r\n#.#.#.......#.#\r\n#.#.#####.###.#\r\n#...........#.#\r\n###.#.#####.#.#\r\n#...#.....#.#.#\r\n#.#.#.###.#.#.#\r\n#.....#...#.#.#\r\n#.###.#.#.#.#.#\r\n#S..#.....#...#\r\n###############";
        private const string Input = "";
    }
}
