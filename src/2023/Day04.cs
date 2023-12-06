using System.Text.RegularExpressions;

namespace AdventOfCode2023;

using AdventOfCode;

internal class Day04 : PuzzleBase
{
    private string[] _data;
    private Regex digitsEx = new(@"(\d)+"); 

    public Day04(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 04 - Scratchcards");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 4, "test.txt")
            .GetInput(Year, 4)
            .ConfigureAwait(false);

        var cardCounts = new List<int>(_data.Length);
        for (var i = 0; i < _data.Length; i++)
        {
            cardCounts.Add(1);
        }

        var partOnePoints = new List<double>();
        var partTwoPoints = new List<double>();
        int counter = 0;
        foreach (var line in _data)
        {
            var sets = line.Split(":")[1].Trim().Split("|");

            var winningNumbers = digitsEx.Matches(sets[0]).Select(match => int.Parse(match.Value));
            var myNumbers = digitsEx.Matches(sets[1]).Select(match => int.Parse(match.Value));

            var matches = myNumbers.Intersect(winningNumbers).Count();

            if (matches > 0)
            {
                partOnePoints.Add(Math.Pow(2, matches - 1));
                
                for (int i = counter + 1; i <= Math.Min(counter + matches, _data.Length); i++)
                {
                    cardCounts[i] += cardCounts[counter];
                }
            }

            counter++;
        }
        
        Utils.WriteResults($"Puzzle 1: {partOnePoints.Sum()}");
        Utils.WriteResults($"Puzzle 2: {cardCounts.Sum()}");
    }
}