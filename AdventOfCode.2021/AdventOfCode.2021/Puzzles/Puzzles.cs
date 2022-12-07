namespace AdventOfCode._2021.Puzzles;

internal static class Puzzles
{
	private const string InputFilesFolderPath = "Puzzles";

	public static void BinaryDiagnosingPartOne()
	{
		const string puzzleName = "BinaryDiagnostic";

		Console.WriteLine($"{puzzleName} - Part 1");

		Console.WriteLine("Test");
		BinaryDiagnosticPartOne.Run($"{InputFilesFolderPath}/{puzzleName}/{puzzleName}-PartOne-test-input.txt");

		Console.WriteLine("Answer");
		BinaryDiagnosticPartOne.Run($"{InputFilesFolderPath}/{puzzleName}/{puzzleName}-PartOne-input.txt");

		Console.WriteLine();
		Console.WriteLine();
	}

	public static void BinaryDiagnosingPartTwo()
	{
		const string puzzleName = "BinaryDiagnostic";
		const string puzzlePartNumber = "Two";

		Console.WriteLine($"{puzzleName} - Part {puzzlePartNumber}");

		Console.WriteLine("Test");
		BinaryDiagnosticPartTwo.Run($"{InputFilesFolderPath}/{puzzleName}/{puzzleName}-Part{puzzlePartNumber}-test-input.txt");

		Console.WriteLine("Answer");
		BinaryDiagnosticPartTwo.Run($"{InputFilesFolderPath}/{puzzleName}/{puzzleName}-Part{puzzlePartNumber}-input.txt");

		Console.WriteLine();
		Console.WriteLine();
	}

	public static void DivingPartOne()
	{
		Console.WriteLine("Dive - Part 1");

		Console.WriteLine("Test");
		DivePartOne.Run($"{InputFilesFolderPath}/Dive/Dive-PartOne-test-input.txt");

		Console.WriteLine("Answer");
		DivePartOne.Run($"{InputFilesFolderPath}/Dive/Dive-PartOne-input.txt");

		Console.WriteLine();
		Console.WriteLine();
	}

	public static void DivingPartTwo()
	{
		Console.WriteLine("Dive - Part 2");

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

