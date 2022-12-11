namespace AdventOfCode2022;

using AdventOfCode;

public class Day02 : IPuzzle
{
	//readonly string DataFile = Path.Combine("day02", "Day02test.txt");
	readonly string DataFile = Path.Combine("day02", "Day02a.txt");

	string[] _data;

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 02 - Rock, Paper, Scissors");
		_data = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));

		Part1(_data);
		Part2(_data);
	}

	void Part1(string[] data)
	{
		// A = rock; B = paper; C = scissors
		// X = rock; Y = paper; Z = scissors
		var outcomes = new Dictionary<string, int>
		{
			{"A X", Points.Draw + Points.Rock},
			{"A Y", Points.Win + Points.Paper},
			{"A Z", Points.Lose + Points.Scissors},

			{"B X", Points.Lose + Points.Rock},
			{"B Y", Points.Draw + Points.Paper},
			{"B Z", Points.Win + Points.Scissors},

			{"C X", Points.Win + Points.Rock},
			{"C Y", Points.Lose + Points.Paper},
			{"C Z", Points.Draw + Points.Scissors}
		};

		int score = 0;

		foreach (string line in data)
		{
			var outcome = outcomes.Single(x => x.Key == line);
			score += outcome.Value;
			//Console.WriteLine($"DEBUG: {line} = {outcome.Value}; Score = {score}");
		}

		Console.WriteLine($"   Part 1 - Score = {score}");
	}

	void Part2(string[] data)
	{
		// A = rock; B = paper; C = scissors
		// X = lose; Y = draw; Z = win
		var outcomes = new Dictionary<string, int>
		{
			{"A X", Points.Lose + Points.Scissors},
			{"A Y", Points.Draw + Points.Rock},
			{"A Z", Points.Win + Points.Paper},

			{"B X", Points.Lose + Points.Rock},
			{"B Y", Points.Draw + Points.Paper},
			{"B Z", Points.Win + Points.Scissors},

			{"C X", Points.Lose + Points.Paper},
			{"C Y", Points.Draw + Points.Scissors},
			{"C Z", Points.Win + Points.Rock},
		};

		int score = 0;

		foreach (string line in data)
		{
			var outcome = outcomes.Single(x => x.Key == line);
			score += outcome.Value;
			//Console.WriteLine($"DEBUG: {line} = {outcome.Value}; Score = {score}");
		}

		Console.WriteLine($"   Part 2 - Score = {score}");
	}

	static class Points
	{
		public static int Rock = 1;		// A,  X
		public static int Paper = 2;	// B, Y
		public static int Scissors = 3;	// C, Z

		public static int Win = 6;
		public static int Lose = 0;
		public static int Draw = 3;
	}
}