namespace AdventOfCode._2021.Common;

internal static class Extensions
{
	internal static string Print(this IEnumerable<int> array)
	{
		return "[" + string.Join(",", array) + "]";
	}

	internal static string Print(this IEnumerable<long> array)
	{
		return "[" + string.Join(",", array) + "]";
	}

	internal static string Print(this IEnumerable<string> array)
	{
		return "[" + string.Join(",", array) + "]";
	}

	internal static string Print(this Dictionary<int, int> dictionary)
	{
		var mapString = string.Empty;

		foreach (var (key, value) in dictionary)
		{
			mapString += $"[{key}={value}],";
		}

		return "{ " + mapString + " }";
	}

	internal static string Print(this Dictionary<int, double> dictionary)
	{
		var mapString = string.Empty;

		foreach (var (key, value) in dictionary)
		{
			mapString += $"[{key}={value}],";
		}

		return "{ " + mapString + " }";
	}

	internal static string Print(this int[,] matrix, bool printZero = true)
	{
		var matrixString = string.Empty;

		for (var y = 0; y < matrix.GetLength(1); y++)
		{
			for (var x = 0; x < matrix.GetLength(0); x++)
			{
				var value = matrix[x, y];
				matrixString += value > 0 || printZero ? value : "-";
			}

			matrixString += Environment.NewLine;
		}

		return matrixString;
	}

	internal static string Print(this char[,] matrix)
	{
		var matrixString = string.Empty;

		for (var y = 0; y < matrix.GetLength(1); y++)
		{
			for (var x = 0; x < matrix.GetLength(0); x++)
			{
				var value = matrix[x, y];
				matrixString += value != default(char) ? value : ".";
			}

			matrixString += Environment.NewLine;
		}

		return matrixString;
	}

	internal static int Sum(this int[,] matrix)
	{
		var sum = 0;

		for (var y = 0; y < matrix.GetLength(1); y++)
		{
			for (var x = 0; x < matrix.GetLength(0); x++)
			{
				sum += matrix[x, y];
			}
		}

		return sum;
	}
}
