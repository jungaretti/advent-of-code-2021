using System.Text;

IEnumerable<string> GetLines(StreamReader reader)
{
    var lines = new List<string>();

    string? current;
    while ((current = reader.ReadLine()) != null)
    {
        lines.Add(current);
    }

    return lines;
}

string MostCommonBits(IEnumerable<string> lines, char tiebreaker = '0')
{
    var queue = new Queue<int>();

    foreach (var line in lines)
    {
        foreach (char bit in line)
        {
            var running = queue.Count() >= line.Count() ? queue.Dequeue() : 0;
            running += (bit == '1') ? 1 : -1;
            queue.Enqueue(running);
        }
    }

    var stringBuilder = new StringBuilder();
    while (queue.Count() > 0)
    {
        var netOne = queue.Dequeue();
        if (netOne > 0)
        {
            stringBuilder.Append('1');
        }
        else if (netOne < 0)
        {
            stringBuilder.Append('0');
        }
        else
        {
            stringBuilder.Append(tiebreaker);
        }
    }

    return stringBuilder.ToString();
}

string LeastCommonBits(IEnumerable<string> lines, char tiebreaker = '0')
{
    return MostCommonBits(lines, tiebreaker)
        .Replace('0', 'T')
        .Replace('1', '0')
        .Replace('T', '1');
}

var reader = new StreamReader("data03.txt");
var lines = GetLines(reader);

var epsilonString = MostCommonBits(lines);
var epsilon = Convert.ToInt32(epsilonString, 2);

var gammaString = LeastCommonBits(lines);
var gamma = Convert.ToInt32(gammaString, 2);

Console.WriteLine($"Part One: {epsilon * gamma}");

var oxygenOptions = new List<string>(lines);
var carbonOptions = new List<string>(lines);

// This is icky but I am struggling
for (int i = 0; i < epsilonString.Count(); i++)
{
    if (oxygenOptions.Count() > 1)
    {
        var oxygenString = MostCommonBits(oxygenOptions, '1');
        oxygenOptions = oxygenOptions.Where(e => e[i] == oxygenString[i]).ToList();
    }
    if (carbonOptions.Count() > 1)
    {
        // I don't know why setting the tiebreaker to 1 works
        var carbonString = LeastCommonBits(carbonOptions, '1');
        carbonOptions = carbonOptions.Where(e => e[i] == carbonString[i]).ToList();
    }
}

var oxygen = Convert.ToInt32(oxygenOptions.First(), 2);
var carbon = Convert.ToInt32(carbonOptions.First(), 2);

Console.WriteLine($"Part Two: {oxygen * carbon}");
