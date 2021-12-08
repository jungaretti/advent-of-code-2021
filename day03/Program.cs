using System.Text;

int PartOne(StreamReader reader)
{
    var queue = new Queue<int>();
    string? current;
    while ((current = reader.ReadLine()) != null)
    {
        foreach (char bit in current)
        {
            var running = queue.Count() >= current.Count() ? queue.Dequeue() : 0;
            running += (bit == '1') ? 1 : -1;
            queue.Enqueue(running);
        }
    }

    var epsilonBuilder = new StringBuilder();
    var gammaBuilder = new StringBuilder();

    while (queue.Count() > 0)
    {
        var netOne = queue.Dequeue();
        var mostCommon = netOne > 0 ? '1' : '0';
        var leastCommon = netOne > 0 ? '0' : '1';

        epsilonBuilder.Append(mostCommon);
        gammaBuilder.Append(leastCommon);
    }

    var epsilon = Convert.ToInt32(epsilonBuilder.ToString(), 2);
    var gamma = Convert.ToInt32(gammaBuilder.ToString(), 2);

    return epsilon * gamma;
}

var reader = new StreamReader("data03.txt");
Console.WriteLine($"Part One: {PartOne(reader)}");
