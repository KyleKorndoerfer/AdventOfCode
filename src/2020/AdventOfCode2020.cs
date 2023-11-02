namespace AdventOfCode2020;

using AdventOfCode;

/// <summary>
/// Runs Advent of Code puzzles for 2020.
/// </summary>
internal class AdventOfCode2020 : PuzzleYearBase
{
    public AdventOfCode2020(Settings settings, Downloader downloader)
            : base(settings, downloader, 2020)
    {
        Utils.WriteYearHeader("2 0 2 0");
    }

    public override async Task Run()
    {
        await Run(nameof(AdventOfCode2020)).ConfigureAwait(false);
    }
}