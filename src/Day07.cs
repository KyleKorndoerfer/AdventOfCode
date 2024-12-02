namespace AdventOfCode2023;

using AdventOfCode;

internal class Day07 : PuzzleBase
{
    private string[] _data;

    private const string StandardSorting = "23456789TJQKA";
    private const string ModifiedSorting = "J23456789TQKA";
    
    public Day07(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 07 - Camel Cards");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 7, "test.txt")
            .GetInput(Year, 7)
            .ConfigureAwait(false);

        var hands = LoadHands();
        
        Puzzle1(hands);
        Puzzle2(hands);
    }
    
    private void Puzzle1(List<Hand> hands)
    {
        var grouped = hands.GroupBy(r => r.Ranking);
        var ordered = grouped.OrderBy(g => g.Key);

        List<Hand> ranked = new();
        foreach (var group in ordered)
        {
            var rankHands = group.Select(g => g).ToList();
            rankHands.Sort(new HandSorter(StandardSorting));
            ranked.AddRange(rankHands);
        }

        long winnings = 0;
        for (var i = 0; i < ranked.Count; i++)
        {
            winnings += ranked[i].Bid * (i + 1);
        }
        
        Utils.WriteResults($"Puzzle 1: {winnings}");
    }

    private void Puzzle2(List<Hand> hands)
    {
        var grouped = hands.GroupBy(r => r.ModifiedRanking);
        var ordered = grouped.OrderBy(g => g.Key);

        List<Hand> ranked = new();
        foreach (var group in ordered)
        {
            var rankHands = group.Select(g => g).ToList();
            rankHands.Sort(new HandSorter(ModifiedSorting));
            ranked.AddRange(rankHands);
        }

        long winnings = 0;
        for (var i = 0; i < ranked.Count; i++)
        {
            winnings += ranked[i].Bid * (i + 1);
        }
        
        Utils.WriteResults($"Puzzle 2: {winnings}");
    }

    private List<Hand> LoadHands()
    {
        return _data
                .Select(line => line.Split(" "))
                .Select(split => new Hand(split[0], split[1]))
                .ToList();
    }
    
    class Hand
    {
        public string Cards { get; }
        public int Bid { get; }
        public int Ranking { get; }
        public int ModifiedRanking { get; }

        public Hand(string cards, string bid)
        {
            Cards = cards;
            Bid = int.Parse(bid);
            Ranking = DetermineRanking(cards);
            ModifiedRanking = DetermineModifiedRanking(cards);
        }

        private int DetermineRanking(string cards)
        {
            var groups = cards.GroupBy(c => c);
            return groups.Count() switch
            {
                // High Card
                5 => 0,
                // One pair
                4 => 1,
                // Two pair
                3 when groups.Count(g => g.Count() == 2) == 2 => 2,
                // Three of a kind
                3 when groups.Any(g => g.Count() == 3) => 3,
                // Full house
                2 when !groups.Any(g => g.Count() == 4) => 4,
                // Four of a kind
                2 when groups.Any(g => g.Count() == 4) => 5,
                // Five of a kind
                _ => 6
            };
        }

        private int DetermineModifiedRanking(string cards)
        {
            return 0;
        }
    }
    
    class HandSorter : Comparer<Hand>
    {
        private readonly string _sortOrder;
        
        public HandSorter(string sortOrder)
        {
            _sortOrder = sortOrder;
        }
        public override int Compare(Hand a, Hand b)
        {
            if (a is null) return -1;
            if (b is null) return 1;
                
            for (var i = 0; i < 5; i++)
            {
                if (a.Cards[i] == b.Cards[i]) continue;
                if (_sortOrder.IndexOf(a.Cards[i]) < _sortOrder.IndexOf(b.Cards[i])) return -1;
                if (_sortOrder.IndexOf(a.Cards[i]) > _sortOrder.IndexOf(b.Cards[i])) return 1;
            }

            return 0;
        }
    }
}