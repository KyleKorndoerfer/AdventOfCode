namespace AdventOfCode2023;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day05 : PuzzleBase
{
    private string[] _data;

    public Day05(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 05 - If You Give A Seed A Fertilizer");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 5, "test.txt")
            .GetInput(Year, 5)
            .ConfigureAwait(false);

        Puzzle1();
        // TODO: will take hours to run based on current approach
        // can try threading each range, but will still be very slow
        // help: https://www.reddit.com/r/adventofcode/comments/18b4b0r/comment/kc2mxc6/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
        Puzzle2();  
    }

    public void Puzzle1()
    {
        var sourceSeeds = DigitsExp.Matches(_data[0].Split(":")[1]).Select(m => long.Parse(m.Value));
        var mappers = LoadMaps();
        
        var lowestLocation = long.MaxValue;
        foreach (var seed in sourceSeeds)
        {
            var destination = seed;
            foreach (var map in mappers)
            {
                destination = map.MapToDestination(destination);
            }

            lowestLocation = Math.Min(lowestLocation, destination);
        }
        
        Utils.WriteResults($"Puzzle 1: {lowestLocation}");
    }

    public void Puzzle2()
    {
        var seeds = DigitsExp.Matches(_data[0].Split(":")[1]).Select(m => long.Parse(m.Value)).ToList();
        List< (long, long)> seedPairs = new();
        for (int i = 0; i < seeds.Count; i += 2)
        {
            seedPairs.Add((seeds[i], seeds[i] + seeds[i+1]));
        }
        
        var mappers = LoadMaps();
        
        var lowestLocation = long.MaxValue;
        foreach ((long start, long end) seedPair in seedPairs)
        {
            for (var i = seedPair.start; i < seedPair.end; i++)
            {
                var destination = i;
                foreach (var map in mappers)
                {
                    destination = map.MapToDestination(destination);
                }

                lowestLocation = Math.Min(lowestLocation, destination);   
            }
        }
        
        Utils.WriteResults($"Puzzle 2: {lowestLocation}");
    }

    private Regex DigitsExp = new(@"(\d)+");
    private List<Mapper> LoadMaps()
    {
        List<Mapper> maps = new();

        Mapper map = new();
        for (int i = 3; i < _data.Length; i++)
        {
            if (string.IsNullOrEmpty(_data[i]))
            {
                maps.Add(map);
                map = new();
                
                continue;
            }

            var matches = DigitsExp.Matches(_data[i]);
            if (matches.Count == 0)
            {
                continue;
            }
            
            map.AddRange(matches[0].Value, matches[1].Value, matches[2].Value);
        }
        maps.Add(map);

        return maps;
    }
    
    class Mapper
    {
        private List<Range> Ranges { get; set; } = new();

        public void AddRange(string destinationEnd, string sourceEnd, string length)
        {
            Ranges.Add(new Range(
                long.Parse(destinationEnd), 
                long.Parse(sourceEnd), 
                long.Parse(length)));
        }
        
        public long MapToDestination(long start)
        {
            var range = Ranges.FirstOrDefault(r => start >= r.SourceStart && start <= r.SourceEnd);
            
            return range != null 
                    ? range.DestinationStart + Math.Abs(range.SourceStart - start)
                    : start;
        }
    }

    class Range
    {
        public long DestinationStart { get; set; }
        public long Length { get; set; }
        public long SourceEnd => SourceStart + Length - 1;
        public long SourceStart { get; set; }

        public Range(long destinationStart, long sourceStart, long length)
        {
            DestinationStart = destinationStart;
            SourceStart = sourceStart;
            Length = length;
        }
    }
}