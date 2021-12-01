var input = File.ReadAllLines("input.txt").Select(x => int.Parse(x)).ToList();

var result = SonarSweep(input);

Console.WriteLine(result);

int SonarSweep(IReadOnlyList<int> numbers)
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
