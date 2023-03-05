# Advent Of Code

These are my attempts at the yearly [Advent of Code](https://adventofcode.com) challenges.

While I primarily use .NET/C# for solving these as that is my primary language, I
might circle back and use these challenges to learn other languages.

## Configuring

I have added some structure to this project to make it easier to build and run one or
more puzzles using reflection. To control which puzzle is run, update the settings in the
`appsettings.json` file as follows:

- `AocYear` specifies the year that you would like to run puzzles from
  - set this value to `0` if you want to run all years there are solutions for
- `AocDay` specifies the day of the puzzle you would like to run
  - set this value to `0` if you want to run all puzzles

### Examples

- If you want to run all puzzles for 2022 you would set `AocYear = 2022`
  and `AocDay = 0`.
- Setting `AocYear = 0` and `AocDay = 1` will run the Day 1 puzzle for each year.

### Data Files

Data files for each puzzle should be placed inside a `data` folder at the root of the
solution and follow this pattern:

`.\data\{year}\{day}\{files}`

_TODO: Build an input downloader that automatically downloads input files if not already downloaded._ 

### _Note_
The code created here is not meant to be a well architected solution to each problem,
but rather an exercise in getting an answer to a solution, generally as fast as I can.
If the problem is particularly interesting to me, I might circle back later and create
a variation of my original solution.