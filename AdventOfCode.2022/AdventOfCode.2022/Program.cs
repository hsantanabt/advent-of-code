﻿using System.Diagnostics;
using AOC2022.Day0;
using AOC2022.Day1;
using AOC2022.Day2;
using AOC2022.Day3;
using AOC2022.Day4;
using AOC2022.Day5;
using AOC2022.Day6;
using AOC2022.Day7;
using AOC2022.Day9;

var timer = Stopwatch.StartNew();

//CalorieCounting.Part1And2();

//RockPaperScissors.Part1();
//RockPaperScissors.Part2();

//Rucksack.Part1();
//Rucksack.Part2();

//CampCleanup.Part1();
//CampCleanup.Part2();

//SupplyStacks.Part1();
//SupplyStacks.Part2();

//TuningTrouble.Part1And2();

//NoDeviceSpace.Part1And2();

//Treetop.Part1();
//Treetop.Part2();

RopeBridge.Part1();

//CRT.Part1();
//CRT.Part2();

//MonkeyMiddle.Part1();
//MonkeyMiddle.Part2();

timer.Stop();

Console.WriteLine();
Console.WriteLine($"Elapse Time: {timer.ElapsedMilliseconds}ms");