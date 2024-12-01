namespace AdventOfCode2024;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day01 : PuzzleBase
{
    private string[] _data;
    
    List<int> _list1 = new();
    List<int> _list2 = new();

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
        
        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        var sortedList1 = _list1.OrderBy(x => x).ToList();
        var sortedList2 = _list2.OrderBy(x => x).ToList();

        long distance = 0;
        for (var i = 0; i < _data.Length; i++)
        {
            distance += Math.Abs(sortedList1[i] - sortedList2[i]);
        }
        
        Utils.WriteResults($"Puzzle 1: {distance}");
    }
    
    private void Puzzle2()
    {
        long similarity = 0;
        for (var i = 0; i < _data.Length; i++)
        {
            similarity += _list2.Count(x => x == _list1[i]) * _list1[i];
        }
        
        Utils.WriteResults($"Puzzle 2: {similarity}");
    }

    private void LoadLists()
    {
        var reg = new Regex(@"(\d+)\s*(\d+)");
        foreach (var line in _data)
        {
            var m = reg.Match(line);
            _list1.Add(Int32.Parse(m.Groups[1].Value));
            _list2.Add(Int32.Parse(m.Groups[2].Value));
        }
    }
}