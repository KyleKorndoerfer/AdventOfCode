namespace AdventOfCode2022;

using System.Text;

using AdventOfCode;

public class Day05 : IPuzzle
{
	//readonly string DataFile = Path.Combine("day05", "Day05test.txt");
	readonly string DataFile = Path.Combine("day05", "Day05a.txt");

	string[] _data;

	Stack<string>[] _stacks;

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 05 - Supply Stacks");
		_data = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));

		Puzzle1();
		Puzzle2();
	}

	void Puzzle1()
	{
		int startingLine = Initialize(_data);

		for (int i = startingLine; i < _data.Length; i++)
		{
			string[] move = _data[i].Split(" ");
			int numberToMove = Int32.Parse(move[1]);
			int fromStack = Int32.Parse(move[3]) - 1;	// 0-based index
			int toStack = Int32.Parse(move[5]) - 1;		// 0-based index

			for (int j = 0; j < numberToMove; j++)
			{
				var crate = _stacks[fromStack].Pop();
				_stacks[toStack].Push(crate);
			}
		}

		Console.WriteLine($"   Puzzle 1: Message = {GenerateMessage()}");
	}

	void Puzzle2()
	{
		int startingLine = Initialize(_data);

		for (int i = startingLine; i < _data.Length; i++)
		{
			string[] move = _data[i].Split(" ");
			int numberToMove = Int32.Parse(move[1]);
			int fromStack = Int32.Parse(move[3]) - 1;	// 0-based index
			int toStack = Int32.Parse(move[5]) - 1;		// 0-based index

			Stack<string> tempStack = new Stack<string>();
			for (int j = 0; j < numberToMove; j++)
			{
				var crate = _stacks[fromStack].Pop();
				tempStack.Push(crate);
			}
			for (int j = 0; j < numberToMove; j++)
			{
				var crate = tempStack.Pop();
				_stacks[toStack].Push(crate);
			}
		}

		Console.WriteLine($"   Puzzle 2: Message = {GenerateMessage()}");
	}

	int Initialize(string[] data)
	{
		Stack<string> startingStackLines = new Stack<string>();
		int lineCount = 0;
		string line;
		do
		{
			line = data[lineCount];

			if (!string.IsNullOrEmpty(line))
			{
				startingStackLines.Push(line);
			}

			lineCount++;
		} while (!string.IsNullOrEmpty(line));

		string stackNumbers = startingStackLines.Pop();
		int numOfStacks = Int32.Parse(stackNumbers.Substring(stackNumbers.Length - 1, 1));
		_stacks = new Stack<string>[numOfStacks];

		// start processing the line stacks to build the starting positions
		while (startingStackLines.Count > 0)
		{
			line = startingStackLines.Pop();

			int stackCount = 0;
			for (int i = 0; i < line.Length; i++)
			{
				var crate = line.Substring(i, 3);

				if (!string.IsNullOrWhiteSpace(crate.Substring(1,1)))
				{
					if (_stacks[stackCount] == null)
					{
						_stacks[stackCount] = new Stack<string>();
					}

					_stacks[stackCount].Push(crate.Substring(1,1));
				}

				i += 3;
				stackCount++;
			}
		};

		return lineCount;
	}

	string GenerateMessage()
	{
		StringBuilder message = new StringBuilder();
		foreach (var stack in _stacks)
		{
			message.Append(stack.Peek());
		}

		return message.ToString();
	}
}