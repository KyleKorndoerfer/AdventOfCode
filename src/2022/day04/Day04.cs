namespace AdventOfCode2022;

using AdventOfCode;

public class Day04 : PuzzleBase
{
	private readonly string _dataFile = Path.Combine(nameof(Day04).ToLower(),
			//"Day04test.txt");
			"Day04a.txt");

	private string[] _data;

	public Day04(string basePath) : base(basePath)
	{
		Utils.WriteDayHeader("Day 04 - Camp Cleanup");
	}

	public override void Run()
	{
		_data = File.ReadAllLines(Path.Combine(BasePath, _dataFile));

		Puzzle1();
		Puzzle2();
	}

	void Puzzle1()
	{
		int count = 0;

		foreach (string line in _data)
		{
			string[] pair = line.Split(",");
			string[] leftRange = pair[0].Split("-");
			string[] rightRange = pair[1].Split("-");

			bool leftContainsRight =
					(Int32.Parse(leftRange[0]) <= Int32.Parse(rightRange[0])) &&
					(Int32.Parse(leftRange[1]) >= Int32.Parse(rightRange[1]));

			bool rightContainsLeft =
					(Int32.Parse(rightRange[0]) <= Int32.Parse(leftRange[0])) &&
					(Int32.Parse(rightRange[1]) >= Int32.Parse(leftRange[1]));

			count += (leftContainsRight || rightContainsLeft) ? 1 : 0;

			//Utils.WriteDebug($"Line = {line}; l[r] = {leftContainsRight}; r[l] = {rightContainsLeft}; Count = {count}");
		}

		Utils.WriteResults($"Puzzle 1: Count = {count}");
	}

	void Puzzle2()
	{
		int count = 0;

		foreach (string line in _data)
		{
			string[] pair = line.Split(",");
			string[] leftRange = pair[0].Split("-");
			string[] rightRange = pair[1].Split("-");

			IList<int> leftList = RangeToList(leftRange);
			IList<int> rightList = RangeToList(rightRange);

			count += leftList.Intersect(rightList).Any() ? 1 : 0;
		}

		Utils.WriteResults($"Part 2: Count = {count}");
	}

	private IList<int> RangeToList(string[] range)
	{
		int start = Int32.Parse(range[0]);
		int end = Int32.Parse(range[1]);

		List<int> list = new List<int>();
		for (int i = start; i <= end; i++)
		{
			list.Add(i);
		}

		return list;
	}
}