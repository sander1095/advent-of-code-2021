var input = File.ReadAllLines("input.txt").ToList();

Console.WriteLine($"Part one: {BinaryDiagnosticPartOne(input)}");
Console.WriteLine($"Part two: {BinaryDiagnosticPartTwo(input)}");

static int BinaryDiagnosticPartOne(IReadOnlyList<string> binaryNumbers)
{
    var gammaRate = 0;
    var epsilonRate = 0;

    var firstBits = new List<int>();
    var secondBits = new List<int>();
    var thirdBits = new List<int>();
    var fourthBits = new List<int>();
    var fifthBits = new List<int>();
    var g = new List<int>();
    var h = new List<int>();
    var i = new List<int>();
    var j = new List<int>();
    var k = new List<int>();
    var l = new List<int>();
    var m = new List<int>();

    foreach (var number in binaryNumbers)
    {
        firstBits.Add(int.Parse(number[0].ToString()));
        secondBits.Add(int.Parse(number[1].ToString()));
        thirdBits.Add(int.Parse(number[2].ToString()));
        fourthBits.Add(int.Parse(number[3].ToString()));
        fifthBits.Add(int.Parse(number[4].ToString()));
        g.Add(int.Parse(number[5].ToString()));
        h.Add(int.Parse(number[6].ToString()));
        i.Add(int.Parse(number[7].ToString()));
        j.Add(int.Parse(number[8].ToString()));
        k.Add(int.Parse(number[9].ToString()));
        l.Add(int.Parse(number[10].ToString()));
        m.Add(int.Parse(number[11].ToString()));
    }

    var firstCommonBit = firstBits.Count(x => x == 0) > firstBits.Count(x => x == 1) ? 0 : 1;
    var secondCommonBit = secondBits.Count(x => x == 0) > secondBits.Count(x => x == 1) ? 0 : 1;
    var thirdCommonBit = thirdBits.Count(x => x == 0) > thirdBits.Count(x => x == 1) ? 0 : 1;
    var fourthCommonBit = fourthBits.Count(x => x == 0) > fourthBits.Count(x => x == 1) ? 0 : 1;
    var fifthCommonBit = fifthBits.Count(x => x == 0) > fifthBits.Count(x => x == 1) ? 0 : 1;
    var gcb = g.Count(x => x == 0) > g.Count(x => x == 1) ? 0 : 1;
    var hcb = h.Count(x => x == 0) > h.Count(x => x == 1) ? 0 : 1;
    var icb = i.Count(x => x == 0) > i.Count(x => x == 1) ? 0 : 1;
    var jcb= j.Count(x => x == 0) > j.Count(x => x == 1) ? 0 : 1;
    var kcb = k.Count(x => x == 0) > k.Count(x => x == 1) ? 0 : 1;
    var lcb= l.Count(x => x == 0) > l.Count(x => x == 1) ? 0 : 1;
    var mcb = m.Count(x => x == 0) > m.Count(x => x == 1) ? 0 : 1;

    var firstUncommonBit = firstBits.Count(x => x == 0) > firstBits.Count(x => x == 1) ? 1 : 0;
    var secondUncommonBit = secondBits.Count(x => x == 0) > secondBits.Count(x => x == 1) ?1 :0;
    var thirdUncommonBit = thirdBits.Count(x => x == 0) > thirdBits.Count(x => x == 1) ? 1: 0;
    var fourthUncommonBit = fourthBits.Count(x => x == 0) > fourthBits.Count(x => x == 1) ?1 : 0;
    var fifthUncommonBit = fifthBits.Count(x => x == 0) > fifthBits.Count(x => x == 1) ? 1 : 0;
    var gub = g.Count(x => x == 0) > g.Count(x => x == 1) ? 1 : 0;
    var hub = h.Count(x => x == 0) > h.Count(x => x == 1) ? 1 : 0;
    var iub = i.Count(x => x == 0) > i.Count(x => x == 1) ? 1 : 0;
    var jub = j.Count(x => x == 0) > j.Count(x => x == 1) ? 1 : 0;
    var kub = k.Count(x => x == 0) > k.Count(x => x == 1) ? 1 : 0;
    var lub = l.Count(x => x == 0) > l.Count(x => x == 1) ? 1 : 0;
    var mub = m.Count(x => x == 0) > m.Count(x => x == 1) ? 1 : 0;

    gammaRate = Convert.ToInt32($"{firstCommonBit}{secondCommonBit}{thirdCommonBit}{fourthCommonBit}{fifthCommonBit}{gcb}{hcb}{icb}{jcb}{kcb}{lcb}{mcb}", 2);
    epsilonRate = Convert.ToInt32($"{firstUncommonBit}{secondUncommonBit}{thirdUncommonBit}{fourthUncommonBit}{fifthUncommonBit}{gub}{hub}{iub}{jub}{kub}{lub}{mub}", 2);

    return gammaRate * epsilonRate;
}

static int BinaryDiagnosticPartTwo(IReadOnlyList<string> binaryNumbers)
{
    var oxygen = CalculateOxygenLevel(binaryNumbers, 0);
    var c02 = CalculateCO2Level(binaryNumbers, 0);

    return oxygen * c02;


    static int CalculateOxygenLevel(IReadOnlyList<string> binaryNumbers, int position)
    {
        if (binaryNumbers.Count == 1)
        {
            return Convert.ToInt32(string.Join("", binaryNumbers), 2);
        }

        var numbersInPosition = binaryNumbers.Select(x => int.Parse(x[position].ToString())).ToList();
        
        var oneWon = false;

        if (numbersInPosition.Count(x => x == 1) >= numbersInPosition.Count(x => x == 0))
        {
            oneWon = true;
        }

        var winnersOfCurrentRound = binaryNumbers.Where(x => x[position].ToString() == (oneWon ? "1" : "0")).ToList();
        return CalculateOxygenLevel(winnersOfCurrentRound, position + 1);
    }

    static int CalculateCO2Level(IReadOnlyList<string> binaryNumbers, int position)
    {
        if (binaryNumbers.Count == 1)
        {
            return Convert.ToInt32(string.Join("", binaryNumbers), 2);
        }

        var numbersInPosition = binaryNumbers.Select(x => int.Parse(x[position].ToString())).ToList();

        var amountOfOnes = numbersInPosition.Count(x => x == 1);
        var amountOfZeroes = numbersInPosition.Count(x => x == 0);

        var shouldContinueWithOne = false;

        if(amountOfOnes < amountOfZeroes)
        {
            shouldContinueWithOne = true;
        }        

        var winnersOfCurrentRound = binaryNumbers.Where(x => x[position].ToString() == (shouldContinueWithOne ? "1" : "0")).ToList();
        return CalculateCO2Level(winnersOfCurrentRound, position + 1);
    }
}