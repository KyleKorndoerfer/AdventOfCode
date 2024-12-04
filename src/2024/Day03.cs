using System.Text.RegularExpressions;

namespace AdventOfCode2024;

using AdventOfCode;

internal class Day03 : PuzzleBase
{
    private const string MulPattern = @"mul\(\d{1,3},\d{1,3}\)";
    private const string OperandsPattern = @"\d{1,3}";
    private const string DoPattern = @"do\(\)";
    private const string DontPattern = @"don't\(\)";
        
    private string[] _data;

    public Day03(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 03 - Mull It Over");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 3, "test.txt")
            //.GetInput(Year, 3, "test2.txt")
            .GetInput(Year, 3)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        var sum = 0;
        foreach (var line in _data)
        {
            var matches = Regex.Matches(line, MulPattern);
            for (var i = 0; i < matches.Count; i++)
            {
                var numbers = Regex.Matches(matches[i].Value, OperandsPattern);
                sum += Int32.Parse(numbers[0].Value) * Int32.Parse(numbers[1].Value);
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {sum}");
    }

    private void Puzzle2()
    {
        var sum = 0;
        var enabled = true;
        foreach (var line in _data)
        {
            var matches = new List<Match>();
            matches.AddRange(Regex.Matches(line, MulPattern));
            matches.AddRange(Regex.Matches(line, DoPattern));
            matches.AddRange(Regex.Matches(line, DontPattern));
            
            foreach (var item in matches.OrderBy(m => m.Index).ToList())
            {
                if (item.Value == "do()")
                {
                    enabled = true;
                }
                else if (item.Value == "don't()")
                {
                    enabled = false;
                }
                else 
                {
                    if (enabled)
                    {
                        var numbers = Regex.Matches(item.Value, OperandsPattern);
                        sum += Int32.Parse(numbers[0].Value) * Int32.Parse(numbers[1].Value);
                    }
                }
            }
        }
        
        Utils.WriteResults($"Puzzle 2: {sum}");
    }
}