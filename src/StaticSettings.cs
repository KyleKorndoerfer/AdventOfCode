namespace AdventOfCode;

/// <summary>
/// Collection of global settings.
/// <summary>
public static class StaticSettings
{
	/// <summary>
	/// Puzzle year to run (if specified). If null or empty, then the most
	/// recent year will be run.
	/// <summary>
	public static string AocYear = Environment.GetEnvironmentVariable("aocYear") ?? string.Empty;

	/// <summary>
	/// Puzzle to run within a given year (if specified). If null or empty, then
	/// the most recent day for the year will be run.
	/// </summary>
	public static string AocDay = Environment.GetEnvironmentVariable("aocDay") ?? "01";
}