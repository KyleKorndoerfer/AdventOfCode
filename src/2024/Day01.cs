namespace AdventOfCode2024;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day01 : PuzzleBase
{
    private string[] _data;

    readonly List<int> _left = new();
    readonly List<int> _right = new();

    public Day01(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 01 - Historian Hysteria");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 1, "test.txt")
            .GetInput(Year, 1)
            .ConfigureAwait(false);

        LoadLists();

        long distance = _left.Select((l, idx) => Math.Abs(l - _right[idx])).Sum();
        Utils.WriteResults($"Puzzle 1: {distance}");
        
        long similarity = _left.Sum(l => l * _right.Count(r => r == l));
        Utils.WriteResults($"Puzzle 2: {similarity}");
    }

    private void LoadLists()
    {
        var reg = new Regex(@"(\d+)\s*(\d+)");
        foreach (var line in _data)
        {
            var m = reg.Match(line);
            _left.Add(Int32.Parse(m.Groups[1].Value));
            _right.Add(Int32.Parse(m.Groups[2].Value));
        }
        
        _left.Sort();
        _right.Sort();
    }
}