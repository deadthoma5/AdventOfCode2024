using System.Text.RegularExpressions;

namespace AdventOfCode2024 {
    class Program03 {
        public static List<string> parseInput(string input) {
            string pattern = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";
            List<string> instructions = new List<string>();

            foreach (Match match in Regex.Matches(input, pattern)) {
                instructions.Add(match.Value);
            }

            return instructions;
        }
        public static List<string> parseConditional(List<string> input) {
            bool enabled = true;
            List<string> output = new List<string>();

            foreach (string instruction in input) {
                if (instruction == "don't()") {
                    enabled = false;
                } else if (instruction == "do()") {
                    enabled = true;
                } else {
                    if (enabled) {
                        output.Add(instruction);
                    }
                }
            }

            return output;
        }
        public static int multiply(string input) {
            string pattern = @"\b(\d{1,3})\b.*?\b(\d{1,3})\b";
            Match results = Regex.Match(input, pattern);

            int.TryParse(results.Groups[1].Value, out int a);
            int.TryParse(results.Groups[2].Value, out int b);

            return a * b;
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
            string input = string.Concat(lines);
            
            // Parse mul(a,b), do(), don't() instructions from input
            List<string> instructions = parseInput(input);
            
            // Calculate answers
            int answerA = instructions.Select(x => multiply(x)).Sum();
            int answerB = parseConditional(instructions).Select(x => multiply(x)).Sum();

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}
