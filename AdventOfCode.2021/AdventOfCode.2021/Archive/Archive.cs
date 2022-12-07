using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode._2021.Common.Search;

internal class Archive
{
	#region Archive

	void TobogganTrajectory()
	{
		const string message = "Trees";

		const bool test = false;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input.txt");
		var testInput = File.ReadAllLines("test-input.txt");

		var input = test ? testInput : challenge;

		var map = ParseMatrix(input);
		var trees = new char[map.GetLength(0), map.GetLength(1)];

		//Console.WriteLine("Map:");
		//Console.WriteLine(map.Print());

		var treeCounts = new List<int>();
		var slopes = new List<(int x, int y)>
		{
			(3, 1),
			(1, 1),
			(5, 1),
			(7, 1),
			(1, 2)
		};

		foreach (var (x, y) in slopes)
		{
			treeCounts.Add(GetTreeCount(map, trees, x, y));
		}

		//Console.WriteLine("Trees:");
		//Console.WriteLine(trees.Print());

		var result = treeCounts.Aggregate((t, c) => t * c);

		Console.WriteLine($"Part 1 - {message}: {treeCounts.First()}");
		Console.WriteLine($"Part 2 - {message}: {result}");
	}

	char[,] ParseMatrix(IReadOnlyList<string> input)
	{
		var matrix = new char[input[0].Length, input.Count];

		for (var y = 0; y < matrix.GetLength(1); y++)
		{
			var line = input[y];

			for (var x = 0; x < matrix.GetLength(0); x++)
			{
				matrix[x, y] = line[x];
			}
		}

		return matrix;
	}

	int GetTreeCount(char[,] map, char[,] trees, int xd, int yd)
	{
		var treeCount = 0;

		var xPosition = 0;
		var yPosition = 0;

		var xLength = map.GetLength(0) - 1;
		var yLength = map.GetLength(1) - 1;

		while (yPosition <= yLength)
		{
			if (xPosition <= xLength &&
			    yPosition <= yLength)
			{
				var isTree = map[xPosition, yPosition] == '#';

				trees[xPosition, yPosition] = isTree ? 'X' : '0';
				treeCount += isTree ? 1 : 0;
			}

			if (xPosition > xLength)
			{
				xPosition = Math.Abs(xPosition - xLength - 1);
			}
			else
			{
				xPosition += xd;
				yPosition += yd;
			}
		}

		return treeCount;
	}

	void Chiton()
	{
		const string message = "Lowest Total Risk Path";

		const bool test = true;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input.txt");
		var testInput = File.ReadAllLines("test-input.txt");

		var input = test ? testInput : challenge;

		var result = 0;
		var riskMap = new int[input[0].Length, input.Length];
		var pathMap = new int[input[0].Length, input.Length];
		var nodeMap = new Node[input[0].Length, input.Length];

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < line.Length; x++)
			{
				riskMap[x, y] = line[x] - '0';
			}
		}

		var random = new Random();
		var map = new Map();

		for (var y = 0; y < riskMap.GetLength(1); y++)
		{
			for (var x = 0; x < riskMap.GetLength(0); x++)
			{
				var newNode = new Node
				{
					Point = new Point
					{
						X = x,
						Y = y
					},
					Id = Guid.NewGuid(),
					Name = riskMap[x, y].ToString(),
					Value = riskMap[x, y]
				};

				nodeMap[x, y] = newNode;
				map.Nodes.Add(newNode);
			}
		}

		foreach (var node in map.Nodes)
		{
			node.ConnectClosestNodes(nodeMap);
		}

		map.StartNode = map.Nodes.First();
		map.EndNode = map.Nodes.Last();

		var searchEngine = new SearchEngine(map);
		var sw = Stopwatch.StartNew();
		//map.ShortestPath = searchEngine.GetShortestPathAStar();
		map.ShortestPath = searchEngine.GetShortestPathDijikstra();

		result = Convert.ToInt32(searchEngine.ShortestPathCost);

		foreach (var node in map.ShortestPath)
		{
			pathMap[node.Point.X, node.Point.Y] = node.Value;
		}

		//Console.WriteLine("Risk Map");
		//Console.WriteLine(riskMap.Print());

		Console.WriteLine("Path Map");
		Console.WriteLine(pathMap.Print());

		Console.WriteLine($"Total: {map.Nodes.Count}\r\n" +
							$"Visited {searchEngine.NodeVisits}\r\n" +
							$"Time: {sw.Elapsed.TotalMilliseconds}ms\r\n" +
							$"Path length: {searchEngine.ShortestPathLength:0.00}\r\n" +
							$"Path Cost: {searchEngine.ShortestPathCost:0.00}");
		Console.WriteLine();

		Console.WriteLine($"Path Length: {searchEngine.ShortestPathLength}");
		Console.WriteLine($"{message}: {result}");
	}

	void CalculateCrabFuel()
	{
		var challenge = new[]
	{
		1101, 1, 29, 67, 1102, 0, 1, 65, 1008, 65, 35, 66, 1005, 66, 28, 1, 67, 65, 20, 4, 0, 1001, 65, 1, 65, 1106, 0, 8,
		99, 35, 67, 101, 99, 105, 32, 110, 39, 101, 115, 116, 32, 112, 97, 115, 32, 117, 110, 101, 32, 105, 110, 116, 99,
		111, 100, 101, 32, 112, 114, 111, 103, 114, 97, 109, 10, 52, 1088, 462, 1398, 576, 241, 636, 512, 28, 390, 168, 262,
		6, 489, 1152, 466, 539, 133, 159, 1481, 128, 198, 858, 57, 12, 1155, 400, 137, 557, 1370, 440, 885, 1433, 360, 387,
		5, 173, 397, 465, 426, 365, 470, 456, 45, 1052, 1116, 26, 17, 585, 647, 357, 786, 313, 1124, 346, 694, 941, 124,
		825, 243, 852, 76, 618, 436, 596, 14, 958, 969, 895, 1745, 246, 822, 239, 952, 928, 206, 406, 190, 459, 841, 25,
		1087, 299, 962, 15, 1539, 1003, 456, 51, 546, 858, 137, 1214, 110, 936, 975, 1164, 51, 82, 947, 1354, 312, 132, 261,
		181, 287, 107, 1411, 332, 930, 60, 1458, 22, 248, 175, 3, 946, 1097, 35, 231, 648, 109, 313, 1061, 163, 1382, 80,
		912, 89, 718, 1068, 419, 703, 155, 321, 909, 9, 212, 478, 315, 118, 206, 38, 125, 130, 1391, 229, 8, 44, 571, 432,
		24, 283, 0, 941, 422, 251, 686, 578, 154, 123, 489, 86, 1217, 129, 227, 638, 47, 187, 946, 2, 536, 227, 640, 1170,
		1444, 286, 1280, 83, 1253, 1735, 70, 52, 104, 658, 367, 302, 462, 394, 13, 798, 514, 104, 260, 479, 526, 632, 1161,
		1118, 320, 196, 262, 571, 1319, 594, 131, 797, 37, 566, 1054, 271, 159, 1021, 244, 204, 447, 624, 825, 723, 364,
		234, 105, 362, 305, 391, 681, 692, 89, 380, 104, 1217, 814, 1467, 898, 207, 1345, 94, 10, 1380, 50, 1192, 178, 1539,
		1712, 145, 390, 9, 878, 144, 1241, 395, 10, 41, 80, 1719, 1077, 113, 46, 1699, 130, 91, 723, 359, 1617, 1065, 530,
		1058, 903, 163, 412, 45, 858, 10, 1704, 141, 451, 1314, 879, 13, 857, 905, 87, 830, 1228, 25, 1594, 153, 4, 585, 46,
		862, 265, 833, 301, 473, 458, 85, 254, 22, 266, 543, 32, 939, 1113, 228, 544, 205, 1617, 1109, 445, 86, 5, 278, 16,
		784, 303, 1022, 1014, 162, 714, 447, 656, 834, 138, 448, 30, 85, 371, 951, 1256, 842, 5, 460, 919, 1019, 785, 1275,
		616, 1593, 168, 1727, 311, 950, 1299, 1131, 796, 522, 443, 703, 836, 47, 300, 449, 11, 360, 682, 487, 108, 1396,
		623, 1108, 239, 379, 0, 822, 109, 60, 98, 667, 242, 1398, 650, 25, 376, 168, 46, 259, 138, 254, 1631, 953, 776, 166,
		0, 628, 75, 413, 1401, 69, 462, 883, 877, 96, 314, 825, 346, 932, 352, 1086, 143, 507, 134, 557, 31, 1663, 565, 275,
		207, 330, 702, 53, 1085, 259, 14, 26, 851, 1571, 1829, 1513, 356, 70, 1393, 426, 345, 412, 129, 908, 959, 896, 1578,
		617, 428, 222, 1256, 3, 863, 237, 5, 357, 92, 292, 514, 4, 919, 5, 848, 1605, 149, 959, 376, 1709, 410, 460, 646,
		389, 13, 1388, 294, 1151, 652, 10, 113, 769, 1519, 57, 685, 1132, 1, 417, 1369, 1396, 248, 496, 145, 64, 798, 719,
		716, 845, 168, 1, 147, 347, 239, 512, 19, 478, 336, 20, 327, 487, 141, 37, 96, 331, 826, 1347, 479, 182, 601, 233,
		564, 196, 0, 811, 19, 318, 86, 1442, 468, 396, 298, 46, 661, 339, 914, 54, 560, 91, 284, 829, 1710, 478, 318, 780,
		738, 807, 1017, 166, 48, 358, 193, 466, 831, 138, 226, 5, 24, 251, 119, 644, 545, 588, 170, 890, 248, 596, 310, 612,
		479, 366, 1374, 465, 32, 467, 79, 603, 220, 1138, 168, 968, 420, 129, 90, 214, 652, 408, 169, 0, 173, 19, 312, 65,
		38, 115, 325, 158, 1458, 744, 1529, 361, 360, 77, 75, 130, 111, 175, 34, 676, 169, 384, 473, 296, 701, 84, 11, 1862,
		223, 193, 118, 678, 403, 1097, 2, 1318, 190, 590, 96, 47, 69, 212, 520, 786, 1569, 703, 1776, 140, 12, 741, 906, 29,
		115, 30, 196, 821, 23, 51, 540, 225, 891, 133, 907, 567, 143, 44, 371, 1038, 237, 0, 222, 1327, 760, 854, 1, 29, 14,
		65, 98, 25, 233, 423, 63, 382, 648, 257, 160, 71, 1287, 315, 627, 40, 159, 202, 112, 657, 87, 94, 93, 362, 23, 501,
		870, 1114, 946, 1007, 453, 159, 493, 590, 665, 28, 435, 7, 1238, 1846, 758, 174, 258, 972, 557, 1431, 482, 429, 57,
		389, 651, 1089, 1490, 821, 844, 458, 712, 259, 433, 418, 344, 466, 60, 123, 1604, 897, 1346, 198, 143, 259, 49, 770,
		1703, 900, 1364, 450, 498, 30, 543, 322, 3, 533, 508, 444, 148, 927, 72, 321, 733, 689, 24, 44, 685, 1021, 324, 182,
		1737, 975, 387, 143, 176, 478, 602, 752, 203, 130, 169, 165, 41, 119, 35, 175, 763, 1147, 5, 137, 10, 357, 54, 1209,
		182, 298, 156, 1488, 176, 86, 548, 2, 37, 36, 76, 100, 1369, 1174, 322, 32, 573, 107, 375, 1210, 51, 597, 902, 878,
		919, 379, 125, 26, 1240, 7, 7, 131, 913, 994, 1097, 576, 112, 694, 805, 551, 512, 663, 361, 747, 161, 691, 63, 119,
		47, 89, 6, 258, 57, 537, 654, 757, 1202, 922, 475, 347, 193, 79, 1177, 443, 33, 1257, 1070, 118, 810, 117, 37, 226,
		230, 552, 618, 341, 530, 681, 1015, 358, 846, 276, 1149, 210, 525, 1144, 272, 30, 551, 55, 512, 229, 90, 1144, 389,
		500, 372, 92, 58, 598, 1362, 475, 70, 748, 1217, 442, 28, 334, 369, 768, 169, 405, 1058, 759, 1087, 268, 714, 81,
		594, 1423, 1004, 694, 61, 1032, 895, 1321, 95, 1512, 646, 818, 845, 1275, 294, 883, 1684, 1062, 2, 851, 304, 306,
		128, 1523, 1594, 190, 73, 809, 175, 321, 407, 424, 109, 48, 234, 437, 968, 284, 1069, 181, 340, 149, 9, 163, 863,
		17, 584, 421, 79, 164, 913, 81
	};
		var testInput = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

		var input = challenge;
		var output = new Dictionary<int, double>();
		var min = input.Min();
		var max = input.Max();

		for (var hp1 = min; hp1 <= max; hp1++)
		{
			Console.WriteLine($"Calculating Fuel Costs for [{hp1}]: ");

			var fuel =
				input.Select(hp2 => Math.Abs(hp1 - hp2))
					.Select(diff => diff * (diff + 1) / 2)
					.Aggregate(0.0, (current, currentFuel) => current + currentFuel);

			output[hp1] = fuel;

			Console.WriteLine($"Total Fuel Cost: {fuel}");
			Console.WriteLine("");
		}

		Console.WriteLine("Fuel Costs: ");
		Console.WriteLine(output.Print());

		Console.WriteLine("Lowest Fuel: ");
		Console.WriteLine(output.Min(k => k.Value));
	}

	void PreviousPuzzles()
	{
		Console.WriteLine("--- Day 1 ---");
		Console.WriteLine("Sweeping Sonar");
		//Puzzles.SweepingSonarPartOne();
		//Puzzles.SweepingSonarPartTwo();
		Console.WriteLine("Done!");
		Console.WriteLine();

		Console.WriteLine("--- Day 2 ---");
		Console.WriteLine("Dive");
		//Puzzles.DivingPartOne();
		//Puzzles.DivingPartTwo();
		Console.WriteLine("Done!");
		Console.WriteLine();

		Console.WriteLine("--- Day 3 ---");
		Console.WriteLine("Binary Diagnostic");
		//Puzzles.BinaryDiagnosingPartOne();
		//Puzzles.BinaryDiagnosingPartTwo();
		Console.WriteLine("Done!");
		Console.WriteLine();
	}

	void CalculateLanternfishGrowth()
	{
		const int days = 256;

		var testInput = new[] { 3, 4, 3, 1, 2 };
		var challengeInput = new[]
		{
			3, 4, 1, 1, 5, 1, 3, 1, 1, 3, 5, 1, 1, 5, 3, 2, 4, 2, 2, 2, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 3, 1, 1, 5, 4, 1, 1, 1, 4,
			1, 1, 1, 1, 2, 3, 2, 5, 1, 5, 1, 2, 1, 1, 1, 4, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 2, 3, 4, 2, 1, 3, 1, 1, 2,
			1, 1, 2, 1, 5, 2, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 5, 1, 4, 1, 1, 1, 3, 3, 1, 3, 1, 3, 1, 4, 1, 1, 1, 1, 1, 4, 5, 1,
			1, 3, 2, 2, 5, 5, 4, 3, 1, 2, 1, 1, 1, 4, 1, 3, 4, 1, 1, 1, 1, 2, 1, 1, 3, 2, 1, 1, 1, 1, 1, 4, 1, 1, 1, 4, 4, 5, 2,
			1, 1, 1, 1, 1, 2, 4, 2, 1, 1, 1, 2, 1, 1, 2, 1, 5, 1, 5, 2, 5, 5, 1, 1, 3, 1, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 4, 1,
			1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 5, 1, 1, 3, 5, 1, 1, 5, 5, 3, 5, 3, 4, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 5, 1, 3,
			1, 5, 1, 1, 4, 1, 3, 1, 1, 1, 2, 1, 1, 1, 2, 1, 5, 1, 1, 1, 1, 4, 1, 3, 2, 3, 4, 1, 3, 5, 3, 4, 1, 4, 4, 4, 1, 3, 2,
			4, 1, 4, 1, 1, 2, 1, 3, 1, 5, 5, 1, 5, 1, 1, 1, 5, 2, 1, 2, 3, 1, 4, 3, 3, 4, 3
		};

		var initialState = challengeInput;
		//Console.WriteLine($"Initial State: {testInput.Print()}");

		var lanternfish = new long[9];

		foreach (var fish in initialState)
		{
			lanternfish[fish]++;
		}

		for (var day = 1; day <= days; day++)
		{
			var newLanternfishes = lanternfish[0];

			lanternfish[0] = lanternfish[1];
			lanternfish[1] = lanternfish[2];
			lanternfish[2] = lanternfish[3];
			lanternfish[3] = lanternfish[4];
			lanternfish[4] = lanternfish[5];
			lanternfish[5] = lanternfish[6];
			lanternfish[6] = lanternfish[7] + newLanternfishes;
			lanternfish[7] = lanternfish[8];
			lanternfish[8] = newLanternfishes;

			//Console.WriteLine($"Day {day}: {lanternfish.Print()}");
		}

		Console.WriteLine($"Lanterfishes: {lanternfish.Sum()}");

	}

	void SmokeBasin()
	{
		const bool test = false;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input.txt");
		var testInput = File.ReadAllLines("test-input.txt");

		var input = test ? testInput : challenge;

		var width = input[0].Length;
		var height = input.Length;

		var heightmap = new int[width, height];
		var lowPoints = new int[width, height];
		var riskLevels = new int[width, height];
		var basins = new int[width, height];
		var pointsVisited = new bool[width, height];

		var basinSizes = new List<int>();

		var largestBasinsProduct = 0;

		for (var y = 0; y < input.Length; y++)
		{
			var line = input[y];

			for (var x = 0; x < line.Length; x++)
			{
				heightmap[x, y] = line[x] - '0';
			}
		}

		for (var y = 0; y < heightmap.GetLength(1); y++)
		{
			for (var x = 0; x < heightmap.GetLength(0); x++)
			{
				var point = heightmap[x, y];

				if (point == 9)
				{
					continue;
				}

				if (IsLowPoint(heightmap, x, y))
				{
					lowPoints[x, y] = point;
					riskLevels[x, y] = point + 1;
					basinSizes.Add(GetBasinSize(heightmap, x, y));
				}

				basins[x, y] = point;
			}
		}

		bool IsLowPoint(int[,] map, int x, int y)
		{
			var isLowPoint = true;
			var pointValue = map[x, y];

			// Top Edge
			if (y > 0)
			{
				isLowPoint &= pointValue < map[x, y - 1];
			}

			// Bottom Edge
			if (y < map.GetLength(1) - 1)
			{
				isLowPoint &= pointValue < map[x, y + 1];
			}

			// Left Edge
			if (x > 0)
			{
				isLowPoint &= pointValue < map[x - 1, y];
			}

			// Right Edge
			if (x < map.GetLength(0) - 1)
			{
				isLowPoint &= pointValue < map[x + 1, y];
			}

			return isLowPoint;
		}

		int GetBasinSize(int[,] map, int x, int y)
		{
			var size = 0;

			if (x < 0 || y < 0 ||
				x > map.GetLength(0) - 1 ||
				y > map.GetLength(1) - 1)
			{
				return size;
			}

			var pointVisited = pointsVisited[x, y];

			if (pointVisited)
			{
				return size;
			}

			pointsVisited[x, y] = true;

			var pointValue = map[x, y];

			if (pointValue == 9)
			{
				return size;
			}

			// Top Edge
			if (y > 0)
			{
				size += GetBasinSize(map, x, y - 1);
			}

			// Bottom Edge
			if (y < map.GetLength(1) - 1)
			{
				size += GetBasinSize(map, x, y + 1);
			}

			// Left Edge
			if (x > 0)
			{
				size += GetBasinSize(map, x - 1, y);
			}

			// Right Edge
			if (x < map.GetLength(0) - 1)
			{
				size += GetBasinSize(map, x + 1, y);
			}

			return size + 1;
		}

		//Console.WriteLine("Heightmap: ");
		//Console.WriteLine(heightmap.Print());

		//Console.WriteLine("Low Points: ");
		//Console.WriteLine(lowPoints.Print());

		//Console.WriteLine("Risk Levels: ");
		//Console.WriteLine(riskLevels.Print(false));

		//Console.WriteLine("Basins: ");
		//Console.WriteLine(basins.Print(false));

		//Console.WriteLine("Simple Basin Size: " + GetBasinSize(heightmap, 6, 4));

		Console.WriteLine("Basin Sizes: " + basinSizes.Print());

		largestBasinsProduct = basinSizes.OrderByDescending(x => x).Take(3).Aggregate((total, next) => total * next);

		//Console.WriteLine("Risk Level Sum: " + riskLevels.Sum());
		Console.WriteLine("Largest Basins Product: " + largestBasinsProduct);

	}

	void Day10()
	{
		const bool test = true;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input-2.txt");
		var testInput = File.ReadAllLines("test-input-2.txt");

		var input = test ? testInput : challenge;
		var characters = ")]}>";
		var characterScore = new[] { 3, 57, 1197, 25137 };

		var result = 0;

		foreach (var line in input)
		{
			var remainingLine = line;

			foreach (var chunkPart in line)
			{
				switch (chunkPart)
				{
					case '(':
						var closeChunkIndex = line.LastIndexOf(')');
						if (closeChunkIndex >= 0)
						{
							//line = line.Remove(closeChunkIndex, 1);
						}


						break;
					case '[':
						break;
					case '{':
						break;
					case '<':
						break;
				}
			}
		}

		Console.WriteLine("Result: " + result);
	}

	#endregion

	#region 2020

	void PasswordPhilosophy()
	{
		const string message = "Valid Passwords";

		const bool test = false;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input.txt");
		var testInput = File.ReadAllLines("test-input.txt");

		var input = test ? testInput : challenge;

		var result = 0;

		foreach (var line in input)
		{
			var passwordPolicies = line.Split(' ');
			var range = passwordPolicies[0].Split('-').Select(int.Parse).ToArray();
			var character = passwordPolicies[1].Trim(':')[0];
			var password = passwordPolicies[2];

			var firstPosition = range[0];
			var secondPosition = range[1];

			//var characterCount = password.Count(c => c == character);

			//Console.WriteLine($"R={range[0]}-{range[1]},Char={character},P={password},Count={characterCount}");

			//if (characterCount >= range[0] && characterCount <= range[1])
			//{
			//	result++;
			//}

			var firstPositionIsValid = password[firstPosition - 1] == character;
			var secondPositionIsValid = password[secondPosition - 1] == character;

			if (firstPositionIsValid ^ secondPositionIsValid)
			{
				result++;
			}

		}

		Console.WriteLine($"{message}: {result}");
	}

	void ReportRepair()
	{
		const string message = "Report Repair";

		const bool test = false;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input.txt");
		var testInput = File.ReadAllLines("test-input.txt");

		var input = test ? testInput : challenge;

		var result = 0;
		var numbers = input.Select(int.Parse).ToList();

		foreach (var first in numbers)
		{
			foreach (var second in numbers)
			{
				foreach (var third in numbers.Where(third => first + second + third == 2020))
				{
					result = first * second * third;
				}
			}
		}

		Console.WriteLine($"{message}: {result}");
	}

	void PassportProcessing()
	{
		const string message = "Passport Processing";

		const bool test = false;

		// ReSharper disable once RedundantAssignment
		var challenge = File.ReadAllLines("input.txt");
		var testInput = File.ReadAllLines("test-input.txt");

		var input = test ? testInput : challenge;

		var passports = new List<Passport>();
		var passport = new Passport
		{
			ShowInvalidData = test
		};

		foreach (var line in input)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				passports.Add(passport);
				passport = new Passport
				{
					ShowInvalidData = test
				};
				continue;
			}

			var fields = line.Split(' ');

			foreach (var field in fields)
			{
				var keyValuePair = field.Split(':');
				var value = keyValuePair[1];

				switch (keyValuePair[0])
				{
					case "byr":
						passport.BirthYear = int.Parse(value);
						break;
					case "iyr":
						passport.IssueYear = int.Parse(value);
						break;
					case "eyr":
						passport.ExpirationYear = int.Parse(value);
						break;
					case "hgt":
						passport.Height = value;

						if (passport.Height.Length >= 4)
						{
							var heightUnit = value[^2..];
							var heightValue = int.Parse(value.Remove(value.Length - 2));

							passport.HeightUnit = heightUnit;
							passport.HeightValue = heightValue;
						}

						break;
					case "hcl":
						passport.HairColor = value;
						break;
					case "ecl":
						passport.EyeColor = value;
						break;
					case "pid":
						passport.PassportId = value;
						break;
					case "cid":
						passport.CountryId = value;
						break;
				}
			}
		}

		passports.Add(passport);

		Console.WriteLine($"Part 1 - {message} Total: {passports.Count}");
		Console.WriteLine($"Part 1 - {message} Required: {passports.Count(p => p.HasRequiredData())}");
		Console.WriteLine($"Part 2 - {message} Valid: {passports.Count(p => p.IsValid())}");
	}

	internal struct Passport
	{
		private static readonly Regex HairColorRegex = new("^#[0-9a-f]{6}$", RegexOptions.Compiled);
		private static readonly Regex PassportIdRegex = new("^\\d{9}$", RegexOptions.Compiled);

		private static readonly HashSet<string> EyeColorSet = new()
		{
			"amb",
			"blu",
			"brn",
			"gry",
			"grn",
			"hzl",
			"oth"
		};

		private bool? _hasRequiredData;
		private bool? _isValid;

		public bool ShowInvalidData { get; set; }

		public int BirthYear { get; set; } = -1;

		public int IssueYear { get; set; } = -1;

		public int ExpirationYear { get; set; } = -1;

		public string Height { get; set; } = string.Empty;

		public int HeightValue { get; set; } = -1;

		public string HeightUnit { get; set; } = string.Empty;

		public string HairColor { get; set; } = string.Empty;

		public string EyeColor { get; set; } = string.Empty;

		public string PassportId { get; set; } = string.Empty;

		public string CountryId { get; set; } = string.Empty;

		public bool HasRequiredData()
		{
			_hasRequiredData ??= BirthYear >= 0 &&
								 IssueYear >= 0 &&
								 ExpirationYear >= 0 &&
								 !string.IsNullOrWhiteSpace(Height) &&
								 !string.IsNullOrWhiteSpace(HairColor) &&
								 !string.IsNullOrWhiteSpace(EyeColor) &&
								 !string.IsNullOrWhiteSpace(PassportId);

			return _hasRequiredData.Value;
		}

		public bool IsValid()
		{
			_isValid ??=
				BirthYear is >= 1920 and <= 2002 &&
				IssueYear is >= 2010 and <= 2020 &&
				ExpirationYear is >= 2020 and <= 2030 &&
				(HeightUnit == "cm" && HeightValue is >= 150 and <= 193 ||
					HeightUnit == "in" && HeightValue is >= 59 and <= 76) &&
				HairColorRegex.IsMatch(HairColor) &&
				EyeColorSet.Contains(EyeColor) &&
				PassportIdRegex.IsMatch(PassportId);

			ReportInvalidData();

			return _isValid.Value;
		}

		private void ReportInvalidData()
		{
			if (!ShowInvalidData || !_isValid.HasValue || _isValid.Value)
			{
				return;
			}

			Console.WriteLine("Failures: ");

			PrintInvalidData("Birth Year", BirthYear is >= 1920 and <= 2002);
			PrintInvalidData("Issue Year", IssueYear is >= 2010 and <= 2020);
			PrintInvalidData("Expiration Year", ExpirationYear is > 2020 and < 2030);
			PrintInvalidData("Height",
				HeightUnit == "cm" && HeightValue is >= 150 and <= 193 ||
				HeightUnit == "in" && HeightValue is >= 59 and <= 76);
			PrintInvalidData("Hair Color", HairColorRegex.IsMatch(HairColor));
			PrintInvalidData("Eye Color", EyeColorSet.Contains(EyeColor));
			PrintInvalidData("Passport ID", PassportIdRegex.IsMatch(PassportId));
		}

		private static void PrintInvalidData(string dataField, bool condition)
		{
			if (!condition)
			{
				Console.WriteLine($"{dataField}: {condition}");
			}
		}
	}

	#endregion
}