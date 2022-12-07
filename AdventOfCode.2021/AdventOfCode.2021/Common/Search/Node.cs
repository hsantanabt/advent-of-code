namespace AdventOfCode._2021.Common.Search
{
    public class Node
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Value { get; set; }

        public Point Point { get; set; } = new();

        public List<Edge> Connections { get; set; } = new();

        public double? MinCostToStart { get; set; }

        public Node? NearestToStart { get; set; }

        public bool Visited { get; set; }

        public double StraightLineDistanceToEnd { get; set; }

        internal static Node GetRandom(Random rnd, string name)
        {
            return new Node
            {
                Point = new Point
                {
                    X = rnd.Next(),
                    Y = rnd.Next()
                },
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        internal void ConnectClosestNodes(List<Node> nodes, int branching, Random rnd, bool randomWeight)
        {
            var connections = new List<Edge>();

            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var node in nodes)
            {
	            if (node.Id == Id)
	            {
		            continue;
	            }

	            var dist = Math.Sqrt(Math.Pow(Point.X - node.Point.X, 2) + Math.Pow(Point.Y - node.Point.Y, 2));
	            connections.Add(new Edge
	            {
		            ConnectedNode = node,
		            Length = dist,
		            Cost = randomWeight ? rnd.NextDouble() : dist,
	            });
            }

            connections = connections.OrderBy(x => x.Length).ToList();
            var count = 0;
            foreach (var cnn in connections)
            {
                //Connect three closes nodes that are not connected.
                if (Connections.All(c => c.ConnectedNode != cnn.ConnectedNode))
                    Connections.Add(cnn);
                count++;

                //Make it a two way connection if not already connected
                if (cnn.ConnectedNode.Connections.All(cc => cc.ConnectedNode != this))
                {
                    var backConnection = new Edge { ConnectedNode = this, Length = cnn.Length };
                    cnn.ConnectedNode.Connections.Add(backConnection);
                }
                if (count == branching)
                    return;
            }
        }

        internal void ConnectClosestNodes(Node[,] nodes)
        {
	        var neighbors = GetNeighbors(nodes);
	        var connections = 
		        (from node in neighbors where node.Id != Id 
			        select new Edge
			        {
				        ConnectedNode = node, 
				        Length = node.Value,
				        Cost = node.Value
					}).OrderBy(x => x.Cost);

	        foreach (var cnn in connections)
	        {
		        //Connect three closes nodes that are not connected.
		        if (Connections.All(c => c.ConnectedNode != cnn.ConnectedNode))
		        {
			        Connections.Add(cnn);
                }

		        //Make it a two way connection if not already connected
		        if (cnn.ConnectedNode.Connections.Any(cc => cc.ConnectedNode == this))
		        {
			        continue;
		        }

		        var backConnection = new Edge
		        {
			        ConnectedNode = this, 
			        Length = cnn.Length
		        };

		        cnn.ConnectedNode.Connections.Add(backConnection);
	        }
        }

        private IEnumerable<Node> GetNeighbors(Node[,] nodes)
        {
            var neighbors = new List<Node>();

            // Top Edge
			//   if (Point.Y > 0)
	        //{
	        // neighbors.Add(nodes[Point.X, Point.Y - 1]);
	        //}

	        // Bottom Edge
	        if (Point.Y < nodes.GetLength(1) - 1)
	        {
		        neighbors.Add(nodes[Point.X, Point.Y + 1]);
            }

	        // Right Edge
	        if (Point.X < nodes.GetLength(0) - 1)
	        {
		        neighbors.Add(nodes[Point.X + 1, Point.Y]);
            }

			//// Left Edge
			//if (Point.X > 0)
			//{
			//	neighbors.Add(nodes[Point.X - 1, Point.Y]);
			//}

			return neighbors;
        }

        public double StraightLineDistanceTo(Node end)
        {
            return Math.Sqrt(Math.Pow(Point.X - end.Point.X, 2) + Math.Pow(Point.Y - end.Point.Y, 2));
        }

        internal bool ToCloseToAny(List<Node> nodes)
        {
	        return nodes
		        .Select(node => Math.Sqrt(Math.Pow(Point.X - node.Point.X, 2) + Math.Pow(Point.Y - node.Point.Y, 2)))
		        .Any(d => d < 0.01);
        }

        public override string? ToString()
        {
            return Name;
        }
    }
}
