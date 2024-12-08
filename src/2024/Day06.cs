namespace AdventOfCode2024;

using AdventOfCode;

internal class Day06 : PuzzleBase
{
    private string[] _data;

    private List<string> Obstacles = new() { "#", "O" };
    private string VisitedSymbol = '\u2713'.ToString();
    
    private string[,] _map;
    private int _mapHeight;
    private int _mapWidth;
    
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

        _mapHeight = _data.Length;
        _mapWidth = _data[0].Length;
        
        var visited = Puzzle1();
        Puzzle2(visited);
    }

    private HashSet<(int, int)> Puzzle1()
    {
        LoadMap();
        //PrintMap("** Part 1: Initialized **");

        (HashSet<(int row, int col)> visited, bool _) result = WalkTheMap();
        //PrintMap("Part 1: Result");
        
        Utils.WriteResults($"Puzzle 1: {result.visited.Count}");
        return result.visited;
    }
    
    private void Puzzle2(HashSet<(int row, int col)> visited)
    {
        HashSet<(int row, int col)> obstaclesAdded = new();

        var visitedCopy = new HashSet<(int row, int col)>(visited);
        
        // for each visited position
        foreach (var visitedCell in visitedCopy)
        {
            // for each adjacent cell
            foreach (var direction in _directions)
            {
                // load the map
                LoadMap();
                
                // place an obstacle in an adjacent cell (if valid)
                if (MoveIsWithinBounds(visitedCell.row, visitedCell.col, direction) && 
                    !Blocked(visitedCell.row, visitedCell.col, direction) &&
                    !(visitedCell.row + direction.row == _startingPosition.startRow && visitedCell.col + direction.col == _startingPosition.startCol))
                {
                    _map[visitedCell.row + direction.row, visitedCell.col + direction.col] = "O";
                }
                else
                {
                    continue;
                }
                
                //PrintMap($"** Obstacle ({visitedCell.row + direction.row}, {visitedCell.col + direction.col}): Start **");

                // walk the map and check for a loop
                (HashSet<(int row, int col)> _, bool loopDetected) result = WalkTheMap();
                //PrintMap($"** Obstacle ({visitedCell.row + direction.row}, {visitedCell.col + direction.col}): Result = {(result.loopDetected ? "LOOP" : "no loop")} **");

                if (result.loopDetected)
                {
                    obstaclesAdded.Add((visitedCell.row + direction.row, visitedCell.col + direction.col));
                }
            }
        }
        
        Utils.WriteResults($"Puzzle 2: {obstaclesAdded.Count}");
    }

    private void LoadMap()
    {
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

    private (HashSet<(int row, int col)> visited, bool loopDetected) WalkTheMap()
    {
        var inbound = true;
        var dir = 0;
        
        var row = _startingPosition.startRow;
        var col = _startingPosition.startCol;

        HashSet<(int, int)> visited = new();
        HashSet<(int row, int col, int dir)> turns = new();
        bool loopDetected = false;
        
        do
        {
            // add current position as visited
            visited.Add((row, col));
            _map[row, col] = VisitedSymbol;
            
            // move (if possible)
            if (MoveIsWithinBounds(row, col, _directions[dir]))
            {
                while (Blocked(row, col, _directions[dir]))
                {
                    // turn right
                    dir = (dir + 1) % 4;

                    if (turns.Contains((row, col, dir)))
                    {
                        loopDetected = true;
                    }
                    
                    turns.Add((row, col, dir));
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

            //PrintMap();
        } while (inbound && loopDetected == false);

        return (visited, loopDetected);
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
        
        return Obstacles.Contains(_map[newRow, newCol]);
    }

    private void PrintMap(string header = "")
    {
        Console.WriteLine(header);
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