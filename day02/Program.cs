var input = File.ReadAllLines("input.txt").Select(x =>
{
    var navigationStepRaw = x.Split(' ');

    return new NavigationStep(Enum.Parse<Direction>(navigationStepRaw[0], ignoreCase: true), int.Parse(navigationStepRaw[1]));
}).ToList();

Console.WriteLine($"Part one: {DivePartOne(input)}");
Console.WriteLine($"Part two: {DivePartTwo(input)}");

static int DivePartOne(IReadOnlyList<NavigationStep> navigationSteps)
{
    var horizontalPosition = 0;
    var depth = 0;

    foreach (var navigationStep in navigationSteps)
    {
        switch (navigationStep.Direction)
        {
            case Direction.Forward:
                horizontalPosition += navigationStep.Units;
                break;
            case Direction.Down:
                depth += navigationStep.Units;
                break;
            case Direction.Up:
                depth -= navigationStep.Units;
                break;
        }
    }

    return horizontalPosition * depth;
}

static int DivePartTwo(IReadOnlyList<NavigationStep> navigationSteps)
{
    var horizontalPosition = 0;
    var depth = 0;
    var aim = 0;

    foreach (var navigationStep in navigationSteps)
    {
        switch (navigationStep.Direction)
        {
            case Direction.Forward:
                horizontalPosition += navigationStep.Units;
                depth += aim * navigationStep.Units;
                break;
            case Direction.Down:
                aim += navigationStep.Units;
                break;
            case Direction.Up:
                aim -= navigationStep.Units;
                break;
        }
    }

    return horizontalPosition * depth;
}

record NavigationStep(Direction Direction, int Units);

enum Direction
{
    Forward,
    Down,
    Up
}
