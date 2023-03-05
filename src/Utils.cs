namespace AdventOfCode;

/// <summary>
/// Collection of utility methods.
/// </summary>
public static class Utils
{
    /// <summary>New Line character for the current platform.</summary>
    public static string NL = Environment.NewLine;

    /// <summary>
    /// Outputs the tittle banner.
    /// </summary>
    public static void WriteBanner()
    {
        Console.WriteLine(" ________________");
        Console.WriteLine("| Advent of Code |");
        Console.WriteLine(" ----------------");
    }
    
    /// <summary>
    /// Outputs the specified Year header.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void WriteYearHeader(string message)
    {
        Console.WriteLine($"{NL} -={{ {message} }}=-");
    }
    
    /// <summary>
    /// Outputs the Day header for a puzzle.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void WriteDayHeader(string message)
    {
        Console.WriteLine($"{NL}>> {message}");    
    }
    
    /// <summary>
    /// Outputs a Result line for a puzzle.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void WriteResults(string message)
    {
        Console.WriteLine($"     {message}");
    }

    /// <summary>
    /// Outputs a DEBUG line.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void WriteDebug(string message)
    {
        Console.WriteLine($"DEBUG: {message}");
    }
}