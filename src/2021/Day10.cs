namespace AdventOfCode2021;

using AdventOfCode;

internal class Day10 : PuzzleBase
{
    private const string openingChars = "([{<";
    private const string scorePositions = " " + openingChars;
    
    private string[] _data;

    public Day10(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 10 - Syntax Scoring");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 10, "Day10test.txt")
            .GetInput(Year, 10)
            .ConfigureAwait(false);

        int invalidLinesScore = 0;
        List<Int64> lineScores = new();

        foreach (var line in _data)
        {
            //Utils.WriteDebug($"\nNew Line: {line}");
            var stack = new Stack<char>();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (openingChars.Contains(c))
                {
                    stack.Push(line[i]);
                    //PrintStack(stack);
                }
                else
                {
                    char top = stack.Pop();
                    //PrintStack(stack);

                    if ((top == '(' && c != ')') ||
                        (top == '[' && c != ']') ||
                        (top == '{' && c != '}') ||
                        (top == '<' && c != '>'))
                    {
                        invalidLinesScore += GetSyntaxScore(c);
                        stack.Clear();
                        break;
                    }
                }
            }

            if (stack.Count != 0) // incomplete line; finish & score it
            {
                Int64 lineScore = 0;
                do
                {
                    var c = stack.Pop();
                    lineScore = (lineScore * 5) + scorePositions.IndexOf(c);

                } while (stack.Count > 0);

                lineScores.Add(lineScore);
            }
        }

        lineScores.Sort();
        Int64 incompleteLineScore = lineScores[(lineScores.Count / 2)];

        Utils.WriteResults($"Puzzle 1: {invalidLinesScore}");
        Utils.WriteResults($"Puzzle 2: {incompleteLineScore}");
    }
    
    private static int GetSyntaxScore(char c)
    {
        int value = c switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => 0
        };

        return value;
    }
        
    private void PrintStack(Stack<char> stack)
    {
        Console.WriteLine();
        foreach (var c in stack)
        {
            Console.Write($" {c}");
        }
    }
}