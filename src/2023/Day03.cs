namespace AdventOfCode2023;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day03 : PuzzleBase
{
    private string[] _data;

    public Day03(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 03 - Gear Ratios");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 3, "test.txt")
            .GetInput(Year, 3)
            .ConfigureAwait(false);

        var symbols = new List<Symbol>();
        var numbers = new List<Number>();

        var numberRegex = new Regex(@"(\d)+");
        var symbolRegex = new Regex(@"[^\.\d\n]");

        var lineCount = 0;
        foreach (var line in _data)
        {
            // find number positions
            foreach (Match numMatch in numberRegex.Matches(line))
            {
                numbers.Add(new Number(
                    Value: int.Parse(numMatch.Value),
                    Pos: new Position(lineCount, numMatch.Index, numMatch.Index + numMatch.Length - 1)));
            }
            
            // find symbol positions
            foreach (Match symbolMatch in symbolRegex.Matches(line))
            {
                symbols.Add(new Symbol(
                    Value: symbolMatch.Value,
                    Pos: new Position(lineCount, symbolMatch.Index, symbolMatch.Index)));
            }

            lineCount++;
        }

        // sum parts
        var partSum = numbers
                .Where(n => symbols.Any(s => s.Pos.IsNearby(n)))
                .Sum(n => n.Value);
        
        Utils.WriteResults($"Puzzle 1: {partSum}");
        
        // sum gear ratios
        var gearSums = symbols
                .Where(s => s.Value is "*")
                .Sum(g =>
                {
                    var nearbyNumbers = numbers
                        .Where(n => g.Pos.IsNearby(n))
                        .ToList();
                    return nearbyNumbers.Count == 2
                        ? nearbyNumbers.Aggregate(1, (acc, n) => acc * n.Value)
                        : 0;
                });

        Utils.WriteResults($"Puzzle 2: {gearSums}");
    }

    record Position(int Row, int Start, int End)
    {
        public bool IsNearby(Number number)
        {
            // directly nearby
            if (Row < number.Pos.Row - 1 || Row > number.Pos.Row + 1)
            {
                return false;
            }

            // indirectly nearby
            return Start >= number.Pos.Start - 1 && Start <= number.Pos.End + 1;
        }
    };

    record Symbol(string Value, Position Pos);
    record Number(int Value, Position Pos);
}