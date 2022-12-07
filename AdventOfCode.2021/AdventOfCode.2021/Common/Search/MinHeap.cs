// C# program to demonstrate common
// Binary Heap Operations - Min Heap

// A class for Min Heap

namespace AdventOfCode._2021.Common.Search;

internal class MinHeap
{
	// To store array of elements in heap
	public int[] HeapArray { get; }

	// max size of the heap
	public int Capacity { get; }

	// Current number of elements in the heap
	public int CurrentHeapSize { get; private set; }

	// Constructor
	public MinHeap(int n)
	{
		Capacity = n;
		HeapArray = new int[Capacity];
		CurrentHeapSize = 0;
	}

	// Swapping using reference
	public static void Swap<T>(ref T lhs, ref T rhs)
	{
		(lhs, rhs) = (rhs, lhs);
	}

	// Get the Parent index for the given index
	public int Parent(int key)
	{
		return (key - 1) / 2;
	}

	// Get the Left Child index for the given index
	public int Left(int key)
	{
		return 2 * key + 1;
	}

	// Get the Right Child index for the given index
	public int Right(int key)
	{
		return 2 * key + 2;
	}

	// Inserts a new key
	public bool InsertKey(int key)
	{
		if (CurrentHeapSize == Capacity)
		{
			// heap is full
			return false;
		}

		// First insert the new key at the end
		var i = CurrentHeapSize;
		HeapArray[i] = key;
		CurrentHeapSize++;

		// Fix the min heap property if it is violated
		while (i != 0 && HeapArray[i] <
		       HeapArray[Parent(i)])
		{
			Swap(ref HeapArray[i],
				ref HeapArray[Parent(i)]);
			i = Parent(i);
		}
		return true;
	}

	// Decreases value of given key to new_val.
	// It is assumed that new_val is smaller
	// than heapArray[key].
	public void DecreaseKey(int key, int newVal)
	{
		HeapArray[key] = newVal;

		while (key != 0 && HeapArray[key] <
		       HeapArray[Parent(key)])
		{
			Swap(ref HeapArray[key],
				ref HeapArray[Parent(key)]);
			key = Parent(key);
		}
	}

	// Returns the minimum key (key at
	// root) from min heap
	public int GetMin()
	{
		return HeapArray[0];
	}

	// Method to remove minimum element
	// (or root) from min heap
	public int ExtractMin()
	{
		switch (CurrentHeapSize)
		{
			case <= 0:
				return int.MaxValue;
			case 1:
				CurrentHeapSize--;
				return HeapArray[0];
		}

		// Store the minimum value,
		// and remove it from heap
		var root = HeapArray[0];

		HeapArray[0] = HeapArray[CurrentHeapSize - 1];
		CurrentHeapSize--;
		MinHeapify(0);

		return root;
	}

	// This function deletes key at the
	// given index. It first reduced value
	// to minus infinite, then calls extractMin()
	public void DeleteKey(int key)
	{
		DecreaseKey(key, int.MinValue);
		ExtractMin();
	}

	// A recursive method to heapify a subtree
	// with the root at given index
	// This method assumes that the subtrees
	// are already heapified
	public void MinHeapify(int key)
	{
		var l = Left(key);
		var r = Right(key);

		var smallest = key;
		if (l < CurrentHeapSize &&
		    HeapArray[l] < HeapArray[smallest])
		{
			smallest = l;
		}
		if (r < CurrentHeapSize &&
		    HeapArray[r] < HeapArray[smallest])
		{
			smallest = r;
		}

		if (smallest == key)
		{
			return;
		}

		Swap(ref HeapArray[key],
			ref HeapArray[smallest]);

		// ReSharper disable once TailRecursiveCall
		MinHeapify(smallest);
	}

	// Increases value of given key to new_val.
	// It is assumed that new_val is greater
	// than heapArray[key].
	// Heapify from the given key
	public void IncreaseKey(int key, int newVal)
	{
		HeapArray[key] = newVal;
		MinHeapify(key);
	}

	// Changes value on a key
	public void ChangeValueOnAKey(int key, int newVal)
	{
		if (HeapArray[key] == newVal)
		{
			return;
		}
		if (HeapArray[key] < newVal)
		{
			IncreaseKey(key, newVal);
		}
		else
		{
			DecreaseKey(key, newVal);
		}
	}
}