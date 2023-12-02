namespace AdventOfCode2023;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day01 : PuzzleBase
{
    private readonly string ExpandedRegex = "(?=([0-9]|zero|one|two|three|four|five|six|seven|eight|nine))";
    private readonly Dictionary<string, string> Numbers = new() {
        {"zero", "0"},
        {"one", "1"},
        {"two", "2"},
        {"three", "3"},
        {"four", "4"},
        {"five", "5"},
        {"six", "6"},
        {"seven", "7"},
        {"eight", "8"},
        {"nine", "9"}
    };

    private string[] _data;

    public Day01(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 01 - Trebuchet?!");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 1, "test.txt")
            //.GetInput(Year, 1, "test2.txt")
            .GetInput(Year, 1)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        List<int> calibrations = _data
            .Select(line => Regex.Matches(line, "[0-9]"))
            .Where(matches => matches.Count > 0)
            .Select(matches => Convert.ToInt32($"{matches.First()}{matches.Last()}"))
            .ToList();
        
        Utils.WriteResults($"Puzzle 1: {calibrations.Sum()}");
    }
    
    // NOTE: This part requires understanding overlapping groups; "twone" = 'two' & 'one'
    private void Puzzle2()
    {
        List<int> calibrations = new();
        foreach (var line in _data)
        {
            var matches = Regex.Matches(line, ExpandedRegex);
            var firstMatch = Numbers.TryGetValue(matches.First().Groups[1].Value, out var number)
                    ? number
                    : matches.First().Groups[1].Value;
            var lastMatch = Numbers.TryGetValue(matches.Last().Groups[1].Value, out var number1)
                    ? number1
                    : matches.Last().Groups[1].Value;
            
            calibrations.Add(Convert.ToInt32($"{firstMatch}{lastMatch}"));
        }
        
        Utils.WriteResults($"Puzzle 2: {calibrations.Sum()}");
    }
}