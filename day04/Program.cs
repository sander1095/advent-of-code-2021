var sample = GetInput("sample.txt");
var input = GetInput("input.txt");

Console.WriteLine($"Part one SAMPLE: {GiantSquidPartOne(sample.input, sample.bingoBoards)}");
Console.WriteLine($"Part one REAL: {GiantSquidPartOne(input.input, input.bingoBoards)}");

Console.WriteLine($"Part two SAMPLE: {GiantSquidPartTwo(sample.input, sample.bingoBoards)}");
Console.WriteLine($"Part two REAL: {GiantSquidPartTwo(input.input, input.bingoBoards)}");

int GiantSquidPartOne(IReadOnlyList<int> input, IReadOnlyList<BingoBoard> bingoBoards)
{

    foreach (var number in input)
    {
        foreach (var bingoBoard in bingoBoards)
        {
            bingoBoard.MarkNumber(number);

            if (bingoBoard.HasBingo())
            {
                return bingoBoard.CalculateFinalScore(number);
            }
        }
    }

    throw new Exception("No bingo found");
}


int GiantSquidPartTwo(IReadOnlyList<int> input, IReadOnlyList<BingoBoard> bingoBoards)
{
    var amountOfBoardsWithBingo = 0;
    foreach (var number in input)
    {
        foreach (var bingoBoard in bingoBoards)
        {
            bingoBoard.MarkNumber(number);

            if(bingoBoards.All(x => x.HasBingo()))
            {
                return bingoBoard.CalculateFinalScore(number);
            }
        }       
    }

    throw new Exception("No bingo found");
}

(IReadOnlyList<int> input, IReadOnlyList<BingoBoard> bingoBoards) GetInput(string file)
{
    var allLines = File.ReadAllLinesAsync(file).GetAwaiter().GetResult();

    var input = allLines.First().Split(',').Select(x => int.Parse(x)).ToList();

    var bingoBoards = new List<BingoBoard>();

    var rowCounter = 0;
    var tempRows = new List<List<BingoNumber>>();
    foreach (var line in allLines.Skip(2))
    {
        if(string.IsNullOrWhiteSpace(line))
        {
            // Start of new bingo board
            bingoBoards.Add(new(tempRows));

            rowCounter = 0;
            tempRows = new();
        }
        else
        {
            tempRows.Add(new());
            var numbers = line.Split(' ').Where(x => x != "").Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < numbers.Length; i++)
            {
                tempRows[rowCounter].Add(new BingoNumber(numbers[i]));
            }

            rowCounter++;
        }
    }

    bingoBoards.Add(new(tempRows));


    return (input, bingoBoards);
}

class BingoBoard
{
    private readonly List<List<BingoNumber>> _items;

    public BingoBoard(List<List<BingoNumber>> items)
    {
        _items = items;
    }

    public void MarkNumber(int number)
    {
        foreach (var item in _items.SelectMany(x => x.Select(y => y)).Where( x => x.Number == number))
        {
            item.Marked = true;
        }
    }

    public int CalculateFinalScore(int winningNumber)
    {
        var allUnmarkedNumbersCombined = _items.SelectMany(x => x).Where(x => !x.Marked).Sum(x => x.Number);

        return allUnmarkedNumbersCombined * winningNumber;
    }

    public bool HasBingo()
    {
        // Bingo in any row?
        foreach (var item in _items)
        {
            if (item.All(x => x.Marked)) return true;
        }

        // Bingo in any column?
        for (int i = 0; i < 5; i++)
        {
            var test = _items.Select(x => x).Select(x => x.ElementAt(i)).All(x => x.Marked);

            if (test) return true;
        }

        return false;
    }
}

class BingoNumber
{
    public BingoNumber(int number)
    {
        Number = number;
    }

    public int Number { get; set; }
    public bool Marked { get; set; }
}


