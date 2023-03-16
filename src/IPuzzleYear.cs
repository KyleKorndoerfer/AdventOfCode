namespace AdventOfCode;

internal interface IPuzzleYear
{
	int Year { get; }
	
	Task Run();
}