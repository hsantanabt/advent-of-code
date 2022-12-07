namespace AOC2022.Day3;

public static class Rucksack
{
	private const string Day = "Day3";

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");

		const string priorityString = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		var prioritySum = 0;

		foreach (var line in input)
		{
			var compartment1 = line[..(line.Length / 2)];
			var compartment2 = line[(line.Length / 2)..];

			var sharedItem = compartment1.Intersect(compartment2).First();
			var priority = priorityString.IndexOf(sharedItem);
			prioritySum += priority;
		}

		Console.WriteLine($"Sum of Priorities: {prioritySum}");
	}

    public static void Part2()
    {
	    var input = File.ReadAllLines($"{Day}/input.txt");

	    const string priorityString = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	    var prioritySum = 0;

	    for (var i = 0; i < input.Length; i+=3)
	    {
		    var elf1 = input[i];
		    var elf2 = input[i + 1];
		    var elf3 = input[i + 2];

		    var sharedItems1 = elf1.Intersect(elf2).ToList();
		    var sharedItems2 = elf2.Intersect(elf3).ToList();

		    var sharedItem = sharedItems1.Intersect(sharedItems2).First();

		    var priority = priorityString.IndexOf(sharedItem);
		    prioritySum += priority;
	    }

	    Console.WriteLine($"Sum of Priorities: {prioritySum}");
	}
}