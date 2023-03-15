namespace AdventOfCode2021;

using AdventOfCode;

internal class Day02 : PuzzleBase
{
    private string[] _data;

    public Day02(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 02 - Dive!");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 2, "Day02test.txt")
            .GetInput(Year, 2)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int depth = 0;
        int position = 0;

        for (int i = 0; i < _data.Length; i++)
        {
            var row = _data[i].Split(" ");
            var command = row[0];
            var amount = Int32.Parse(row[1]);

            switch (command)
            {
                case "forward":
                    position += amount;
                    break;
                case "down":
                    depth += amount;
                    break;
                case "up":
                    depth -= amount;
                    break;
            }
        }

        Utils.WriteResults($"Puzzle 1: Depth '{depth}' X Position '{position}' => {depth * position}");
    }

    private void Puzzle2()
    {
        int depth = 0;
        int position = 0;
        int aim = 0;

        for(int i = 0; i < _data.Length; i++)
        {
            var row = _data[i].Split(" ");
            var command = row[0];
            var amount = Int32.Parse(row[1]);

            switch (command)
            {
                case "forward":
                    position += amount;
                    depth += (aim * amount);
                    break;
                case "down":
                    aim += amount;
                    break;
                case "up":
                    aim -= amount;
                    break;
            }
        }

        Utils.WriteResults($"Puzzle 2: Depth '{depth}' X Position '{position}' => {depth * position}");
    }
}