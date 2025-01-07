using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day14
    {
        public static int Main14(string[] args)
        {
            const string input = Day14.Input;
            Robot[] robots;
            string errorMessage;
            Robot.XY boardSize = new Robot.XY(101, 103);
            const int seconds = 100;

            if (!Day14.TryParse(input, boardSize, out robots, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            var locationsFinal = Simulate(ref robots, boardSize, seconds);
            var quadrants = QuadrantCount(ref locationsFinal, boardSize);
            Console.WriteLine($"safety: {quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3]}");
            return 0;
        }

        private static Robot.Location[] Simulate(ref Robot[] robots, Robot.XY boardSize, int seconds)
        {
            var locations = new Robot.Location[robots.Length];
            for (int i = 0; i < robots.Length; ++i)
            {
                locations[i] = new Robot.Location((robots[i].location.x + (robots[i].velocity.x * seconds)) % boardSize.x,
                                                  (robots[i].location.y + (robots[i].velocity.y * seconds)) % boardSize.y);
                if (locations[i].x < 0)
                {
                    locations[i].x += boardSize.x;
                }
                if (locations[i].y < 0)
                {
                    locations[i].y += boardSize.y;
                }
            }
            return locations;
        }

        private static int[] QuadrantCount(ref Robot.Location[] locations, Robot.XY boardSize)
        {
            Robot.XY midpoint = new Robot.XY(boardSize.x / 2, boardSize.y / 2);
            int[] quadrants = new int[4];
            foreach (var location in locations)
            {
                if (location.x == midpoint.x || location.y == midpoint.y)
                {
                    continue;
                }

                if (location.y > midpoint.y)
                {
                    if (location.x > midpoint.x)
                    {
                        ++quadrants[0];
                        continue;
                    }
                    ++quadrants[1];
                    continue;
                }
                if (location.x < midpoint.x)
                {
                    ++quadrants[2];
                    continue;
                }
                ++quadrants[3];
                continue;
            }

            return quadrants;
        }

        private static bool TryParse(string input, Robot.XY boardSize, out Robot[] robots, out string errorMessage)
        {
            robots = null;
            errorMessage = null;

            var lines = input.Split(Environment.NewLine);
            var robotsTemp = new Robot[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                if (!Robot.TryParse(lines[i], boardSize, out robotsTemp[i], out errorMessage))
                {
                    return false;
                }
            }

            robots = robotsTemp;
            return true;
        }

        private const string Example = "p=0,4 v=3,-3\r\np=6,3 v=-1,-3\r\np=10,3 v=-1,2\r\np=2,0 v=2,-1\r\np=0,0 v=1,3\r\np=3,0 v=-2,-2\r\np=7,6 v=-1,-3\r\np=3,0 v=-1,-2\r\np=9,3 v=2,3\r\np=7,3 v=-1,2\r\np=2,4 v=2,-3\r\np=9,5 v=-3,-3";
        private const string Debug = "p=2,4 v=2,-3";
        private const string Input = "p=31,100 v=-36,-71\r\np=29,22 v=9,29\r\np=26,16 v=-32,-28\r\np=89,102 v=-63,35\r\np=84,78 v=-59,-49\r\np=28,66 v=-87,81\r\np=34,58 v=-29,-17\r\np=13,20 v=26,37\r\np=11,29 v=23,-9\r\np=32,3 v=-17,36\r\np=7,54 v=-75,65\r\np=58,68 v=-56,-76\r\np=69,9 v=4,-39\r\np=11,51 v=-68,84\r\np=82,73 v=-60,-38\r\np=86,58 v=-99,-15\r\np=59,40 v=58,19\r\np=99,66 v=27,82\r\np=14,60 v=25,88\r\np=50,24 v=56,22\r\np=3,24 v=62,-75\r\np=94,73 v=-62,-76\r\np=32,66 v=58,-87\r\np=51,19 v=-46,90\r\np=4,27 v=27,86\r\np=5,33 v=30,-96\r\np=90,83 v=-8,-34\r\np=34,29 v=-29,-51\r\np=69,93 v=-4,-52\r\np=2,96 v=-34,-31\r\np=3,12 v=-79,86\r\np=86,87 v=-16,39\r\np=15,27 v=31,-1\r\np=74,73 v=41,-42\r\np=55,10 v=51,40\r\np=65,32 v=99,-47\r\np=39,5 v=-90,32\r\np=68,65 v=51,-64\r\np=81,11 v=99,36\r\np=31,28 v=-93,71\r\np=50,31 v=20,-64\r\np=52,67 v=-94,-11\r\np=35,2 v=-34,13\r\np=53,88 v=-89,-48\r\np=20,88 v=-61,-83\r\np=95,86 v=32,-18\r\np=85,11 v=-34,4\r\np=19,78 v=72,-49\r\np=91,27 v=86,41\r\np=11,14 v=-12,-18\r\np=90,57 v=87,80\r\np=70,11 v=-47,-74\r\np=49,91 v=10,43\r\np=91,60 v=28,67\r\np=78,91 v=-94,43\r\np=44,57 v=-92,38\r\np=22,42 v=50,59\r\np=25,33 v=-75,-66\r\np=97,81 v=-25,62\r\np=84,64 v=91,-72\r\np=98,48 v=84,-8\r\np=54,63 v=-44,27\r\np=61,69 v=-86,-99\r\np=47,72 v=20,-22\r\np=100,7 v=-22,-63\r\np=95,26 v=38,23\r\np=67,27 v=-50,18\r\np=10,98 v=84,-71\r\np=59,13 v=16,-61\r\np=14,45 v=71,-58\r\np=11,81 v=-26,66\r\np=29,72 v=-39,46\r\np=2,24 v=-80,-94\r\np=79,95 v=-9,-37\r\np=47,86 v=-86,-41\r\np=9,92 v=24,-14\r\np=18,69 v=-4,45\r\np=49,3 v=14,-71\r\np=90,0 v=-64,-44\r\np=75,79 v=96,-83\r\np=82,27 v=41,-70\r\np=92,85 v=66,71\r\np=5,57 v=-80,31\r\np=92,21 v=-15,41\r\np=91,43 v=88,-66\r\np=62,7 v=58,-52\r\np=87,23 v=89,-97\r\np=54,56 v=62,27\r\np=29,65 v=-36,-34\r\np=84,72 v=36,27\r\np=29,36 v=17,41\r\np=47,49 v=-92,-46\r\np=20,81 v=36,-80\r\np=12,42 v=27,46\r\np=87,58 v=88,38\r\np=96,25 v=-20,-70\r\np=44,72 v=-4,24\r\np=59,83 v=-99,35\r\np=31,74 v=-32,5\r\np=7,10 v=20,-30\r\np=43,44 v=84,-53\r\np=24,32 v=-33,-81\r\np=26,80 v=-82,73\r\np=99,95 v=-98,44\r\np=22,73 v=-49,54\r\np=96,48 v=79,41\r\np=85,21 v=42,48\r\np=53,72 v=-42,-53\r\np=58,35 v=-38,41\r\np=13,41 v=-53,86\r\np=57,12 v=-46,-21\r\np=21,28 v=19,-78\r\np=59,32 v=1,7\r\np=12,24 v=-27,10\r\np=9,101 v=-36,-60\r\np=6,22 v=-24,-94\r\np=31,67 v=-93,-65\r\np=84,1 v=92,24\r\np=24,55 v=68,-50\r\np=93,86 v=78,96\r\np=85,65 v=-14,-99\r\np=14,96 v=29,-3\r\np=30,20 v=57,-57\r\np=70,80 v=46,43\r\np=49,39 v=-45,95\r\np=33,42 v=-20,11\r\np=24,6 v=-1,69\r\np=2,90 v=29,-14\r\np=75,6 v=51,28\r\np=29,88 v=-92,58\r\np=72,24 v=44,88\r\np=78,63 v=-56,-45\r\np=84,35 v=95,-68\r\np=29,97 v=90,-94\r\np=97,1 v=28,-74\r\np=70,34 v=-31,71\r\np=79,52 v=43,84\r\np=67,93 v=-51,-75\r\np=55,84 v=11,-29\r\np=85,48 v=-66,-35\r\np=34,82 v=-87,-64\r\np=21,89 v=-29,-41\r\np=81,89 v=25,-55\r\np=87,22 v=-12,25\r\np=75,41 v=-60,-39\r\np=22,100 v=-32,-52\r\np=81,19 v=-14,-54\r\np=76,95 v=-11,-14\r\np=87,45 v=39,-88\r\np=95,77 v=-66,96\r\np=41,25 v=-40,2\r\np=37,99 v=63,-75\r\np=22,50 v=-68,8\r\np=57,39 v=32,-39\r\np=73,81 v=-17,43\r\np=50,58 v=-97,38\r\np=3,78 v=-17,-30\r\np=39,76 v=10,-91\r\np=95,67 v=79,-18\r\np=13,6 v=-76,40\r\np=93,21 v=-96,-2\r\np=67,72 v=-50,-53\r\np=88,64 v=-61,-84\r\np=30,20 v=40,-59\r\np=77,83 v=94,-7\r\np=41,71 v=10,58\r\np=88,12 v=43,74\r\np=49,88 v=-97,20\r\np=22,9 v=-26,1\r\np=37,86 v=-37,58\r\np=91,77 v=78,-14\r\np=44,79 v=7,16\r\np=25,26 v=68,37\r\np=52,41 v=7,-16\r\np=53,19 v=12,33\r\np=50,66 v=52,25\r\np=40,65 v=61,-69\r\np=8,13 v=26,2\r\np=9,79 v=77,-41\r\np=20,14 v=21,21\r\np=40,97 v=12,-44\r\np=8,43 v=-71,2\r\np=5,45 v=-80,49\r\np=90,81 v=69,-86\r\np=60,33 v=-56,-69\r\np=14,48 v=-75,-96\r\np=26,18 v=62,30\r\np=47,37 v=-78,59\r\np=56,4 v=-97,-51\r\np=67,8 v=47,9\r\np=74,47 v=-65,77\r\np=12,47 v=75,-77\r\np=17,66 v=72,12\r\np=77,34 v=29,11\r\np=77,90 v=91,-44\r\np=0,52 v=6,-68\r\np=25,97 v=73,5\r\np=61,30 v=19,-69\r\np=52,2 v=54,-29\r\np=40,12 v=-45,-75\r\np=65,96 v=92,5\r\np=73,39 v=-56,11\r\np=17,98 v=25,-75\r\np=15,16 v=-25,-28\r\np=5,97 v=-78,-59\r\np=54,100 v=-47,78\r\np=18,61 v=-79,-15\r\np=51,37 v=50,-4\r\np=92,16 v=78,99\r\np=70,74 v=-55,-99\r\np=82,44 v=48,-19\r\np=20,95 v=-83,-10\r\np=52,59 v=-43,99\r\np=49,51 v=-78,8\r\np=31,24 v=49,61\r\np=9,73 v=46,4\r\np=64,52 v=50,-92\r\np=51,45 v=-49,-16\r\np=83,60 v=47,-68\r\np=8,44 v=68,85\r\np=81,98 v=45,5\r\np=51,77 v=59,-11\r\np=40,66 v=-71,-89\r\np=8,21 v=-16,18\r\np=9,51 v=33,64\r\np=59,32 v=99,37\r\np=46,83 v=-66,-56\r\np=53,36 v=5,83\r\np=48,84 v=-48,77\r\np=19,57 v=-81,-23\r\np=73,82 v=-26,7\r\np=21,7 v=-88,-28\r\np=49,92 v=-87,-26\r\np=5,59 v=-82,-18\r\np=84,6 v=-69,-13\r\np=34,90 v=-87,-44\r\np=6,79 v=-40,-97\r\np=42,69 v=-88,88\r\np=95,53 v=84,30\r\np=33,86 v=-18,-19\r\np=77,98 v=-10,70\r\np=87,98 v=96,82\r\np=16,13 v=73,-17\r\np=85,57 v=88,-27\r\np=24,99 v=-85,-14\r\np=98,36 v=-61,-1\r\np=60,58 v=3,42\r\np=5,56 v=-58,85\r\np=24,92 v=18,-94\r\np=82,87 v=-10,-87\r\np=69,32 v=51,95\r\np=63,51 v=-52,15\r\np=44,21 v=51,32\r\np=83,2 v=-21,-64\r\np=79,38 v=85,-43\r\np=53,44 v=-98,-31\r\np=41,42 v=-83,26\r\np=36,59 v=-38,-61\r\np=6,62 v=81,-23\r\np=57,68 v=40,-71\r\np=25,101 v=-1,-66\r\np=53,35 v=38,39\r\np=74,10 v=61,53\r\np=83,90 v=83,-29\r\np=81,13 v=-59,-74\r\np=25,0 v=26,5\r\np=19,42 v=99,-50\r\np=84,75 v=-58,8\r\np=99,83 v=85,-34\r\np=55,19 v=-26,-78\r\np=84,44 v=-11,72\r\np=82,60 v=-13,-60\r\np=30,23 v=67,26\r\np=52,57 v=-89,-83\r\np=88,35 v=84,-93\r\np=40,10 v=-45,-10\r\np=72,26 v=-51,44\r\np=76,19 v=-50,48\r\np=81,52 v=38,-8\r\np=26,35 v=-95,-68\r\np=52,63 v=-54,35\r\np=16,11 v=-77,6\r\np=99,69 v=70,-97\r\np=53,6 v=85,44\r\np=73,12 v=-99,-29\r\np=3,86 v=-93,-64\r\np=46,18 v=12,-86\r\np=74,0 v=-59,13\r\np=25,95 v=67,63\r\np=71,98 v=-12,-40\r\np=71,93 v=-12,-87\r\np=36,86 v=-40,-3\r\np=10,36 v=-32,-73\r\np=71,96 v=-34,-89\r\np=71,1 v=-4,-17\r\np=31,5 v=8,-78\r\np=100,2 v=31,13\r\np=98,44 v=6,-44\r\np=84,66 v=-13,23\r\np=32,30 v=20,-67\r\np=70,53 v=83,-99\r\np=15,93 v=25,1\r\np=2,71 v=-71,84\r\np=13,94 v=-25,47\r\np=73,34 v=-60,88\r\np=90,33 v=86,98\r\np=15,52 v=16,68\r\np=10,70 v=-87,-91\r\np=59,50 v=5,95\r\np=16,77 v=23,20\r\np=49,24 v=80,-2\r\np=29,97 v=-84,-64\r\np=20,48 v=-81,-65\r\np=78,39 v=50,-62\r\np=68,67 v=-5,-72\r\np=81,100 v=-58,-14\r\np=35,23 v=21,-43\r\np=85,91 v=92,-37\r\np=35,97 v=6,-29\r\np=80,51 v=-16,-98\r\np=70,76 v=71,44\r\np=17,53 v=-37,-12\r\np=8,1 v=-22,78\r\np=100,74 v=-79,-87\r\np=43,39 v=-90,-81\r\np=2,83 v=73,-68\r\np=96,14 v=84,-4\r\np=32,18 v=-47,-78\r\np=66,87 v=-3,-45\r\np=24,93 v=73,-26\r\np=83,63 v=87,23\r\np=44,93 v=-46,1\r\np=84,46 v=-63,15\r\np=81,77 v=85,-90\r\np=83,6 v=87,-6\r\np=47,44 v=7,-19\r\np=95,71 v=-9,20\r\np=91,77 v=-8,-75\r\np=64,89 v=-4,12\r\np=95,37 v=-14,-59\r\np=55,62 v=35,82\r\np=41,70 v=62,50\r\np=78,43 v=-8,-20\r\np=92,57 v=86,84\r\np=25,82 v=-30,16\r\np=11,7 v=-78,44\r\np=19,38 v=51,25\r\np=17,101 v=-28,-21\r\np=97,59 v=38,15\r\np=34,95 v=65,9\r\np=17,88 v=45,-95\r\np=80,27 v=-12,-93\r\np=66,72 v=-52,-91\r\np=21,64 v=24,-16\r\np=37,18 v=-86,48\r\np=53,15 v=-89,-13\r\np=0,45 v=28,-73\r\np=47,79 v=-99,99\r\np=70,95 v=-88,-10\r\np=72,50 v=-6,80\r\np=77,58 v=-83,91\r\np=15,29 v=-57,94\r\np=7,36 v=61,18\r\np=3,44 v=-19,-19\r\np=52,92 v=8,35\r\np=8,77 v=-22,12\r\np=88,33 v=36,83\r\np=32,45 v=-42,-85\r\np=52,98 v=-44,-10\r\np=1,51 v=-63,26\r\np=7,64 v=-61,79\r\np=35,11 v=66,-59\r\np=80,92 v=92,97\r\np=77,49 v=-53,-15\r\np=80,100 v=-10,78\r\np=47,33 v=58,43\r\np=37,91 v=-92,24\r\np=75,1 v=47,97\r\np=7,59 v=93,8\r\np=98,72 v=-67,50\r\np=76,1 v=-59,63\r\np=94,88 v=37,-79\r\np=54,82 v=-54,-87\r\np=43,1 v=-92,-86\r\np=54,63 v=-54,69\r\np=13,80 v=-76,-60\r\np=56,42 v=-46,3\r\np=48,52 v=-95,-95\r\np=93,67 v=-68,69\r\np=37,65 v=64,-61\r\np=95,79 v=32,-91\r\np=96,95 v=-68,-14\r\np=23,89 v=-79,-79\r\np=43,63 v=-88,-19\r\np=40,62 v=56,98\r\np=52,26 v=6,-70\r\np=26,12 v=-63,2\r\np=89,61 v=88,42\r\np=81,55 v=91,38\r\np=72,92 v=-63,-2\r\np=64,77 v=-51,-45\r\np=38,68 v=-71,25\r\np=58,32 v=-33,-53\r\np=55,60 v=97,-80\r\np=49,102 v=-11,-26\r\np=95,4 v=36,82\r\np=83,33 v=67,-70\r\np=90,59 v=-57,-80\r\np=12,16 v=-28,36\r\np=50,24 v=-92,-28\r\np=13,72 v=76,-84\r\np=91,98 v=-80,-69\r\np=88,10 v=55,-24\r\np=97,67 v=-19,92\r\np=72,93 v=-7,5\r\np=85,41 v=-64,83\r\np=5,5 v=-42,87\r\np=3,46 v=-22,72\r\np=33,37 v=-30,77\r\np=83,27 v=8,46\r\np=11,2 v=-75,-82\r\np=41,7 v=-87,78\r\np=51,30 v=58,-47\r\np=24,15 v=-33,-97\r\np=92,50 v=-69,35\r\np=74,38 v=-64,-70\r\np=12,62 v=-76,-65\r\np=11,74 v=-80,5\r\np=96,65 v=-70,69\r\np=1,32 v=-71,-77\r\np=82,39 v=33,26\r\np=28,46 v=80,-98\r\np=51,85 v=-2,5\r\np=69,52 v=-52,76\r\np=22,68 v=-23,-22\r\np=56,13 v=61,71\r\np=79,78 v=-95,-50\r\np=4,90 v=-71,-14\r\np=76,31 v=-50,94\r\np=69,72 v=95,78\r\np=5,96 v=-70,-85\r\np=17,87 v=28,-67\r\np=10,3 v=95,-27\r\np=44,94 v=-40,-29\r\np=29,49 v=18,53\r\np=4,97 v=-19,-29\r\np=1,98 v=32,47\r\np=25,34 v=11,72\r\np=39,81 v=-89,1\r\np=37,42 v=-39,45\r\np=64,4 v=-8,52\r\np=7,8 v=-37,-37\r\np=58,94 v=54,-82\r\np=63,27 v=39,74\r\np=8,30 v=25,75\r\np=60,90 v=-51,70\r\np=94,90 v=-23,-9\r\np=91,45 v=-14,38\r\np=63,42 v=-51,30\r\np=66,83 v=28,4\r\np=58,79 v=5,-72\r\np=34,99 v=-42,10\r\np=51,69 v=-47,69\r\np=38,77 v=64,-45\r\np=18,59 v=20,-38\r\np=79,31 v=43,-62\r\np=29,0 v=-84,63\r\np=72,12 v=38,71\r\np=61,56 v=-50,88\r\np=32,37 v=-51,-23\r\np=42,43 v=-24,61\r\np=51,41 v=-95,92\r\np=39,12 v=12,73\r\np=83,21 v=-72,1\r\np=12,79 v=-77,12\r\np=15,60 v=67,30\r\np=62,92 v=-47,62\r\np=42,95 v=53,-64\r\np=100,45 v=-68,-92\r\np=50,73 v=-95,-95\r\np=30,88 v=16,-79\r\np=64,51 v=45,64\r\np=98,72 v=-64,46\r\np=65,34 v=-3,76\r\np=26,88 v=65,78\r\np=5,60 v=36,42\r\np=3,33 v=-39,84\r\np=41,90 v=-64,2\r\np=90,52 v=-65,-65\r\np=39,63 v=16,-45\r\np=85,19 v=37,98\r\np=64,29 v=-5,-31\r\np=82,9 v=-21,2\r\np=34,93 v=-39,-60\r\np=86,1 v=91,40\r\np=49,57 v=-50,25\r\np=26,82 v=67,-25\r\np=10,23 v=-81,21\r\np=7,7 v=25,82\r\np=87,24 v=-83,92";

        private class Robot
        {
            public Location location;
            public Velocity velocity;

            public Robot(Location location, Velocity velocity)
            {
                this.location = location;
                this.velocity = velocity;
            }

            public override string ToString()
            {
                return $"loc: [{this.location.x},{this.location.y}], vel: [{this.velocity.x},{this.velocity.y}]";
            }

            public static bool TryParse(string input, Robot.XY boardSize, out Robot robot, out string errorMessage)
            {
                robot = null;
                errorMessage = null;

                var split = input.Split(' ');
                if (split.Length != 2)
                {
                    errorMessage = $"Invalid robot input: '{input}'.";
                    return false;
                }

                Location location;
                Velocity velocity;
                if (!Location.TryParse(split[0], boardSize, out location, out errorMessage)
                 || !Velocity.TryParse(split[1], out velocity, out errorMessage))
                {
                    return false;
                }

                robot = new Robot(location, velocity);
                return true;
            }

            public class XY
            {
                public int x;
                public int y;

                public XY(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public static bool TryParse(string input, out XY xy, out string errorMessage)
                {
                    xy = null;
                    errorMessage = null;

                    int x, y;
                    var intStrs = input.Split(',');
                    if (intStrs.Length != 2
                        || !int.TryParse(intStrs[0], out x)
                        || !int.TryParse(intStrs[1], out y))
                    {
                        errorMessage = $"Invalid x, y components: '{input}'.";
                        return false;
                    }

                    xy = new XY(x, y);
                    return true;
                }
            }

            public class Location : XY
            {
                private const string prefix = "p=";

                public Location(int x, int y) : base(x, y) { }
                public Location(XY xy) : base(xy.x, xy.y) { }

                public static bool TryParse(string input, XY boardSize, out Location location, out string errorMessage)
                {
                    location = null;
                    errorMessage = null;

                    if (input == null || !input.StartsWith(Location.prefix))
                    {
                        errorMessage = $"Invalid velocity input: '{input}'.";
                        return false;
                    }

                    XY xy;
                    if (!XY.TryParse(input.Substring(Location.prefix.Length), out xy, out errorMessage))
                    {
                        return false;
                    }

                    location = new Location(xy.x, boardSize.y - 1 - xy.y);
                    return true;
                }
            }

            public class Velocity : XY
            {
                private const string prefix = "v=";

                public Velocity(int x, int y) : base(x, y) { }
                public Velocity(XY xy) : base(xy.x, xy.y) { }

                public static bool TryParse(string input, out Velocity velocity, out string errorMessage)
                {
                    velocity = null;
                    errorMessage = null;

                    if (input == null || !input.StartsWith(Velocity.prefix))
                    {
                        errorMessage = $"Invalid velocity input: '{input}'.";
                        return false;
                    }

                    XY xy;
                    if (!XY.TryParse(input.Substring(Velocity.prefix.Length), out xy, out errorMessage))
                    {
                        return false;
                    }

                    velocity = new Velocity(xy.x, -xy.y);
                    return true;
                }
            }
        }
    }
}
