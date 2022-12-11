namespace AdventOfCode2022;

using AdventOfCode;

public class Day09 : IPuzzle
{
	//readonly string DataFile = Path.Combine("day09", "Day09test.txt");
	//readonly string DataFile = Path.Combine("day09", "Day09test2.txt");
	readonly string DataFile = Path.Combine("day09", "Day09a.txt");

	const int ShortRopeLength = 2;
	const int LongRopeLength = 10;

	string[] _data;
	Point[] knots = new Point[LongRopeLength];

	HashSet<Point> _shortVisited = new HashSet<Point>();
	HashSet<Point> _longVisited = new HashSet<Point>();

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 09 - Rope Bridge");
		_data = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));

		foreach	(string line in _data)
		{
			string[] move = line.Split(" ");
			string direction = move[0];
			int distance = Int32.Parse(move[1]);

			for (int i = 0; i < distance; i++)
			{
				// move head
				switch (direction)
				{
					case "U": knots[0].Y++; break;
					case "D": knots[0].Y--; break;
					case "L": knots[0].X--; break;
					case "R": knots[0].X++; break;
				};

				// move tail
				for (int j = 1; j < LongRopeLength; j++)
				{
					int dx = knots[j - 1].X - knots[j].X;
					int dy = knots[j - 1].Y - knots[j].Y;

					if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
					{
						knots[j].X += Math.Sign(dx);
						knots[j].Y += Math.Sign(dy);
					}
				}

				_shortVisited.Add(knots[ShortRopeLength - 1]);
				_longVisited.Add(knots[LongRopeLength - 1]);
			}
		}

		Console.WriteLine($"   Puzzle 1: Visited locations = {_shortVisited.Count}");
		Console.WriteLine($"   Puzzle 2: Visited locations = {_longVisited.Count}");
	}

	struct Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }

		public override string ToString()
		{
			return $"[{X}, {Y}]";
		}
	}
}