namespace AdventOfCode2021;

using AdventOfCode;

internal class Day08 : PuzzleBase
{
    private string[] _data;

    public Day08(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 08 - Seven Segment Search");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 8, "Day08test.txt")
            .GetInput(Year, 8)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int count = 0;
        foreach (var line in _data)
        {
            var outputDigits = line.Split('|')[1].Trim().Split(' ');

            count += outputDigits.Count(d => d.Length == 2 || d.Length == 3 || d.Length == 4 || d.Length == 7);
        }

        Utils.WriteResults($"Puzzle 1: {count}");
    }

    private void Puzzle2()
    {
        int sum = 0;
        foreach (var line in _data)
        {
            var patterns = line.Split('|')[0].Trim().Split(' ');
            var outputDigits = line.Split('|')[1].Trim().Split(' ');

            var one = (patterns.Where(p => p.Length == 2).First()).ToList();
            var four = (patterns.Where(p => p.Length == 4).First()).ToList();

            string output = string.Empty;
            foreach (var digit in outputDigits)
            {
                if (digit.Length == 2) output += "1";
                else if (digit.Length == 3) output += "7";
                else if (digit.Length == 4) output += "4";
                else if (digit.Length == 5)
                {
                    if (digit.Intersect(one).Count() == 2) output += "3";
                    else if (digit.Intersect(four).Count() == 3) output += "5";
                    else output += "2";
                }
                else if (digit.Length == 6)
                {
                    if (digit.Intersect(four).Count() == 4) output += "9";
                    else if (digit.Intersect(one).Count() == 2) output += "0";
                    else output += "6";
                }
                else output += "8";
            }

            sum += Int32.Parse(output);
        }

        Utils.WriteResults($"Puzzle 2: {sum}");
    }
}