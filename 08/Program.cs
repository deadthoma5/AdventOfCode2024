using System.Collections;

namespace AdventOfCode2024 {
    class Point {
        public int X {get; private set; }
        public int Y {get; private set; }
        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public static Point operator +(Point p1, Point p2) {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2) {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator *(int a, Point p) {
            return new Point(a * p.X, a * p.Y);
        }

        public override string ToString() {
            return string.Format($"({X}, {Y})");
        }
    }
    class Program08 {
        static void Main(string[] args) {
            // Use test input data?
            bool testing = false;
            
            // Initialize file path
            string filePath;

            if (testing) {
                filePath = "input_test.txt";
            } else {
                filePath = "input.txt";
            }

            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            int minX = 0;
            int minY = 0;
            int maxX = lines[0].Length - 1;
            int maxY = lines.Length - 1;

            Dictionary<char, List<Point>> antennas = new Dictionary<char, List<Point>>();
            for (int y = 0; y < lines.Length; y++) {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++) {
                    char symbol = line[x];
                    if (symbol == '.') {
                        continue;
                    } else {
                        Point antenna = new Point(x, y);
                        if (!antennas.ContainsKey(symbol)) {
                            antennas.Add(symbol, new List<Point>() { antenna });
                        } else {
                            if (antennas[symbol].Any(p => p.X == x && p.Y == y)) {
                                continue;
                            } else {
                                antennas[symbol].Add(antenna);
                            }
                        }
                    }
                }
            }

            List<Point> antinodes = new List<Point>();
            List<Point> harmonics = new List<Point>();
            foreach (char frequency in antennas.Keys) {
                foreach (Point a in antennas[frequency]) {
                    if (!harmonics.Any(p => p.X == a.X && p.Y == a.Y)) {
                        harmonics.Add(a);
                    }
                    foreach (Point b in antennas[frequency]) {
                        if (!harmonics.Any(p => p.X == b.X && p.Y == b.Y)) {
                            harmonics.Add(b);
                        }
                        if (a != b) {
                            Point antinode = 2 * a - b;
                            if (antinode.X < minX || antinode.X > maxX || antinode.Y < minY || antinode.Y > maxY) {
                                continue;
                            } else {
                                if (!antinodes.Any(p => p.X == antinode.X && p.Y == antinode.Y)) {
                                    antinodes.Add(antinode);
                                }
                            }

                            int n = 0;
                            while (true) {
                                Point harmonic = ((n + 1) * a) - (n * b);
                                if (harmonic.X < minX || harmonic.X > maxX || harmonic.Y < minY || harmonic.Y > maxY) {
                                    break;
                                } else {
                                    n += 1;
                                    if (!harmonics.Any(p => p.X == harmonic.X && p.Y == harmonic.Y)) {
                                        harmonics.Add(harmonic);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            
            int answerA = antinodes.Count;
            int answerB = harmonics.Count;

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}