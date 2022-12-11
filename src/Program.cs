// See https://aka.ms/new-console-template for more information

using System.Reflection;

using AdventOfCode;

Console.WriteLine(" ________________");
Console.WriteLine("| Advent of Code |");
Console.WriteLine(" ----------------");

const string AOC_Prefix = "AdventOfCode";

string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

// reflect over the assembly looking for year implementations
var puzzleYears = Assembly
		.GetExecutingAssembly()
		.GetTypes()
		.Where(x => x.GetInterfaces().Contains(typeof(IPuzzleYear)))
		.OrderBy(x => x);

// select the specified year or take the most recent one
var puzzleYear = string.IsNullOrEmpty(StaticSettings.AocYear)
		? puzzleYears.TakeLast(1).First()
		: puzzleYears.First(t => t.Name == $"{AOC_Prefix}{StaticSettings.AocYear}");

// create instance & run it
IPuzzleYear instance = Activator.CreateInstance(puzzleYear) as IPuzzleYear;
instance.Run(baseDirectory);
