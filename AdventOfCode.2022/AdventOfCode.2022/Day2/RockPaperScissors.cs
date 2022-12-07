namespace AOC2022.Day2;

public static class RockPaperScissors
{
    public static void Part1()
    {
        var input = File.ReadAllLines("Day2/input.txt");

        var handMap = new Dictionary<string, string>
        {
	        { "A", "Rock" },
	        { "B", "Paper" },
	        { "C", "Scissors" },
	        { "X", "Rock" },
	        { "Y", "Paper" },
	        { "Z", "Scissors" }
		};

        var handBeatsMap = new Dictionary<string, string>
        {
	        { "Rock", "Scissors" },	// Rock (A & X) defeats Scissors (C & Z)
	        { "Paper", "Rock" },	// Paper (B & Y) defeats Rock (A & X)
	        { "Scissors", "Paper" } // Scissors (C & Z) defeats Paper (B & Y)
        };

        var handPointsMap = new Dictionary<string, int>
        {
	        { "Rock", 1 },
	        { "Paper", 2 },
	        { "Scissors", 3 }
        };

        var totalPoints = 0;

		foreach (var line in input)
		{
			var roundPoints = 0;
			var play = line.Split(' ');
			var opponentHand = handMap[play[0]];
			var playerHand = handMap[play[1]];

			// Draw
			if (opponentHand == playerHand)
			{
				roundPoints = 3;
			} 
			// Player Wins
			else if (handBeatsMap[playerHand] == opponentHand)
			{
				roundPoints = 6;
			}

			totalPoints += roundPoints + handPointsMap[playerHand];
		}

        Console.WriteLine($"Total Score: {totalPoints}");
    }

    public static void Part2()
    {
		var input = File.ReadAllLines("Day2/input.txt");

		var handMap = new Dictionary<string, string>
		{
			{ "A", "Rock" },
			{ "B", "Paper" },
			{ "C", "Scissors" }
		};

		var handBeatsMap = new Dictionary<string, string>
		{
			{ "Rock", "Scissors" },	// Rock (A) defeats Scissors (C)
			{ "Paper", "Rock" },	// Paper (B) defeats Rock (A)
			{ "Scissors", "Paper" },	// Scissors (C) defeats Paper (B)
		};

		var handPointsMap = new Dictionary<string, int>
		{
			{ "Rock", 1 },
			{ "Paper", 2 },
			{ "Scissors", 3 }
		};

		var outcomeMap = new Dictionary<string, string>
		{
			{ "X", "Loss" },
			{ "Y", "Draw" },
			{ "Z", "Win" },
		};

		var totalPoints = 0;

		foreach (var line in input)
		{
			var roundPoints = 0;
			var play = line.Split(' ');
			var opponentHand = handMap[play[0]];
			var playerOutcome = outcomeMap[play[1]];
			var playerHand = "";

			switch (playerOutcome)
			{
				case "Draw":
					playerHand = opponentHand;
					break;
				case "Loss":
					playerHand = handBeatsMap[opponentHand];
					break;
				case "Win":
					playerHand = handBeatsMap[handBeatsMap[opponentHand]];
					break;
			}

			// Draw
			if (opponentHand == playerHand)
			{
				roundPoints = 3;
			}
			// Player Wins
			else if (handBeatsMap[playerHand] == opponentHand)
			{
				roundPoints = 6;
			}

			totalPoints += roundPoints + handPointsMap[playerHand];
		}

		Console.WriteLine($"Total Score: {totalPoints}");
	}
}