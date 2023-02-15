namespace AOC2022.Day9;

public static class RopeBridge
{
	private const string Day = "Day9";

	private struct Knot
	{
		public string Name;
		public (int X, int Y) Position;
	}

	private static readonly Dictionary<string, (int X, int Y)> MoveMap = new() {
		{"L", (-1, 0)},
		{"U", (0, 1)},
		{"R", (1, 0)},
		{"D", (0, -1)},
	};

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/sample.txt");
		var maxSteps = input.Select(line => line.Split(' ')).Select(move => int.Parse(move[1])).Max();
		var state = new string[maxSteps,maxSteps + 1];

		var startPosition = (X: 0, Y: state.GetLength(0) - 1);
		state[startPosition.Y, startPosition.X] = "s";

		var head = new Knot
		{
			Name = "H",
			Position = (startPosition.X, startPosition.Y)
		};
		var tail = new Knot
		{
			Name = "T",
			Position = (startPosition.X, startPosition.Y)
		};

		var visits = new Dictionary<(int X, int Y), int>();

		foreach (var line in input)
		{
			var move = line.Split(' ');
			var direction = move[0];
			var steps = int.Parse(move[1]);

			var moveDelta = MoveMap[direction];

			for (var s = 1; s <= steps; s++)
			{
				head.Position.X += moveDelta.X;
				head.Position.Y += moveDelta.Y;

				var dX = Math.Abs(head.Position.X - tail.Position.X);
				var dY = Math.Abs(head.Position.Y - tail.Position.Y);

				if (dX < 2 && dY < 2)
				{
					continue;
				}

				// Same column
				if (head.Position.X == tail.Position.X)
				{
					if (head.Position.Y > tail.Position.Y)
					{
						tail.Position.Y++;
					}
					else
					{
						tail.Position.Y--;
					}
				}
				// Same row
				else if (head.Position.Y == tail.Position.Y)
				{
					if (head.Position.X > tail.Position.Y)
					{
						tail.Position.X++;
					}
					else
					{
						tail.Position.X--;
					}
				}
				// Diagonal
				else
				{
					if (head.Position.X > tail.Position.Y)
					{
						tail.Position.X++;
					}
					else
					{
						tail.Position.X--;
					}

					if (head.Position.Y > tail.Position.Y)
					{
						tail.Position.Y++;
					}
					else
					{
						tail.Position.Y--;
					}
				}

				visits.TryAdd((tail.Position.X, tail.Position.Y), 0);
				visits[(tail.Position.X, tail.Position.Y)]++;
			}
		}

		//state[head.Position.Y, head.Position.X] = head.Name;
		//state[tail.Position.Y, tail.Position.X] = tail.Name;

		PrintState(state);

		
		Console.WriteLine($"Tail Visits: {visits.Count}");
	}

    public static void Part2()
    {
	    var input = File.ReadAllLines($"{Day}/sample.txt");

	    foreach (var line in input)
	    {

	    }

	    Console.WriteLine($"Test: {0}");
	}

    private static void PrintState(string[,] grid)
    {
	    for (var y = 0; y < grid.GetLength(0); y++)
	    {
		    for (var x = 0; x < grid.GetLength(1); x++)
		    {
			    var value = grid[y, x];
			    Console.Write(string.IsNullOrWhiteSpace(value) ? "." : value);
		    }
		    Console.WriteLine();
	    }
	}
}