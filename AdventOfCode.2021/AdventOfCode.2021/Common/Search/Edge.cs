namespace AdventOfCode._2021.Common.Search
{
	public class Edge
	{
		public double Length { get; set; }
		public double Cost { get; set; }

		public Node ConnectedNode { get; set; } = new();

		public override string ToString()
		{
			return "-> " + ConnectedNode;
		}
	}
}
