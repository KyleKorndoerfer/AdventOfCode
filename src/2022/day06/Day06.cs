namespace AdventOfCode2022;

using AdventOfCode;

public class Day06 : IPuzzle
{
	/* Other sample inputs:
	 *		bvwbjplbgvbhsrlpgdmjqwftvncz
	 *		nppdvjthqldpwncqszvftbrmjlhg
	 *		nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
	 *		zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw
	 */
	//readonly string DataFile = Path.Combine("day06", "Day06test.txt");
	readonly string DataFile = Path.Combine("day06", "Day06a.txt");

	string _data;

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 06 - Tuning Trouble");
		_data = File.ReadAllText(Path.Combine(dataDirectory, DataFile));

		Console.WriteLine($"   Puzzle 1: Marker position = {FindMarkerPosition(4)}");
		Console.WriteLine($"   Puzzle 2: Marker position = {FindMarkerPosition(14)}");
	}

	private int FindMarkerPosition(int markerLength)
	{
		int markerPos = 0;
		for (int i = 0; i < _data.Length - markerLength; i++)
		{
			var mostRecentChars = _data.Substring(i, markerLength);
			if (mostRecentChars.ToArray().Distinct().Count() == markerLength)
			{
				markerPos = i + markerLength;
				break;
			}
		}

		return markerPos;
	}
}