namespace AdventOfCode2024;

using AdventOfCode;

internal class Day02 : PuzzleBase
{
    private string[] _data;
    
    public Day02(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 2 - Red-Nosed Reports");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 2, "test.txt")
            .GetInput(Year, 2)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int safe = 0;
        foreach (var line in _data)
        {
            var levels = line.Split(' ').Select(int.Parse).ToArray();
            var diffs = new int[levels.Length-1];
            
            for (int i = 0; i < diffs.Length; i++)
            {
                diffs[i] = levels[i] - levels[i + 1];
            }

            if (diffs.All(d => d is > 0 and < 4) || diffs.All(d => d is < 0 and > -4))
            {
                safe++;
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {safe}");
    }
    
    private void Puzzle2()
    {
        Utils.WriteResults($"Puzzle 2: ");
    }
}