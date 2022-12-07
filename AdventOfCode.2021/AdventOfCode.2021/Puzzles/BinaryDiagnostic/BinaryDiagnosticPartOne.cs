namespace AdventOfCode._2021.Puzzles.BinaryDiagnostic
{
	internal static class BinaryDiagnosticPartOne
	{
		public static void Run(string inputFilePath)
		{
			var inputLines = File.ReadAllLines(inputFilePath);
			var powerConsumption = CalculateSubPowerConsumption(inputLines);

			Console.WriteLine("Sub Power Consumption: " + powerConsumption);
		}

		private static long CalculateSubPowerConsumption(string[] inputLines)
		{
			var size = inputLines[0].Length;
			var zeroCount = new int[size];
			var oneCount = new int[size];

			for (int i = 0; i < inputLines.Length; i++)
			{
				var line = inputLines[i];

				for (int j = 0; j < line.Length; j++)
				{
					var bit = line[j];

					if (bit == '0')
					{
						zeroCount[j]++;
					} else
					{
						oneCount[j]++;
					}
				}
			}

			Console.WriteLine("Zero Count: " + zeroCount.Print());
			Console.WriteLine("One Count: " + oneCount.Print());

			var gammaRateString = string.Empty;
			var epsilonRateString = string.Empty;

			for (int i = 0; i < size; i++)
			{
				if (zeroCount[i] > oneCount[i])
				{
					gammaRateString += "0";
					epsilonRateString += "1";
				} else
				{
					gammaRateString += "1";
					epsilonRateString += "0";
				}
			}

			var gammaRate = Convert.ToInt64(gammaRateString, 2);
			var epsilonRate = Convert.ToInt64(epsilonRateString, 2);

			Console.WriteLine("Gamma Rate String: " + gammaRateString);
			Console.WriteLine("Gamma Rate: " + gammaRate);
			Console.WriteLine("Epsilon Rate String: " + epsilonRateString);
			Console.WriteLine("Epsilon Rate: " + epsilonRate);

			return gammaRate * epsilonRate;
		}

		private static string Print(this int[] array)
		{
			return "[" + string.Join(",", array) + "]";
		}
	}
}
