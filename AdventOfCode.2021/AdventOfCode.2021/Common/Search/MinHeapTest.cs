namespace AdventOfCode._2021.Common.Search;

// This code is contributed by
// Dinesh Clinton Albert(dineshclinton)
internal static class MinHeapTest
{
	// Driver code
	public static void Run(string[] args)
	{
		var h = new MinHeap(11);
		h.InsertKey(3);
		h.InsertKey(2);
		h.DeleteKey(1);
		h.InsertKey(15);
		h.InsertKey(5);
		h.InsertKey(4);
		h.InsertKey(45);

		Console.Write(h.ExtractMin() + " ");
		Console.Write(h.GetMin() + " ");

		h.DecreaseKey(2, 1);
		Console.Write(h.GetMin());
	}
}