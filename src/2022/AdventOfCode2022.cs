namespace AdventOfCode2022;

using AdventOfCode;

/// <summary>
/// Runs Advent of Code puzzles for 2022.
/// </summary>
internal class AdventOfCode2022 : PuzzleYearBase
{
	public AdventOfCode2022(Settings settings, Downloader downloader)
			: base(settings, downloader, 2022)
	{
		Utils.WriteYearHeader("2 0 2 2");
	}
	
	public override async Task Run()
	{
		await Run(nameof(AdventOfCode2022)).ConfigureAwait(false);
	}
}