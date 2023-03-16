namespace AdventOfCode2021;

using AdventOfCode;

internal class Day11 : PuzzleBase
{
    private string[] _data;
    
    private Point[,] _grid;     // for easy coordinate navigation
    private List<Point> _pointList; // for easy scanning


    public Day11(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 11 - Dumbo Octopus");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 11, "Day11test.txt")
            .GetInput(Year, 11)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        const int steps = 100;

        Init();
        //PrintGrid();

        uint flashes = 0;

        for (int i = 0; i < steps; i++)
        {
            _pointList.ForEach(p => { p.AddEnergy(); });
            //Utils.WriteDebug($"\nAfter updating energy levels:");
            //PrintGrid();

            while (_pointList.Any(p => p.Value > 9))
            {
                flashes++;  // increase count
                    
                Point p = _pointList.Where(p => p.Value > 9).First();
                p.Flash();
                UpdateNeighbors(p.X, p.Y);

                //Utils.WriteDebug($"After updating {p}'s neighbors");
                //PrintGrid();
            }

            // reset flash points
            _pointList.ForEach(p => { p.ResetFlash(); });
        }

        Utils.WriteResults($"Puzzle 1: {flashes} flashes");
    }

    private void Puzzle2()
    {
        Init();

        int steps = 0;

        do
        {
            _pointList.ForEach(p => { p.AddEnergy(); });

            while (_pointList.Any(p => p.Value > 9))
            {
                Point p = _pointList.Where(p => p.Value > 9).First();
                p.Flash();
                UpdateNeighbors(p.X, p.Y);
            }

            // reset flash points
            _pointList.ForEach(p => { p.ResetFlash(); });

            steps++;
        } while (!_pointList.All(p => p.Value == 0));

        Utils.WriteResults($"Puzzle 2: {steps} steps");
    }
    
    /// <summary>
    /// Flash found at this point; update neightbors energy levels
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void UpdateNeighbors(int x, int y)
    {
        // upper-left?
        if (x - 1 >= 0 && y - 1 >= 0) _grid[x - 1, y - 1].AddEnergy();
        // up?
        if (x - 1 >= 0) _grid[x - 1, y].AddEnergy();
        // upper-right?
        if (x - 1 >= 0 && y + 1 < 10) _grid[x - 1, y + 1].AddEnergy();
        // left?
        if (y - 1 >= 0) _grid[x, y - 1].AddEnergy();
        // right?
        if (y + 1 < 10) _grid[x, y + 1].AddEnergy();
        // lower-left
        if (x + 1 < 10 && y - 1 >= 0) _grid[x + 1, y - 1].AddEnergy();
        // down
        if (x + 1 < 10) _grid[x + 1, y].AddEnergy();
        // lower-right
        if (x + 1 < 10 && y + 1 < 10) _grid[x + 1, y + 1].AddEnergy();
    }

    private void Init()
    {
        _grid = new Point[10, 10];
        _pointList = new List<Point>(100);

        for (int x = 0; x < _data.Length; x++)
        {
            for (int y = 0; y < _data[x].Length; y++)
            {
                Point p = new(x, y, Int32.Parse(_data[x][y].ToString()));

                _grid[x, y] = p;
                _pointList.Add(p);
            }
        }
    }

    private void PrintGrid()
    {
        for (int x = 0; x < 10; x++)
        {
            Console.WriteLine();
            for (int y = 0; y < 10; y++)
            {
                Console.Write($"{_grid[x, y].Value, 3}");
            }
        }

        Console.WriteLine();
    }
    
    private class Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public bool HasFlashed { get; private set; }
        public int Value { get; private set; }

        public Point(int x, int y, int value)
        {
            X = x; 
            Y = y;
            Value = value;
        }

        // adds energy to the point if it hasn't already flashed
        public void AddEnergy()
        {
            if (!HasFlashed)
            {
                Value++;
            }
        }

        public void Flash()
        {
            HasFlashed = true;
            Value = 0;
        }

        public void ResetFlash()
        {
            HasFlashed = false;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}