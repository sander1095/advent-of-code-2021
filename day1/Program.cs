var input = File.ReadAllLines("input.txt").Select(x => int.Parse(x)).ToList();

var partOneResult = SonarSweepPartOne(input);
var partTwoResult = SonarSweepPartTwo(input);

Console.WriteLine($"Part one: {partOneResult}");
Console.WriteLine($"Part two: {partTwoResult}");

int SonarSweepPartOne(IReadOnlyList<int> numbers)
{
    var increases = 0;

    // Skip the last item because it has already been compared
    for (int i = 0; i < numbers.SkipLast(1).Count(); i++)
    {
        var firstItem = numbers[i];
        var secondItem = numbers[i + 1];
       
        if (secondItem > firstItem)
        {
            increases++;
        }
    }

    return increases;
}

int SonarSweepPartTwo(IReadOnlyList<int> numbers)
{
    var increases = 0;
    // Admittedly, I cheated a little bit on this one.. It was already past 00:00 🕕😴
    // But hey, I learned some cool stuff! Thanks u/Zeeterm!
    for (int i = 3; i < numbers.Count ; i++)
    {
        if (numbers[i] > numbers[i - 3])
        {
            increases++;
        }
    }
    return increases;
}
