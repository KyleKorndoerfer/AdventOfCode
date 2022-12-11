namespace AdventOfCode2022;

using System.Linq;

using AdventOfCode;

public class Day01 : IPuzzle
{
	readonly string DataFile = Path.Combine("day01", "Day01test.txt");
	//readonly string DataFile = Path.Combine("day01", "Day01a.txt");

	string[] _rawData;

	List<int> elves = new List<int>();

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 01 - Calorie Counting");
		_rawData = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));

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

		Console.WriteLine($"   Part 1 - Most calories: {elves.Max()}");

		int topCalories = elves.OrderByDescending(x => x).Take(3).Sum();
		Console.WriteLine($"   Part 2 - Top 3 Elves calories: {topCalories}");
	}
}