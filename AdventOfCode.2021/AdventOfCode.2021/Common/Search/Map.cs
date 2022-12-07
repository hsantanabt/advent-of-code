namespace AdventOfCode._2021.Common.Search
{
	internal class Map
	{
		public List<Node> Nodes { get; set; } = new List<Node>();

		public Node StartNode { get; set; } = new();

		public Node EndNode { get; set; } = new();

		public List<Node> ShortestPath { get; set; } = new List<Node>();
	}
}
