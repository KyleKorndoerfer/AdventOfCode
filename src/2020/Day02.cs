namespace AdventOfCode2020;

using AdventOfCode;

internal class Day02 : PuzzleBase
{
    private string[] _data;
    
    public Day02(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 2 - Password Philosophy");
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
        var input = ProcessData();
        
        var validPasswordCount = 0;
        foreach (var item in input)
        {
            var count = item.Password.ToCharArray().Count(x => x == item.Letter);
            var isValid = count >= item.MinOccurs && count <= item.MaxOccurs;

            //Utils.WriteDebug($"INPUT: {item}");
            //Utils.WriteDebug($"\tFound {count} instances; Valid = {isValid}");

            if (isValid)
            {
                validPasswordCount++;
            }
        }

        Utils.WriteResults($"Puzzle 1: {validPasswordCount}");
    }

    private void Puzzle2()
    {
        var input = ProcessData2();
        
        var validPasswordCount = 0;
        foreach (var item in input)
        {
            var chars = item.Password.ToCharArray();
            var isValid = chars[item.FirstPosition-1] == item.Letter ^ chars[item.SecondPosition-1] == item.Letter;

            //Utils.WriteDebug($"INPUT: {item}\n\tValid = {isValid}");

            if (isValid)
            {
                validPasswordCount++;
            }
        }

        Utils.WriteResults($"Puzzle 2: {validPasswordCount}");
    }

    private IList<DataInput> ProcessData()
    {
        var input = new List<DataInput>();
        
        foreach (var line in _data)
        {
            var lineTokens = line.Split(" ");
            var minMaxTokens = lineTokens[0].Split("-");

            var item = new DataInput
            {
                MinOccurs = Convert.ToInt32(minMaxTokens[0]),
                MaxOccurs = Convert.ToInt32(minMaxTokens[1]),
                Letter = lineTokens[1].Substring(0, 1)[0],
                Password = lineTokens[2]
            };

            input.Add(item);
        }

        return input;
    }

    private IList<DataInput2> ProcessData2()
    {
        List<DataInput2> input = new List<DataInput2>();

        foreach (var line in _data)
        {
            var lineTokens = line.Split(" ");
            var minMaxTokens = lineTokens[0].Split("-");

            var item = new DataInput2
            {
                FirstPosition = Convert.ToInt32(minMaxTokens[0]),
                SecondPosition = Convert.ToInt32(minMaxTokens[1]),
                Letter = lineTokens[1].Substring(0, 1)[0],
                Password = lineTokens[2]
            };

            input.Add(item);
        }

        return input;
    }
    
    private class DataInput
    {
        public int MinOccurs { get; set; }
        public int MaxOccurs { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Min = {MinOccurs}, Max = {MaxOccurs}, Letter = {Letter}, Password = {Password}";
        }
    }
    
    private class DataInput2
    {
        public int FirstPosition { get; set; }
        public int SecondPosition { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"First = {FirstPosition}, Second = {SecondPosition}, Letter = {Letter}, Password = {Password}";
        }
    }
}