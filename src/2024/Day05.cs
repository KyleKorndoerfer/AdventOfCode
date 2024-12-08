namespace AdventOfCode2024;

using AdventOfCode;

internal class Day05 : PuzzleBase
{
    private string[] _data;

    private readonly HashSet<(int, int)> _rules = new();
    private int _updatesStartingRow;
    
    public Day05(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 05 - Print Queue");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 5, "test.txt")
            .GetInput(Year, 5)
            .ConfigureAwait(false);

        LoadRules();
        (var validSum, var invalidSum) = ProcessReports();
        
        Utils.WriteResults($"Puzzle 1: {validSum}");
        Utils.WriteResults($"Puzzle 2: {invalidSum}");
    }

    private void LoadRules()
    {
        for (var i = 0; i < _data.Length; i++)
        {
            if (string.IsNullOrEmpty(_data[i]))
            {
                _updatesStartingRow = i + 1;
                break;
            }
            
            var split = _data[i].Split('|').Select(Int32.Parse).ToArray();
            _rules.Add((split[0], split[1]));
        }
    }

    private (int validSum, int invalidSum) ProcessReports()
    {
        var validSum = 0;
        var invalidSum = 0;
        
        for (var i = _updatesStartingRow; i < _data.Length; i++)
        {
            var pages = _data[i].Split(',').Select(Int32.Parse).ToArray();

            var valid = pages.Zip(pages[1..], (x, y) => _rules.Contains((x, y))).All(r => r);

            if (valid)
            {
                validSum += pages[pages.Length / 2];
            }
            else
            {
                Array.Sort(pages, (x, y) => _rules.Contains((x, y)) ? -1 : 1);
                invalidSum += pages[pages.Length / 2];
            }
        }
        
        return (validSum, invalidSum);
    }
}