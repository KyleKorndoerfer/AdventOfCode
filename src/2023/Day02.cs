using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2023;

using AdventOfCode;

internal class Day02 : PuzzleBase
{
    private const string Red = "red";
    private const string Green = "green";
    private const string Blue = "blue";
    
    private string[] _data;
    private List<Game> _games;

    public Day02(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 02 - Cube Conundrum");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 2, "test.txt")
            .GetInput(Year, 2)
            .ConfigureAwait(false);
     
        _games = _data.Select(line => new Game(line)).ToList();
        
        Puzzle1();
        Puzzle2();
    }

    private void Puzzle1()
    {
        var sum = _games
                .Where(game => game.IsValid(12, 13, 14))
                .Select(game => game.Number)
                .Sum();
        
        Utils.WriteResults($"Puzzle 1: {sum}");
    }
    
    private void Puzzle2()
    {
        var sum = _games.Select(game => game.Power()).Sum();
        
        Utils.WriteResults($"Puzzle 2: {sum}");
    }

    private class Game
    {
        public int Number { get; }
        private List<Dictionary<string, int>> Rounds { get; set; }

        // parse:  "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
        public Game(string input)
        {
            Rounds = new List<Dictionary<string, int>>();
            // Utils.WriteDebug($"Input: {input}");
            
            // 0 = "Game 1"
            // 1 = " 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
            var gameSplit = input.Split(":");

            Number = Int32.Parse(gameSplit[0].Split(" ")[1]);
            
            // 0 = " 3 blue, 4 red"
            // 1 = " 1 red, 2 green, 6 blue"
            // 2 = " 2 green"
            var rounds = gameSplit[1].Split(";");

            foreach (var round in rounds)
            {
                // Utils.WriteDebug($"  Round: {round}");
                var cubes = round.Trim().Split(",");

                Rounds.Add(cubes.Select(cube => cube.Trim().Split(" ")).ToDictionary(parts => parts[1].Trim(), parts => Int32.Parse(parts[0].Trim())));
            }
        }

        // determines if all of the rounds in the game are valid based on the passed in starting values
        public bool IsValid(int reds, int greens, int blues)
        {
            return !Rounds.Any(round => 
                (round.ContainsKey(Red) && round[Red] > reds) ||
                (round.ContainsKey(Green) && round[Green] > greens) || 
                (round.ContainsKey(Blue) && round[Blue] > blues));
        }

        public long Power()
        {
            var reds = Rounds
                    .Where(round => round.ContainsKey(Red))
                    .Select(round => round[Red])
                    .Max();
            var greens = Rounds
                    .Where(round => round.ContainsKey(Green))
                    .Select(round => round[Green])
                    .Max();
            var blues = Rounds
                    .Where(round => round.ContainsKey(Blue))
                    .Select(round => round[Blue])
                    .Max();

            return reds * greens * blues;
        }
    }
}