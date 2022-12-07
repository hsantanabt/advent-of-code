Console.WriteLine("Advent of Code");
Console.WriteLine();

const string message = "Passport Processing";

const bool test = true;

// ReSharper disable once RedundantAssignment
var challenge = File.ReadAllLines("input.txt");
var testInput = File.ReadAllLines("test-input.txt");

var input = test ? testInput : challenge;
var result = 0;

foreach (var line in input)
{
}

Console.WriteLine($"Part 1 - {message}: {result}");