namespace AdventOfCode2021;

using AdventOfCode;

/// <summary>
/// Runs Advent of Code puzzles for 2021.
/// </summary>
internal class AdventOfCode2021 : PuzzleYearBase
{
	public AdventOfCode2021(Settings settings, Downloader downloader) 
			: base(settings, downloader, 2021)
	{
		Utils.WriteYearHeader("2 0 2 1");
	}
	
	public override async Task Run()
	{
		await Run(nameof(AdventOfCode2021)).ConfigureAwait(false);
	}
}