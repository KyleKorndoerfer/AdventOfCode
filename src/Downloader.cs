namespace AdventOfCode;

using Microsoft.Extensions.Options;

public class Downloader : IDisposable
{
    private readonly HttpClient _http;
    private readonly string _dataPath;

    public Downloader(IOptions<Settings> settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        
        if (string.IsNullOrEmpty(settings.Value.AocSessionToken))
        {
            throw new ArgumentNullException(
                    nameof(settings), 
            "A session cookie value is required to download files");
        }

        _dataPath = Path.Combine(settings.Value.SolutionRoot, "data");

        _http = new HttpClient
        {
            BaseAddress = new Uri("https://adventofcode.com"),
        };
        _http.DefaultRequestHeaders.Add("cookie", $"session={settings.Value.AocSessionToken}");
    }

    /// <summary>
    /// Gets the input file data, downloading it if needed.
    /// </summary>
    /// <param name="year">Year the puzzle input is for.</param>
    /// <param name="day">Day the puzzle input is for.</param>
    /// <param name="filename">The name of the file to load. If not specified then
    ///     the default filename of 'input.txt' will be used.</param>
    /// <returns>
    /// The puzzle input for the specified year & day.
    /// </returns>
    public async Task<string[]> GetInput(int year, int day, string filename = null)
    {
        string[] input;
        filename ??= "input.txt";   
        var filePath = Path.Combine(_dataPath, $"{year}", $"day{day:00}", filename);
        
        if (File.Exists(filePath))
        {
            input = ReadLocalFile(filePath);
        }
        else
        {
            var reqUri = new Uri($"/{year}/day/{day}/input", UriKind.Relative);
            var rawData = await ReadRemoteFile(reqUri).ConfigureAwait(false);

            await WriteLocalFile(filePath, rawData).ConfigureAwait(false);
            input = ReadLocalFile(filePath);
        }

        return input;
    }

    public void Dispose()
    {
        _http?.Dispose();
    }

    /// <summary>
    /// Fetches the data contained in a local file.
    /// </summary>
    /// <param name="filePath">The full file path.</param>
    /// <returns>The contents of the local file.</returns>
    private static string[] ReadLocalFile(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            if (string.IsNullOrWhiteSpace(lines.Last()))
            {
                lines = lines.Take(lines.Length - 1).ToArray();
            }

            return lines;
        }
        catch (Exception e)
        {
            Utils.WriteError($"There was a problem reading local file '{filePath}'", e);
            throw;
        }
    }

    /// <summary>
    /// Writes the retrieved data to disk.
    /// </summary>
    /// <param name="filePath">The full file path to write to.</param>
    /// <param name="rawData">The file contents.</param>
    private static async Task WriteLocalFile(string filePath, string rawData)
    {
        try
        {
            await File.WriteAllTextAsync(filePath, rawData).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Utils.WriteError($"There was an error writing the input file '{filePath}' to disk", e);
            throw;
        }
    }
    
    /// <summary>
    /// Fetches the input data from the Advent of Code website.
    /// </summary>
    /// <param name="uriPath">The URI path to the input file for a specified year & day.</param>
    /// <returns>The raw input data as a single string.</returns>
    private async Task<string> ReadRemoteFile(Uri uriPath)
    {
        try
        {
            return await _http.GetStringAsync(uriPath).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Utils.WriteError("There was an error fetching the input from the Advent of Code website.", e);
            throw;
        }
    }
}