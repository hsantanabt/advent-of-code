namespace AdventOfCode._2021.Puzzles.Dive;

internal struct SubPosition
{
	public int Horizontal;
	public int Depth;
	public int Aim;

	public string Print()
	{
		return $"SubPos: H={Horizontal} D={Depth} A={Aim}";
	}
}
