namespace AdventOfCode2020;

using AdventOfCode;

internal class Day03 : PuzzleBase
{
    private string[] _data;

    public Day03(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 3 - Toboggan Trajectory");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 3, "test.txt")
            .GetInput(Year, 3)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        Utils.WriteResults($"Puzzle 1: {Traverse(1, 3)}");
    }
    
    private void Puzzle2()
    {
        var slopes = new List<(int, int)> { (1, 1), (1, 3), (1, 5), (1, 7), (2, 1) };
        var result = slopes
                .Select(slope => Traverse(slope.Item1, slope.Item2))
                .Aggregate((x, y) => x * y);
        
        Utils.WriteResults($"Puzzle 2: {result}");
    }

    private int Traverse(int rowIncrement, int colIncrement)
    {
        var row = 0;
        var col = 0;
        var trees = 0;
        
        while (row < _data.Length)
        {
            trees += _data[row].Substring(col, 1) == "#" ? 1 : 0; 
            
            // move to next point
            row += rowIncrement;
            col = (col + colIncrement) % _data[0].Length;
        }

        return trees;
    }
}