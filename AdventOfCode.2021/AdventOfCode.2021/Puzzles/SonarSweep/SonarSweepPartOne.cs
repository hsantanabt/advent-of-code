namespace AdventOfCode._2021.Puzzles.SonarSweep
{
	internal static class SonarSweepPartOne
	{
		public static void Run(string inputFilePath)
		{
			var measurementLines = File.ReadAllLines(inputFilePath);
			var measurements = measurementLines.Select(int.Parse).ToList();
			var count = CountIncreasingMeasurementTrend(measurements);
			
			Console.WriteLine("Larger Measurements Found: " + count);
		}

		private static int CountIncreasingMeasurementTrend(IReadOnlyList<int> measurements)
		{
			if (measurements.Count == 0)
			{
				return -1;
			}

			var previousMeasurement = measurements[0];
			var count = 0;

			foreach (var measurement in measurements)
			{
				Console.WriteLine($"Current Trend: P={previousMeasurement} C={measurement}");

				var trend = MeasurementTrend(previousMeasurement, measurement);

				if (trend > 0)
				{
					count++;
				}

				previousMeasurement = measurement;
			}

			return count;
		}

		private static int MeasurementTrend(int previousMeasurement, int currentMeasurement)
		{
			if (previousMeasurement == currentMeasurement)
			{
				return 0; // No change
			}

			if (previousMeasurement < currentMeasurement)
			{
				return 1; // Increased
			}

			return -1; // Decreased
		}
	}
}
