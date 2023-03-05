namespace AdventOfCode2022;

using AdventOfCode;

public class Day11 : PuzzleBase
{
	private readonly string DataFile = Path.Combine(nameof(Day11).ToLower(),
			//"Day11test.txt");
			"Day11a.txt");

	private string[] _data;

	private List<Monkey> _monkeys;

	public Day11(string basePath) : base(basePath)
	{
		Utils.WriteDayHeader("Day 11 - Monkey in the Middle");
	}
	public override void Run()
	{
		_data = File.ReadAllLines(Path.Combine(BasePath, DataFile));

		_monkeys = InitializeMonkeys();
		Puzzle1();

		_monkeys = InitializeMonkeys();
		Puzzle2();
	}

	List<Monkey> InitializeMonkeys()
	{
		int numOfMonkeys = _data.Where(x => string.IsNullOrEmpty(x)).Count() + 1;
		List<Monkey> monkeys = new List<Monkey>(numOfMonkeys);

		// create monkeys so they can reference each other
		for (int i = 0; i < numOfMonkeys; i++)
		{
			monkeys.Add(new Monkey());
		}

		// intialize each monkey
		for (int i = 0; i < numOfMonkeys; i++)
		{
			List<string> monkeyData = _data.Skip((i * 7) + 1).Take(5).ToList();
			monkeys[i].Initialize(monkeyData, monkeys);
		}

		return monkeys;
	}

	void Puzzle1()
	{
		for (int i = 0; i < 20; i++)
		{
			foreach(Monkey monkey in _monkeys)
			{
				monkey.InspectAndThrow(3);
			}
		}

		long sumOfTopInspections = _monkeys
				.OrderByDescending(x => x.Inspections)
				.Take(2)
				.Select(x => x.Inspections)
				.Aggregate((a, b) => a * b);

		Utils.WriteResults($"Puzzle 1: {sumOfTopInspections}");
	}

	void Puzzle2()
	{
		int reliefFactor = _monkeys.Select(x => x.TestDivisor).Aggregate((a, b) => a * b);
		for (int i = 0; i < 10000; i++)
		{
			foreach(Monkey monkey in _monkeys)
			{
				monkey.InspectAndThrow(reliefFactor, true);
			}
		}

		Console.WriteLine($"     Puzzle 1: {0}",
			_monkeys
				.OrderByDescending(x => x.Inspections)
				.Take(2)
				.Select(x => x.Inspections)
				.Aggregate((a, b) => a * b));
	}

	/* Helper classes */
	class Monkey
	{
		Queue<long> _items = new Queue<long>();
		Operation _operation;
		int _testDivisor;
		Monkey _falseMonkey;
		Monkey _trueMonkey;
		long _inspections = 0;

		public void Initialize(IList<string> input, IList<Monkey> monkeys)
		{
			InitItems(input[0]);
			_operation = new Operation(input[1]);
			_testDivisor = Int32.Parse(input[2].Split(":")[1].Trim().Split(" ")[2]);
			_trueMonkey = monkeys[Int32.Parse(input[3].Split(":")[1].Trim().Split(" ")[3])];
			_falseMonkey = monkeys[Int32.Parse(input[4].Split(":")[1].Trim().Split(" ")[3])];
		}

		public void AddItem(long item) => _items.Enqueue(item);
		public long Inspections => _inspections;
		public int TestDivisor => _testDivisor;

		public void InspectAndThrow(int reliefFactor = 1, bool isWorried = false)
		{
			while (_items.Count > 0)
			{
				long item = _items.Dequeue();
				long newWorryLevel = _operation.CalculateNewWorryLevel(item);

				long adjustedWorryLevel = isWorried
						? newWorryLevel % reliefFactor
						: (long)Math.Floor(newWorryLevel / 3.0);

				// throw item
				if (adjustedWorryLevel % _testDivisor == 0)
				{
					_trueMonkey.AddItem(adjustedWorryLevel);
				}
				else
				{
					_falseMonkey.AddItem(adjustedWorryLevel);
				}

				_inspections++;
			}
		}

		void InitItems(string itemsLine)
		{
			var worryLevels = itemsLine.Split(":")[1].Trim().Split(",");
			foreach (var item in worryLevels)
			{
				_items.Enqueue(Int32.Parse(item));
			}
		}
	}

	class Operation
	{
		string _op;
		string _operand;

		public Operation(string opLine)
		{
			var opParts = opLine.Split("=")[1].Split(" ");
			_op = opParts[2].Trim();
			_operand = opParts[3];
		}

		public long CalculateNewWorryLevel(long currentLevel)
		{
			long opValue = _operand == "old" ? currentLevel : int.Parse(_operand);

			long newLevel = 0;

			switch (_op)
			{
				case "*": newLevel = currentLevel * opValue; break;
				case "+": newLevel = currentLevel + opValue; break;
			}

			return newLevel;
		}
	}
}