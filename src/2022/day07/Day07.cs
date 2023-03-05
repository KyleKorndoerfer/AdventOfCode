namespace AdventOfCode2022;

using AdventOfCode;

public class Day07 : PuzzleBase
{
	private readonly string _dataFile = Path.Combine(nameof(Day07).ToLower(),
			//"Day07test.txt");
			"Day07a.txt");

	private string[] _data;

	public Day07(string basePath) : base(basePath)
	{
		Utils.WriteDayHeader("Day 07 - No Space Left On Device");
	}
	
	public override void Run()
	{
		_data = File.ReadAllLines(Path.Combine(BasePath, _dataFile));

		DirectoryNode filesystem = BuildFilesystem();
		//PrintFilesystem(filesystem);	// DEBUG

		IList<DirectoryNode> directories = FindDirectories(filesystem);

		List<long> directorySizes = new List<long>();
		foreach (DirectoryNode directory in directories)
		{
			directorySizes.Add(GetDirectorySize(directory));
		}

		Puzzle1(directorySizes);
		Puzzle2(directorySizes);
	}

	void Puzzle1(List<long> directorySizes)
	{
		long sum = directorySizes.Where(x => x <= 100000).Sum();

		Utils.WriteResults($"Puzzle 1: Sum = {sum}");
	}

	void Puzzle2(List<long> directorySizes)
	{
		long deviceSize = 70000000;
		long freeSpaceNeeded = 30000000;

		// root drive will be the largest
		long usedSpace = directorySizes.Max();
		long freeSpace = deviceSize - usedSpace;
		long minSpaceToFree = freeSpaceNeeded - freeSpace;

		long minSizeToDelete = directorySizes.Where(x => x >= minSpaceToFree).Min();

		Utils.WriteResults($"Puzzle 2: Minumum size = {minSizeToDelete}");
	}

	DirectoryNode BuildFilesystem()
	{
		DirectoryNode root = new DirectoryNode("/");		// line 1

		DirectoryNode currentDirectory = root;
		for (int i = 1; i < _data.Length; i++)
		{
			var line = _data[i];

			if (!line.StartsWith("$"))
			{
				string[] info = line.Split(" ");
				if (info[0].StartsWith("dir"))
				{
					currentDirectory.AddChild(new DirectoryNode(info[1]));
				}
				else
				{
					currentDirectory.AddChild(new FileNode(info[1], Int32.Parse(info[0])));
				}
			}
			else if (line.StartsWith("$ cd"))
			{
				currentDirectory = currentDirectory.ChangeDirectory(line.Split(" ")[2]);
			}
			// ignore 'ls'; essentially a no-op
		}

		return root;
	}

	IList<DirectoryNode> FindDirectories(DirectoryNode currentNode)
	{
		List<DirectoryNode> nodes = new List<DirectoryNode>();

		foreach (DirectoryNode directory in currentNode.Directories)
		{
			nodes.AddRange(FindDirectories(directory));
		}

		nodes.Add(currentNode);
		return nodes;
	}

	long GetDirectorySize(DirectoryNode node)
	{
		long sum = 0;

		foreach (DirectoryNode directory in node.Directories)
		{
			sum += GetDirectorySize(directory);
		}

		return sum + node.DirectorySize;
	}

	void PrintFilesystem(Node node, int depth = 0)
	{
		var output = node.ToString();
		Console.WriteLine(output.PadLeft(output.Length + (depth * 2)));

		foreach(Node child in node.Children)
		{
			PrintFilesystem(child, depth + 1);
		}
	}

	abstract class Node
	{
		readonly List<Node> _children;

		public Node(string name)
		{
			Name = name;

			_children = new List<Node>();
		}

		public string Name { get; private set; }
		public Node Parent { get; private set; }
		public IList<Node> Children =>
				_children.OrderBy(x => x.Name).ToList<Node>();
		public IList<Node> Directories =>
				_children.Where(x => x is DirectoryNode).OrderBy(x => x.Name).ToList<Node>();
		public IList<Node> Files =>
				_children.Where(x => x is FileNode).OrderBy(x => x.Name).ToList<Node>();

		public void AddChild(Node child)
		{
			child.Parent = this;	// add self as the parent
			_children.Add(child);
		}
	}

	class DirectoryNode : Node
	{
		public DirectoryNode(string name) : base(name) { }

		public long DirectorySize =>
				Children.Where(x => x is FileNode).Sum(x => (x as FileNode).Size);

		public DirectoryNode ChangeDirectory(string name)
		{
			return name == ".."
				? (DirectoryNode)Parent ?? this
				: (DirectoryNode)Directories.Single(x => x.Name == name);
		}

		public override string ToString() => $"- {Name} (dir)";
	}

	class FileNode : Node
	{
		public FileNode(string name, int size) : base(name)
		{
			Size = size;
		}

		public int Size { get; private set; }

		public override string ToString() => $"- {Name} (file; Size={Size})";
	}
}