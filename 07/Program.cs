namespace AdventOfCode2024 {
    class Program07 {
        public static bool checkA(long testValue, long accumulated, Queue<long> sequence) {
            if (sequence.Count == 0 && testValue == accumulated) {
                return true;
            } else {
                if (sequence.Count == 0 || accumulated > testValue) {
                    return false;
                } else {
                    var number = sequence.Dequeue();
                    var newSequenceAdd = new Queue<long>(sequence);
                    var newSequenceMultiply = new Queue<long>(sequence);

                    bool checkAdd = checkA(testValue, accumulated + number, newSequenceAdd);
                    bool checkMultiply = checkA(testValue, accumulated * number, newSequenceMultiply);

                    return checkAdd || checkMultiply;
                }
            }
        }
        public static bool checkB(long testValue, long accumulated, Queue<long> sequence) {
            if (sequence.Count == 0 && testValue == accumulated) {
                return true;
            } else {
                if (sequence.Count == 0 || accumulated > testValue) {
                    return false;
                } else {
                    var number = sequence.Dequeue();
                    var newSequenceAdd = new Queue<long>(sequence);
                    var newSequenceMultiply = new Queue<long>(sequence);
                    var newSequenceConcat = new Queue<long>(sequence);

                    bool checkAdd = checkB(testValue, accumulated + number, newSequenceAdd);
                    bool checkMultiply = checkB(testValue, accumulated * number, newSequenceMultiply);
                    bool checkConcat = checkB(testValue, long.Parse(accumulated.ToString() + number.ToString()), newSequenceConcat);

                    return checkAdd || checkMultiply || checkConcat;
                }
            }
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
            List<long> testValues = new List<long>();
            List<Queue<long>> numbers = new List<Queue<long>>();          

            foreach (string line in lines) {
                testValues.Add(long.Parse(line.Split(':')[0]));
                List<long> tempList = line.Split(':')[1].TrimStart().Split(' ').Select(long.Parse).ToList();
                numbers.Add(new Queue<long>(tempList));
            }
            
            // Part A
            List<Queue<long>> numbersA = new List<Queue<long>>();
            foreach (var queue in numbers) {
                numbersA.Add(new Queue<long>(queue));
            }
            Dictionary<long, bool> resultsA = new Dictionary<long, bool>();

            for (int i = 0; i < testValues.Count; i++) {
                resultsA.Add(testValues[i], checkA(testValues[i], numbersA[i].Dequeue(), numbersA[i]));
            }

            long answerA = resultsA.Where(x => x.Value).ToDictionary(x => x.Key, x => x.Value).Keys.ToList().Sum();

            // Part B
            List<Queue<long>> numbersB = new List<Queue<long>>(numbers);
            foreach (var queue in numbers) {
                numbersB.Add(new Queue<long>(queue));
            }
            Dictionary<long, bool> resultsB = new Dictionary<long, bool>();

            for (int i = 0; i < testValues.Count; i++) {
                resultsB.Add(testValues[i], checkB(testValues[i], numbersB[i].Dequeue(), numbersB[i]));
            }

            long answerB = resultsB.Where(x => x.Value).ToDictionary(x => x.Key, x => x.Value).Keys.ToList().Sum();

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}