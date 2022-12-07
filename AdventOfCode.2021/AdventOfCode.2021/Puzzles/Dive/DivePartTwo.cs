namespace AdventOfCode._2021.Puzzles.Dive
{
	internal static class DivePartTwo
	{
		public static void Run(string inputFilePath)
		{
			var inputLines = File.ReadAllLines(inputFilePath);
			var product = CalculateSubPositionProduct(inputLines);

			Console.WriteLine("Sub Product: " + product);
		}

		private static int CalculateSubPositionProduct(IEnumerable<string> commands)
		{
			var subPosition = new SubPosition();

			foreach (var commandString in commands)
			{
				var inputs = commandString.Split(" ");
				var command = inputs[0];
				var change = int.Parse(inputs[1]);
				Console.WriteLine("----------------------------------------");
				Console.WriteLine("Current SubPos: " + subPosition.Print());

				subPosition = GetPosition(subPosition, command, change);

				Console.WriteLine("New SubPos: " + subPosition.Print());
			}

			return subPosition.Horizontal * subPosition.Depth;
		}

		private static SubPosition GetPosition(SubPosition currentPosition, string command, int change)
		{
			switch (command)
			{
				case "forward":
					currentPosition.Horizontal += change;
					currentPosition.Depth += currentPosition.Aim * change;
					break;
				case "down":
					//currentPosition.Depth += change;
					currentPosition.Aim += change;
					break;
				case "up":
					//currentPosition.Depth -= change;
					currentPosition.Aim -= change;
					break;
			}

			return currentPosition;
		}
	}
}
