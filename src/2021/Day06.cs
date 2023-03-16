namespace AdventOfCode2021;

using AdventOfCode;

internal class Day06 : PuzzleBase
{
    private string[] _data;

    Int64[] _spawnCycles;
    
    public Day06(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 06 - Lanternfish");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 6, "Day06test.txt")
            .GetInput(Year, 6)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        Init();
        int days = 80;

        Run(days);

        //DisplaySpawnCycles();
        Utils.WriteResults($"Puzzle 1: {SumOfFish()}");
    }

    private void Puzzle2()
    {
        Init();
        int days = 256;

        Run(days);

        //DisplaySpawnCycles();
        Utils.WriteResults($"Puzzle 2: {SumOfFish()}");
    }
    
    private void Init()
    {
        _spawnCycles = new Int64[9]; // 0 - 8 inclusive

        foreach (int i in _data[0].Split(',').Select(x => Int32.Parse(x)))
        {
            _spawnCycles[i]++;
        }
    }

    private void Run(int days)
    {
        for (int i = 1; i <= days; i++)
        {
            // these fish are spawning new fish
            Int64 newfish = _spawnCycles[0];

            // shift spawning generations
            for (int j = 1; j <= 8; j++)
            {
                _spawnCycles[j - 1] = _spawnCycles[j];
            }

            // add fish that just spawned to the 6th cycle day
            _spawnCycles[6] += newfish;

            // add newly spawned fish
            _spawnCycles[8] = newfish;
        }
    }

    private void DisplaySpawnCycles()
    {
        Console.WriteLine();
        for (int i = 0; i < _spawnCycles.Length; i++)
        {
            Utils.WriteDebug($"Cycle {i}: {_spawnCycles[i]}");
        }
    }

    private Int64 SumOfFish()
    {
        Int64 sum = 0;

        foreach (var f in _spawnCycles)
        {
            sum += f;
        }

        return sum;
    }
}