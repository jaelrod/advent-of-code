using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day15
    {
        public static int Main15(string[] args)
        {
            string input = Day15.Input;
            Warehouse warehouse;
            string errorMessage;

            if (!Warehouse.TryParse(input, out warehouse, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            warehouse.Simulate(false);
            Console.WriteLine($"sum GPS: {warehouse.SumGPS()}");
            
            return 0;
        }

        private const string Example = "##########\r\n#..O..O.O#\r\n#......O.#\r\n#.OO..O.O#\r\n#..O@..O.#\r\n#O#..O...#\r\n#O..O..O.#\r\n#.OO.O.OO#\r\n#....O...#\r\n##########\r\n\r\n<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^\r\nvvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v\r\n><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<\r\n<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^\r\n^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><\r\n^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^\r\n>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^\r\n<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>\r\n^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>\r\nv^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^";
        private const string ExampleSmall = "########\r\n#..O.O.#\r\n##@.O..#\r\n#...O..#\r\n#.#.O..#\r\n#...O..#\r\n#......#\r\n########\r\n\r\n<^^>>>vv<v>>v<<";
        private const string Input = "##################################################\r\n#..O...OOO..O..O..O.O.OOOO.#OO...O.O.......OO....#\r\n#O...OO#..#.#O...OOOO...O#O..O#.O..OO...O.O......#\r\n#..OO......OO....#O...OO...O...#O........O......O#\r\n##..#..OOO..O.....OO....##......OO..O.....O.O..O.#\r\n#..O#.O.O..O..O.#OOOO...OO......#OO..OO..#.OOO...#\r\n#O.....O.OOO....O....O#.....O.#....#........O.O..#\r\n#...O....O.OO..O#..#O.#..O...........O#.....#O..O#\r\n#....O....OO.O.##.......#....O.O.O.O.O..#O...O...#\r\n#O......#..OO.....O...OO.OOO.#.O#.#..OO.....#.O.O#\r\n#O....O.#O..O..O...O#.OO..O.........OOO..O...OOO.#\r\n#OO..O....#OOO...#.O..#.....O#O#..#.......O..OO.##\r\n#..O.O.......#O...#.#O..O#.O.O.OO...O......O.....#\r\n#....O...O##.....O..OO#..#O..O.....O...O.#.#O....#\r\n#...#.OO#.O.........O.....O..O.O#O#.....OO.O....##\r\n#..#...OO.O#OOO..O.#..........O.......O..##O...O##\r\n#.O..O.OO#.....OO.O.O....O.#.....O#OO..OOO.##....#\r\n#O...OO#O......O...#.O....O...O.#......OO..O.O...#\r\n#.#..O.......O..OO....OOOO...O#...O....#.O.O.....#\r\n#...OO.OO..OO....OO...........#.O#...O#.O.O#.OO.##\r\n#O....O..#O.....O#.......#.O....O.....O....O.O..O#\r\n##..O..O.O..#..O#......O......O..O#...#.O..O...#O#\r\n#OO..OO.O..OO....OO.O#.OO.O.O#OO.#.OOOO...#.OO#..#\r\n#.#O.O..OO.O....O...O...O.......#O..#O#.........##\r\n#O.#O..#.O....#.#O.O.O..@..O.......O#.O...#..O...#\r\n#O...O..#.#..O.OO....O.....O.O#....O..O.O#.OO..O.#\r\n#....O.....O#..O....#......O.O.#..OO#OOO.O#.....O#\r\n#O.#.O..#O#.....OO.O..O......#O...O...OO.....O...#\r\n##O.O.O....OO.OOOOO..O..#.....O#OO.......OO....#.#\r\n#.OO..O#.OO.......OOO#.O..O.OO.O..OO...O...O..O#.#\r\n#O.#O..#O..O...O...O..O..O..O.....OO..O..O.....OO#\r\n#O......O...O.OO..O.#..O..OO..OO...OOO.O.......O.#\r\n#.OO....#....OO........O..O...OOO...O.O...O#..O..#\r\n#..O....#..OO.O.OO..O#OO...#..O...#....O.....O..O#\r\n#O...O#O.O..OO.#O.OOO.#.O..O#.O.......OO.......O##\r\n#..##...O..O...#O.OOO.OO.#....O..O.O.O...##O...O.#\r\n###......#.OO#.....O.O......O#.O...OO......O..O#.#\r\n#.O.......O#OO#.O.O..#.....OOO....#.....OOO.OO..O#\r\n#.O.#....O....#...O.....O....#....O............O.#\r\n#OO.O..OO..OO...O.O.....O.##O..............O....O#\r\n#....O..OO.O..O.......OOO..O..#..#OOO.O.O....OO..#\r\n#O..O#..O#.#...O......O..O..O.O.#....O....OO#..OO#\r\n#...OO........#.........O........O..O.....O......#\r\n#O..OOO..O...O.O.OO#O#..O##..#OOO.O.OO..O..O...O.#\r\n#.....#..#.O.....O....O#OO....O..O......#........#\r\n#.......O.O....O.OO.#..O#....#.O..O..OO..OO#O...##\r\n#..OO..O..O..O..#OO.OOO.....#.O.O..O.OOO..#O.....#\r\n#O.OOO.#..OO...O......O.#.O...#...O..O......OO...#\r\n#..OOOO.O#..........#.OOO......#....OO..OO..O.O..#\r\n##################################################\r\n\r\nv<v^^><^<vv>^<v^>^<^><>^>>^vvvv>^<^><^vv^v><<>><v>^>v<<^>^^^^^>v<v<^v^v<v>>vv<^>v^^v><<<^<><^>^<^<^^^<^^^v><<>v<><<<v<v<<v>>>^v<<<>^<>^v<<^v<^>v<<>><<v^^^^^^v>>vv^>>^^>^v^v^<<>^v<v<^>><^<v^^^v^<^><>v^<^>vv>v><^<><>^^>>^^<>>^v>v<<^v^^>v^<<^v^^><^^>v>v<><>>vv<^^v>><v^vvv^v^^<>>v<^v^v<>v<vvv>^<v<^<v<>^^<<<<v<<<v^^><^<^^<vv^v^v>><^<>^^><<><>><><v<>^v^<^>v<<>><<<v>^>vv><<<<v^^>^vv>v<>>^><v^^^>vv<v^>v>>v^^^^>>^<v>v<<^><^<v>v^>>>^v>^<v>>^<vv<>>v>v>v^<v^v<^vv<^><<^>vv><^<>>^>>v^>^v<<><<v<>^<<^^>v^^<v<^^^^^^>^<^v>v><vv^^^>vv<^vv<>vv><^v>^v<>v^>^<>>>>><^>^<vv<<><<v^>>v><^<^<<<>><>vv^>>vv>^^<vv<><v>vvv^>^v><v^^>v><^>vv<v^v>v><v>>v<vv<v<^vv^vv>>^>>^v>^>^v<vv>^v^<><v<>v<<<<^v>>>v>>v<^v>^<^>^<^<vv<<^v^>^^>><v<<^<v>v<>^>v>^^vv<<v>^>^^<^>v^>>v>>><<vvvv^vv^><^^v^^v<<v>><^^>v>^v<v>v><>vv^><>><v><^v<v><vv>^<<>>><<>^^<^v<<^^>v^<^^><^v^^>>v>^^>>^<v><^>><<^^v<><<<v<>vv^^<vv><^>>>^<vv^><<<<<^<vv<><v^>>^>>>v>>>^^<<>^v>^^<^<<><vv^>v^>v<>>><<vv^<vvv<<<<^<^>^^^^vvv<^>v^<^>>v^<^^^<<vv>^<<^^v>^^<<^><vv><^v<>>v><^^\r\n>>vv<vvv<^<>><<^vv<^><vv^>><>>^^<^>vvvv^^v>>^><^vv><v^^>^v<^v^>>v^^^<<<<<v^^<>><><>^>v>><^>v>v^<<<^>^<^<v><><v>^v^v<>vv<^<<>vv><^^<><<^^^^<vv>^^><^>v^<v^v^^v<<>><>v^<>v>>^^v^v^^vvvvv<>^>v^>>>>>v<<v<>>>^><^<<^^^v<>v<^<>>vv<>^<v<^>vv^^<<<<<>v<^v>><<^v^><<v<^>^v^<>><^<><<<v<^^><v<^^<>v^<>^><><v<^<v>>v<v^<<^<v<^v<>><<^><^<><<vv>^<^vv><<^><^>^^vvv<^v^^<>v><<v<v<v^>v^>^>^vv^v>v<<>><<<v^>>v<^>^^^<v><v<<<<v<>>v^<>>v<vv>v^v>^>^v>>^^vv<v^^>><<v><<><>vv>vv^<>v^v^^v><>>vvv^v<vv<>^>v<^<v><>^v>><<^^^<^^><v^^<vv^vv^<^><vv<^><^^>>><^>^>vvv><<<><vv>v<<^>><><<<v<<^v>><<<>^^v<<>>^>><^^v>>vv<^^^v<vv^^<^<<>^v>>^v<v><<^v^^v>><>vvv>>v^<v<<<<v^>^v^<^<^v^vv^<^v^^^>^v><v^v^v<>^<^<>^^^>^^<>v<<>^<><>>>>><><<>vvv>v>>v><>>>>v^^v^<^><><<>^v^^v><<^^v><<v<v>>>^vvv^>vvvvv<v^^>>>v^>><^>v<^>^^v^<<>v<<<>vvv^<v<v>^v^vv^^<>^<vv<>^><<><v<<<v^><<v^<<vv><v>><^<<^<vv^<<<^^^<^v^vv><<<<><>v^vv<<<^vv^>^<<<v<^^<^v<>><><^^^v>>^vv^<>vv^^<^vvv^<>>><<^<>v<^>^^<^>v<^<vv>>>^^<^<v<^^>>^^>>v>>v<v<>>v<<>>>>^v^vvv>><^>>^v><^^>^<>^>^^vv>^<<>>\r\n<<v<v>v^v>v>^^<>v<><^>^v<^>v^><>^>^vvv>^<^v>vv^>vv^^^<>>>v^<>^^<v<^>v>^<>^<>^><vv><<<v<^^^v>vvvv^^v<v<><>v>vv<<v^^vv>^v^v<v><<^^^^<v<^v<vvv>v<vvv>v>v><<vvvv^<<>v><^>v>^^^><v>>^v^v>v^^>v>^^v>v<<^<v>><<v^>><<<>>vv><<^><<v>vv>>^<<>^^<^v><v<v<>v>^v>>^<>>vv<vv<<^<^v^<v<>>^v^^>^>v>><v^^v<><<>v>><><^vv^^^v>v>v<>v<v<<v<<<^<<<^>v^<<vvv<v<v><^><v>^>v><<^>>v>>vvvv<v>v>^^><^><^vv^<><vv<<<>v<v<^<^>v^<^>>vv><^>v<>v<v><>v<^^<^^<<vv<>v>^v>^^>^<<^vv^>v<>vvv<<<<>>>>v>>v<>>><vvv>v<vv<>v<<>vv>>^vv^^<v>>v>v^<^><^^><^<<>^>v<vvvv<^^v^vv><^<^>vv^<>vv<v^^v><<>v<^^v>vv>^<vv>>^<>>v<v^^<><>^<v<vv><>v<<v>v><>v^<>>v^v<>>>^>>>v>^<^v<^<^^vvvvv>^<v<<v>>>><<^v^^^<v^<^<<v<>v<<<^<^^v<<v>>vv<>>><><><<<>^<vv>>v><<><<>^^v><v>>vvv<^>v>^v<>>>^>>>^^^>v>><^^>v^vv^^v>>>^<>>vvv>v<v<vv^vv<^v^>>>^v>v>^vvv>^v>><<<v><^>^<^><vv<<>v>>>>^v<vvv>>^<>v>^^^><>>v^>>v>^^>v>^^>^^<v<>>><vvvvv<vvv^<<<v<<<>^^v<^><>^>v<^^>>v>>>>>^^<<^<><>^^>>>v><^>>^>^^^^><v^^><<<^<^vv>v<>^<<v^<^v^<<<<>><>>><^<><><v^v^<vv<>v<^>^>>>^<v><^v^^<<><>>v<<^v<<^v^^<<vvv^^\r\n>^<<^>><>>^v>^<<^^vv<<>^^v^<v<^^<>><>^<<^^v^<v>><><v<v^<>v<^>^v>^v^v^><<>>^v>><>>^<<>vvv^<v>^v>v<vv<><<>^^^>>><<>^><vvvv^<<<v><<^v^^^>>vv^<^<v>v<<<><v>><>v^^^<^^^>^>>>v><v<v<<><><>vv^v^<^<<<^>vv^vv>vv<>>>^vvv<><>^><>^^vvvv<^v<<<v>><>^<<v<^<v<^><>^v^^>^^<<<>v<><vvvv>v>v<^vv>vv^^<^^>vv>>^<<<v^<<<<^>^><^>>^>>^^<vv^^v<^>><^^^v>^><^>^^<v^^^>>^^v>>>^<>>^v><vv^>v<>^<<^<^^v>><>>^^^>>v>><<^vvv<<^^v^>^vvvvv^v^vv><^^<v<vv>v>><^^v<>^v>>>^v<><v<<>^<v^>>vv>v^>><^<^<>v^<v^vv^^^^^>^><^>^<><^<v<><v>>>>v^>v<vv^<^vv^^v><><><vv^vvv^>v<v>^^<<^><<>vvv>^vvvv^<>^>><^>v<v<^v<v^^><>v^v^<v><<^<v><v^vvv>^vv>vvvv^^^vvv>>v>>^v>v^<<v^^<<<^>v<v>v>v<vv^><<v>^vv>>v<<^<>^>^v<<<^<>v<^<<^^>>><vv<^<^^v><vv>><vvv>v<^v>><^^v<v^<^<^v^v>^><vv>^^vvv<<v><<>v<v<vv>^<^><><<>v^^v^^^^^>vv^^v<^><^vv<<<v<>><^<^>vvv<<v<^^^^<^v^<><vv^>v<vv<>^^v^>^<v^^>v^>^^><>^><>^<v>^v><<<>^^<v^>^^<v^^>^><>^^<^<vv>vv<<<v>>^>v>vv^v><>vv^vv>^^^><<vv>^v>v^v^vv<<^^<^<^^^v^<v><<<v<<^v<vv<<>v^^<^<<v>v<^^<<vv^><<<v<v<vv<vv^^^>^v^>v^<<^^><^>v<<^<^^>><<>><<><><\r\n>v<^^>>v<><vvv^<><><v>vv^<>^^^<v<>>><>><<<>v^^^<<^v>>><v>^>v^<>^vvvvv^>v^<<<<>^>v><^>^>>><v>><v>^^vv^<v^^^<<<>>>>^><>>>>vvv>^v<<<>v^<^v^<^<>^v><>^>v^^<><^v<^<<<>^v>><vv><><<>^<vv^>^>>v^vvv^<<<vv<vvv<<v>vvv<v^<>v>>^^<<vv>v<v<^^>^^>^>>>>vv<v>v^>>^<>v<>v^>v^>v<^<<^<<<^<>><v<^vv><<v><>vv<<<<v<>^^>v<^>v<>^><>^<vvv>><<><>>^vv<>>^<<<v^v^^v<<v<^<v<^^>v><<^<v^v^^v<^^><>>v<>><^^>>>v^^<>v><<v<>>v>>vv>^v>vv><>vv^^<<<v<vv>>><^<>^^<^>>>^<v<<v>><^^<<^<><v<v<v<><v>vv<<<^><<v^^>v<v><v^^vv^>v>^<^v>v>>><<^v><<>^v^^^>v>vv<v>^vv>vv<<v^<><<^>v>^v^<>v^^^<>^<<^<v^<>v<v<<v<v<<>^^vv>^>^>^v>>^>^>vv^vv<^v<<v>v^<><><^^v^>v^<^>^v^<v><<>v>><^<<^<^>^<<v<><<^^<v<v>v^<<v<<>^vv>>v^>><<^^v>^v^v<<^<^v>>^<^>v>^^v^v><^<^>^^v<<^v^<<^>v^>^<<v>><>v<>>>>><><><v^^v<v<<><^v<^<>>v<>v>^<v<^vv^>>><^v<v<>vv^v^<^<><<^>>^^<<<<>>v<v^^v>><^^^v>^v>v>><^<>>^v>>vv>^^>v><<v^<>^^<^^>^<v<<>><v^<>><v>v<^v<^v<<vv>><v<v<v^vv<<>^^>^<v>><v^^v^><><<^<^><^vv<<vvvv^>>^>vv^<^>><^<v>^><><^>v^>>^><^<^<v>^>^>>^v>v<>>vvv><v^<>^vv<<v<^^^^^^vv<<<^^v<^<^vv^v<\r\n>><v<^<v^><>v^<>v^<<v^>>v<^v><v^<v<^><<>^>v<>>><><^^^vvv^^^^v>>^^v>><v<v<^<>vv^><><^^v^^vv>>^v<><<v^v>v^<^^<^^^><vv<v>^vv<v<^^<^vvv><<<^>><^^<^>^>>>^v<^^><>^>v><><v^<>v><v<v<<^<<vv>>^v<^v>^>>>v>>^>^<<><<^>>>^<>v^^v><^^<^^v<<><v>>^<>^><<<>>>^>><<vv>^<^<>><v>^<>v^>>>v<vv<^<^vv^vv>>>^^>>^v^v^>><><<<>vvvv<v>>><^<><><>^>v^^<v^<<>>>>v><v>vv^<>v>^^v^><^vv>><v>>>v^^vv><<^vv<^><><vv<><<v<v>^vvv^<>><^>><<<><<^>>v>><vv>v^v<^<>^<^v^>>^><><v>^vv>^<v<vvv^v^><>>>^v<<>><v<>v>^v>>v><<<^v<v><<>>v<^>^><v>v^^v^<>^><^<>^v^^^v^><<^vv<vv>^<v<v><<<vv><>>>v<<v^<>>>vv><v<><vvv>><<^>>>v^v^<^>^^vv>>v<v><^<>^^<<<^>v><>v>v<v^<>>^>v><>^>><<>^vv^v>^^v>v^^v>v><>^>^<>vv^<v^<>><^v<v><<^><^<<<v^^^<v^<>v<^>^v>^<^<>^>v>v>^>vv^^v<<><v>>><^<vv^<>v<<>>v<><v<vv<<^<v><>v^^^<v^v^<<^>^<<<<<<^>>>>>^^><<^>>^v>^><>v^>^v^v>>vvvv>vvv><v<<<^vv^>^^^><<<v<<>vv^>^<<v^<v>^v<<<^>v<<v<v<<^^^<<<^>><v<vv><vv>><<<<v^<^v<^^<<v<<<^<v>v^^^<<<<>vv>^v>^<v<^^v^>^v^><>>v^^>v^^^<^^<v>>^>>^<^><vv^v^<><><>^v>>>^<^v>>^vvv><><v<<^v<^><<v><vvv<>><<vv>><^>^v\r\n>>^>^^^<v<>^v^^<<^^<v<<v>>^<vv<^<v^<^^v<^>^<vv>v<<v><v>vvv^>^v>>>>v<v^>v^<<>^<<>^v^^^><<vv>^^^<^v>^<>v<^><vv^>v><^^>>>^>v<<>^><>^^^>>vvv>^v^v>>>^<v>><^<<v^v>^v^>v>^v^<>^>>^>>>>>^<v^><<<>v<vv<<<^v>vv^^>^>>v><^<v<v<vvv<<<v>^<^><><vv><^^^^>v<v>>>v<<><^^>v^v<<v^^^v>><<>v^v<^^vv^>^v>^<^>^>v<<>v<v<^^<<<<^^>>v<>>v<><^^^v^<<><<^v^<v<<^>^>>^v^<^<>>^<<^>v<>>>^<vv^^vv<>><^^<v<v>^><vvv^^<>><>vvvv^>^v^><<<vv^^v^v<v<v<<^<>vv>^>>><>>vv^>^>>^^<v^<v^^<v<>^><<^>v<>^v><<v<v><^<^^<v>v>vv>^^^^vvv>^v>>^^^^<v^>>^^<^>vv^>^<^^^<^^<^>><>^<>^v<<v>vvv^v^<v^<<^v^<v>>v^><>><v<<^>^^^>vv^<<^<^<v^^<<<^<<><>^><>v^^><>^^^><<<vvvvv^^^<v<^^<>vv>>>^vv><v<>>^v>>><<>v><<<<<>><^vv><>>v<>^<v><<vvv<>><<><v^^<<<^<>^>^<^<<^^>^><>v<>vv<v<<><>^^v^>v<^^<v^^v><^^vv<>><>v>>v<>>v^^>v><><>v^v<^>><<>>^<^<v<>^><^<><>>><<<<><>^v<>^^<><<v>^>v<^<^<v>>^v^vvv<^vv^^>^^v<>>^^<^>^v>^><v<v<>v^<<<vvv^<>>vv^>>^<^v<<>v>v>^^<^<^<v>^<v><>vv>>^<^<^^^>>^^v^^^^<^vv<^^>^<><>v<>vv^>^v<><>>>><v^<v^><<^^^v>^>^>>^>>v>^vv>v^>vv^v<v^v<^<^>^vvvv^<><v>><<v>><^^v>>\r\n^vv^<vv><^v<>^v><^v>><^<><^v^>v>^<^<>vv>>^<^^^v<<^^>v<vvvv^<<v>^<v^<v<v<^>>><v^>v><<vv^^<v>^<<<^>^v>v>>v>><>v^>v><>^>^>v<<<>^v><><><<>^>^v><>v<^<v<^v<>^<^v>v>^vv<v<>><<v<v>><vv>^<^<^^^<<>^<<<>v<<^^v>vv<^v^v>>^<v<^^vv^vv>>^^^v><>>^v<<^^>^>^>>^<<<v^^><<<^^v^vv<<>v^>>v>^vvvv<<><><>vvv^v><<^v^v^>^><vvv^<<^^<><^<<>^v^<v^v<<<>^^^^v^v<^^><<>^^>v><<^vv>>vv<v<v<<>v^^^<<v^^>^v>v>vvvv^v<v>v^^<>>^>^^>vv>^>>v>>><v^^>v<^<<><v<v^<^<v><><><^>>^^^>v<^v^><^v^>^><><>^v>^v^<><^>>vvv>v^v>><^^^vv<><>>>v>^<v<<^^v>>v<^vv<vv^^<v>v<^<^>v>v><v^><^><<^v>v<<<>^^>>>^vv<^v^<<><<^<^<v^^<v><>^>vvv<v<vv>^<^^^^><<>>v^>^<vv<v><<^^v<^>v^^^v^^<>v<<<v<><v<<>^v<>><<v^<^<^^^<<<<<>v>v^<^^<^><>^vv<>><^^v<^^^<^<<>v><<v^<<>v<<<^^><^<^vvv><vv^v>^><v^^v<v<v<<<vv>><^^>^><^>v<v<>vvv<<v>><v^v<vv<^vv<^v^>v<^^vv^^<>^v>^^><^^<vvv<^v><^v<<vv^>^v<v^<v^>v><^v^<>>vv^^>^<>v^v>v><<^<>>>v<><<<<>v<<<<<v<>^v>v^<^>><^^<v><vvvv^>^^>vv><><<^<^>^<>><^<v^^^<>>^<^^<vvvvvvv>^^<v<v<^>>v>vvvv>^<vv>^<<v^v>v^>>v^>^^v<v^>^>^^vvvv>v<>><^v>v<>vv><^v<^>>^^<vv^<\r\nv<<v^v^>^<^<><>>^<v<^v^>^>^><v>v><^>^^>v><^<><<<<>^^v^^^v>^v^<^^vv^vvv>v^vv^>^<><vvv<<<vv<>^<v^v>^^^>><<><^>^>^^v>v^<>vv>v^>^v<<v<v^>>v^<>vv<<<<<^<>^vv>vvv><><>v^>^v<^vv><^vvv^^>>^^>v^^vvv^<^>>v<^>v<<v<vvv>>>>>v<><>v>>v^<^^>vv><<>^vv<^<>>>^>>^<<>><^>v<><>^>>>vv^<>><<>v>v^>>^vvv^v^<>><^^^v><<vv^^><v^v^><v<<^<<v<>^vv^<^><^v>v<><^vv>^^^<vv><vv^^><<v^vv>vv^<>^<v<v<vv>^<v^<<>vv<^v>^vv<>^vvv>>><<v<v>^v>>><<^>^<>^v>v<vv^^v<^^<>><^v^v<v<<^>>v<<<^v^>^>^><^vvv>>><^>>v^^^<<>><vv<><v<>>>v>^>vvv<<^>v^<^><<v<^v^>>>>v<^<vv>v<<^>^><><<vv>>>v^<>>v^^v<>v<v^v<v^<v<^^vv^>>v^>^<v<vv><v<<vv^>v^<<^^>v^v<^vv<v<vv>>^>vv^vv<>v^<<><<>vv<><^v<^v<^^>^<<^<>><v>v>v><<>^v^<<>><vvv>^v^v>^>>v^<^<vvvv^vv^v>v>>^><<<^^^vv>^<><^v>^v^v^<^>v<>^v^><v>><>><<<<<><>^^<>^<>^<v<v^<<<^v^>>>>>^^<v^^>><>^^^v>^<>^>^>>^<v<>^v<>^>v>^^>^^^^^vv<^<<^<<<^<^>>>^><^^v>^^^>>^>>v^v^v^^<vvv<>>>>v>vv>v>>v>^>^<>>^<><>>^>^vv>>>v^<>><<^^><v>v^>>><>v<v^<v<vv<^>^<><<^><v<v<vv^>>^><v^^><<<>><>>>v^>^<<<v<<v<vv>v>>>>>>^v^vvv^v^<vv>v>v>^v^v<>^^<v<v^vv>><<\r\nv>^^^v<v<>>^^<>^>^^<<^^vvv><<>vv^^^<vv^^><>vvv<^v><<>>^v^^>^v<^^v<>>^>v><<^>^^<<<v<<>^<v>>>v<>v<^v^^vvv^<^><v<>v<v<<v<vv^<<<^<>v<>>^>v^<v>v>^<>>v^^<vv><v>^^^<v>^^v^^>^^><<v^><v>^^^v^^>v^v>^<><^>^<<>>v<^<^>^<><<^v><<^><^v<v>^<v<^^^^^^><v^v<>v<>><^^^^<^><v<<^>>v<^>v<<<^<>vv<>v<<v<v^v^v^<<><v<<<vvv>>^<^>^>v^vv<^v<v<><^<v<>v<<^>v^v>v^^<v>>^<v^^<vv<<>><^><>^>>v><^v^<>v<^v<^>^>v><vvv>>>><>^>>vv^><>><<^>>vv^>><>v>>>^^<^^>vvvv>^^<<><vv>>^>^^><>>>v^>v><vv>v^<vv<<><>><v<vv<v<v>^^<><v^>v^^^v<<<^<<v^^<v><^^<vv>^vv>><^^<><<v^<>><<><>^^^vvv^v<^^^^<v^>^>^^<v>v>v<v>v>>v<<^>>^^^v<vv^v<^v^<^v^>^<<><v<>>>v^><^><>>v>^>^^vv^^<^<^v<^>^<<>>>^^>v>>^^^^^>v<<^^^<<^^^v>v^^>v>>v<^<^<v>>>>>>^>^^^v^v^^<v<<<<><<^^<^v>><^v<>^>^^v>>v^^<>vvvv>v>^<^vv><v>v>><v<v^<^^^><<>^v^<v><>^v<vv><>^><>^^v<<v>v><>^<v<^v^v<<<><vv><<<>^<<v>><>><v^>>vvv^<vv>>v^<^^<v<><vv>>v^>>^>>>v>^>>>>^<v^<<vv>^v^^>><vv>>v>>>v>v<>^vv^v><<<>>vvv<><^^<^^^^>>v<vv^><v^v^^^vv><>v<^^^^vv<^^<v>^^^><^>^^><<^><<^><v<v>^vvv<><<v^<<^^<>>^v<v<v^<>>^<^v>vv^^<>^>v\r\n<v<v<v^>^<>^v<><>v><<>v^<<><>^^<<v>><<^v<v^v<>>v<^<<v<^v<v^v>><><><<<v>^v^>^<v>^<<^v<vv^^^<>>v^^<vv>>^^^><<vvv>^^<<v>>><v^v>>>^v^<vv<^>^<^<<v<>v>v<<vv^>v<><>><<>v<><^^<^v^^^v><^^^<v>^>^v^^v>^<^><v^<>^>v<<v>><^vv><^<<<<vv^<^^^v>^v<^<<><^^<>^v>v<>>^^v>^>^^v<<^>vv<vv^<>vv^<<^<^>vvvvv^v<<v^^<^v<v>^<v^v>^>v^^>^>v^><v>^vv<vv^v^v>>v^><<<>^<<<^<<><>^v<v><vv^v^v^v^v<<v^>^<><^^><^><<>>><>vv>>>>^<<^>v<^v<<<v<>>^^>^>>vv<v<v<<v<v^^><>><>^>^<^<<v<v^vv<^<>vv^>^<>^vv^^^<<><v^>v<v^v^>>v<^>v<<<>^^>vv^<v^>^v<>^^>v^v<^><v^vv^>^v>vv^>^v^^>v^<^<^<v<<v^<<^^^v>><>>^^^^<^^>^>><v<<<><<^^<^<vv<^<><<v<<<><<v^^><<>vvv>^><<<vv>^>v>v>vvv^^v>>^v><>>v<><<>>>v^><^^<v<vv^<<<v^^>><^>v><<><<v<^v<<>^^><vv<^<vv>><>>><<><v^<^^<v<^vv>v>^>^>>^^<v<^v<vv>^<^>>^><>v>v<<<^><vv>^^<^^<>vvvv>>>>>^^<<<^>>vv^<vv<v^<<^>^^v^^>vv<<>vvv>v^>^v^<^^^>v>^<v<<^vv><v><v>><v>>>^v>^v^v>^<^>v><vv^^v<>>><<><>^>vv^vv<vvvv>vv<<vvvvvv>^>v><>v^vv<^v<v^^<v<v><^>>v^>>>vv^<v>v>v>>vv<v<><>>v^<v<<>>>>v^>^v>>>v^v<<<v>v><vv<>v<^>^<^>>v<v<>^v>v>^v<<<^^<<^^<<v^^\r\n<>v>v^^>v<<>>>^v^v<>^>^>v^v><v>^>vv>vv<<v<v<^^v><>^^^<<^v<^^><>v>><v><^^vv><^v>>vv>vv^^<><vv^><>vv<v>>vv>vv<^^>v>v^v<>^^<<^<>>>v>^^<^>v>v^^v>vv<>^^v<<<>^<v<v^^<<<>^>^<>^>v<><v>>>>vv><v><<v<<><>><<<^>v^^^vv<v<<<<v>v>^<><>v>>^<<<>>^<vv^>v^v<v^>>>v>^>v>>^v<<><<vv<v^>v^^vv^>>v>v>v^v<v>v^>v>v><^<<vv><>>^<>^v<v>vvv>v>^v^>>^><vv^^^><>^<vv<^<>^v>><v>^v><><^vv<><^><<<v<^<v>v^<^v<>^vvv<><^v<v^><>v><>><<>^^v><^^<^>^^<>vv^^v<^^vv^<v<<^v<vv>><>v^<^v^<<v><v>><^v^^><^><v><>^^^<<^v<v>>>v<<v<<<vvv<>>^^>>^vv>><^^<<^^vvv^<v>>>vv^v><<>>^>^vv<>vv<>vv<>^^<><>>v<^<^^^>v<^^<>vv<^><v^><^<^^v^^>v<>>v>^v>>>vvvv>^v<<>v>^^<>vv><^v<^>^v<>v>^<^v>>^vv^v^>v>><v^><<>^^<>>^v^^^^>^^><v>^v>v^^vv>^v<><^v<<^v^^>>v>>vv<>^^>>>^^^^v>>^^^<v>^>v><vv><v<<v>^><^vv<>>vvv^^><^^<<v>^>^>v^^v<^>v><v^<>v>^vv<v>^v>v^^<>vv>><>v>v^>^^>v<>^>vv>>><^^v<vv>^^>vv><v^<<^<v^<^<v<vvvv^^^<^v><^<>>^^>^<v<v>^>>^^<<><^^v<v^v>vvv<vv>^v<v<<^<>>^vv>^>v^<<^<>><vv<>^^^><v<v><^>>><>^^><<^^^<vv<<<<v>>v>^><<><<<^vvv^^v<vv>^v>>^<<v>>^v^^>^><v>^<v<v^<>v<<<<>><^\r\n^v<^vv>>>>vv>v>><^<<vv><<^<>>>^vv^v^v>vv^<<<v>>>v><v><>^><<<^>v^>v<v<v<<v>^>vv^>v^^>>^^><>>><<^<>^^<>><><><><><v<v^<v>v^^^vvv>>^vv>^>>>vvvvv><<^v<<>v^v<>^><>>>vv<^>vv^<^v<><v>^^>v<v<<>vv^^<>vvv^>>^>><v>v<^>>>>>>><<v>^<vv<>^<>v^>^><>v<vv><<>><<vv^>>^<^v<>>^v>^>>^vv>>v<^>^^<v^^vvv<<<vv><><v^><><>v>>v><<<<>^>><^<>v>^^<vv<^<v<<>v>>^^<<<<>>v>^>v>v<^<^>><vv>v>v><>^<>^^>>^<v<<^>^>^><>vv^vv>^^<>>^<v<^^<<<v><^^<^>vvv>v^^^>vvv><^>v<^<v^<>>>^v^^<<v^<<^v^><<><<vv>^><<v>>^v><v^v<^>v<v^^^<>v<v>^><<v<vv><>>v<<<<><<v><<vv<v>>^^>>^vvvv^<^<<v^<v<v><<^>v>v>^^><^v^^<>v^^>>^vv><^^v<<<<vv><v>^^><v^>>^>^v<<^><<>>^^<^v<v><vv^<<><<v>vvv>^<<vvvv^>v<><v^v^vv^v<v>v<>>^v^<v<<>^v^^><><<^^^^^^<vv^<<<vv<<v><vv<^^<^^vv<<<>><^^>v^v>^><><<^vvv>^^>>v<^<<<^>v<v<>v>vvvv^>>>v>^vv>^<<<v<<^>^<^><vv><<<^>>^^>>^vv>>>v>^vvvv^><v<^vv<^^<v<<><^vv>><>>^^^vv^^v^<>v<>><<<v^^>v>^<v>^v^<<<><>>>v^>^^^vv^v<<v^>^^><>v>v<^>><v<>v>^>v>vv^>^^^^^vv<v>>>^^^<^<<>>^^v<^^><vv^v><v<v^>>v>v<vv^^>><v^><vvvv^<vv<v<>>v><vvv>v<^^>^>^v><^>^v<v><<v<<>><^\r\n>v^vv^v>^^<vv>^^v^<<v>^>><>^vv^>^<vv^><<<<^v^<<^><v<^vv^^v^<^vv><>vv<v>>vvvv^vv<^>vvvv^^^><>v><>^v><<<^<<^v>>vvv><^<<vv^<^^>v>><>v^^v>^><^>><>>>>><>v^<^v^>v^><v^>v<>vvv^<<v>>vvv>vv>v><<<>^^^<^>^^vv<>^<^v>^^v^v<><v<vv>>^<v>v^<^>^<><v<<<<>^vv>>v^>^v^<><v^<<^v^<>^v<^<v^^>v<<>><^^v>^>v>><v^<>>v><^v<^^vv^<^>>>^>v>>^>vv^><v<^v>^v<>><>^><<vv^<>vv>^<><^^v><><v>^>^<<v>>^^^>^v^v<^>>^vv^><<><<>>><^>>^>>>><^^^v^><<v^>^v>vv^<>><^^>v><<><v^<v^<v>>>v^>>>>><<vv>v<>v^^>v^v<^vv^^><vv^><v>^v^^><v<<>^>>><^^<^v^>v>><<^vv>vv>>^<vv<vv>^><<<vv<v<<^<^>^<vv^v^vv^vv<><^<<^^^v<^<v<>^^^v^^>v>^^>vvvv><^<^^^<v>vv<vv<^><^<>^v^<vv^<<v>>v>^<^^v^<^>v>><^^^<<>^<v>^><<>v><^^v><vv^>^^<vv>^<<v>v<^^>v<v^>>^^<^>v<<^v>^>^v>><v>^<>>>v^^v<v>>^vv>^><v>>^vv^>^>^>v^v><v>>v^v<^v^<^<v^><<<v<v^^v>>><><<^><<<^>>>vvv<<^<^>vv^^^^<<^v^><^>^v^>v^^>vv<<^<^^^v<v>^>>v^>^^><>>v><>>^<v<>^>>vvv<^<<<^vv>vv<v^<^v>><><<vvv^><<<^^<>^^<<^<^<^^^<>^<>^^v><^^vv<^<v<^^><<v^>>v^^v>^vv>^v<v<<><<^vvvvv>vv^v^>vv<^v<^<^<>^>^v^><v^<^<v<<^vv<>^>v^v>^v<^v^<>^<v>\r\n>><vv<v>^<v<^v><^<>v>^><^v><vv>>^<v><v><^^<^>v>vv<<<v^^>^^^>>v<v><^^>v^>><v<<^vvvv<^>>vvv^>>>^^><>v<<<>^v>><^<v>vvv>vvv<vvvv>vvvv^vv^^>v<^>>>v<<vv<^>v^<<v^vv><<<^<<^^<>^><>><>^vv<^v<vv<<><v>^v><^<<v<<v^<>v<^v^>v^<>^vvv<<v>^v^>>v^^>v>^<v>>^><v>^v<vv><>v^>>>v^vvvvv<>v<><>v<^>>v<<v^<><v^>^<v^<^v^^v^^^>v<v<>vv^^<>^<^v^>>>^vv^v><^>^v<^<>v>v>^<v<>^^v^>^>><<vv^^^>><><>^^<>^<<>v>v<v<^<^vvv><<><><^>v>><><^^>>v<v<<v><>>>>v><<><<^<v<v^^>>^><^^>^<<>>>v<vv^^>v^v^>>>vv>^^^^<v^^>^<v>^><v^<vv^>>v^v>^vv<vvv>>>><^^v><^v^<>^<<>^>^^>vv^^v<>^^<^^^>><v<<v<v>^v^v><>^<^>>v<vv<v>v>>^^^^v^v<<<>v<^<><>>v>>>>>v>>>>^<^vvvv>>v<>>><v^^^>^^v<v^v^vv<><>^<v<^<<<<^^<<v^<>v>v<>^<^v^v<v^<^vvv><^>v<v<><<^<v^>vv^<<v^<^<<^<<<v><v<>^^v<>><v>^^^^v^>vv>v^^^^<^^<<v<<>><vv<>>><<^v<>>v<^><^<>v<vvv>v>^<>vvv^vv^v<<<><v<<<>v>v><><v<^v<>^^<>>>><>>^<<<^<^<v<>v>>>><v>>^>>>^<v>^v^<>>^<v^^^><^vv>>v><<^<v^v^v>v>^v^^v^>><v>^<>>^>v^^<^vv>v^v><v<<v>^<<>v^>vv><<^<v<><v>>^>>v<^<^^^^^<>>>>>vv<v>>^>^<>^<v>v><^v<v^^^^^<^><<<v^^><<><^<<v>>^^v<^<vv^\r\n>><<^<<v<>^><^v>>>vv^<<><v>^<<<^>^><><v<^^^<^<>>>><vv<v^<v>^^vv>v^<<><vv^<^<vv>^>vv<^v^v>>v^<>v<<<<>>^<v>^<^<>^^<<>>>>^>><^>^<^^^>^^<vv>v^<><<^v^v^^^><^v>>>v>^^>^v>^>v>v<<<vv>><>v<^^v<>v>^<<<v^<>>>^^<<^^>>>^<^<^v^>><<v<<>^<^<v><<^<<v<<vvv<<>v<v<^<^>v^>vv^>vv<v^<<<v<^v^<v^v>v<v><v<^>>^^v><><^<v^>^^<<^^>^^^<>v>^>^<^<v>v>v<^^>vv<<><><>>>^^^>>^^^<><v><vv^<<>vv^v<v^>^v<^<vv^^>vvv><>^v^>^>>v<v^v<>>^^^<v^>^^<^<^v<<v^vv><>vv^<>>vvvvv<^^v^>^<vv^v>>><vvv>>>>v<^v>vv<v^v^<^><^<>v<>^v^^^^^<v>v^<>>>^v>^vv<<v<<v<v<vv<v>v^>v>^v>>v<>><>>v>v<<>>^v^vv<<vv^<<<>v^^>^<<v>^^^><v^^<^<vv<vvvv>v^^<<><<<>^<^>v>v^vv^><>>v>^>>>>>^^v><^<>v^<^v><^vv^<v<v<><<v><^<>vv<^^vv>>>>^vv<>v^v>^^<<<<v>v>>v><^>v^>>v^><^v^vv^vv<>^vv>v>>v>vv>^v<v^v^<^><<v^>vv^vv>>>v>v<v^<<>^vv<^^<^vv<>v^>^>>>vv><v^<v>>^v>>v<^v>>>>^<^v<<<>v^v^>^v<<vv^^^vvv>v<v><>^>^vvv^>^^>><^><><v><^^><<><<^<v>^vv>>^vv>>>>^^v<^<^^><<>vv^<^v<v<>^^<><^<<v^v^<<<>^vv^^^vv>>^^>^<<v>v>v^^v^v<>^v^^><><>v^^vv^v^v>>><<<^v>v^v><^^<>vvv>^v<^^v^^vv>^<^<v^^>^vvv^<>>^<>><<v><v\r\n>^^vvv^><<^>^^v<<^v<^v^>vvv><vvv>><v<<<v^v^^<v>><<v><<v>^>vvv>>>v<<^>^<>^^<^<^^^v>^<>>^vvv^^<>vvv^^^<<>>>v<^v<<>^^^vv<<><<>v<>v>^<>>v^>^^v^>v<<^v<v<>^v^vvv>vvv<^<^><v>v>><<^<>>^v>^vv>vv^>^v^><>v^<^>vvv^v>>>^<>v<>^^^^<vv>>^>>>><><<>>v<<<vv^^>>v>^>vv^>^<<<>>^v^><^<vvvv^v^^<v^v^^<v^><>v>v<>v>>^^vvv>^v^<v<v^^<<^<vv^v<>v<v<<vvvv>^><v>>>>^>^^^<<<<<>^<><v<^<<^^vv^>v><v><^^>v^>^>^^^<<<><<><>^>><<^<<^v>^><>^^^^^<^^<^^^vvv^v^^<v<vv^<^<^v^v><<<>vv^v<><<v<v<^^>>^>>^<^v^v<>v^>^v<><<v^>v<<v<v^v>>^<<<^><>^^^<<><^<^^^>v<><v>v<>^>><vvv><v><^<^v^^<>^^^>>^^><vvv^<^<<><^^^v<v<>^^v^<v>><v^<<vv^>^>v^^<>^v<v>^vv<><^<^v<v<>>v<v><><^v^<vvv^>>v^vv<<v>>>^v>^<vv><v<>>^<v>>><vv>><v^^>>>>^^<>^v>>v<v^^^v^v<<^^>>^^^<>>>v>v><v><vv>><>>><>>v<>^^>>>^v<^^^^v^v><v><<^><>v^^<><<>v>>vvvvv^v>>>v^<v<<<^<<^^>v^^v^^^<vv<>^^<>>vv>^^v><v><v>v<<>^vv^<v>^v<v<v>>v^<<^vv^>>>vv>>^<<vv<<><^><^^vv^>^<<v^v^^<>>^>^v<<v^v>>v><<<<><v^>>>^<>>>^>^>>>^>><vv>>^<v>>v<<<<>^^<>^>v>v>^^v>v>v>>^^<<<<><v^^^>^v<v>><^vv^>><v<v<v>vv^<v><<vv<v><^^<><<^v<\r\nv^<>v<<^><<<<>^v>^^<<><^>v<^>v<v>^v<<v>^^^vv<<<^v^^<^<v^v^v<><><<<^<v<^^<vv^^vvv<^v<>><>>^v>^v^^<>vvv^>^<>>v<^<<vv<^<v><v^>>^>v>vvvv<>>>vv><<^^>^<^^>v>><^^^^<v><>v<^^<<><<vv>v<vv<>vvv>^<<>>vv^>v>>vv^>>v<<^<<^^>><v^^<>>v<v<>v<<^<<^<vvv^^v><<>v^v<<v<<>vv^vvv>>^>^<<^><><<^<^v^>><vvv^<>vvv^^>^><^<<v^^>>^^<<>^^vv><>^vv><>v>>><v>>^<v<v<vv<^vv^^<v>v>>v<>>>>^>v><^^v<>>^>>v<^^^^>><^v^^v^v><<<^^>v<v>^>v^<><<vv<v<^^v>^>^><><>^v<^^^v>>^<^<v><^vvv>v^<^^^^^<v><><v^^<>><v^>^<v<^^>vvv<v<<<>>v<^>>>v<<>^vv>v^^vv<^<^v<>^v<<>v^v<^vv<>v>><<v>>^<>^<><v<<v^>v<v>><^>>v<>>^v<<^v<v<<^><^><<>>>>><v<^^><>><v>>v^^v<^<>^>>v><v<vv^vv><>v><v>>^v><>v<>^>v^<v^v<>>^>^vv><>^vvv^v^v>v^^v^<>^<>^^>v>>v^v^^><>^>^^<^>^>^v><v^<^>>vv>^<<<<^>^<^^>>>^>>^v<<>^<^<v^^vv^^^vv<<v>>v<vv<v<^<^v<<^<^^^<v<^v^<v><v^v>v<v<^>^^vv>^^<><<v>>^v<v<v><>>^^v<vv<>vvv<v^>v>vv<v><vvvv<vvvvv<v>v>^vv^>>v>^^^v>>><>^vv^^<v^v>>v<v^v<>v>^^v^^<>v^>>>><v><v^^<v^^>^^^>^>>^>^v^><vv>^v^^<^>>^vv^^v>v^><>vv<vv^>v><<<<^<<>>vvv^vvv<<<>><v>^<>>vv><>>>^v<>v<>^v>v<>>>\r\n^>^v<><>v>>^^^v<<<^^<v^<v>v^<<<<v<>v^^^vvv>v>^<><><^<<^>>v>><<>>^^<<<>^<^<<>^<^vv>><^vv>>vv>v^><<><^^><>>>^^v<^<v><vv<<^^^^^>^v>>^>>>>^>v^>v^vv^>>>v^>v<>^><v^^^^>^<>^<vv<><>>v<<><^^<><vvv<<>^^<v>^<<^^^vvv><<^v<<<>^^^^v>v^<<<^<^<<v>^^><vv^><<>^^>><<>vvv^<><>>><v>v<v<<>>v>vv<><^<>>><<^<v>^^v^v<<><v<<>v^^<v>^^<^>v^v^^^^>^^>^>v^>^v><^vv>^><>>>^<^<v<>^^^vv^>^<<^>v>>v^^>^v^>^<vv^>^><vv<<>v^<vvvvv><^>>v<<>>>^>vv<>vv^<vv<v^<v^vv<^<^^vv<v>^^v<^<v>vvv^^^<^<v>^v<<>v><v<<<><>><><<<v^><>>v<vv^<>^<<>^><v^<<<>v>v<>v<<^<^<<^v<^>^>>>^>^^<v<v^<<^<^>vvv^<v<>^<><>v><<<<>v^^v^v^vv^v<^^^>^v>^^v>vv<>^<v<<><><^><<<v<>v^^<v>><^vv^^>vv<<<^^vvv<><>>>^v^><<v^vv><>^<v><<>>vv>^<v<>^<v^^^^v>^>>vv<vv>vv>>v><<^v<>><<><v<<<<^<^<^^>v<>>v^^<<<>v>^v><<>>>v<^<^>v>^v<<v>>^>^>vv<>>^<^vv^v^>^><^>^^<^<v^v><^v<<v^^<^^>>>^<<v<<vv^>v<v^>^vv^<vv><<<>vvvvv><<^>vvv<>>>v<<v<^>>^>^>v^vv^^^><v^<v^v<<vv<^<><><>v><v<<^vv<v<^<><<<><>>vv^>^v>v<><<>>v^^v^>^<v<<<v^v>vv<^<<vv^v^<v^^^^<vv^<v>^^<^>>^<<v>^>>v<<v^^<<v<vv<<v^><^^^^^<^v<^v<<v^^v>^v\r\n>vv>vv>^^<v<vv^>><^>v<<>v<^^<^<^>v^<vv><>^><v^^^>><>^<^v>v^>vv>>^>><v^^^<^<^<^v<v><^^<>vv^<v<<<^<>v<v>v<<^v^^^>v><><^<^<<v<^v<vv>^vvvv<v<>^>>v<<^>>vv^vv>vv^<^<<<>^vv^<<>v^<^<^>>v>^<v>>>^><><^<v^>v^^>^>>v<<>^>><><<<<vvv^^vv<^<^>vv>><v<<>^vv<><<vv^<^<>^<<^v^v><<<>v>>^vv>>^><><>^^><^v<<>vv^>^v<^>^^^>><^v>v^v<>^^^^v><v<^^^^<v>>><^><<<v<vv^<vvvv^v>>v>v>^v^>v^^>^>>><>^<<^<^v^v<vv<<v>^>><^><<v<<<><^<<v<^<^<><<<v>v^>><vvv>v>v^<>v<vv<>v<<^vvv<^<<><>vvv<<^^v<>^<<^>><v^^v>>^>>v<>>^v^v>vvv^<<v<<<<>vv><><<v<>vv>^<v>^<vvvv^>^vv<><v<<>v^<><<^v<<vv^v>v^^^v^<<>v><>><<^>^^<v<<^vvv^^>^v<>>><<>><<>>>>>>>v>^v^>>^>>>>^vv<<v^^<v<<v^^>v<>^<^^>>v<<^v^v<<><v^^<v>v^>^<^vv>v><v<^<>v<<^v>v<>>^v>>v>^<vv>^^v>>^v>v>>>^<<vv<>>^<>^>^v^v<><^><>vv^<v^><>^^vvvv<<<><v^<<vvv^<>^<<^<^v>>><^>v^>><><<>^^^>vvvvv>v<<<vv<<>^vv^<<>^^<>>^>v^<v^^v^v<v^>><<v^^^v^v^^^v^vvvv<^^v^^<>vv>^><>>v<^^<v<^>>^^<<v>^>^^><v^<^^>^><^^>^vv>>>vv>vvv>>v<v>><^<<<vvv^><<>^>^><>^v<<vvv<^^v^<<<v>v>v><<^v><v>>^^<>>>vv>^^<v<^>v^^>>>vvv<>v<<<vv>>^v><^<<^<^v";

        private class Warehouse
        {
            private class Robot
            {
                public enum Move
                {
                    None,
                    Right,
                    Up,
                    Left,
                    Down
                }

                private int row;
                private int col;
                private Move[] moves;
                private int instruction;

                public Robot(int row, int col, Move[] moves)
                {
                    this.row = row;
                    this.col = col;
                    this.moves = moves;
                    this.instruction = 0;
                }

                public bool HasMove()
                {
                    return this.instruction < this.moves.Length;
                }

                public void MoveNext(ref Occupancy[][] grid, bool debug = false)
                {
                    if (!this.HasMove())
                    {
                        return;
                    }

                    Move move = this.moves[this.instruction++];
                    if (debug)
                    {
                        Console.WriteLine($"Robot executing instruction '{move.ToString()}'");
                    }

                    switch (move)
                    {
                        case Robot.Move.Right:
                            switch (grid[this.row][this.col + 1])
                            {
                                case Occupancy.Free:
                                    grid[this.row][this.col] = Occupancy.Free;
                                    ++this.col;
                                    grid[this.row][this.col] = Occupancy.Robot;
                                    return;
                                case Occupancy.Box:
                                    // look what is past the box(es)
                                    for (int c = this.col + 1; c < grid[this.row].Length; ++c)
                                    {
                                        if (grid[this.row][c] != Occupancy.Box)
                                        {
                                            // if there is a free space, move the boxes
                                            if (grid[this.row][c] == Occupancy.Free)
                                            {
                                                for (int c2 = c; c2 > this.col + 1; --c2)
                                                {
                                                    grid[this.row][c2] = grid[this.row][c2 - 1];
                                                }

                                                // move the robot
                                                grid[this.row][this.col] = Occupancy.Free;
                                                ++this.col;
                                                grid[this.row][this.col] = Occupancy.Robot;
                                            }
                                            return;
                                        }
                                    }
                                    return;
                                case Occupancy.Wall:
                                default:
                                    return;
                            }

                        case Robot.Move.Up:
                            switch (grid[this.row - 1][this.col])
                            {
                                case Occupancy.Free:
                                    grid[this.row][this.col] = Occupancy.Free;
                                    --this.row;
                                    grid[this.row][this.col] = Occupancy.Robot;
                                    return;
                                case Occupancy.Box:
                                    // look what is past the box(es)
                                    for (int r = this.row - 1; r > 0; --r)
                                    {
                                        if (grid[r][this.col] != Occupancy.Box)
                                        {
                                            // if there is a free space, move the boxes
                                            if (grid[r][this.col] == Occupancy.Free)
                                            {
                                                for (int r2 = r; r2 < this.row - 1; ++r2)
                                                {
                                                    grid[r2][this.col] = grid[r2 + 1][this.col];
                                                }

                                                // move the robot
                                                grid[this.row][this.col] = Occupancy.Free;
                                                --this.row;
                                                grid[this.row][this.col] = Occupancy.Robot;
                                            }
                                            return;
                                        }
                                    }
                                    return;
                                case Occupancy.Wall:
                                default:
                                    return;
                            }

                        case Robot.Move.Left:
                            switch (grid[this.row][this.col - 1])
                            {
                                case Occupancy.Free:
                                    grid[this.row][this.col] = Occupancy.Free;
                                    --this.col;
                                    grid[this.row][this.col] = Occupancy.Robot;
                                    return;
                                case Occupancy.Box:
                                    // look what is past the box(es)
                                    for (int c = this.col - 1; c > 0; --c)
                                    {
                                        if (grid[this.row][c] != Occupancy.Box)
                                        {
                                            // if there is a free space, move the boxes
                                            if (grid[this.row][c] == Occupancy.Free)
                                            {
                                                for (int c2 = c; c2 < this.col - 1; ++c2)
                                                {
                                                    grid[this.row][c2] = grid[this.row][c2 + 1];
                                                }

                                                // move the robot
                                                grid[this.row][this.col] = Occupancy.Free;
                                                --this.col;
                                                grid[this.row][this.col] = Occupancy.Robot;
                                            }
                                            return;
                                        }
                                    }
                                    return;
                                case Occupancy.Wall:
                                default:
                                    return;
                            }

                        case Robot.Move.Down:
                            switch (grid[this.row + 1][this.col])
                            {
                                case Occupancy.Free:
                                    grid[this.row][this.col] = Occupancy.Free;
                                    ++this.row;
                                    grid[this.row][this.col] = Occupancy.Robot;
                                    return;
                                case Occupancy.Box:
                                    // look what is past the box(es)
                                    for (int r = this.row + 1; r < grid.Length; ++r)
                                    {
                                        if (grid[r][this.col] != Occupancy.Box)
                                        {
                                            // if there is a free space, move the boxes
                                            if (grid[r][this.col] == Occupancy.Free)
                                            {
                                                for (int r2 = r; r2 > this.row + 1; --r2)
                                                {
                                                    grid[r2][this.col] = grid[r2 - 1][this.col];
                                                }

                                                // move the robot
                                                grid[this.row][this.col] = Occupancy.Free;
                                                ++this.row;
                                                grid[this.row][this.col] = Occupancy.Robot;
                                            }
                                            return;
                                        }
                                    }
                                    return;
                                case Occupancy.Wall:
                                default:
                                    return;
                            }
                    }
                }

                public static bool TryParse(int row, int col, string movesInput, out Robot robot, out string errorMessage)
                {
                    robot= null;
                    errorMessage = null;

                    var moves = new Move[movesInput.Length];
                    for (int i = 0; i < movesInput.Length; ++i)
                    {
                        switch (movesInput[i])
                        {
                            case '\r': continue;
                            case '\n': continue;
                            case '>':
                                moves[i] = Move.Right;
                                break;
                            case '^':
                                moves[i] = Move.Up;
                                break;
                            case '<':
                                moves[i] = Move.Left;
                                break;
                            case 'v':
                                moves[i] = Move.Down;
                                break;
                            default:
                                errorMessage = $"Invalid move control: '{movesInput[i]}'.";
                                return false;
                        }
                    }

                    robot = new Robot(row, col, moves);
                    return true;
                }
            }

            public enum Occupancy
            {
                None,
                Free,
                Wall,
                Box,
                Robot
            }

            private Occupancy[][] grid;
            private Robot robot;

            private Warehouse(Occupancy[][] grid, Robot robot)
            {
                this.grid = grid;
                this.robot = robot;
            }

            public void Simulate(bool debug = false)
            {
                if (debug)
                {
                    this.Print();
                }

                while (this.robot.HasMove())
                {
                    robot.MoveNext(ref this.grid, debug);
                    if (debug)
                    {
                        this.Print();
                    }
                }
            }

            public int SumGPS()
            {
                int sum = 0;
                for (int row = 0; row < this.grid.Length; ++row)
                {
                    for (int col = 0; col < this.grid[row].Length; ++col)
                    {
                        if (this.grid[row][col] == Occupancy.Box)
                        {
                            sum += 100 * row + col;
                        }
                    }
                }
                return sum;
            }

            private void Print()
            {
                foreach (var line in grid)
                {
                    Console.WriteLine(
                        string.Join("",
                            line.Select(o =>
                            {
                                switch (o)
                                {
                                    case Occupancy.Wall: return '#';
                                    case Occupancy.Box: return 'O';
                                    case Occupancy.Robot: return '@';
                                    case Occupancy.Free: return '.';
                                    default: return '?';
                                }
                            })));
                }
                Console.WriteLine();
            }

            public static bool TryParse(string input, out Warehouse warehouse, out string errorMessage)
            {
                warehouse = null;
                errorMessage = null;

                var pieces = input.Split($"{Environment.NewLine}{Environment.NewLine}");
                var lines = pieces[0].Split(Environment.NewLine);
                var grid = new Occupancy[lines.Length][];
                Robot robot = null;

                for (int row = 0; row < lines.Length; ++row)
                {
                    grid[row] = new Occupancy[lines[row].Length];
                }

                for (int row = 0; row < lines.Length; ++row)
                {
                    for (int col = 0; col < lines[row].Length; ++col)
                    {
                        switch (lines[row][col])
                        {
                            case '.':
                                grid[row][col] = Occupancy.Free;
                                break;
                            case '#':
                                grid[row][col] = Occupancy.Wall;
                                break;
                            case 'O':
                                grid[row][col] = Occupancy.Box;
                                break;
                            case '@':
                                grid[row][col] = Occupancy.Robot;
                                if (!Robot.TryParse(row, col, pieces[1], out robot, out errorMessage))
                                {
                                    return false;
                                }
                                break;
                            default:
                                errorMessage = $"Invalid warehouse occupancy: '{lines[row][col]}";
                                return false;
                        }
                    }
                }

                warehouse = new Warehouse(grid, robot);
                return true;
            }
        }
    }
}
