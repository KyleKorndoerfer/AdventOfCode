namespace AdventOfCode2022;

using AdventOfCode;

internal class Day06 : PuzzleBase
{
	/* Other sample inputs:
	 *		bvwbjplbgvbhsrlpgdmjqwftvncz
	 *		nppdvjthqldpwncqszvftbrmjlhg
	 *		nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg
	 *		zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw
	 */

	private string _data;

	public Day06(int year, Downloader downloader) : base(year, downloader)
	{
		Utils.WriteDayHeader("Day 06 - Tuning Trouble");
	}
	
	public override async Task Run()
	{
		_data = (await Downloader
				//.GetInput(Year, 6, "Day06test.txt")
				.GetInput(Year, 6)
				.ConfigureAwait(false))[0];

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