namespace AdventOfCode2020;

using System.Linq;

using AdventOfCode;

internal class Day05 : PuzzleBase
{
    private string[] _data;

    public Day05(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 5 - Binary Boarding");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 5, "test.txt")
            .GetInput(Year, 5)
            .ConfigureAwait(false);

        var seats = new List<double>();
        foreach (var input in _data)
        {
            var row = GetIndexes(Reverse(input.Substring(0, 7)), "B").Sum(x => Math.Pow(2, x));
            var col = GetIndexes(Reverse(input.Substring(7, input.Length - 7)), "R").Sum(x => Math.Pow(2, x));
            
            seats.Add(row * 8 + col);
        }
        
        Puzzle1(seats);
        Puzzle2(seats);
    }

    private void Puzzle1(IList<double> seats)
    {
        Utils.WriteResults($"Puzzle 1: {seats.Max()}");
    }

    private void Puzzle2(IList<double> seats)
    {
        var orderedSeats = seats.Order().ToList();
        double seatNumber = 0;
        for (var i = 1; i < orderedSeats.Count(); i++)
        {
            if ((orderedSeats[i] - orderedSeats[i-1]) > 1)
            {
                seatNumber = orderedSeats[i] - 1;
                break;
            }
        }
        
        Utils.WriteResults($"Puzzle 2: {seatNumber}");
    }

    private string Reverse(string input)
    {
        var charArray = input.ToCharArray();
        Array.Reverse( charArray );
        return new string( charArray );
    }
    
    private static int[] GetIndexes(string input, string pattern)
    {
        var indexes = new List<int>();
        int index = 0;

        while ((index = input.IndexOf(pattern, index)) != -1)
        {
            indexes.Add(index++);
        }

        return indexes.ToArray();
    }
}