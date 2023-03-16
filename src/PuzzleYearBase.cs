namespace AdventOfCode;

using System.Reflection;

internal abstract class PuzzleYearBase : IPuzzleYear
{
    private const string AocDayPrefix = "Day";

    private readonly Settings _settings;
    private readonly Downloader _downloader;
    
    public int Year { get; init; }
    
    protected PuzzleYearBase(Settings settings, Downloader downloader, int year)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(downloader);

        _settings = settings;
        _downloader = downloader;
        Year = year;
    }

    public abstract Task Run();

    protected async Task Run(string aocYear)
    {
        // find implementations of 'IPuzzle' in the 'AdventOfCode2022' namespace
        var puzzles = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == aocYear && t.GetInterfaces().Contains(typeof(IPuzzle)))
            .OrderBy(t => t.Name)
            .ToList();

        // build list of puzzles to run 
        var puzzlesToRun = _settings.AocDay == default
            ? puzzles
            : puzzles.Where(t => t.Name == $"{AocDayPrefix}{_settings.AocDay:00}").ToList();

        foreach (var puzzle in puzzlesToRun)
        {
            var instance = Activator.CreateInstance(puzzle, Year, _downloader) as IPuzzle;
            await instance.Run().ConfigureAwait(false);			
        }
    }
}