namespace AOC2022.Day4;

public static class CampCleanup
{
	private const string Day = "Day4";

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");
		var assignments = 0;

		foreach (var line in input)
		{
			var pairs = line.Split(',');
			var pair1 = pairs[0].Split('-');
			var pair2 = pairs[1].Split('-');

			var startRange1 = int.Parse(pair1[0]);
			var endRange1 = int.Parse(pair1[1]);

			var startRange2 = int.Parse(pair2[0]);
			var endRange2 = int.Parse(pair2[1]);

			if (startRange1 <= startRange2 && endRange1 >= endRange2 ||
			    startRange2 <= startRange1 && endRange2 >= endRange1)
			{
				assignments++;
			}
		}

		Console.WriteLine($"Number of Assignments: {assignments}");
	}

    public static void Part2()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");
		var assignments = 0;

		foreach (var line in input)
		{
			var pairs = line.Split(',');
			var pair1 = pairs[0].Split('-');
			var pair2 = pairs[1].Split('-');

			var startRange1 = int.Parse(pair1[0]);
			var endRange1 = int.Parse(pair1[1]);

			var startRange2 = int.Parse(pair2[0]);
			var endRange2 = int.Parse(pair2[1]);

			if (startRange1 <= startRange2 && endRange1 >= startRange2 ||
			    startRange1 <= endRange2 && endRange1 >= endRange2 ||
			    startRange2 <= startRange1 && endRange2 >= startRange1 ||
			    startRange2 <= endRange1 && endRange2 >= endRange1)
			{
				assignments++;
			}
		}

		Console.WriteLine($"Number of Assignments: {assignments}");
	}
}