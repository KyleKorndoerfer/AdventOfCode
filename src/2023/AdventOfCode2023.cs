namespace AdventOfCode2023;

using AdventOfCode;

/// <summary>
/// Runs Advent of Code puzzles for 2023.
/// </summary>
internal class AdventOfCode2023 : PuzzleYearBase
{
    public AdventOfCode2023(Settings settings, Downloader downloader)
            : base(settings, downloader, 2023)
    {
        Utils.WriteYearHeader("2 0 2 3");
    }

    public override async Task Run()
    {
        await Run(nameof(AdventOfCode2023)).ConfigureAwait(false);
    }
}