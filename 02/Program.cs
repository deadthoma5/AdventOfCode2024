namespace AdventOfCode2024
{
    class Program02
    {
        public static int[] calcDifferences(int[] input)
        {
            int[] diffs = new int[input.Length - 1];

            for (int i = 0; i < diffs.Length; i++)
            {
                diffs[i] = input[i+1] - input[i];
            }

            return diffs;
        }
        public static bool isMonotonic(int[] input)
        {
            int sign = Math.Sign(input[0]);
            foreach (int i in input)
            {
                if (Math.Sign(i) != sign)
                    return false;
            }
            return true;
        }
        public static bool isSafeMagnitude(int[] input)
        {
            foreach (int i in input)
            {
                int magnitude = Math.Abs(i);

                if (magnitude < 1 || magnitude > 3)
                    return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            // Use test input data?
            bool testing = false;
            
            // Initialize file path
            string filePath;

            if (testing)
            {
                filePath = "input_test.txt";
            } else
            {
                filePath = "input.txt";
            }

            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            List<bool> statusA = new List<bool>();
            List<bool> statusB = new List<bool>();

            foreach (string line in lines)
            {
                int[] report = line.Split()
                    .Where(x => int.TryParse(x, out _))
                    .Select(int.Parse)
                    .ToArray();

                // Part A
                int[] diffs = calcDifferences(report);
                bool stA = isMonotonic(diffs) && isSafeMagnitude(diffs);
                statusA.Add(stA);

                // Part B
                bool stB = stA;
                for (int i = 0; i < report.Length; i++)
                {
                    if (stB)
                    {
                        break;
                    } else
                    {
                        List<int> tempReportList = report.ToList();
                        tempReportList.RemoveAt(i);
                        int[] tempReport = tempReportList.ToArray();
                        int[] tempDiffs = calcDifferences(tempReport);
                        stB = isMonotonic(tempDiffs) && isSafeMagnitude(tempDiffs);
                    }
                }
                statusB.Add(stB);
            }
            
            // Calculate answers
            int answerA = statusA.Count(c => c);
            int answerB = statusB.Count(c => c);

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}
