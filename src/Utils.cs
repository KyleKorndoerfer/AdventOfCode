namespace AdventOfCode;

/// <summary>
/// Collection of utility methods.
/// </summary>
internal static class Utils
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
    /// Outputs the time it took to solve all of the puzzles.
    /// </summary>
    /// <param name="ms"></param>
    public static void WriteTiming(long ms)
    {
        Console.WriteLine($"     > Time: {ms}ms");
    }

    /// <summary>
    /// Outputs a DEBUG line.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void WriteDebug(string message)
    {
        Console.WriteLine($"DEBUG: {message}");
    }

    /// <summary>
    /// Outputs and ERROR to the console.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="e">The exception being thrown.</param>
    public static void WriteError(string message, Exception e)
    {
        Console.WriteLine($"ERROR: {message}");
        Console.WriteLine(e);
    }
}