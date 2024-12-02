namespace AdventOfCode2024
{
    class Program01
    {
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

            // Parse the lists of integers
            List<int> left = new List<int>();
            List<int> right = new List<int>();
    
            foreach (string line in lines)
            {
                string[] subs = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                int leftNum = int.Parse(subs[0]);
                int rightNum = int.Parse(subs[1]);
                
                left.Add(leftNum);
                right.Add(rightNum);
            }
            
            // Sort the lists of integers
            left.Sort();
            right.Sort();

            // Calculate similarity scores
            List<int> scores = new List<int>();
            foreach (int leftNum in left)
            {
                int count = right.Where(x => x.Equals(leftNum)).Count();
                scores.Add(leftNum * count);
            }
            
            // Calculate answers
            int answerA = left.Zip(right, (a, b) => a - b).Sum(Math.Abs);
            int answerB = scores.Sum();

            // Print answers
            Console.WriteLine(answerA);
            Console.WriteLine(answerB);
        }
    }
}
