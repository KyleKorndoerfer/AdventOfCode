namespace AdventOfCode2022;

using System.Linq;

using AdventOfCode;

internal class Day01 : PuzzleBase
{
	private string[] _data;

	public Day01(int year, Downloader downloader) : base(year, downloader)
	{
		Utils.WriteDayHeader("Day 01 - Calorie Counting");
	}

	public override async Task Run()
	{
		List<int> elves = new();
		_data = await Downloader
				//.GetInput(Year, 1, "Day01test.txt")
				.GetInput(Year, 1)
				.ConfigureAwait(false);

		int calories = 0;
		foreach (var line in _data)
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