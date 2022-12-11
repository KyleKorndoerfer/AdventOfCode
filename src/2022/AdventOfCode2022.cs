namespace AdventOfCode2022;

using System.Reflection;

using AdventOfCode;

public class AdventOfCode2022 : IPuzzleYear
{
	const string AOC_DayPrefix = "Day";

	public void Run(string dataDirectory)
	{
		Console.WriteLine($"\n -=[ 2 0 2 2 ]=-\n");

		var puzzles = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.Namespace == "AdventOfCode2022" && t.GetInterfaces().Contains(typeof(IPuzzle)))
				.OrderBy(t => t.Name);

		var puzzle = string.IsNullOrEmpty(StaticSettings.AocDay)
				? puzzles.TakeLast(1).First()
				: puzzles.First(t => t.Name == $"{AOC_DayPrefix}{StaticSettings.AocDay}");

		IPuzzle instance = Activator.CreateInstance(puzzle) as IPuzzle;
		instance.Run(Path.Combine(dataDirectory, "2022"));
	}
}