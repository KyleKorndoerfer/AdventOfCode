namespace AdventOfCode2020;

using AdventOfCode;

internal class Day06 : PuzzleBase
{
    private string[] _data;

    public Day06(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 6 - Custom Customs");
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
    
    private void Puzzle1()
    {
        List<HashSet<char>> groups = new();
        HashSet<char> chars = new();

        for (var i = 0; i <= _data.Length; i++)
        {
            if (i == _data.Length || string.IsNullOrEmpty(_data[i]))
            {
                groups.Add(chars);
                chars = new();
                continue;
            }
            
            foreach (var c in _data[i].ToCharArray())
            {
                chars.Add(c);
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {groups.Sum(x => x.Count)}");
    }
    
    private void Puzzle2()
    {
        List<int> counts = new();
        List<HashSet<char>> group = new();
        
        for (var i = 0; i <= _data.Length; i++)
        {
            if (i == _data.Length || string.IsNullOrEmpty(_data[i]))
            {
                if (group.Count == 1)
                {
                    counts.Add(group[0].Count);
                }
                else
                {
                    var intersection = group
                            .Skip(1)
                            .Aggregate(
                                group.First(), 
                                (h, e) => { h.IntersectWith(e); return h; });
                    counts.Add(intersection.Count);
                }

                group = new();
                continue;
            }
            
            group.Add(new HashSet<char>(_data[i].ToCharArray()));
        }
        
        
        Utils.WriteResults($"Puzzle 2: {counts.Sum()}");
    }
}