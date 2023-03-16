namespace AdventOfCode2021;

using AdventOfCode;

internal class Day03 : PuzzleBase
{
    private string[] _data;

    public Day03(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 03 - Binary Diagnostic");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 3, "Day03test.txt")
            .GetInput(Year, 3)
            .ConfigureAwait(false);

        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int[] ones = DetermineMostSignificantValues();

        string gamma = string.Empty;
        string epsilon = string.Empty;
        for (int i = 0; i < ones.Length; i++)
        {
            string value = (ones[i] > _data.Length / 2 ? "1" : "0");

            gamma += value;
            epsilon += (value == "1" ? "0" : "1");
        }

        int gammaInt = Convert.ToInt32(gamma, 2);
        int epsilonInt = Convert.ToInt32(epsilon, 2);

        //Utils.WriteDebug($"Gamma   = {gamma} ({gammaInt})");
        //Utils.WriteDebug($"Epsilon = {epsilon} ({epsilonInt})");
        Utils.WriteResults($"Puzzle 1: {gammaInt * epsilonInt}");
    }

    private void Puzzle2()
    {
        int bits = _data[0].Length;
            
        string[] o2Ratings = (string[])_data.Clone();
        string[] co2Ratings = (string[])_data.Clone();

        for (int i = 0; i < bits; i++)
        {
            int ones = 0;
            int zeros = 0;
            char selectiveChar;

            // filter O2 ratings 
            if (o2Ratings.Length > 1)
            {
                ones = CountOf('1', o2Ratings, i);
                zeros = o2Ratings.Length - ones;
                selectiveChar = ones >= zeros ? '1' : '0';
                o2Ratings = o2Ratings.Where(x => x[i] == selectiveChar).ToArray();
            }

            // filter CO2 ratings
            if (co2Ratings.Length > 1)
            {
                zeros = CountOf('0', co2Ratings, i);
                ones = co2Ratings.Length - zeros;
                selectiveChar = zeros <= ones ? '0' : '1';
                co2Ratings = co2Ratings.Where(x => x[i] == selectiveChar).ToArray();
            }

            if (o2Ratings.Length == 1 && co2Ratings.Length == 1)
            {
                break;  // no sense processing further!
            }
        }

        int o2RatingValue = Convert.ToInt32(o2Ratings.First(), 2);
        int co2RatingValue = Convert.ToInt32(co2Ratings.First(), 2);

        //Utils.WriteDebug($"\nO2 rating  = {o2Ratings.First()} ({o2RatingValue})");
        //Utils.WriteDebug($"CO2 rating = {co2Ratings.First()} ({co2RatingValue})");
        Utils.WriteResults($"Puzzle 2: {o2RatingValue * co2RatingValue}");
    }
    
    private int[] DetermineMostSignificantValues()
    {
        int bits = _data[0].Length;
        int[] ones = new int[bits];

        // loop over data rows
        foreach (string row in _data)
        {
            // loop over characters in the row
            for (int i = 0; i < bits; i++)
            {
                if (row[i] == '1')
                {
                    ones[i]++;
                }
            }
        }

        return ones;
    }
    
    private int CountOf(char c, string[] rows, int position)
    {
        int count = 0;

        // loop over data rows
        foreach (string row in rows)
        {
            count += (row[position] == c) ? 1 : 0;
        }

        return count;
    }
}