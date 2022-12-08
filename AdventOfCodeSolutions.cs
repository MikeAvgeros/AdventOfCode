using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoC2022.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                var opponent = new Day2Model(round.First());
                var me = new Day2Model(round.Last());

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
                var opponent = new Day2Model(round.First());
                var me = new Day2Model2(opponent, round.Last());
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
                if (int.Parse(pairs.First().First()) <= int.Parse(pairs.Last().First()) &&
                    int.Parse(pairs.Last().Last()) <= int.Parse(pairs.First().Last()) ||
                    int.Parse(pairs.Last().First()) <= int.Parse(pairs.First().First()) &&
                    int.Parse(pairs.First().Last()) <= int.Parse(pairs.Last().Last()))
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
                if (int.Parse(pairs.Last().First()) <= int.Parse(pairs.First().Last()) &&
                    int.Parse(pairs.First().First()) <= int.Parse(pairs.Last().Last()))
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
    }
}
