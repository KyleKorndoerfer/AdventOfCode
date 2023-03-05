namespace AdventOfCode2022;

using AdventOfCode;

public class Day06 : PuzzleBase
{
	/* Other sample inputs:
	 *		bvwbjplbgvbhsrlpgdmjqwftvncz
	 *		nppdvjthqldpwncqszvftbrmjlhg
	 *		nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
	 *		zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw
	 */
	private readonly string _dataFile = Path.Combine(nameof(Day06).ToLower(),
			//"Day06test.txt");
			"Day06a.txt");

	private string _data;

	public Day06(string basePath) : base(basePath)
	{
		Utils.WriteDayHeader("Day 06 - Tuning Trouble");
	}
	
	public override void Run()
	{
		_data = File.ReadAllText(Path.Combine(BasePath, _dataFile));

		Utils.WriteResults($"Puzzle 1: Marker position = {FindMarkerPosition(4)}");
		Utils.WriteResults($"Puzzle 2: Marker position = {FindMarkerPosition(14)}");
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