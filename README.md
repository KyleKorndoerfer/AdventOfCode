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

You can also override these settings using environment variables as well.

You will also need to configure the `AocSessionToken` in order to automatically download
the official puzzle input files from the AOC website. See the [Data Files](#data-files) 
section below for more information.  

### Examples

- If you want to run all puzzles for 2022 you would set `AocYear = 2022`
  and `AocDay = 0`.
- Setting `AocYear = 0` and `AocDay = 1` will run the Day 1 puzzle for each year.

## Data Files

Data files for each puzzle should be placed inside a `data` folder at the root of the
solution and follow this pattern:

`.\data\{year}\{day}\{files}`

Each puzzle gets an instance of the `Downloader` class injected into it that allows you to
easily load input files for the current puzzle, downloading it as needed.

- `GetInput(int year, int day, string filename)` - use this method to load test files that
you have manually created in the folder structure specified above.

- `GetInput(year, day)` - use this method to load the official input file for the puzzle. If
this file doesn't already exist in the folder structure specified above, it will be downloaded
from the Advent of Code website and saved locally.

### Session Token

In order to download the puzzle input files, you will need to log into the AOC website and use
the browser tools to inspect the request headers. In the `cookie` header you sill see 
`session={token_value}`. Grab to token value and use the following commands to add it to the
project secure settings.

If this is the first time running this project, you will need to initialize the user secrets store:

```
dotnet user-secrets init
```
After that, you can run this command anytime you need to add/update the session token value
(last for about 30 days):

```
dotnet user-secrets set "Settings:AocSessionToken" "{token_value}"
```

## _Note_
The code created here is not meant to be a well architected solution to each problem,
but rather an exercise in getting an answer to a solution, generally as fast as I can.
If the problem is particularly interesting to me, I might circle back later and create
a variation of my original solution.