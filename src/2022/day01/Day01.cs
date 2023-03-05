namespace AdventOfCode2022;

using System.Linq;

using AdventOfCode;

public class Day01 : PuzzleBase
{
	readonly string _dataFile = Path.Combine(nameof(Day01).ToLower(), 
			//"Day01test.txt");
			"Day01a.txt");
	
	private string[] _rawData;

	public Day01(string basePath) : base(basePath)
	{
		Utils.WriteDayHeader("Day 01 - Calorie Counting");
	}

	public override void Run()
	{
		List<int> elves = new();
		_rawData = File.ReadAllLines(Path.Combine(BasePath, _dataFile));

		int calories = 0;
		foreach (var line in _rawData)
		{
			if (string.IsNullOrEmpty(line))
			{
				elves.Add(calories);
				calories = 0;
			}
			else
			{
				calories += Convert.ToInt32(line);
			}
		}
		elves.Add(calories); // add last row

		Utils.WriteResults($"Part 1 - Most calories: {elves.Max()}");

		int topCalories = elves.OrderByDescending(x => x).Take(3).Sum();
		Utils.WriteResults($"Part 2 - Top 3 Elves calories: {topCalories}");
	}
}