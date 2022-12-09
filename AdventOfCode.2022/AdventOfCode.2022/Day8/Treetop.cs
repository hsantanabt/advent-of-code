namespace AOC2022.Day0;

public static class Treetop
{
	private const string Day = "Day8";

	private struct Tree
	{
		public int Height;
		public bool Visible;

		public int ScenicScore => LeftScore * UpScore * RightScore * DownScore;

		public int LeftScore;
		public int UpScore;
		public int RightScore;
		public int DownScore;
	}

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/sample.txt");

		var height = input.Length;
		var width = input[0].Length;

		var treeMap = new Tree[height, width];

		for (var y = 0; y < treeMap.GetLength(1); y++)
		{
			var line = input[y];
			for (var x = 0; x < treeMap.GetLength(0); x++)
			{
				treeMap[y, x] = new Tree
				{
					Height = int.Parse(line[x].ToString())
				};
			}
		}

		var visibleTrees = 0;
		int previousTreeHeight;

		// left to right
		for (var y = 0; y < treeMap.GetLength(0); y++)
		{
			previousTreeHeight = -1;

			for (var x = 0; x < treeMap.GetLength(1); x++)
			{
				var tree = treeMap[y, x];

				if (previousTreeHeight >= tree.Height)
				{
					continue;
				}

				previousTreeHeight = tree.Height;
				tree.Visible = true;
				treeMap[y, x] = tree;
			}
		}

		// right to left
		for (var y = 0; y < treeMap.GetLength(0); y++)
		{
			previousTreeHeight = -1;

			for (var x = treeMap.GetLength(1) - 1; x >= 0; x--)
			{
				var tree = treeMap[y, x];

				if (previousTreeHeight >= tree.Height)
				{
					continue;
				}

				previousTreeHeight = tree.Height;
				tree.Visible = true;
				treeMap[y, x] = tree;
			}
		}

		// top to bottom
		for (var x = 0; x < treeMap.GetLength(1); x++)
		{
			previousTreeHeight = -1;

			for (var y = 0; y < treeMap.GetLength(0); y++)
			{
				var tree = treeMap[y, x];

				if (previousTreeHeight >= tree.Height)
				{
					continue;
				}

				previousTreeHeight = tree.Height;
				tree.Visible = true;
				treeMap[y, x] = tree;
			}
		}

		// bottom to top
		for (var x = 0; x < treeMap.GetLength(1); x++)
		{
			previousTreeHeight = -1;

			for (var y = treeMap.GetLength(0) - 1; y >= 0; y--)
			{
				var tree = treeMap[y, x];

				if (previousTreeHeight >= tree.Height)
				{
					continue;
				}

				previousTreeHeight = tree.Height;
				tree.Visible = true;
				treeMap[y, x] = tree;
			}
		}

		//PrintVisibleGrid(treeMap);

		// Check Visibility
		for (var y = 0; y < treeMap.GetLength(1); y++)
		{
			for (var x = 0; x < treeMap.GetLength(0); x++)
			{
				var tree = treeMap[y, x];

				if (tree.Visible)
				{
					visibleTrees++;
				}
			}
		}

		Console.WriteLine($"Visible Trees: {visibleTrees}");
	}

    public static void Part2()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");

		var height = input.Length;
		var width = input[0].Length;

		var treeMap = new Tree[height, width];

		for (var y = 0; y < treeMap.GetLength(1); y++)
		{
			var line = input[y];
			for (var x = 0; x < treeMap.GetLength(0); x++)
			{
				treeMap[y, x] = new Tree
				{
					Height = int.Parse(line[x].ToString()),
					LeftScore = 0,
					UpScore = 0,
					RightScore = 0,
					DownScore = 0
				};
			}
		}

		for (var treeY = 0; treeY < treeMap.GetLength(1); treeY++)
		{
			for (var treeX = 0; treeX < treeMap.GetLength(0); treeX++)
			{
				var tree = treeMap[treeY, treeX];

				// left
				for (var x = treeX - 1; x >= 0; x--)
				{
					tree.LeftScore++;

					if (treeMap[treeY, x].Height >= tree.Height)
					{
						break;
					}
				}

				// top
				for (var y = treeY - 1; y >= 0; y--)
				{
					tree.UpScore++;

					if (treeMap[y, treeX].Height >= tree.Height)
					{
						break;
					}

				}

				// right
				for (var x = treeX + 1; x < treeMap.GetLength(1); x++)
				{
					tree.RightScore++;

					if (treeMap[treeY, x].Height >= tree.Height)
					{
						break;
					}
				}

				// bottom
				for (var y = treeY + 1; y < treeMap.GetLength(0); y++)
				{
					tree.DownScore++;

					if (treeMap[y, treeX].Height >= tree.Height)
					{
						break;
					}
				}

				treeMap[treeY, treeX] = tree;
			}
		}

		//PrintScenicGrid(treeMap);

		// Check Scenic Score
		var scenicScores = new List<int>();
		for (var y = 0; y < treeMap.GetLength(1); y++)
		{
			for (var x = 0; x < treeMap.GetLength(0); x++)
			{
				var tree = treeMap[y, x];
				scenicScores.Add(tree.ScenicScore);
			}
		}

		var scenicScore = scenicScores.Max();
		Console.WriteLine($"Highest Scenic Score: {scenicScore}");
	}

    private static void PrintVisibleGrid(Tree[,] grid)
    {
	    for (var y = 0; y < grid.GetLength(1); y++)
	    {
		    for (var x = 0; x < grid.GetLength(0); x++)
		    {
			    var tree = grid[y, x];

			    if (tree.Visible)
			    {
				    Console.Write(tree.Height);
				}
			    else
			    {
				    Console.Write("-");
				}
		    }
			Console.WriteLine();
	    }
	}

    private static void PrintScenicGrid(Tree[,] grid)
    {
	    for (var y = 0; y < grid.GetLength(1); y++)
	    {
		    for (var x = 0; x < grid.GetLength(0); x++)
		    {
			    var tree = grid[y, x];

			    if (tree.ScenicScore > 0)
			    {
				    Console.Write(tree.ScenicScore);
			    }
			    else
			    {
				    Console.Write("-");
			    }
		    }
		    Console.WriteLine();
	    }
    }
}