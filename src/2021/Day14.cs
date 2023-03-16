namespace AdventOfCode2021;

using AdventOfCode;

internal class Day14 : PuzzleBase
{
    private string[] _data;

    private string template;
    private Dictionary<string, string> rules;

    Dictionary<string, Int64> pairs;
    Dictionary<string, Int64> elements;
    
    public Day14(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 14 - Extended Polymerization");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 14, "Day14test.txt")
            .GetInput(Year, 14)
            .ConfigureAwait(false);

        Utils.WriteResults($"Puzzle 1: {Process(10)}");
        Utils.WriteResults($"Puzzle 2: {Process(40)}");
    }

    private Int64 Process(int steps)
    {
        Init();

        pairs = new Dictionary<string, Int64>();
        elements = new Dictionary<string, Int64>();

        // set initial state
        for (int i = 0; i < template.Length - 1; i++)
        {
            UpdatePair($"{template[i]}{template[i + 1]}");
            UpdateElements($"{template[i]}");
        }
        UpdateElements($"{template[template.Length - 1]}");

        // main procesing
        for (int count = 0; count < steps; count++)
        {
            foreach (var pair in new Dictionary<string, Int64>(pairs))
            {
                var newElement = rules[pair.Key];
                UpdateElements(newElement, pair.Value);
                pairs[pair.Key] -= pair.Value;
                UpdatePair($"{pair.Key[0]}{newElement}", pair.Value);
                UpdatePair($"{newElement}{pair.Key[1]}", pair.Value);
            }
        }

        Int64 max = elements.Select(p => p.Value).Max();
        Int64 min = elements.Select(p => p.Value).Min();

        return max - min;
    }
    
    private void UpdatePair(string pair, Int64 count = 1)
    {
        if (pairs.ContainsKey(pair))
        {
            pairs[pair] += count;
        }
        else
        {
            pairs.Add(pair, count);
        }
    }

    private void UpdateElements(string c, Int64 count = 1)
    {
        if (elements.ContainsKey(c))
        {
            elements[c] += count;
        }
        else
        {
            elements.Add(c, count);
        }
    }

    private void Init()
    {
        rules = new Dictionary<string, string>();

        foreach (var line in _data)
        {
            if (line.Contains("->"))
            {
                var rule = line.Split(" -> ");
                rules.Add(rule[0], rule[1]);
            }
            else if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            else
            {
                template = line;
            }
        }
    }
}