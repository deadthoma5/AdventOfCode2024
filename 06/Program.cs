using System.Drawing;

namespace AdventOfCode2024 {
    class Program06 {
        public static Point TurnRight(Point input, double angle) {
            return new Point(input.Y, -input.X);
        }
        public static (int, bool) Simulation(Dictionary<Point, char> grid, Point guard, Point direction) {
            bool isOOB = false;
            bool isLoop = false;
            Dictionary<Point, List<Point>> visited = new Dictionary<Point, List<Point>>();
            visited.Add(guard, new List<Point>(){direction});

            while (!isOOB && !isLoop) {
                Point testPos = guard + (Size)direction;
                char symbol;

                if (grid.TryGetValue(testPos, out symbol)) {
                    switch (symbol) {
                        case '.':
                            if (!visited.Keys.Contains(testPos)) {
                                visited.Add(testPos, new List<Point>(){direction});
                            } else {
                                if (visited[testPos].Contains(direction)) {
                                    isLoop = true;
                                    break;
                                }
                            }
                            guard = testPos;
                            break;
                        case '#':
                            direction = TurnRight(direction, -Math.PI/2);
                            break;
                    }
                } else {
                    isOOB = true;
                }
            }

            return (visited.Count, isLoop);
        }
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
            
            // Initialize grid from input + remember "X" starting points
            Dictionary<Point, char> grid = new Dictionary<Point, char>();
            Point guard = new Point();
            Point direction = new Point();
            for (int row = 0; row < lines.Length; row++) {
                string line = lines[row];
                for (int col = 0; col < line.Length; col++) {
                    Point pos = new Point(row, col);
                    char symbol = line[col];
                    switch (symbol) {
                        case '#':
                            grid[pos] = '#';
                            break;
                        case '^':
                            guard = new Point(row, col);
                            direction = new Point(-1, 0);
                            goto case '.';
                        case '.':
                            grid[pos] = '.';
                            break;
                    }
                }
            }

            // Part A
            (int answerA, bool _) = Simulation(grid, guard, direction);

            // Part B
            int answerB = 0;
            foreach (Point pos in grid.Keys) {
                if (grid[pos] == '.') {
                    var tempGrid = new Dictionary<Point, char>(grid);
                    tempGrid[pos] = '#';
                    (int _, bool isLoop_B) = Simulation(tempGrid, guard, direction);
                    if (isLoop_B) {
                        answerB += 1;
                    }
                }
            }

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}
