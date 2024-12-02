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

        Solve();
    }

    private void Solve()
    {
        var safe = 0;
        var dampenerSafe = 0;
        
        foreach (var line in _data)
        {
            var levels = line.Split(' ').Select(int.Parse).ToList();
            var diffs = GetDifferences(levels);

            if (IsSafe(diffs))
            {
                safe++;
            }
            else if (IsDampenerSafe(levels.ToArray()))
            {
                dampenerSafe++;
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {safe}");
        Utils.WriteResults($"Puzzle 2: {safe + dampenerSafe}");
    }

    private static List<int> GetDifferences(IList<int> levels)
    {
        var diffs = new List<int>(levels.Count - 1);
        
        for (var i = 0; i < levels.Count - 1; i++)
        {
            diffs.Add(levels[i] - levels[i + 1]);
        }

        return diffs;
    }
    
    private static bool IsSafe(IList<int> diffs)
    {
        return diffs.All(d => d is > 0 and < 4) || diffs.All(d => d is < 0 and > -4);
    }

    private static bool IsDampenerSafe(int[] levels)
    {
        for (var i = 0; i < levels.Length; i++)
        {
            var prunedList = levels.ToList();
            prunedList.RemoveAt(i);
            
            var diffs = GetDifferences(prunedList);
            if (IsSafe(diffs))
            {
                return true;
            }
        }

        return false;
    }
}