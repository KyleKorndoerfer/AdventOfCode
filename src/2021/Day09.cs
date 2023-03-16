namespace AdventOfCode2021;

using AdventOfCode;

internal class Day09 : PuzzleBase
{
    private string[] _data;

    private int rows = 0;
    private int cols = 0;
    private int[,] grid;
    
    public Day09(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 09 - Smoke Basin");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 9, "Day09test.txt")
            .GetInput(Year, 9)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        Init();
            
        int riskPoints = 0;
        List<Point> lowPoints = FindLowPoints();

        foreach (var point in lowPoints)
        {
            riskPoints += grid[point.X, point.Y] + 1;
        }

        Utils.WriteResults($"Puzzle 1: {riskPoints} risk points");
    }

    private void Puzzle2()
    {
        Init();

        List<Point> lowPoints = FindLowPoints();
        List<int> basinSizes = new();

        foreach (var point in lowPoints)
        {
            basinSizes.Add(TraverseBasin(point.X, point.Y));
        }

        // find 3 largest
        basinSizes.Sort();
        int result = basinSizes
            .Skip(basinSizes.Count - 3)
            .Take(3)
            .Aggregate((x, y) => x * y);

        Utils.WriteResults($"Puzzle 2: {result}");
    }
    
    private void Init()
    {
        rows = _data.Length;
        cols = _data[0].Length;
        grid = new int[rows, _data[0].Length];

        for (int x = 0; x < _data.Length; x++)
        {
            for (int y = 0; y < _data[x].Length; y++)
            {
                grid[x, y] = Int32.Parse(_data[x].Substring(y, 1));
            }
        }
    }

    private List<Point> FindLowPoints()
    {
        List<Point> lowPoints = new List<Point>();

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                bool up = x - 1 >= 0 && grid[x - 1, y] <= grid[x, y];
                bool down = x + 1 < rows && grid[x + 1, y] <= grid[x, y];
                bool left = y - 1 >= 0 && grid[x, y - 1] <= grid[x, y];
                bool right = y + 1 < cols && grid[x, y + 1] <= grid[x, y];

                if (up || down || left || right) continue;

                lowPoints.Add(new Point(x, y));
            }
        }

        return lowPoints;
    }

    // recursive
    private int TraverseBasin(int x, int y)
    {
        // stop if point is a 9
        if (grid[x, y] == 9)
        {
            return 0;
        }

        int sum = 1;    // add the curent point
        int currentPointValue = grid[x, y];

        // mark current point as no longer traversable so we don't go over it again
        grid[x, y] = 9;

        // can we move up?
        if (x - 1 >= 0 && grid[x - 1, y] > currentPointValue)
        {
            sum += TraverseBasin(x - 1, y);
        }

        // can we move down?
        if (x + 1 < rows && grid[x + 1, y] > currentPointValue)
        {
            sum += TraverseBasin(x + 1, y);
        }

        // can we move left?
        if (y - 1 >= 0 && grid[x, y - 1] > currentPointValue)
        {
            sum += TraverseBasin(x, y - 1);
        }

        // can we move right?
        if (y + 1 < cols && grid[x, y + 1] > currentPointValue)
        {
            sum += TraverseBasin(x, y + 1);
        }

        return sum;
    }

    private void PrintGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            Console.WriteLine();
            for (int y = 0; y < cols; y++)
            {
                Console.Write(grid[x, y]);
            }
        }
    }
    
    private struct Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}