namespace AOC2022.Day0;

public static class MonkeyMiddle
{
	private const string Day = "Day11";

	private record Monkey
	{
		public int Id;
		public List<long> Items = new(0);
		public string Operator = string.Empty;
		public string Operand = string.Empty;
		public int DivideBy;
		public int ResultTrue;
		public int ResultFalse;
		public int Inspected;

		public long Operate(long operand2)
		{
			var operand1 = Operand == "old" ? operand2 : long.Parse(Operand);

			var result = 0L;
			var operandString = Operand == "old" ? "itself" : operand1.ToString();
			var operationString = "shrugs from";

			switch (Operator)
			{
				case "+":
					result = operand1 + operand2;
					operationString = "increases by";
					break;
				case "-":
					result = operand1 - operand2;
					operationString = "decreases by";
					break;
				case "*":
					result = operand1 * operand2;
					operationString = "is multiplied by";
					break;
				case "/":
					result = operand1 / operand2;
					operationString = "is divided by";
					break;
			}

			//Console.WriteLine($"    Worry level {operationString} {operandString} to {result}.");

			return result;
		}

		public bool Test(long operand)
		{
			return operand % DivideBy == 0;
		}
	}

	public static void Part1()
    {
		var input = File.ReadAllLines($"{Day}/input.txt");
		var monkeys = new Dictionary<int, Monkey>();

		for (var i = 0; i < input.Length; i+=7)
		{
			var id = int.Parse(input[i].Trim().Split(' ')[1].TrimEnd(':'));
			var items = input[i + 1].Trim().Split(": ")[1].Split(", ").Select(long.Parse).ToList();
			var operation = input[i + 2].Trim().Split(": ")[1].Split(' ');
			var operatorValue = operation[^2];
			var operand = operation.Last();
			var divideBy = int.Parse(input[i + 3].Trim().Split(' ').Last());
			var testTrue = int.Parse(input[i + 4].Trim().Split(' ').Last());
			var testFalse = int.Parse(input[i + 5].Trim().Split(' ').Last());

			var monkey = new Monkey
			{
				Id = id,
				Items = items,
				Operator = operatorValue,
				Operand = operand,
				DivideBy = divideBy,
				ResultTrue = testTrue,
				ResultFalse = testFalse,
			};

			monkeys.Add(monkey.Id, monkey);
			//PrintMonkey(monkey);
		}

		const int maxRound = 20;
		for (var round = 1; round <= maxRound; round++)
		{
			for (var i = 0; i <= monkeys.Keys.Max(); i++)
			{
				var monkey = monkeys[i];

				//Console.WriteLine($"Monkey {monkey.Id}:");

				while (monkey.Items.Count != 0)
				{
					monkey.Inspected++;
					var item = monkey.Items.First();
					monkey.Items.RemoveAt(0);

					//Console.WriteLine($"  Monkey inspects an item with a worry level of {item}.");
					
					var newItem = monkey.Operate(item) / 3;

					//Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by 3 to {newItem}.");
					//Console.WriteLine($"    Current worry level is {(monkey.Test(newItem) ? "" : "not")} divisible by {monkey.DivideBy}.");

					if (monkey.Test(newItem))
					{
						monkeys[monkey.ResultTrue].Items.Add(newItem);
					}
					else
					{
						monkeys[monkey.ResultFalse].Items.Add(newItem);
					}

					//Console.WriteLine($"    Item with worry level {newItem} is thrown to monkey {(monkey.Test(newItem) ? monkey.ResultTrue : monkey.ResultFalse)}.");
				}
			}
		}

		Console.WriteLine($"After round {maxRound}, the monkeys are holding items with these worry levels:");

		var inspectedItems = new SortedList<int, int>();
		foreach (var monkey in monkeys.Values)
		{
			inspectedItems.Add(monkey.Inspected, monkey.Inspected);
			PrintMonkeyItems(monkey);
		}

		var monkeyBusiness = inspectedItems.Values.TakeLast(2).Aggregate((total, next) => total * next); ;
		Console.WriteLine($"Monkey Business Level: {monkeyBusiness}");
	}

    public static void Part2()
    {
		var input = File.ReadAllLines($"{Day}/sample.txt");
		var monkeys = new Dictionary<int, Monkey>();

		for (var i = 0; i < input.Length; i += 7)
		{
			var id = int.Parse(input[i].Trim().Split(' ')[1].TrimEnd(':'));
			var items = input[i + 1].Trim().Split(": ")[1].Split(", ").Select(long.Parse).ToList();
			var operation = input[i + 2].Trim().Split(": ")[1].Split(' ');
			var operatorValue = operation[^2];
			var operand = operation.Last();
			var divideBy = int.Parse(input[i + 3].Trim().Split(' ').Last());
			var testTrue = int.Parse(input[i + 4].Trim().Split(' ').Last());
			var testFalse = int.Parse(input[i + 5].Trim().Split(' ').Last());

			var monkey = new Monkey
			{
				Id = id,
				Items = items,
				Operator = operatorValue,
				Operand = operand,
				DivideBy = divideBy,
				ResultTrue = testTrue,
				ResultFalse = testFalse,
			};

			monkeys.Add(monkey.Id, monkey);
		}

		const int maxRound = 10000;
		var mod = monkeys.Values.Select(m => m.DivideBy).Aggregate((total, next) => total * next);

		for (var round = 1; round <= maxRound; round++)
		{
			for (var i = 0; i <= monkeys.Keys.Max(); i++)
			{
				var monkey = monkeys[i];

				while (monkey.Items.Count != 0)
				{
					monkey.Inspected++;
					var item = monkey.Items.First();
					monkey.Items.RemoveAt(0);

					var newItem = monkey.Operate(item);

					if (newItem > mod)
					{
						newItem %= mod;
					}

					if (monkey.Test(newItem))
					{
						monkeys[monkey.ResultTrue].Items.Add(newItem);
					}
					else
					{
						monkeys[monkey.ResultFalse].Items.Add(newItem);
					}
				}
			}
		}

		//Console.WriteLine($"== After round {maxRound} ==");

		var inspectedItems = new SortedList<long, long>();
		foreach (var monkey in monkeys.Values)
		{
			inspectedItems.Add(monkey.Inspected, monkey.Inspected);
			//PrintMonkeyInspection(monkey);
			//PrintMonkeyItems(monkey);
		}

		var monkeyBusiness = inspectedItems.Values.TakeLast(2).Aggregate((total, next) => total * next);
		Console.WriteLine($"Monkey Business Level: {monkeyBusiness}");
	}

    private static void PrintMonkey(Monkey monkey)
    {
		Console.WriteLine($"Monkey {monkey.Id}:");
		Console.WriteLine($"\tStarting items: {string.Join(',',monkey.Items)}");
		Console.WriteLine($"\tOperation: new = old {monkey.Operator} {monkey.Operand}");
		Console.WriteLine($"\tTest: divisible by {monkey.DivideBy}");
		Console.WriteLine($"\t\tIf true: throw to monkey {monkey.ResultTrue}");
		Console.WriteLine($"\t\tIf false: throw to monkey {monkey.ResultFalse}");
	}

    private static void PrintMonkeyItems(Monkey monkey)
    {
	    Console.WriteLine($"Monkey {monkey.Id}: {string.Join(',', monkey.Items)}");
    }

    private static void PrintMonkeyInspection(Monkey monkey)
    {
	    Console.WriteLine($"Monkey {monkey.Id} inspected items {monkey.Inspected} times.");
	}
}