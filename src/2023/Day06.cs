namespace AdventOfCode2023;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day06 : PuzzleBase
{
    private string[] _data;

    private Regex DigitsExp = new(@"(\d)+");

    public Day06(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 06 - Wait For It");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 6, "test.txt")
            .GetInput(Year, 6)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    /*
     * find the first number (x) that beats the record
     * take the number of ms for the race and subtract x giving y
     * number of winning races = y - x + 1 (inclusive range)
     */
    private void Puzzle1()
    {
        List<int> durations = DigitsExp.Matches(_data[0]).Select(m => int.Parse(m.Value)).ToList();
        List<int> records = DigitsExp.Matches(_data[1]).Select(m => int.Parse(m.Value)).ToList();

        List<int> winners = new();
        for (var i = 0; i < durations.Count; i++)
        {
            for (var j = 1; j < durations[i]; j++)
            {
                var distance = (durations[i] - j) * j;
                if (distance > records[i])
                {
                    winners.Add( (durations[i] - j) - j + 1);
                    break;
                }
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {winners.Aggregate(1, (x, y) => x * y)}");
    }
    
    private void Puzzle2()
    {
        long duration = long.Parse(_data[0].Replace(" ", "").Split(":")[1]);
        long record = long.Parse(_data[1].Replace(" ", "").Split(":")[1]);

        long winners = 0;
        for (var j = 1; j < duration; j++)
        {
            var distance = (duration - j) * j;
            if (distance > record)
            {
                winners = (duration - j) - j + 1;
                break;
            }
        }
        
        Utils.WriteResults($"Puzzle 2: {winners}");
    }
}