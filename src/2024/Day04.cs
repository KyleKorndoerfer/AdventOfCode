namespace AdventOfCode2024;

using AdventOfCode;

internal class Day04 : PuzzleBase
{
    private const string FindWord = "XMAS";
    
    private string[] _data;
    private string[,] _grid;
    private int _rows;
    private int _cols;
    
    private readonly List<(int, int)> _directions = new()
    {
        (0, 1),     // right
        (0, -1),    // left
        (1, 0),     // down
        (-1, 0),    // up
        (1, 1),     // diagonal down-right
        (1, -1),    // diagonal down-left
        (-1, -1),   // diagonal up-left
        (-1, 1)     // diagonal up-right
    };
    
    public Day04(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 04 - Ceres Search");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 4, "test.txt")
            .GetInput(Year, 4)
            .ConfigureAwait(false);

        _rows = _data.Length;
        _cols = _data[0].Length;
        
        _grid = new string[_data.Length, _data[0].Length];
        LoadMatrix();
        
        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        var count = 0;
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _cols; c++)
            {
                foreach (var (rowDir, colDir) in _directions)
                {
                    if (CheckDirection(r, c, rowDir, colDir))
                    {
                        count++;
                    }
                }
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {count}");
    }

    private void Puzzle2()
    {
        List<string> validCombos = new() { "MMSS", "SMMS", "SSMM", "MSSM" };
        
        var count = 0;
        for (var r = 1; r < _rows - 1; r++)
        {
            for (var c = 1; c < _cols - 1; c++)
            {
                var upperLeft = _grid[r - 1, c - 1];
                var upperRight = _grid[r - 1, c + 1];
                var lowerLeft = _grid[r + 1, c - 1];
                var lowerRight = _grid[r + 1, c + 1];
                
                if (_grid[r, c] == "A" && validCombos.Contains($"{upperLeft}{upperRight}{lowerRight}{lowerLeft}"))
                {
                    count++;
                }
            }
        }
        
        Utils.WriteResults($"Puzzle 2: {count}");
    }

    private void LoadMatrix()
    {
        for (var r = 0; r < _data.Length; r++)
        {
            for (var c = 0; c < _data[0].Length; c++)
            {
                _grid[r, c] = _data[r].Substring(c, 1);
            }
        }
    }

    private bool CheckDirection(int startRow, int startCol, int rowDir, int colDir)
    {
        for (var i = 0; i < FindWord.Length; i++)
        {
            var newRow = startRow + i * rowDir;
            var newCol = startCol + i * colDir;

            if (newRow < 0 || newRow >= _rows || newCol < 0 || newCol >= _cols || _grid[newRow, newCol] != FindWord.Substring(i, 1))
            {
                return false;
            }
        }

        return true;
    }
}