using System.Data;

namespace AdventOfCode2024 {
    class Program05 {
        public static (List<int[]>, List<int[]>) parseInput(string[] lines) {
            int splitSize = 0;
            for (int i = 0; i < lines.Length; i++) {
                if (lines[i] == "") {
                    splitSize = i;
                    break;
                }
            }
            string[] ruleStrings = new string[splitSize];
            string[] updateStrings = new string[lines.Length - splitSize - 1];

            Array.Copy(lines, 0, ruleStrings, 0, splitSize);
            Array.Copy(lines, splitSize + 1, updateStrings, 0, lines.Length - splitSize - 1);

            List<int[]> rules = new List<int[]>();
            List<int[]> updates = new List<int[]>();

            foreach (string rS in ruleStrings) {
                int[] rule = rS.Split('|').Select(int.Parse).ToArray();
                rules.Add(rule);
            }

            foreach (string uS in updateStrings) {
                int[] update = uS.Split(',').Select(int.Parse).ToArray();
                updates.Add(update);
            }

            return (rules, updates);
        }
        public static bool isInOrder(int[] update, List<int[]> rules) {
            List<int[]> validRules = rules.Where(r => update.Contains(r[0]) && update.Contains(r[1])).ToList();
            return validRules.All(r => Array.IndexOf(update, r[0]) < Array.IndexOf(update, r[1]));
        }
        public static int[] sortUpdate(int[] update, List<int[]> rules) {
            List<int> sorted = new List<int>();

            foreach (int n in update) {
                if (!sorted.Any()) {
                    sorted.Add(n);    // add first to list
                } else {
                    List<int[]> validRules = rules.Where(r => (sorted.Contains(r[0]) || sorted.Contains(r[1])) && (n == r[0] || n == r[1])).ToList();

                    for (int i = 0; i < sorted.Count; i++) {
                        List<int> tempList = new List<int>(sorted);
                        tempList.Insert(i, n);
                        int[] temp = tempList.ToArray();
                        if (validRules.All(r => Array.IndexOf(temp, r[0]) < Array.IndexOf(temp, r[1]))) {
                            sorted.Insert(i, n);
                            break;
                        } else {
                            if (i == sorted.Count - 1) {
                                sorted.Add(n);    // add to end of list
                                break;
                            }
                        }
                    }
                }
            }

            return sorted.ToArray();
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

            // Read all lines from the file + parse rules/updates chunks
            string[] lines = File.ReadAllLines(filePath);
            (List<int[]> rules, List<int[]> updates) = parseInput(lines);

            // Compare updates to rules
            List<int[]> correctlyOrdered = new List<int[]>();      // Part A
            List<int[]> incorrectlyOrdered = new List<int[]>();    // Part B
            List<int[]> sorted = new List<int[]>();                // Part B

            foreach (int[] update in updates) {
                if (isInOrder(update, rules)) {
                    correctlyOrdered.Add(update);         // Part A
                } else {
                    incorrectlyOrdered.Add(update);       // Part B
                }
            }

            foreach (int[] update in incorrectlyOrdered) {
                sorted.Add(sortUpdate(update, rules));    // Part B
            }

            // Calculate answers
            int answerA = correctlyOrdered.Sum(x => x[x.Length / 2]);
            int answerB = sorted.Sum(x => x[x.Length / 2]);

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}
