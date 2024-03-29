namespace AdventOfCode2022;

using AdventOfCode;

internal class Day10 : PuzzleBase
{
	private string[] _data;

	public Day10(int year, Downloader downloader) : base(year, downloader)
	{
		Utils.WriteDayHeader("Day 10 - Cathode-Ray Tube");
	}
	public override async Task Run()
	{
		_data = await Downloader
				//.GetInput(Year, 10, "Day10test.txt")
				//.GetInput(Year, 10, "Day10test2.txt")
				.GetInput(Year, 10)
				.ConfigureAwait(false);
		
		int cycle = 0;
		int regX = 1;
		Dictionary<int, int> registerHistory = new Dictionary<int, int>();
		bool[,] display = new bool[6,40];

		foreach (string line in _data)
		{
			cycle++;
			registerHistory.Add(cycle, regX);	// cycle
			UpdateDisplay(display, cycle, regX);


			if (line == "noop")
			{
				// No-Op; 1 cycle
				continue;
			}
			else
			{
				// Operation; 2 cycles (1 cycle already done)
				cycle++;
				registerHistory.Add(cycle, regX);	// cycle 2 of 2
				UpdateDisplay(display, cycle, regX);
			}

			regX += Int32.Parse(line.Split(" ")[1]);
		}

		// DEBUG
		// foreach(var (key, val) in registerHistory)
		// {
		// 	Console.WriteLine($"Cycle {key}; Register: {val}");
		// }

		// Part 1 answer
		int sum = 0;
		for (int i = 20; i <= 220; i += 40)
		{
			sum += i * registerHistory[i];
			//Console.WriteLine($"i = {i}; value = {registerHistory[i]}; Sum = {sum}");	// DEBUG
		}

		Utils.WriteResults($"Puzzle 1: Signal Strength Sum = {sum}");

		// Part 2 answer = PGHFGLUG
		Console.WriteLine("     Puzzle 2:");
		for (int i = 0; i < 6; i++)
		{
			Console.Write("\t");
			for (int j = 0; j < 40; j++)
			{
				Console.Write(display[i, j] ? "#" : ".");
			}
			Console.Write(Environment.NewLine);
		}
	}

	void UpdateDisplay(bool[,] display, int cycle, int regX)
	{
		int row = (cycle - 1) / 40;
		int hPos = (cycle -1) % 40;

		if (hPos >= regX - 1 && hPos <= regX + 1)
		{
			display[row, hPos] = true;
		}
	}
}