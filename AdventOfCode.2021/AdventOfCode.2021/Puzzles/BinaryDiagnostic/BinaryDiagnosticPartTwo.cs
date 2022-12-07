namespace AdventOfCode._2021.Puzzles.BinaryDiagnostic;

internal static class BinaryDiagnosticPartTwo
{
	public static void Run(string inputFilePath)
	{
		var inputLines = File.ReadAllLines(inputFilePath);
		var powerConsumption = CalculateLineSupportRating(inputLines);

		Console.WriteLine("Sub Line Support Rating: " + powerConsumption);
	}

	private static long CalculateLineSupportRating(IReadOnlyList<string> inputLines)
	{
		var oxygenGeneratorRatings = GetCommonBitRating(inputLines.ToList(), true);
		var co2ScrubberRatings = GetCommonBitRating(inputLines.ToList(), false);

		Console.WriteLine("Oxygen Generator Rating - Binary: " + oxygenGeneratorRatings.Print());
		Console.WriteLine("CO2 Scrubber Ratings - Binary: " + co2ScrubberRatings.Print());

		var oxygenGeneratorRating = Convert.ToInt64(oxygenGeneratorRatings[0], 2);
		var co2ScrubberRating = Convert.ToInt64(co2ScrubberRatings[0], 2);

		Console.WriteLine("Oxygen Generator Rating: " + oxygenGeneratorRating);
		Console.WriteLine("CO2 Scrubber Rating: " + co2ScrubberRating);

		return oxygenGeneratorRating * co2ScrubberRating;
	}

	private static List<string> GetCommonBitRating(List<string> ratings, bool mostCommon)
	{
		var index = 0;
		var size = ratings[0].Length;

		while (ratings.Count > 1 && index < size)
		{
			var commonBit = GetCommonBit(ratings, index, mostCommon);

			//Console.WriteLine("Common Bit: " + commonBit);
			//Console.WriteLine("Ratings: " + ratings.Print());
			//Console.WriteLine("--------------------------------------------------------------");

			ratings = ratings.Where(binaryNumber => binaryNumber[index] == commonBit).ToList();
			index++;
		}

		return ratings;
	}

	private static char GetCommonBit(IEnumerable<string> binaryNumbers, int index, bool mostCommon)
	{
		var zeroCount = 0;
		var oneCount = 0;

		foreach (var binaryNumber in binaryNumbers)
		{
			foreach (var _ in binaryNumber)
			{
				if (binaryNumber[index] == '0')
				{
					zeroCount++;
				}
				else
				{
					oneCount++;
				}
			}
		}

		if (mostCommon)
		{
			return zeroCount > oneCount ? '0' : '1';
		}

		return oneCount < zeroCount ? '1' : '0';
	}
}
