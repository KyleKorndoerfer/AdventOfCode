namespace AdventOfCode;

using System.Security;

/// <summary>
/// Holds all of the application settings.
/// </summary>
internal class Settings
{
    /// <summary>
    /// The name of the section the settings belong to in the loaded configurations. 
    /// </summary>
    public const string SectionName = "Settings";
    
    /// <summary>
    /// The year to target for running.
    /// </summary>
    public int AocYear { get; init; }

    /// <summary>
    /// The day to run for the given year.
    /// </summary>
    public int AocDay { get; init; }

    /// <summary>
    /// The Advent of Code session token.
    /// </summary>
    /// <remarks>This is only used for download the puzzle input.</remarks>
    public string AocSessionToken { get; set; }
    
    /// <summary>
    /// The root folder for the solution.
    /// </summary>
    public string SolutionRoot { get; set; }
}