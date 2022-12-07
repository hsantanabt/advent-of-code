using AdventOfCode._2021.Puzzles.Dive;
using AdventOfCode._2021.Puzzles.SonarSweep;

namespace AdventOfCode._2021.Puzzles
{
	internal static class Puzzles
	{
		private const string InputFilesFolderPath = "Puzzles";

		public static void DivingPartOne()
		{
			Console.WriteLine("DivePartOne - Part 1");

			Console.WriteLine("Test");
			DivePartOne.Run($"{InputFilesFolderPath}/Dive/Dive-PartOne-test-input.txt");

			Console.WriteLine("Answer");
			DivePartOne.Run($"{InputFilesFolderPath}/Dive/Dive-PartOne-input.txt");

			Console.WriteLine();
			Console.WriteLine();
		}

		public static void DivingPartTwo()
		{
			Console.WriteLine("DivePartOne - Part 2");

			Console.WriteLine("Test");
			DivePartTwo.Run($"{InputFilesFolderPath}/Dive/Dive-PartTwo-test-input.txt");

			Console.WriteLine("Answer");
			DivePartTwo.Run($"{InputFilesFolderPath}/Dive/Dive-PartTwo-input.txt");

			Console.WriteLine();
			Console.WriteLine();
		}

		public static void SweepingSonarPartOne()
		{
			Console.WriteLine("Sweeping Sonar - Part 1");

			Console.WriteLine("Test");
			SonarSweepPartOne.Run($"{InputFilesFolderPath}/SonarSweep/SonarSweep-PartOne-test-input.txt");

			Console.WriteLine("Answer");
			SonarSweepPartOne.Run($"{InputFilesFolderPath}/SonarSweep/SonarSweep-PartOne-input.txt");

			Console.WriteLine();
			Console.WriteLine();
		}

		public static void SweepingSonarPartTwo()
		{
			Console.WriteLine("Sweeping Sonar - Part 2");

			Console.WriteLine("Test");
			SonarSweepPartTwo.Run($"{InputFilesFolderPath}/SonarSweep/SonarSweep-PartTwo-test-input.txt");

			Console.WriteLine("Answer");
			SonarSweepPartTwo.Run($"{InputFilesFolderPath}/SonarSweep/SonarSweep-PartTwo-input.txt");

			Console.WriteLine();
			Console.WriteLine();
		}
	}
}
