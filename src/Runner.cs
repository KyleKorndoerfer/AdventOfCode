﻿namespace AdventOfCode;

using System;
using System.Reflection;

using Microsoft.Extensions.Options;

/// <summary>
/// Root runner for the Advent of Code puzzles.
/// </summary>
public class Runner
{
    /// <summary>Prefix for the 'Year' classes.</summary>
    private const string AocPrefix = "AdventOfCode";

    private readonly Downloader _downloader;
    private readonly Settings _settings;

    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="options">Loaded settings from configuration sources.</param>
    /// <param name="downloader">Download manager for input files.</param>
    public Runner(IOptions<Settings> options, Downloader downloader)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(downloader);

        _settings = options.Value;
        _downloader = downloader;
    }

    /// <summary>
    /// Execute the chosen puzzle(s) for this year.
    /// </summary>
    public async Task Run()
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
            var instance = Activator.CreateInstance(puzzleYear, _settings, _downloader) as IPuzzleYear;
            await instance.Run().ConfigureAwait(false); 
        }
    }
}