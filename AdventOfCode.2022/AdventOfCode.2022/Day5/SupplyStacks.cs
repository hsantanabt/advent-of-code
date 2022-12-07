namespace AOC2022.Day5;

public static class SupplyStacks
{
	private const string Day = "Day5";

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");

		var stackIds = 
			input.SkipWhile(l => !l.StartsWith(" 1")).First()
				.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		var stacks = stackIds.ToDictionary(int.Parse, _ => new List<char>());

		// Crates
		foreach (var line in input)
		{
			if (line.StartsWith(" 1"))
			{
				break;
			}

			var stackId = 0;
			for (var i = 0; i < line.Length; i+=4)
			{
				stackId++;
				var crate = line.ElementAt(i + 1);

				if (crate == ' ')
				{
					continue;
				}

				stacks[stackId].Add(crate);
			}
		}

		// Moves
		foreach (var line in input.SkipWhile(l => !l.StartsWith("move")))
		{
			var moves = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			var quantity = int.Parse(moves[1]);
			var fromStack = int.Parse(moves[3]);
			var toStack = int.Parse(moves[5]);

			for (var i = 1; i <= quantity; i++)
			{
				stacks[toStack].Insert(0, stacks[fromStack].First());
				stacks[fromStack].RemoveAt(0);
			}
		}

		var topCrates = stacks.Aggregate(string.Empty, (current, stack) => current + stack.Value.First());
		Console.WriteLine($"Crates on Top: {topCrates}");
	}

    public static void Part2()
    {
	    var input = File.ReadAllLines($"{Day}/input.txt");

	    var stackIds =
		    input.SkipWhile(l => !l.StartsWith(" 1")).First()
			    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

	    var stacks = stackIds.ToDictionary(int.Parse, _ => new List<char>());

	    // Crates
	    foreach (var line in input)
	    {
		    if (line.StartsWith(" 1"))
		    {
			    break;
		    }

		    var stackId = 0;
		    for (var i = 0; i < line.Length; i += 4)
		    {
			    stackId++;
			    var crate = line.ElementAt(i + 1);

			    if (crate == ' ')
			    {
				    continue;
			    }

			    stacks[stackId].Add(crate);
		    }
	    }

	    // Moves
	    foreach (var line in input.SkipWhile(l => !l.StartsWith("move")))
	    {
		    var moves = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		    var quantity = int.Parse(moves[1]);
		    var fromStack = int.Parse(moves[3]);
		    var toStack = int.Parse(moves[5]);

		    var crates = new List<char>();
		    for (var i = 1; i <= quantity; i++)
		    {
				crates.Add(stacks[fromStack].First());
				stacks[fromStack].RemoveAt(0);
		    }

		    stacks[toStack].InsertRange(0, crates);
		}

	    var topCrates = stacks.Aggregate(string.Empty, (current, stack) => current + stack.Value.First());
	    Console.WriteLine($"Crates on Top: {topCrates}");
	}
}