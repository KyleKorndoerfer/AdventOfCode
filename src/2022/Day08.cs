namespace AdventOfCode2022;

using AdventOfCode;

internal class Day08 : PuzzleBase
{
	private string[] _data;
	private int[,] _matrix;
	private int _rows;
	private int _cols;

	public Day08(int year, Downloader downloader) : base(year, downloader)
	{
		Utils.WriteDayHeader("Day 08 - Treetop Tree House");
	}
	
	public override async Task Run()
	{
		_data = await Downloader
				//.GetInput(Year, 8, "Day08text.txt")
				.GetInput(Year, 8)
				.ConfigureAwait(false);
		
		_rows = _data.Length;
		_cols = _data[0].Length;

		_matrix = InitializeMatrix(_rows, _cols);
		var result = ProcessMatrix();

		Utils.WriteResults($"Puzzle 1: # of visible trees = {result.VisibleTrees}");
		Utils.WriteResults($"Puzzle 2: Maximum sight score = {result.MaxSightScore}");
	}

	(int VisibleTrees, int MaxSightScore) ProcessMatrix()
	{
		int visibleTrees = (2 * _rows) + (2 * (_cols - 2)); // perimeter
		int maxSightScore = 0;

		for (int i = 1; i < _rows - 1; i++)
		{
			for (int j = 1; j < _cols - 1; j++)
			{
				List<(bool IsVisible, int MaxSightScore)> results = new List<(bool IsVisible, int MaxSightScore)>
				{
					LookUp(i, j),
					LookDown(i, j),
					LookLeft(i, j),
					LookRight(i, j)
				};

				visibleTrees += results.Any(x => x.IsVisible) ? 1 : 0;

				var nonZeroScores = results.Where(x => x.MaxSightScore > 0);
				if (nonZeroScores.Count() > 0)
				{
					int currentSightScore = nonZeroScores.Select(x => x.MaxSightScore).Aggregate((x, y) => x * y);
					maxSightScore = Math.Max(maxSightScore, currentSightScore);
				}
			}
		}

		return (visibleTrees, maxSightScore);
	}

	int[,] InitializeMatrix(int rows, int columns)
	{
		int[,] matrix = new int[rows, columns];

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				matrix[i, j] = Int32.Parse(_data[i].Substring(j, 1));
			}
		}

		return matrix;
	}

	(bool IsVisible, int SightScore) LookUp(int startRow, int startCol)
	{
		bool isVisible = true;
		int sightScore = 0;
		int height = _matrix[startRow, startCol];

		for (int i = startRow - 1; i >= 0; i--)
		{
			sightScore++;

			if (_matrix[i, startCol] >= height)
			{
				isVisible = false;
				break;
			}
		}

		return (isVisible, sightScore);
	}

	(bool IsVisible, int SightScore) LookDown(int startRow, int startCol)
	{
		bool isVisible = true;
		int sightScore = 0;
		int height = _matrix[startRow, startCol];

		for (int i = startRow + 1; i < _rows; i++)
		{
			sightScore++;

			if (_matrix[i, startCol] >= height)
			{
				isVisible = false;
				break;
			}
		}

		return (isVisible, sightScore);
	}

	(bool IsVisible, int SightScore) LookLeft(int startRow, int startCol)
	{
		bool isVisible = true;
		int sightScore = 0;
		int height = _matrix[startRow, startCol];

		for (int i = startCol - 1; i >= 0; i--)
		{
			sightScore++;

			if (_matrix[startRow, i] >= height)
			{
				isVisible = false;
				break;
			}
		}

		return (isVisible, sightScore);
	}

	(bool IsVisible, int SightScore) LookRight(int startRow, int startCol)
	{
		bool isVisible = true;
		int sightScore = 0;
		int height = _matrix[startRow, startCol];

		for (int i = startCol + 1; i < _cols; i++)
		{
			sightScore++;

			if (_matrix[startRow,i] >= height)
			{
				isVisible = false;
				break;
			}
		}

		return (isVisible, sightScore);
	}
}