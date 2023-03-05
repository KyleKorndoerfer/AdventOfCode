using Microsoft.Extensions.Options;

namespace AdventOfCode;

using System;
using System.Reflection;

/// <summary>
/// Root runner for the Advent of Code puzzles.
/// </summary>
public class Runner
{
    /// <summary>Prefix for the 'Year' classes.</summary>
    private const string AocPrefix = "AdventOfCode";

    private readonly string _baseDirectory;
    private readonly Settings _settings;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="options">Loaded settings from configuration sources.</param>
    public Runner(IOptions<Settings> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _settings = options.Value;
    }

    /// <summary>
    /// Execute the chosen puzzle(s) for this year.
    /// </summary>
    public void Run()
    {
        // reflect over the assembly looking for implementations of 'IPuzzleYear'
        var puzzleYears = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.GetInterfaces().Contains(typeof(IPuzzleYear)))
            .OrderBy(x => x)
            .ToList();

        // filter down to single year (if specified)
        if (_settings.AocYear != default)
        {
            puzzleYears = puzzleYears.Where(_ => _.Name == $"{AocPrefix}{_settings.AocYear}").ToList();
        }

        foreach (var puzzleYear in puzzleYears)
        {
            var instance = Activator.CreateInstance(puzzleYear, _settings, _baseDirectory) as IPuzzleYear;
            instance?.Run(); 
        }
    }
}