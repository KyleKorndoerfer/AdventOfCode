namespace AdventOfCode2022;

using System.Reflection;

using AdventOfCode;

/// <summary>
/// Runs Advent of Code puzzles for 2022.
/// </summary>
public class AdventOfCode2022 : IPuzzleYear
{
	private const string AocDayPrefix = "Day";
	private const int Year = 2022;

	private readonly Settings _settings;
	private readonly string _basePath;
	
	public AdventOfCode2022(Settings settings, string basePath)
	{
		ArgumentNullException.ThrowIfNull(settings);
		ArgumentNullException.ThrowIfNull(basePath);

		_settings = settings;
		_basePath = basePath;
	}
	
	public void Run()
	{
		Utils.WriteYearHeader("2 0 2 2");

		// find implementations of 'IPuzzle' in the 'AdventOfCode2022' namespace
		var puzzles = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.Namespace == nameof(AdventOfCode2022) && t.GetInterfaces().Contains(typeof(IPuzzle)))
				.OrderBy(t => t.Name)
				.ToList();

		// build list of puzzles to run 
		var puzzlesToRun = _settings.AocDay == default
				? puzzles
				: puzzles.Where(t => t.Name == $"{AocDayPrefix}{_settings.AocDay:00}").ToList();

		foreach (var puzzle in puzzlesToRun)
		{
			var instance = Activator.CreateInstance(puzzle, Path.Combine(_basePath, $"{Year}")) as IPuzzle;
			instance?.Run();			
		}
	}
}