using System.Text.RegularExpressions;

namespace AdventOfCode2024;

using AdventOfCode;

internal class Day03 : PuzzleBase
{
    private string[] _data;

    public Day03(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 03 - Mull It Over");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 3, "test.txt")
            .GetInput(Year, 3)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        var finder = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
        var operands = new Regex(@"\d{1,3}");

        var sum = 0;
        foreach (var line in _data)
        {
            var matches = finder.Matches(_data[0]);
            for (var i = 0; i < matches.Count; i++)
            {
                var numbers = operands.Matches(matches[i].Value);
                sum += Int32.Parse(numbers[0].Value) * Int32.Parse(numbers[1].Value);
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {sum}");
    }

    private void Puzzle2()
    {
        Utils.WriteResults($"Puzzle 2: ");
    }
}