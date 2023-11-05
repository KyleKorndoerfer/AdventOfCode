namespace AdventOfCode2020;

using System.Text.RegularExpressions;

using AdventOfCode;

internal class Day04 : PuzzleBase
{
    private string[] _data;
    private List<Passport> _passports = new();

    public Day04(int year, Downloader downloader) : base(year, downloader)
    {
        Utils.WriteDayHeader("Day 4 - Passport Processing");
    }

    public override async Task Run()
    {
        _data = await Downloader
            //.GetInput(Year, 4, "test.txt")
            .GetInput(Year, 4)
            .ConfigureAwait(false);

        ParseInput();
        
        Puzzle1();
        Puzzle2();  // 191 too high
    }

    private void Puzzle1()
    {
        Utils.WriteResults($"Puzzle 1: {_passports.Count(x => x.HasRequiredFields())}");
    }

    private void Puzzle2()
    {
        Utils.WriteResults($"Puzzle 2: {_passports.Count(x => x.HasValidFields())}");
    }

    private void ParseInput()
    {
        var tokens = new Dictionary<string, string>();
        for (var i = 0; i <= _data.Length; i++)
        {
            if (i == _data.Length || string.IsNullOrEmpty(_data[i]))
            {
                _passports.Add(new Passport(tokens));
                tokens = new Dictionary<string, string>();
                
                //Utils.WriteDebug($"Passport: {_passports.Last()} | Has Req = {_passports.Last().HasRequiredFields()} | IsValid = {_passports.Last().HasValidFields()}");
                
                continue;
            }

            var row = _data[i];
            var groups = row.Split(" ");
            foreach (var pair in groups)
            {
                var kvp = pair.Split(":");
                tokens.Add(kvp[0], kvp[1]);
            }
        }
    }

    private class Passport
    {
        private readonly string[] _validEyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public Passport(Dictionary<string, string> props)
        {
            BirthYear = props.TryGetValue("byr", out var byr) ? Convert.ToInt32(byr) : default;
            IssueYear = props.TryGetValue("iyr", out var iyr) ? Convert.ToInt32(iyr) : default;
            ExpirationYear = props.TryGetValue("eyr", out var eyr) ? Convert.ToInt32(eyr) : default;
            Height = props.TryGetValue("hgt", out var hgt) ? hgt : default;
            HairColor = props.TryGetValue("hcl", out var hcl) ? hcl : default;
            EyeColor = props.TryGetValue("ecl", out var ecl) ? ecl : default;
            PassportId = props.TryGetValue("pid", out var pid) ? pid : default;
        }

        private int BirthYear { get; }
        private int IssueYear { get; }
        private int ExpirationYear { get; }
        private string Height { get; }
        private string HairColor { get; }
        private string EyeColor { get; }
        private string PassportId { get; }

        public override string ToString()
        {
            return $"byr = {BirthYear}; iyr = {IssueYear}; eyr = {ExpirationYear}; hgt = {Height}; hcl = {HairColor}; ecl = {EyeColor}; pid = {PassportId}";
        }

        public bool HasRequiredFields()
        {
            return BirthYear != default &&
                   IssueYear != default &&
                   ExpirationYear != default &&
                   !string.IsNullOrEmpty(Height) &&
                   !string.IsNullOrEmpty(HairColor) &&
                   !string.IsNullOrEmpty(EyeColor) &&
                   !string.IsNullOrEmpty(PassportId);
        }
        
        public bool HasValidFields()
        {
            return HasRequiredFields() &&
                   Enumerable.Range(1920, 83).Contains(BirthYear) &&
                   Enumerable.Range(2010, 11).Contains(IssueYear) &&
                   Enumerable.Range(2020, 11).Contains(ExpirationYear) &&
                   Regex.IsMatch(Height, "^1(([5-8][0-9])|9[0-3])cm$|^(([5-6][0-9])|7[0-6])in$") &&
                   Regex.IsMatch(HairColor, "^#[0-9a-f]{6}$") &&
                   _validEyeColors.Contains(EyeColor) &&
                   Regex.IsMatch(PassportId, "^[0-9]{9}$");
        }
    }
}