using System;
using System.IO;
using System.Linq;
using AoC2022.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2022
{
    [TestClass]
    public class AdventOfCode
    {
        [TestMethod]
        public void Day1_1()
        {
            var input = File.ReadAllText("Resources/input1.txt");
            var inputList = input.Split("\r\n\r\n").Select(s => s.Split("\r\n").Sum(int.Parse));
            Console.WriteLine(inputList.OrderByDescending(x => x).FirstOrDefault());
        }

        [TestMethod]
        public void Day1_2()
        {
            var input = File.ReadAllText("Resources/input1.txt");
            var inputList = input.Split("\r\n\r\n").Select(s => s.Split("\r\n").Sum(int.Parse));
            Console.WriteLine(inputList.OrderByDescending(x => x).Take(3).Sum());
        }

        [TestMethod]
        public void Day2_1()
        {
            var input = File.ReadAllText("Resources/input2.txt");
            var inputList = input.Split("\r\n").Select(s => s.Split(" ")).ToList();
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
            var input = File.ReadAllText("Resources/input2.txt");
            var inputList = input.Split("\r\n").Select(s => s.Split(" ")).ToList();
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
            var input = File.ReadAllText("Resources/input4.txt");
            var inputList = input.Split("\r\n")
                    .Select(s => s.Split(",")
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
            var input = File.ReadAllText("Resources/input4.txt");
            var inputList = input.Split("\r\n")
                .Select(s => s.Split(",")
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
    }
}
