namespace AdventOfCode2021;

using AdventOfCode;

internal class Day04 : PuzzleBase
{
    private int[] _moves;
    private string[] _data;
    private List<Board> _boards;

    public Day04(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 04 - Giant Squid");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 4, "Day04test.txt")
            .GetInput(Year, 4)
            .ConfigureAwait(false);

        _boards = new List<Board>();
        _moves = _data[0].Split(',').Select(x => Int32.Parse(x)).ToArray();
        
        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        BuildBoards();
        int result = 0;

        // call numbers
        for (int i = 0; i < _moves.Length; i++)
        {
            foreach (var board in _boards)
            {
                board.MarkSquare(_moves[i]);

                if (board.IsWinner())
                {
                    result = board.SumOfUnmarkedSquares() * _moves[i];
                    break;
                }
            }

            if (result != 0) break;
        }

        Utils.WriteResults($"Puzzle 1: Score = {result}");
    }

    private void Puzzle2()
    {
        BuildBoards();
        int result = 0;
        int boardsLeft = _boards.Count;

        // call numbers
        for (int i = 0; i < _moves.Length; i++)
        {
            foreach (var board in _boards.Where(x => x.Active))
            {
                board.MarkSquare(_moves[i]);

                if (board.IsWinner())
                {
                    if (boardsLeft == 1)
                    {
                        result = board.SumOfUnmarkedSquares() * _moves[i];
                        break;
                    }
                    else
                    {
                        boardsLeft--;
                    }
                }
            }

            if (result != 0) break;
        }

        Utils.WriteResults($"Puzzle 2: Score = {result}");
    }
    
    private void BuildBoards()
    {
        _boards = new List<Board>();

        for (int i = 2; i < _data.Length; i += 6)
        {
            var rows = _data.Skip(i).Take(5);
            _boards.Add(new Board(rows));
        }
    }

    internal class Board
    {
        private const int ROWS = 5;
        private const int COLS = 5;

        private Square[,] _squares = new Square[ROWS, COLS];

        public bool Active { get; private set; }

        public Board(IEnumerable<string> rows)
        {
            Active = true;

            int rowCounter = 0;
            foreach (var row in rows)
            {
                var cols = row.Split(" ")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => Int32.Parse(x.Trim()))
                    .ToArray();

                for (int i = 0; i < cols.Length; i++)
                {
                    _squares[rowCounter, i] = new Square(cols[i]);
                }

                rowCounter++;
            }
        }

        /// <summary>
        /// Determines if a row or column has all squares marked.
        /// </summary>
        /// <returns>true if the board contains a winning row/column; false otherwise.</returns>
        public bool IsWinner()
        {
            bool isWinner = CheckRows() || CheckColumns();
            Active = !isWinner;

            return isWinner;
        }

        /// <summary>
        /// Checks the board for the called number and marks it if found.
        /// </summary>
        /// <param name="value">The number called.</param>
        public void MarkSquare(int value)
        {
            bool found = false;

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (_squares[i, j].Value == value)
                    {
                        _squares[i, j].Mark();
                        found = true;
                    }

                    if (found) break;
                }

                if (found) break;
            }
        }

        /// <summary>
        /// Sums up all of the unmarked squares on the board.
        /// </summary>
        /// <returns>The sum of all unmaked squares.</returns>
        public int SumOfUnmarkedSquares()
        {
            int sum = 0;

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    sum += (_squares[i, j].Marked) ? 0 : _squares[i, j].Value;
                }
            }

            return sum;
        }

        /// <summary>
        /// Determines if any rows have all squares marked.
        /// </summary>
        /// <returns>true if any row is comppletely marked; false otherwise.</returns>
        private bool CheckRows()
        {
            bool winner = false;

            for (int i = 0; i < ROWS; i++)
            {
                winner = _squares[i, 0].Marked && _squares[i, 1].Marked && _squares[i, 2].Marked &&
                         _squares[i, 3].Marked && _squares[i, 4].Marked;

                if (winner) break;
            }

            return winner;
        }

        /// <summary>
        /// Determines if any columns have all sqaures marked.
        /// </summary>
        /// <returns>true if any column is completely marked; false otherwise.</returns>
        private bool CheckColumns()
        {
            bool winner = false;

            for (int i = 0; i < COLS; i++)
            {
                winner = _squares[0, i].Marked && _squares[1, i].Marked && _squares[2, i].Marked &&
                         _squares[3, i].Marked && _squares[4, i].Marked;

                if (winner) break;
            }

            return winner;
        }
    }
    
    internal class Square
    {
        public int Value { get; }
        
        public bool Marked { get; private set; }

        public Square(int value)
        {
            Value = value;
        }

        public void Mark()
        {
            Marked = true;
        }
    }
}