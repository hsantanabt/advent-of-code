namespace AOC2022.Day0;

public static class MonkeyMiddle
{
	private const string Day = "Day11";

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/sample.txt");
		var monkeys = new Dictionary<int, int>();
		foreach (var line in input)
		{
			if (string.IsNullOrWhiteSpace(line))
			{

				continue;
			}
		}

		Console.WriteLine($"Test: {0}");
	}

    public static void Part2()
    {
	    var input = File.ReadAllLines($"{Day}/sample.txt");

	    foreach (var line in input)
	    {

	    }

	    Console.WriteLine($"Test: {0}");
	}
}