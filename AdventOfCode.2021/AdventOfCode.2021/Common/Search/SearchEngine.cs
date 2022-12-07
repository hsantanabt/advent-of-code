namespace AdventOfCode._2021.Common.Search
{
	internal class SearchEngine
	{
		public Map Map { get; set; }

		public Node Start { get; set; }

		public Node End { get; set; }

		public int NodeVisits { get; private set; }

		public double ShortestPathLength { get; set; }

		public double ShortestPathCost { get; private set; }

		public SearchEngine(Map map)
		{
			Map = map;
			End = map.EndNode;
			Start = map.StartNode;
		}

		private void BuildShortestPath(ICollection<Node> list, Node node)
		{
			if (node.NearestToStart == null)
			{
				return;
			}
				
			list.Add(node.NearestToStart);

			ShortestPathLength += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Length;
			ShortestPathCost += node.Connections.Single(x => x.ConnectedNode == node.NearestToStart).Cost;

			BuildShortestPath(list, node.NearestToStart);
		}

		public List<Node> GetShortestPathAStar()
		{
			foreach (var node in Map.Nodes)
			{
				node.StraightLineDistanceToEnd = node.StraightLineDistanceTo(End);
			}

			AStarSearch();

			var shortestPath = new List<Node> {End};
			BuildShortestPath(shortestPath, End);
			shortestPath.Reverse();

			return shortestPath;
		}

		public List<Node> GetShortestPathDijikstra()
		{
			DijkstraSearch();
			var shortestPath = new List<Node> {End};
			BuildShortestPath(shortestPath, End);
			shortestPath.Reverse();
			return shortestPath;
		}

		private void AStarSearch()
		{
			Start.MinCostToStart = 0;
			var priorityQueue = new List<Node> { Start };

			do
			{
				priorityQueue = priorityQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();

				var node = priorityQueue.First();
				priorityQueue.Remove(node);

				NodeVisits++;

				foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
				{
					var childNode = cnn.ConnectedNode;
					if (childNode.Visited)
					{
						continue;
					}

					if (childNode.MinCostToStart != null &&
					    !(node.MinCostToStart + cnn.Cost < childNode.MinCostToStart))
					{
						continue;
					}

					childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
					childNode.NearestToStart = node;

					if (!priorityQueue.Contains(childNode))
					{
						priorityQueue.Add(childNode);
					}
				}

				node.Visited = true;

				if (node == End)
				{
					return;
				}
					
			} while (priorityQueue.Any());
		}

		private void DijkstraSearch()
		{
			NodeVisits = 0;
			Start.MinCostToStart = 0;

			var priorityQueue = new List<Node> { Start };

			do
			{
				priorityQueue = priorityQueue.OrderBy(x => x.MinCostToStart).ToList();

				var node = priorityQueue.First();
				priorityQueue.Remove(node);

				NodeVisits++;

				foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
				{
					var childNode = cnn.ConnectedNode;
					if (childNode.Visited)
					{
						continue;
					}
						
					if (childNode.MinCostToStart != null &&
					    !(node.MinCostToStart + cnn.Cost < childNode.MinCostToStart))
					{
						continue;
					}

					childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
					childNode.NearestToStart = node;

					if (!priorityQueue.Contains(childNode))
					{
						priorityQueue.Add(childNode);
					}
				}

				node.Visited = true;

				if (node == End)
				{
					return;
				}
					
			} while (priorityQueue.Any());
		}
	}
}
