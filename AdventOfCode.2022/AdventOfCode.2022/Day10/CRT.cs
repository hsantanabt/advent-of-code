// ReSharper disable InconsistentNaming
namespace AOC2022.Day0;

// ReSharper disable once InconsistentNaming
public static class CRT
{
	private const string Day = "Day10";

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");
		var instructions = new Queue<(string Instruction, int Value)>();

		foreach (var line in input)
		{
			var program = line.Split(' ');
			var instruction = program[0];
			var value = program.Length > 1 ? int.Parse(program[1]) : 0;

			switch (instruction)
			{
				case "noop":
					instructions.Enqueue((instruction, 0));
					break;
				default:
					instructions.Enqueue(("noop", 0));
					instructions.Enqueue((instruction, value));
					break;
			}
		}

		var registerX = 1;
		var cycles = instructions.Count;
		var signalCycles = new List<int> {20, 60, 100, 140, 180, 220};
		var signalStrengths = new List<int>();

		for (var cycle = 1; cycle <= cycles; cycle++)
		{
			if (signalCycles.Contains(cycle))
			{
				//Console.WriteLine($"Cycle # {cycle} - Register X: {registerX}");
				signalStrengths.Add(cycle * registerX);
			}

			var instruction = instructions.Dequeue();
			registerX += instruction.Value;

			//Console.WriteLine($"Cycle # {cycle} - Register X: {registerX}");
		}

		Console.WriteLine($"Sum of Signal Strengths: {signalStrengths.Sum()}");
	}

	private static readonly string DarkPixels = new ('.', 40);

    public static void Part2()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");
		var instructions = new Queue<(string Instruction, int Value)>();
		
		foreach (var line in input)
		{
			var program = line.Split(' ');
			var instruction = program[0];
			var value = program.Length > 1 ? int.Parse(program[1]) : 0;

			switch (instruction)
			{
				case "noop":
					instructions.Enqueue((instruction, 0));
					break;
				default:
					instructions.Enqueue(("noop", 0));
					instructions.Enqueue((instruction, value));
					break;
			}
		}

		var registerX = 1;
		var cycles = instructions.Count;
		var CRTRows = new Queue<string>();

		var currentCRTRow = string.Empty;
		var currentCRTIndex = 0;

		//DrawSpritePosition(registerX);
		//Console.WriteLine();

		for (var cycle = 1; cycle <= cycles; cycle++)
		{
			currentCRTRow += registerX - 1 <= currentCRTIndex && currentCRTIndex <= registerX + 1 ? "#" : ".";

			//var paddedCycle = cycle.ToString().PadLeft(3, ' ');
			//Console.WriteLine($"During cycle {paddedCycle}: CRT draws pixel in position {currentCRTIndex}");
			//Console.WriteLine($"Start of cycle {paddedCycle}: Register X is {registerX}");
			//Console.WriteLine($"Current CRT row: {currentCRTRow}");

			var instruction = instructions.Dequeue();
			registerX += instruction.Value;

			//Console.WriteLine($"End of cycle  {paddedCycle}: finish executing {instruction.Instruction} (Register X is now {registerX})");

			//DrawSpritePosition(registerX);
			//Console.WriteLine();

			currentCRTIndex++;

			if (cycle % 40 == 0)
			{
				CRTRows.Enqueue(currentCRTRow);
				currentCRTRow = string.Empty;
				currentCRTIndex = 0;
			}
		}

		foreach (var crtRow in CRTRows)
		{
			Console.WriteLine(crtRow);
		}
    }

    private static void DrawTestPixel(int sCycle, int eCycle)
    {
	    var pixels = new StringBuilder(DarkPixels);
	    pixels.Insert(19, "#").Remove(39, 1);
	    var cycleStart = sCycle.ToString().PadLeft(3, ' ');
		var cycleEnd = eCycle.ToString().PadLeft(3, ' ');
		Console.WriteLine($"Cycle {cycleStart} -> {pixels} <- Cycle {cycleEnd}");
    }

    private static void DrawSpritePosition(int spriteIndex)
    {
	    var pixels = new StringBuilder(DarkPixels);
	    pixels
		    .Insert(spriteIndex - 1, "#")
			.Insert(spriteIndex, "#")
		    .Insert(spriteIndex + 1, "#")
		    .Remove(37, 3);
		Console.WriteLine($"Sprite position: {pixels}");
    }
}