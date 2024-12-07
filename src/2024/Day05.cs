namespace AdventOfCode2024;

using AdventOfCode;

internal class Day05 : PuzzleBase
{
    private string[] _data;

    private Dictionary<int, List<int>> _rules = new();
    private int _updatesStartingRow;
    
    public Day05(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 05 - Print Queue");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 5, "test.txt")
             .GetInput(Year, 5)
            .ConfigureAwait(false);

        LoadRules();
        
        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        int sum = 0;
        for (var i = _updatesStartingRow; i < _data.Length; i++)
        {
            var valid = true;
            //Utils.WriteDebug($"Pages: {_data[i]}");
            var _pages = _data[i].Split(',').Select(Int32.Parse).ToArray();
            for (var y = 0; y < _pages.Length - 1; y++)
            {
                //Utils.WriteDebug($"\tChecking Page '{_pages[y]}'");
                if (_rules.ContainsKey(_pages[y]))
                {
                    var rule = _rules[_pages[y]];
                    for (var j = y + 1; j < _pages.Length; j++)
                    {
                        valid = rule.Contains(_pages[j]);
                        //Utils.WriteDebug($"\t\tDoes page '{_pages[y]}' come before page '{_pages[j]}'?  {valid}");
                        if (!valid)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    //Utils.WriteDebug($"\t\tNo rule for for page '{_pages[y]}'");
                    valid = false;
                }

                if (!valid)
                {
                    break;
                }
            }
            
            if (valid)
            {
                //Utils.WriteDebug($"\tValid; Middle page # = {_pages[_pages.Length / 2]}");
                sum += _pages[_pages.Length / 2];
            }
        }
        
        Utils.WriteResults($"Puzzle 1: {sum}");
    }

    private void Puzzle2()
    {
        
        Utils.WriteResults($"Puzzle 2: ");
    }

    private void LoadRules()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (string.IsNullOrEmpty(_data[i]))
            {
                _updatesStartingRow = i + 1;
                break;
            }
            
            var split = _data[i].Split('|').Select(Int32.Parse).ToArray();

            if (_rules.ContainsKey(split[0]))
            {
                _rules[split[0]].Add(split[1]);
            }
            else
            {
                _rules.Add(split[0], new List<int> { split[1] });
            }
        }
    }
}