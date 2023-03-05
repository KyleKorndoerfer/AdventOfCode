namespace AdventOfCode2022;

using AdventOfCode;

public class Day03 : PuzzleBase
{
	private const string PosValues = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private readonly string _dataFile = Path.Combine(nameof(Day03).ToLower(), 
			//"Day03test.txt");
			"Day03a.txt");

	private string[] _data;

	public Day03(string basePath) : base(basePath)
	{
		Utils.WriteDayHeader("Day 03 - Rucksack Reorganization");
	}

	public override void Run()
	{
		_data = File.ReadAllLines(Path.Combine(BasePath, _dataFile));

		Puzzle1();
		Puzzle2();
	}

	void Puzzle1()
	{
		int sum = 0;

		foreach (var line in _data)
		{
			int middle = (line.Length / 2);
			var left = line.Substring(0, middle).ToArray();
			var right = line.Substring(middle, line.Length - middle).ToArray();
			var common = left.Intersect(right).First();

			sum += PosValues.IndexOf(common);
			//Utils.WriteDebug($"char = {common}; Pos = {PosValues.IndexOf(common)}; Sum = {sum}");
		}

		Utils.WriteResults($"Part 1: Sum = {sum}");
	}

	void Puzzle2()
	{
		int sum = 0;

		for(int i = 0; i < _data.Length; i += 3)
		{
			var elf1 = _data[i].ToArray();
			var elf2 = _data[i+1].ToArray();
			var elf3 = _data[i+2].ToArray();

			var common = elf1.Intersect(elf2).Intersect(elf3).First();

			sum += PosValues.IndexOf(common);
			//Utils.WriteDayHeader($"common = {common}; Pos = {PosValues.IndexOf(common)}; Sum = {sum}");
		}

		Utils.WriteResults($"Part 2: Sum = {sum}");
	}
}