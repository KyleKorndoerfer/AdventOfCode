namespace AdventOfCode2022;

using AdventOfCode;

public class Day08 : IPuzzle
{
	//readonly string DataFile = Path.Combine("day08", "Day08test.txt");
	readonly string DataFile = Path.Combine("day08", "Day08a.txt");

	string[] _data;
	int[,] _matrix;
	int _rows;
	int _cols;

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 08 - Treetop Tree House");
		_data = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));
		_rows = _data.Length;
		_cols = _data[0].Length;

		_matrix = InitializeMatrix(_rows, _cols);
		var result = ProcessMatrix();

		Console.WriteLine($"   Puzzle 1: # of visile trees = {result.VisibleTrees}");
		Console.WriteLine($"   Puzzle 2: Maximum sight score = {result.MaxSightScore}");
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