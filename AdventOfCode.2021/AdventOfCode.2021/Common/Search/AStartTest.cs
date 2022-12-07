using System.Diagnostics;

namespace AdventOfCode._2021.Common.Search
{
	internal static class AStartTest
	{
		public static Map Randomize(int nodeCount, int branching, int seed, bool randomWeights)
		{
			var rnd = new Random(seed);
			var map = new Map();

			for (var i = 0; i < nodeCount; i++)
			{
				var newNode = Node.GetRandom(rnd, i.ToString());

				if (!newNode.ToCloseToAny(map.Nodes))
				{
					map.Nodes.Add(newNode);
				}
			}

			foreach (var node in map.Nodes)
			{
				node.ConnectClosestNodes(map.Nodes, branching, rnd, randomWeights);
			}

			//map.StartNode = map.Nodes.OrderBy(n => n.Point.X + n.Point.Y).First();
			//map.EndNode = map.Nodes.OrderBy(n => n.Point.X + n.Point.Y).Last();

			map.EndNode = map.Nodes[rnd.Next(map.Nodes.Count - 1)];
			map.StartNode = map.Nodes[rnd.Next(map.Nodes.Count - 1)];

			foreach (var node in map.Nodes)
			{
				Debug.WriteLine($"{node}");

				foreach (var cnn in node.Connections)
				{
					Debug.WriteLine($"{cnn}");
				}
			}

			return map;
		}
    }
}
