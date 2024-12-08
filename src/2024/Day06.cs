namespace AdventOfCode2024;

using AdventOfCode;

internal class Day06 : PuzzleBase
{
    private string[] _data;

    private const string Obstruction = "#";
    private string VisitedSymbol = '\u2713'.ToString();
    
    private string[,] _map;
    private int _mapHeight;
    private int _mapWidth;
    
    private HashSet<(int, int)> _visited = new();
    
    private (int startRow, int startCol) _startingPosition;
    
    private List<(int row, int col)> _directions = new()
    {
        (-1, 0), // up
        (0, 1), // right
        (1, 0), // down
        (0, -1) // left
    };

    private string[] _dirSymbols =
    {
        '\u2191'.ToString(),  // up
        '\u2192'.ToString(),  // right
        '\u2193'.ToString(),  // down
        '\u2190'.ToString(),  // left
    };
    
    public Day06(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 06 - Guard Gallivant");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 6, "test.txt")
            .GetInput(Year, 6)
            .ConfigureAwait(false);

        LoadMap();
        // PrintMap();
        
        Puzzle1();
        //Puzzle2();
    }

    private void Puzzle1()
    {
        var inbound = true;
        var dir = 0;
        
        var row = _startingPosition.startRow;
        var col = _startingPosition.startCol;

        do
        {
            // add current position as visited
            _visited.Add((row, col));
            _map[row, col] = VisitedSymbol;
            
            // move (if possible)
            if (MoveIsWithinBounds(row, col, _directions[dir]))
            {
                while (Blocked(row, col, _directions[dir]))
                {
                    // turn right
                    dir = (dir + 1) % 4;
                }
                
                // move
                row += _directions[dir].row;
                col += _directions[dir].col;
                
                _map[row, col] = _dirSymbols[dir];
            }
            else
            {
                inbound = false;
            }

            // PrintMap();
        } while (inbound);
        
        Utils.WriteResults($"Puzzle 1: {_visited.Count}");
    }

    private void Puzzle2()
    {
        Utils.WriteResults($"Puzzle 2: ");
    }

    private void LoadMap()
    {
        _mapHeight = _data.Length;
        _mapWidth = _data[0].Length;
        
        _map = new string[_mapHeight, _mapWidth];
        
        for (var r = 0; r < _mapHeight; r++)
        {
            for (var c = 0; c < _mapWidth; c++)
            {
                var cell = _data[r].Substring(c, 1);
                _map[r, c] = cell;

                if (cell == "^")
                {
                    _map[r, c] = _dirSymbols[0];
                    _startingPosition = (r, c);
                }
            }
        }
    }

    private bool MoveIsWithinBounds(int row, int col, (int row, int col) direction)
    {
        var newRow = row + direction.row;
        var newCol = col + direction.col;

        return newRow >= 0 && newRow < _mapHeight && newCol >= 0 && newCol < _mapWidth;
    }

    private bool Blocked(int row, int col, (int row, int col) direction)
    {
        var newRow = row + direction.row;
        var newCol = col + direction.col;
        
        return _map[newRow, newCol] == Obstruction;
    }

    private void PrintMap()
    {
        Console.WriteLine();
        for (var r = 0; r < _mapHeight; r++)
        {
            for (var c = 0; c < _mapWidth; c++)
            {
                Console.Write($"{_map[r, c]} ");
            }

            Console.WriteLine();
        }
        Console.WriteLine();
    }
}