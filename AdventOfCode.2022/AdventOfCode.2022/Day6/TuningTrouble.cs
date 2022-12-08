namespace AOC2022.Day6;

public static class TuningTrouble
{
	private const string Day = "Day6";

	public static void Part1And2()
    {
	    var input = File.ReadAllLines($"{Day}/input.txt");

		// Part 1
		ProcessMarker(input, 4);

		// Part 2
		ProcessMarker(input, 14);
	}

	private static void ProcessMarker(IEnumerable<string> input, int markerLength)
    {
	    var markerCharacter = 1;

	    foreach (var line in input)
	    {
		    for (var i = 0; i < line.Length; i++)
		    {
			    var marker = line.Substring(i, Math.Min(markerLength, line.Length - (i + 1)));

			    if (marker.Distinct().Count() != markerLength)
			    {
				    continue;
			    }

			    markerCharacter = i + markerLength;
			    break;
		    }

		    Console.WriteLine($"Characters Processed: {markerCharacter}");
	    }
	}
}