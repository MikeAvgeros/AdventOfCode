using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AoC2022.Models;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Directory = AoC2022.Models.Directory;
using FileInfo = AoC2022.Models.FileInfo;

namespace AoC2022
{
    [TestClass]
    public class AdventOfCodeSolutions
    {
        [TestMethod]
        public void Day1_1()
        {
            var input = File.ReadAllText("Resources/input1.txt");
            var inputList = input.Split("\r\n\r\n")
                                               .Select(s => s.Split("\r\n")
                                               .Sum(int.Parse));
            Console.WriteLine(inputList.OrderByDescending(x => x).FirstOrDefault());
        }

        [TestMethod]
        public void Day1_2()
        {
            var input = File.ReadAllText("Resources/input1.txt");
            var inputList = input.Split("\r\n\r\n")
                                               .Select(s => s.Split("\r\n")
                                               .Sum(int.Parse));
            Console.WriteLine(inputList.OrderByDescending(x => x).Take(3).Sum());
        }

        [TestMethod]
        public void Day2_1()
        {
            var input = File.ReadAllLines("Resources/input2.txt");
            var inputList = input.Select(s => s.Split(" ")).ToList();
            long totalScore = 0;

            foreach (var round in inputList)
            {
                var opponent = new Round(round.First());
                var me = new Round(round.Last());

                if (me.Shape == opponent.Shape)
                {
                    totalScore += me.ShapePoints + 3;
                    continue;
                }

                if (me.WinsAgainst == opponent.Shape)
                {
                    totalScore += me.ShapePoints + 6;
                    continue;
                }
                totalScore += me.ShapePoints;
            }

            Console.WriteLine(totalScore);
        }

        [TestMethod]
        public void Day2_2()
        {
            var input = File.ReadAllLines("Resources/input2.txt");
            var inputList = input.Select(s => s.Split(" ")).ToList();
            long totalScore = 0;

            foreach (var round in inputList)
            {
                var opponent = new Round(round.First());
                var me = new Strategy(opponent, round.Last());
                totalScore += me.Points;
            }

            Console.WriteLine(totalScore);
        }

        [TestMethod]
        public void Day3_1()
        {
            var input = File.ReadAllText("Resources/input3.txt");
            var inputList = input.Split("\r\n").ToList();
            long total = 0;

            foreach (var line in inputList)
            {
                var intersect = line.Take(line.Length / 2)
                    .Intersect(line.TakeLast(line.Length / 2))
                    .Sum(s => char.IsUpper(s) ? s - 38 : s - 96);
                total += intersect;
            }

            Console.WriteLine(total);
        }

        [TestMethod]
        public void Day3_2()
        {
            var input = File.ReadAllText("Resources/input3.txt");
            var inputList = input.Split("\r\n").ToList();
            long total = 0;

            while (inputList.Count > 0)
            {
                var three = inputList.Take(3).ToList();
                var intersect = three[0].Intersect(three[1]).Intersect(three[2])
                    .Sum(s => char.IsUpper(s) ? s - 38 : s - 96);
                total += intersect;
                inputList.RemoveRange(0, 3);
            }

            Console.WriteLine(total);
        }

        [TestMethod]
        public void Day4_1()
        {
            var input = File.ReadAllLines("Resources/input4.txt");
            var inputList = input.Select(s => s.Split(",")
                    .Select(x => x.Split("-")).ToList())
                    .ToList();

            var duplicatePairs = 0;

            foreach (var pairs in inputList)
            {
                var firstStart = int.Parse(pairs.First().First());
                var firstEnd = int.Parse(pairs.First().Last());
                var secondStart = int.Parse(pairs.Last().First());
                var secondEnd = int.Parse(pairs.Last().Last());

                if (firstStart <= secondStart && secondEnd <= firstEnd ||
                    secondStart <= firstStart && firstEnd <= secondEnd)
                {
                    duplicatePairs++;
                }
            }

            Console.WriteLine(duplicatePairs);
        }

        [TestMethod]
        public void Day4_2()
        {
            var input = File.ReadAllLines("Resources/input4.txt");
            var inputList = input.Select(s => s.Split(",")
                    .Select(x => x.Split("-")).ToList())
                    .ToList();

            var duplicatePairs = 0;

            foreach (var pairs in inputList)
            {
                var firstStart = int.Parse(pairs.First().First());
                var firstEnd = int.Parse(pairs.First().Last());
                var secondStart = int.Parse(pairs.Last().First());
                var secondEnd = int.Parse(pairs.Last().Last());

                if (secondStart <= firstEnd && firstStart <= secondEnd)
                {
                    duplicatePairs++;
                }
            }

            Console.WriteLine(duplicatePairs);
        }

        [TestMethod]
        public void Day5_1()
        {
            var input = File.ReadAllLines("Resources/input5.txt").ToList();
            var stacks = input.Take(input.IndexOf(string.Empty)).ToList();
            var numOfStacks = Regex.Replace(stacks.Last(), @"\s+", "").Length;
            var crates = new Stack<char>[numOfStacks];

            foreach (var line in stacks.SkipLast(1).Reverse())
            {
                for (var i = 0; i < numOfStacks; i++)
                {
                    var crate = line[i * 4 + 1];
                    if (!char.IsLetter(crate)) continue;
                    crates[i] ??= new Stack<char>();
                    crates[i].Push(crate);
                }
            }

            var movesInfo = input.Skip(input.IndexOf(string.Empty) + 1).ToList();
            var moveRegex = new Regex("move (?<amount>\\d+) from (?<from>\\d+) to (?<to>\\d+)");
            var moves = new List<Move>();

            foreach (var move in movesInfo)
            {
                var amount = moveRegex.Match(move).Groups["amount"].Success
                    ? int.Parse(moveRegex.Match(move).Groups["amount"].Value)
                    : 0;
                var from = moveRegex.Match(move).Groups["from"].Success
                    ? int.Parse(moveRegex.Match(move).Groups["from"].Value)
                    : 0;
                var to = moveRegex.Match(move).Groups["to"].Success
                    ? int.Parse(moveRegex.Match(move).Groups["to"].Value)
                    : 0;
                var newMove = new Move(amount, from, to);
                moves.Add(newMove);
            }

            foreach (var move in moves)
            {
                for (var i = 1; i <= move.Amount; i++)
                {
                    if (crates[move.From - 1].TryPop(out var crate))
                    {
                        crates[move.To - 1].Push(crate);
                    }
                }
            }

            var output = "";

            foreach (var crate in crates)
            {
                if (crate.TryPeek(out var character))
                {
                    output += character;
                }
            }

            Console.WriteLine(output);
        }

        [TestMethod]
        public void Day5_2()
        {
            var input = File.ReadAllLines("Resources/input5.txt").ToList();
            var stacks = input.Take(input.IndexOf(string.Empty)).ToList();
            var numOfStacks = Regex.Replace(stacks.Last(), @"\s+", "").Length;
            var crates = new Stack<char>[numOfStacks];

            foreach (var line in stacks.SkipLast(1).Reverse())
            {
                for (var i = 0; i < numOfStacks; i++)
                {
                    var crate = line[i * 4 + 1];
                    if (!char.IsLetter(crate)) continue;
                    crates[i] ??= new Stack<char>();
                    crates[i].Push(crate);
                }
            }

            var movesInfo = input.Skip(input.IndexOf(string.Empty) + 1).ToList();
            var moveRegex = new Regex("move (?<amount>\\d+) from (?<from>\\d+) to (?<to>\\d+)");
            var moves = new List<Move>();

            foreach (var move in movesInfo)
            {
                var amount = moveRegex.Match(move).Groups["amount"].Success
                    ? int.Parse(moveRegex.Match(move).Groups["amount"].Value)
                    : 0;
                var from = moveRegex.Match(move).Groups["from"].Success
                    ? int.Parse(moveRegex.Match(move).Groups["from"].Value)
                    : 0;
                var to = moveRegex.Match(move).Groups["to"].Success
                    ? int.Parse(moveRegex.Match(move).Groups["to"].Value)
                    : 0;
                var newMove = new Move(amount, from, to);
                moves.Add(newMove);
            }

            foreach (var move in moves)
            {
                var tempCrateStack = new Stack<char>();

                for (var i = 1; i <= move.Amount; i++)
                {
                    if (crates[move.From - 1].TryPop(out var crate))
                    {
                        tempCrateStack.Push(crate);
                    }
                }

                foreach (var crate in tempCrateStack)
                {
                    crates[move.To - 1].Push(crate);
                }
            }

            var output = "";

            foreach (var crate in crates)
            {
                if (crate.TryPeek(out var character))
                {
                    output += character;
                }
            }

            Console.WriteLine(output);
        }

        [TestMethod]
        public void Day6_1()
        {
            var input = File.ReadAllText("Resources/input6.txt");
            var charList = input.ToCharArray().ToList();
            var tempCharList = new List<char>(charList);

            while (tempCharList.Count != 0)
            {
                var check = tempCharList.Take(4).Distinct().ToList();
                if (check.Count == 4) break;
                tempCharList.RemoveRange(0, 1);
            }

            var marker = (charList.Count - tempCharList.Count) + 4;

            Console.WriteLine(marker);
        }

        [TestMethod]
        public void Day6_2()
        {
            var input = File.ReadAllText("Resources/input6.txt");
            var charList = input.ToCharArray().ToList();
            var tempCharList = new List<char>(charList);

            while (tempCharList.Count != 0)
            {
                var check = tempCharList.Take(14).Distinct().ToList();
                if (check.Count == 14) break;
                tempCharList.RemoveRange(0, 1);
            }

            var marker = (charList.Count - tempCharList.Count) + 14;

            Console.WriteLine(marker);
        }

        [TestMethod]
        public void Day7_1()
        {
            var input = File.ReadAllLines("Resources/input7.txt");
            var rootDirectory = new RootDirectory("/");
            var currentDirectory = new Directory("");
            var directoryRegex = new Regex("dir (?<name>\\w+)");
            var fileRegex = new Regex("(?<filesize>\\d+) (?<filename>\\D+)");

            foreach (var line in input)
            {
                if (line.Contains("cd"))
                {
                    var cmdInfo = line.Split(' ');

                    if (cmdInfo.Length == 3)
                    {
                        if (string.Equals(cmdInfo[2], "/"))
                        {
                            currentDirectory = rootDirectory;
                        }

                        if (Regex.Match(cmdInfo[2], "\\w+").Success)
                        {
                            var newDir = currentDirectory?.SubDirectories.FirstOrDefault(x => 
                                             x.Name == cmdInfo[2]) 
                                             ?? new Directory(cmdInfo[2])
                                             {
                                                 ParentDirectory = currentDirectory
                                             };
                            currentDirectory = newDir;
                        }

                        if (string.Equals(cmdInfo[2], ".."))
                        {
                            currentDirectory = currentDirectory.ParentDirectory;
                        }
                    }
                }

                if (directoryRegex.Match(line).Success)
                {
                    var directory = new Directory(directoryRegex.Match(line).Groups["name"].Value)
                    {
                        ParentDirectory = currentDirectory
                    };
                    currentDirectory.SubDirectories.Add(directory);
                }

                if (fileRegex.Match(line).Success)
                {
                    var file = new FileInfo
                    {
                        FileSize = long.Parse(fileRegex.Match(line).Groups["filesize"].Value),
                        FileName = fileRegex.Match(line).Groups["filename"].Value
                    };
                    currentDirectory.Files.Add(file);
                }
            }

            Console.WriteLine(rootDirectory.CalculateTotalSize());
        }

        [TestMethod]
        public void Day7_2()
        {
            var input = File.ReadAllLines("Resources/input7.txt");
            var rootDirectory = new RootDirectory("/");
            var currentDirectory = new Directory("");
            var directoryRegex = new Regex("dir (?<name>\\w+)");
            var fileRegex = new Regex("(?<filesize>\\d+) (?<filename>\\D+)");

            foreach (var line in input)
            {
                if (line.Contains("cd"))
                {
                    var cmdInfo = line.Split(' ');

                    if (cmdInfo.Length == 3)
                    {
                        if (string.Equals(cmdInfo[2], "/"))
                        {
                            currentDirectory = rootDirectory;
                        }

                        if (Regex.Match(cmdInfo[2], "\\w+").Success)
                        {
                            var newDir = currentDirectory?.SubDirectories.FirstOrDefault(x =>
                                             x.Name == cmdInfo[2])
                                             ?? new Directory(cmdInfo[2])
                                             {
                                                 ParentDirectory = currentDirectory
                                             };
                            currentDirectory = newDir;
                        }

                        if (string.Equals(cmdInfo[2], ".."))
                        {
                            currentDirectory = currentDirectory.ParentDirectory;
                        }
                    }
                }

                if (directoryRegex.Match(line).Success)
                {
                    var directory = new Directory(directoryRegex.Match(line).Groups["name"].Value)
                    {
                        ParentDirectory = currentDirectory
                    };
                    currentDirectory.SubDirectories.Add(directory);
                }

                if (fileRegex.Match(line).Success)
                {
                    var file = new FileInfo
                    {
                        FileSize = long.Parse(fileRegex.Match(line).Groups["filesize"].Value),
                        FileName = fileRegex.Match(line).Groups["filename"].Value
                    };
                    currentDirectory.Files.Add(file);
                }
            }

            const long totalDiskSize = 70000000;
            const long updateSizeReq = 30000000;

            var remainingDiskSize = totalDiskSize - rootDirectory.TotalFileSize;
            var offset = updateSizeReq - remainingDiskSize;

            var deletionOptions = RootDirectory.GetAllDirectories(rootDirectory)
                .Where(x => x.TotalFileSize >= offset).ToList();

            Console.WriteLine(deletionOptions.OrderBy(x => x.TotalFileSize).FirstOrDefault()?.TotalFileSize);
        }
    }
}
