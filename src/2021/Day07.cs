namespace AdventOfCode2021;

using AdventOfCode;

internal class Day07 : PuzzleBase
{
    private string[] _data;

    public Day07(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 07 - Whales");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 7, "Day07test.txt")
            .GetInput(Year, 7)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int[] input = Init();

        int min = input.Min();
        int max = input.Max();
        int[] fuelCosts = new int[max - min];

        for (int i = min; i < max; i++)
        {
            fuelCosts[i] = input.Select(j => Math.Abs(i - j)).Sum();
        }

        Utils.WriteResults($"Puzzle 1: {fuelCosts.Min()}");
    }

    private void Puzzle2()
    {
        int[] input = Init();

        int min = input.Min();
        int max = input.Max();
        int[] fuelCosts = new int[max - min];

        for (int i = min; i < max; i++)
        {
            fuelCosts[i] = input
                .Select(j => {
                    int steps = Math.Abs(i - j);
                    return (steps * (steps + 1)) / 2;
                })
                .Sum();
        }

        Utils.WriteResults($"Puzzle 2: {fuelCosts.Min()}");
    }
    
    private int[] Init()
    {
        return _data[0].Split(',').Select(x => Int32.Parse(x)).ToArray();
    }
}