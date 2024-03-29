namespace AdventOfCode2022;

using AdventOfCode;

internal class Day09 : PuzzleBase
{
	private const int ShortRopeLength = 2;
	private const int LongRopeLength = 10;

	private string[] _data;
	private Point[] knots = new Point[LongRopeLength];

	private HashSet<Point> _shortVisited = new HashSet<Point>();
	private HashSet<Point> _longVisited = new HashSet<Point>();

	public Day09(int year, Downloader downloader) : base(year, downloader)
	{
		Utils.WriteDayHeader("Day 09 - Rope Bridge");
	}
	
	public override async Task Run()
	{
		_data = await Downloader
				//.GetInput(Year, 9, "Day09test.txt")
				//.GetInput(Year, 9, "Day09test2.txt")
				.GetInput(Year, 9)
				.ConfigureAwait(false);

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

		Utils.WriteResults($"Puzzle 1: Visited locations = {_shortVisited.Count}");
		Utils.WriteResults($"Puzzle 2: Visited locations = {_longVisited.Count}");
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