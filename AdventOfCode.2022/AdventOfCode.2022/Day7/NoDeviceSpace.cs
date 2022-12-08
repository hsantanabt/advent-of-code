#pragma warning disable CS8602

namespace AOC2022.Day7;

public static class NoDeviceSpace
{
	private const string Day = "Day7";

	private enum NodeType
	{
		// ReSharper disable once InconsistentNaming
		dir,
		// ReSharper disable once InconsistentNaming
		file
	}

	private class Node
	{
		public NodeType Type { get; init; }

		public string Name { get; init; } = string.Empty;

		public int Size { get; set; }

		public Node? Parent { get; init; }

		public Dictionary<string, Node> Children { get; } = new ();
	}

	private const int MaxDirectorySize = 100000;
	private const int UnusedSizeNeeded = 30000000;
	private const int MaxFileSystemSize = 70000000;

	private static int _unusedSpace = -1;
	private static int _spaceNeeded = -1;

	private static readonly List<Node> DeleteCandidates = new();
	private static readonly List<Node> MaxSizeDirectories = new();

	public static void Part1And2()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");

		var root = new Node
		{
			Type = NodeType.dir,
			Name = "/",
			Size = 0,
			Parent = null
		};
		var currentDirectory = root;

		foreach (var line in input)
		{
			var lineData = line.Split(' ');

			switch (lineData[0])
			{
				case "$":

					switch (lineData[1])
					{
						case "cd":

							var directoryName = lineData[2];

							currentDirectory = directoryName switch
							{
								"/" => root,
								".." => currentDirectory?.Parent,
								_ => currentDirectory?.Children[directoryName]
							};
							break;
						case "ls":
							continue;
					}

					break;
				case "dir":
					var directoryNode = new Node
					{
						Type = NodeType.dir,
						Name = lineData[1],
						Size = 0,
						Parent = currentDirectory
					};
					currentDirectory?.Children.Add(directoryNode.Name, directoryNode);
					break;
				default:
					var fileNode = new Node
					{
						Type = NodeType.file,
						Name = lineData[1],
						Size = int.Parse(lineData[0]),
						Parent = currentDirectory
					};
					currentDirectory?.Children.Add(fileNode.Name, fileNode);
					break;
			}
		}

		UpdateNodeSize(root, false);
		//PrintTree(root);

		var totalSizeMaxDirectories = MaxSizeDirectories.Sum(directoryNode => directoryNode.Size);

		Console.WriteLine($"Max Size Directories - Total Size: {totalSizeMaxDirectories}");

		_unusedSpace = MaxFileSystemSize - root.Size;
		_spaceNeeded = UnusedSizeNeeded - _unusedSpace;

		UpdateNodeSize(root, true);

		Console.WriteLine($"Free Space: {_unusedSpace}");
		Console.WriteLine($"Space Needed: {_spaceNeeded}");

		var directoriesToDelete = DeleteCandidates.Select(n => n.Name);
		Console.WriteLine($"Directories to Delete: {string.Join(',', directoriesToDelete)}");

		var directoryToDelete = DeleteCandidates.Min(n => n.Size);
		Console.WriteLine($"Directory to Delete Size: {directoryToDelete}");
	}

	private static void PrintTree(Node? node, int level = 0)
    {
	    if (node == null)
	    {
			return;
	    }

	    var formattedNode = $"{node.Name} ({node.Type}{(node.Size > 0 ? $", size={node.Size}" : "")})";
	    Console.WriteLine($"{new string(' ', level + 1)}- {formattedNode}");

	    foreach (var childNode in node.Children)
	    {
		    PrintTree(childNode.Value, level + 1);
	    }
	}

    private static int UpdateNodeSize(Node? node, bool checkForDeletion)
    {
	    if (node == null)
	    {
		    return 0;
	    }

	    if (node.Type == NodeType.file)
	    {
		    return node.Size;
	    }

	    var size = node.Children.Sum(childNode => UpdateNodeSize(childNode.Value, checkForDeletion));
	    node.Size = size;

	    if (size <= MaxDirectorySize)
	    {
		    MaxSizeDirectories.Add(node);
	    }

	    if (checkForDeletion & size >= _spaceNeeded)
	    {
			DeleteCandidates.Add(node);
	    }

	    return size;
    }
}