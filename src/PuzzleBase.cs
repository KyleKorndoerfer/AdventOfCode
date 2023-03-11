namespace AdventOfCode;

/// <summary>
/// Provide base structure for all Puzzles.
/// </summary>
public abstract class PuzzleBase : IPuzzle
{
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="year">The year for the puzzle.</param>
    /// <param name="downloader">The downloader instance to retrieve files.</param>
    protected PuzzleBase(int year, Downloader downloader)
    {
        ArgumentNullException.ThrowIfNull(downloader);
        Downloader = downloader;

        if (year == default)
        {
            throw new ArgumentException("Year must be specified", nameof(year));
        }
        
        Year = year;
    }

    protected Downloader Downloader { get; init; }
    
    protected int Year { get; init; }
    
    public abstract Task Run();
}