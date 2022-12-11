namespace AdventOfCode2022;

using AdventOfCode;

public class Day03 : IPuzzle
{
	const string posValues = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	//readonly string DataFile = Path.Combine("day03", "Day03test.txt");
	readonly string DataFile = Path.Combine("day03", "Day03a.txt");

	string[] _data;

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 03 - Rucksack Reorganization");
		_data = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));

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

			sum += posValues.IndexOf(common);
			//Console.WriteLine($"DEBUG: char = {common}; Pos = {posValues.IndexOf(common)}; Sum = {sum}");
		}

		Console.WriteLine($"   Part 1: Sum = {sum}");
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

			sum += posValues.IndexOf(common);
			//Console.WriteLine($"DEBUG: common = {common}; Pos = {posValues.IndexOf(common)}; Sum = {sum}");
		}

		Console.WriteLine($"   Part 2: Sum = {sum}");
	}
}