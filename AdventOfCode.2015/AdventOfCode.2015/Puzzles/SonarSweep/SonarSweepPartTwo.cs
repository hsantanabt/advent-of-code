namespace AdventOfCode._2021.Puzzles.SonarSweep
{
	internal static class SonarSweepPartTwo
	{
		public static void Run(string inputFilePath)
		{
			var measurementLines = File.ReadAllLines(inputFilePath);
			var measurements = measurementLines.Select(int.Parse).ToList();
			var count = CountIncreasingSlidingMeasurementTrend(measurements);
			
			Console.WriteLine("Larger Measurements Found: " + count);
		}

		private static int CountIncreasingSlidingMeasurementTrend(IReadOnlyList<int> measurements)
		{
			if (measurements.Count < 3)
			{
				return -1;
			}

			var previousSlidingTrend = new[]
			{
				measurements[0],
				measurements[1],
				measurements[2]
			};

			var count = 0;

			for (var i = 0; i < measurements.Count; i++)
			{
				if (measurements.Count < i + 1 || 
				    measurements.Count < i + 2 ||
				    measurements.Count < i + 3)
				{
					break;
				}

				var currentSlidingTrend = new[]
				{
					measurements[i],
					measurements[i+1],
					measurements[i+2]
				};

				Console.WriteLine($"Current Trend: P={previousSlidingTrend.Print()} C={currentSlidingTrend.Print()}");

				var trend = MeasurementSlidingTrend(previousSlidingTrend, currentSlidingTrend);

				if (trend > 0)
				{
					count++;
				}

				previousSlidingTrend = currentSlidingTrend;
			}

			return count;
		}

		private static int MeasurementSlidingTrend(IEnumerable<int> previousSlidingWindow, IEnumerable<int> currentSlidingWindow)
		{
			var previousSum = previousSlidingWindow.Sum();
			var currentSum = currentSlidingWindow.Sum();

			return MeasurementTrend(previousSum, currentSum);
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

		private static string Print(this IEnumerable<int> array)
		{
			return string.Join(",", array);
		}
	}
}
