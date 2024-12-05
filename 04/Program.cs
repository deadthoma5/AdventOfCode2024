using System.Collections;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace AdventOfCode2024 {
    class Program04 {
        public static bool findXMAS(Dictionary<Vector2, char> grid, Vector2 r0, Vector2 dr) {
            string word = "XMAS";
            bool isMatch = true;

            for (int n = 1; n < word.Length; n++) {
                Vector2 r = r0 + n*dr;
                char letter;

                if (grid.TryGetValue(r, out letter)) {
                    if (word[n] != letter) {
                        isMatch = false;
                        break;
                    }
                } else {    // out of bounds
                    isMatch = false;
                    break;
                }
            }

            return isMatch;
        }
        public static bool findX(Dictionary<Vector2, char> grid, Vector2 r0) {
            string word = "MAS";
            bool d1Match = true;
            bool d2Match = true;

            Vector2 d1 = new Vector2(1, 1);     // down-right
            Vector2 d2 = new Vector2(1, -1);    // down-left

            for (int sign = -1; sign <= 1; sign += 2) {
                d1Match = true;
                for (int n = -1; n <= 1; n++) {    // search up-left to down-right
                    Vector2 r = r0 + sign*n*d1;
                    char letter;

                    if (grid.TryGetValue(r, out letter)) {
                        if (word[n+1] != letter) {
                            d1Match = false;
                            break;
                        }
                    } else {    // out of bounds
                        d1Match = false;
                        break;
                    }
                }
                if (d1Match) {
                    break;
                }
            }

            if (d1Match) {
                for (int sign = -1; sign <= 1; sign += 2) {
                    d2Match = true;
                    for (int n = -1; n <= 1; n++) {    // search up-right to down-left
                        Vector2 r = r0 + sign*n*d2;
                        char letter;

                        if (grid.TryGetValue(r, out letter)) {
                            if (word[n+1] != letter) {
                                d2Match = false;
                                break;
                            }
                        } else {    // out of bounds
                            d2Match = false;
                            break;
                        }
                    }
                    if (d2Match) {
                        break;
                    }
                }
            }
            
            return d1Match && d2Match;
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
            Dictionary<Vector2, char> grid = new Dictionary<Vector2, char>();
            Dictionary<Vector2, int> locX = new Dictionary<Vector2, int>();    // Part A
            Dictionary<Vector2, int> locA = new Dictionary<Vector2, int>();    // Part B

            for (int row = 0; row < lines.Length; row++) {
                string line = lines[row];
                for (int col = 0; col < line.Length; col++) {
                    Vector2 position = new Vector2(row, col);
                    grid.Add(position, lines[row][col]);
                    if (lines[row][col] == 'X') {
                        locX.Add(position, 0);
                    } else if (lines[row][col] == 'A') {
                        locA.Add(position, 0);
                    }
                }
            }

            // Initialize list of possible directions
            List<Vector2> directions = new List<Vector2>();
            
            for (int row = -1; row <= 1; row++) {
                for (int col = -1; col <= 1; col++) {
                    if (row == 0 && col == 0) {
                        continue;
                    }
                    directions.Add(new Vector2(row, col));
                }
            }

            // Part A: Check all 8 directions around candidate 'X' for a match
            foreach (Vector2 position in locX.Keys) {
                foreach (Vector2 direction in directions) {
                    if (findXMAS(grid, position, direction)) {
                        locX[position] += 1;
                    }
                }
            }

            // Part B: Check X-shape around each candidate 'A' for a match
            foreach (Vector2 position in locA.Keys) {
                    if (findX(grid, position)) {
                        locA[position] += 1;
                    }
            }

            // Calculate answers
            int answerA = locX.Sum(x => x.Value);
            int answerB = locA.Sum(x => x.Value);

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}
