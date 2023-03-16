namespace AdventOfCode2021;

using AdventOfCode;

internal class Day05 : PuzzleBase
{
    private string[] _data;
    
    private List<Line> _lines;
    private int xMax = 0;
    private int yMax = 0;
    private int[,] _grid;

    public Day05(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 05 - Hydrothermal Venture");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 5, "Day05test.txt")
            .GetInput(Year, 5)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        Init();
        MarkGridSimple();
            
        var result = DeterminePoints();
        Utils.WriteResults($"Puzzle 1: {result}");
    }

    private void Puzzle2()
    {
        Init();
        MarkGridComplex();

        var result = DeterminePoints();
        Utils.WriteResults($"Puzzle 2: {result}");
    }

    /// <summary>
    /// Initializes the data structures.
    /// </summary>
    private void Init()
    {
        _lines = new List<Line>(_data.Length);

        BuildLines();

        _grid = new int[++xMax, ++yMax];
    }

    /// <summary>
    /// Process the coordinate lines.
    /// </summary>
    private void BuildLines()
    {
        foreach (var row in _data)
        {
            var coordPair = row.Split(" -> ");

            var src = coordPair[0].Split(',');
            var x1 = Int32.Parse(src[0]);
            var y1 = Int32.Parse(src[1]);
            
            var dest = coordPair[1].Split(',');
            var x2 = Int32.Parse(dest[0]);
            var y2 = Int32.Parse(dest[1]);

            _lines.Add(new Line(new Point(x1, y1), new Point(x2, y2)));

            // check for max grid dimensions
            xMax = x1 > xMax ? x1 : xMax;
            xMax = x2 > xMax ? x2 : xMax;

            yMax = y1 > yMax ? y1 : yMax;
            yMax = y2 > yMax ? y2 : yMax;
        }
    }

    /// <summary>
    /// Only process horizontal/vertical lines.
    /// </summary>
    private void MarkGridSimple()
    {
        foreach (var line in _lines)
        {
            if (line.IsHorizontal)
            {
                MarkHorizontalLine(line);
            }
            else if (line.IsVertical)
            {
                MarkVerticalLine(line);
            }

            //PrintPrettyGrid(line);
        }
    }

    /// <summary>
    /// Process all lines.
    /// </summary>
    private void MarkGridComplex()
    {
        foreach (var line in _lines)
        {
            if (line.IsHorizontal)
            {
                MarkHorizontalLine(line);
            }
            else if (line.IsVertical)
            {
                MarkVerticalLine(line);
            }
            else
            {
                MarkDiagonalLine(line);
            }

            //PrintPrettyGrid(line);
        }
    }

    private void MarkHorizontalLine(Line line)
    {
        // normalize line direction
        int direction = line.Source.X < line.Destination.X ? 1 : -1;
        int src = direction == 1 ? line.Source.X : line.Destination.X;
        int dest = direction == 1 ? line.Destination.X : line.Source.X;

        for (int x = src; x <= dest; x++)
        {
            _grid[line.Source.Y, x]++;
        }
    }

    private void MarkVerticalLine(Line line)
    {
        // normalize line direction
        int direction = line.Source.Y < line.Destination.Y ? 1 : -1;
        int src = direction == 1 ? line.Source.Y : line.Destination.Y;
        int dest = direction == 1 ? line.Destination.Y : line.Source.Y;

        for (int y = src; y <= dest; y++)
        {
            _grid[y, line.Source.X]++;
        }
    }

    private void MarkDiagonalLine(Line line)
    {
        int xDirection = line.Source.X < line.Destination.X ? 1 : -1;
        int yDirection = line.Source.Y < line.Destination.Y ? 1 : -1;

        int length = Math.Abs(line.Source.X - line.Destination.X);

        for (int i = 0; i <= length; i++)
        {
            int xPosition = line.Source.X + (xDirection * i);
            int yPosition = line.Source.Y + (yDirection * i);

            _grid[yPosition, xPosition]++;
        }
    }

    /// <summary>
    /// Counts all of the grid points that have a value >= 2.
    /// </summary>
    /// <returns>Total</returns>
    private int DeterminePoints()
    {
        int count = 0;

        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                count += _grid[x, y] >= 2 ? 1 : 0;
            }
        }

        return count;
    }

    /// <summary>
    /// Prints the current grid status to the console for debugging.
    /// </summary>
    private void PrintPrettyGrid(Line line)
    {
        Console.Write($"\nLine: {line}");
        for (int x = 0; x < xMax; x++)
        {
            Console.WriteLine();
            for (int y = 0; y < yMax; y++)
            {
                Console.Write(_grid[x, y] == 0 ? "." : _grid[x, y]);
            }
        }
        Console.WriteLine();
    }
    
    private struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public bool HasZeroPoint => X == 0 || Y == 0;

        public override string ToString() => $"({X}, {Y})";
    }
    
    private class Line
    {
        public Point Source { get; private set; }

        public Point Destination { get; private set; }


        public Line(Point src, Point dest)
        {
            Source = src;
            Destination = dest;
        }

        /// <summary>
        /// Determines if the line segment runs horizontally or not.
        /// </summary>
        /// <returns>true if it does; false otherwise.</returns>
        public bool IsHorizontal => Source.Y == Destination.Y;

        /// <summary>
        /// Determines if the line segment runs vertically or not.
        /// </summary>
        /// <returns>true if it does; false otherwise.</returns>
        public bool IsVertical => Source.X == Destination.X;

        /// <summary>
        /// Determines if the source or destiantion has a 0 coordinate.
        /// </summary>
        public bool HasZeroPoint => Source.HasZeroPoint || Destination.HasZeroPoint;

        /// <summary>
        /// Creates a printable representation of the line segment.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Source} -> {Destination}";
    }
}