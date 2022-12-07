namespace AOC2022.Day1;

public static class CalorieCounting
{
    public static void Part1And2()
    {
        var input = File.ReadAllLines("Day1/input.txt");
        
        // avoid duplicates
        var elves = new SortedSet<int>();
        var currentCalories = 0;

		foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                elves.Add(currentCalories);
                currentCalories = 0;
                continue;
            }

            var calories = int.Parse(line);
            currentCalories += calories;
        }

        // Off-by one
        elves.Add(currentCalories);

        Console.WriteLine($"Highest Calorie Count: {elves.Max}");

        var top3 = elves.ToList().GetRange(elves.Count - 3, 3).Sum();

        Console.WriteLine($"Top 3 Calorie Count: {top3}");
    }
}