namespace AdventOfCode2020;

using AdventOfCode;

internal class Day01 : PuzzleBase
{
    private string[] _data;
    private List<int> _input;

    public Day01(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 01 - Report Repair");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 1, "Day01test.txt")
            .GetInput(Year, 1)
            .ConfigureAwait(false);
     
        _input = _data.Select(x => Convert.ToInt32(x)).ToList();
        
        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int product = 0;
        
        for (var i = 0; i < _input.Count; i++)
        {
            var valueLookingFor = 2020 - _input[i];
            var indexOfResult = _input.IndexOf(valueLookingFor);

            //Utils.WriteDebug($"For value {input[i]}, looking for {valueLookingFor}... ");

            if (indexOfResult != -1 && i != indexOfResult)
            {
                product = _input[i] * valueLookingFor;
                break;
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {product}");
    }

    private void Puzzle2()
    {
        for (var i = 0; i < _input.Count; i++)
        {
            for (var j = 0; j < _input.Count; j++)
            {
                if (j == i) continue;

                for (var k = 0; k < _input.Count; k++)
                {
                    if (k == i || k == j) continue;

                    var value1 = _input[i];
                    var value2 = _input[j];
                    var value3 = _input[k];
                    var total = value1 + value2 + value3;

                    if (2020 == total)
                    {
                        double product = value1 * value2 * value3;
                        //Utils.WriteDebug($"Match found! Values {value1} + {value2} + {value3} = {total}; Product = {product}");
                        Utils.WriteResults($"Puzzle 2: {product}");
                        
                        return;
                    }
                }
            }
        }
    }
}