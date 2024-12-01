namespace AdventOfCode2024;

using AdventOfCode;

/// <summary>
/// Runs Advent of Code puzzles for 2023.
/// </summary>
internal class AdventOfCode2024 : PuzzleYearBase
{
    public AdventOfCode2024(Settings settings, Downloader downloader)
        : base(settings, downloader, 2024)
    {
        Utils.WriteYearHeader("2 0 2 4");
    }

    public override async Task Run()
    {
        await Run(nameof(AdventOfCode2024)).ConfigureAwait(false);
    }
}