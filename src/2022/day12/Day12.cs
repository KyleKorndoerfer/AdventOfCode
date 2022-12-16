namespace AdventOfCode2022;

using AdventOfCode;

public class Day12 : IPuzzle
{
	//readonly string DataFile = Path.Combine("day12", "Day12test.txt");
	readonly string DataFile = Path.Combine("day12", "Day12a.txt");

	string[] _data;

	const string Dest = "E";
	const string Start ="S";
	const string Elevations = "abcdefghijklmnopqrstuvwxyz";

	// used for directional movement (up, down, left, right)
	static int[] rowNum = { -1, 1, 0, 0 };
	static int[] colNum = { 0, 0, -1, 1 };

	int _rows;
	int _cols;

	Point _start;
	Point _dest;

	List<Point> _lowPoints = new List<Point>();

	public void Run(string dataDirectory)
	{
		Console.WriteLine("\n>> Day 12 - Hill Climbing Algorithm");
		_data = File.ReadAllLines(Path.Combine(dataDirectory, DataFile));

		_rows = _data.Length;
		_cols = _data[0].Length;

		string [,] matrix = LoadMatrix(_rows, _cols);

		Puzzle1(matrix);
		Puzzle2(matrix);
	}

	void Puzzle1(string[,] matrix)
	{
		int shortestPath = Traverse(matrix, _start, _dest);

		Console.WriteLine($"   Puzzle 1: shortest path = {shortestPath}");
	}

	void Puzzle2(string[,] matrix)
	{
		int shortestPath = int.MaxValue;

		foreach (Point p in _lowPoints)
		{
			int distance = Traverse(matrix, p, _dest);

			shortestPath = distance > 0
					? Math.Min(shortestPath, distance)
					: shortestPath;
		}

		Console.WriteLine($"   Puzzle 2: shortest path = {shortestPath}");
	}

	// traverse matrix using Breadth-First Search (BFS)
	int Traverse(string[,] matrix, Point source, Point dest)
	{
		bool [,] visited = new bool[_rows, _cols];

		visited[source.X, source.Y] = true;	// source is a visited location

		Queue<Node> q = new Queue<Node>();	// neighbors to visit

		// source Node is a distance of 0
		Node sourceNode = new Node(source, 0);
		q.Enqueue(sourceNode);

		// start traversing from the source cell
		while (q.Count != 0)
		{
			Node currentNode = q.Peek();
			Point nodePoint = currentNode.Point;

			// have we reached the destiantion Node?
			if (nodePoint.Equals(dest))
			{
				return currentNode.Distance;
			}

			// remove Node from queue & add neightbors
			q.Dequeue();

			// check neighbors for valid moves
			for (int i = 0; i < 4; i++)
			{
				int row = nodePoint.X + rowNum[i];
				int col = nodePoint.Y + colNum[i];

				// check neighbor is in bounds, has not been visited, & is a valid move
				if (IsInRange(row, col) && !visited[row, col] &&
						IsValidMove(matrix, nodePoint, new Point(row, col)))
				{
					// mark node as visited and enqueue
					visited[row, col] = true;
					Node neighbor = new Node(new Point(row, col), currentNode.Distance + 1)	;
					q.Enqueue(neighbor);
				}
			}
		}

		return -1;	// destination could not be reached
	}

	// determine if specified coordinate is within the matrix boundaries
	bool IsInRange(int row, int col)
	{
		return (row >= 0) && (row < _rows) && (col >= 0) && (col < _cols);
	}

	bool IsValidMove(string[,] matrix, Point current, Point neighbor)
	{
		// get elevations
		int srcElevation = matrix[current.X, current.Y] == "S"
				? Elevations.IndexOf("a")
				: Elevations.IndexOf(matrix[current.X, current.Y]);
		int destEvelvation = matrix[neighbor.X, neighbor.Y] == "E"
				? Elevations.IndexOf("z")
				: Elevations.IndexOf(matrix[neighbor.X, neighbor.Y]);

		int diff = destEvelvation - srcElevation;

		return diff <=0 || diff == 1;
	}

	string[,] LoadMatrix(int rows, int cols)
	{
		string[,] matrix = new string[_rows, _cols];

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				string c = _data[i].Substring(j, 1);
				matrix[i, j] = c;

				if (c == Start)
				{
					_start = new Point(i, j);
					_lowPoints.Add(new Point(i, j));
				}
				else if (c == "a")
				{
					_lowPoints.Add(new Point(i, j));
				}
				else if (c == Dest)
				{
					_dest = new Point(i, j);
				}
			}
		}

		return matrix;
	}

	class Point : IEquatable<Point>
	{
		public int X;
		public int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj) => Equals(obj as Point);
		public bool Equals(Point other) => (other != null && X == other.X && Y == other.Y);
		public override int GetHashCode() => HashCode.Combine(X, Y);
		public override string ToString() => $"[{X}, {Y}]";
	}

	class Node
	{
		// location in the matrix
		public Point Point;
		// distance from the starting point
		public int Distance;

		public Node(Point p, int d)
		{
			Point = p;
			Distance = d;
		}

		public override string ToString() => $"({Point.X}, {Point.Y})={Distance}";
	}
}