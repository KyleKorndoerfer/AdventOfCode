namespace AdventOfCode2021;

using AdventOfCode;

internal class Day01 : PuzzleBase
{
    private string[] _data;

    public Day01(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 01 - Sonar Sweep");
    }

    public override async Task Run()
    {
        _data = await Downloader
            .GetInput(Year, 1, "Day01test.txt")
            //.GetInput(Year, 1)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }
    
    private void Puzzle1()
    {
        int count = 0;
        for (int i = 1; i < _data.Length; i++)
        {
            int previous = Int32.Parse(_data[i - 1]);
            int current = Int32.Parse(_data[i]);

            //Utils.WriteDebug($"{current} > {previous}? {current > previous}");
            if (current > previous)
            {
                count++;
            }
        }

        Utils.WriteResults($"Puzzle 1 Result: {count}");
    }

    private void Puzzle2()
    {
        int count = 0;
        for(int i = 0; i < _data.Length - 3; i++)
        {
            int block1 = Int32.Parse(_data[i]) + Int32.Parse(_data[i + 1]) + Int32.Parse(_data[i + 2]);
            int block2 = Int32.Parse(_data[i + 1]) + Int32.Parse(_data[i + 2]) + Int32.Parse(_data[i + 3]);

            //Utils.WriteDebug($"{i} - {block1} > {block2}? {block1 > block2}");
            if (block2 > block1)
            {
                count++;
            }
        }

        Utils.WriteResults($"Puzzle 2 Result: {count}");
    }
}