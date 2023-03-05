namespace AdventOfCode;

/// <summary>
/// Provide base structure for all Puzzles.
/// </summary>
public abstract class PuzzleBase : IPuzzle
{
    protected readonly string BasePath;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="basePath">The base path to the data directory.</param>
    protected PuzzleBase(string basePath)
    {
        ArgumentNullException.ThrowIfNull(basePath);
        BasePath = basePath;
    }

    public abstract void Run();
}