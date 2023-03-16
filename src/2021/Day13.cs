namespace AdventOfCode2021;

using AdventOfCode;

internal class Day13 : PuzzleBase
{
    private string[] _data;
    
    private List<Point> _points;
    private List<string> _instructions;

    public Day13(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 13 - Transparent Origami");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 13, "Day13test.txt")
            .GetInput(Year, 13)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        Init();

        var temp = _instructions[0].Substring(_instructions[0].IndexOf('=') - 1).Split('=');
        var dir = temp[0];
        var foldLine = Int32.Parse(temp[1]);

        if (dir == "y")
        {
            FoldByRows(foldLine);
        }
        else
        {
            FoldByColumns(foldLine);
        }

        Utils.WriteResults($"Puzzle 1: {_points.Count} points");
    }

    private void Puzzle2()
    {
        Init();

        foreach (var instruction in _instructions)
        {
            var temp = instruction.Substring(instruction.IndexOf('=') - 1).Split('=');
            var dir = temp[0];
            var foldLine = Int32.Parse(temp[1]);

            if (dir == "y")
            {
                FoldByRows(foldLine);
            }
            else
            {
                FoldByColumns(foldLine);
            }
        }

        Utils.WriteResults("Puzzle 2:");
        ConvertPointsToGrid();
    }
    
    private void FoldByRows(int foldline)
        {
            var rowsAfterFold = _points.Where(p => p.X > foldline).ToList();

            foreach (var pt in rowsAfterFold)
            {
                int newRowNumber = foldline - (pt.X - foldline);

                var existingPoint = _points.Where(p => p.X == newRowNumber && p.Y == pt.Y).FirstOrDefault();
                if (existingPoint == null)
                {
                    _points.Add(new Point(newRowNumber, pt.Y));
                }

                _points.Remove(pt);
            }
        }

        private void FoldByColumns(int foldline)
        {
            var columnsAfterFold = _points.Where(p => p.Y > foldline).ToList();

            foreach (var pt in columnsAfterFold)
            {
                int newColumnNumber = foldline - (pt.Y - foldline);

                var existingPoint = _points.Where(p => p.Y == newColumnNumber && p.X == pt.X).FirstOrDefault();
                if (existingPoint == null)
                {
                    _points.Add(new Point(pt.X, newColumnNumber));
                }

                _points.Remove(pt);
            }
        }

        private void ConvertPointsToGrid()
        {
            int xMax = _points.Select(p => p.X).Max();
            int yMax = _points.Select(p => p.Y).Max();

            string[,] grid = new string[xMax + 1, yMax + 1];

            foreach (var p in _points)
            {
                grid[p.X, p.Y] = "#";
            }

            for (int x = 0; x <= xMax; x++)
            {
                Console.WriteLine();
                for (int y = 0; y <= yMax; y++)
                {
                    Console.Write(grid[x, y] == "#" ? "#" : " ");
                }
            }

            Console.WriteLine();
        }

        private void Init()
        {
            int i = 0;

            // read points
            _points = new List<Point>();
            
            do
            {
                var temp = _data[i].Split(',');
                Point p = new Point(Int32.Parse(temp[1]), Int32.Parse(temp[0]));
                _points.Add(p);

                i++;
            } while (!string.IsNullOrEmpty(_data[i]));

            i++; // skip empty line

            // read instructions
            _instructions = new List<string>(); 
            
            for (int j = i; j < _data.Length; j++)
            {
                _instructions.Add(_data[j]);
            }
        }
    
    private class Point
    {
        public int X { get; init; }
        public int Y { get; init; }

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