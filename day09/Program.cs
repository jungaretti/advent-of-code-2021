using System.Collections;

public class Node
{
    public int Height;
    public bool Sink = true;

    public Node(int height)
    {
        this.Height = height;
    }
}

// public class Edge
// {
//     public Node From;
//     public Node To;

//     public Edge(Node from, Node to)
//     {
//         this.From = from;
//         this.To = to;
//     }
// }

class Program
{
    static Node[][] BuildNodes(StreamReader reader)
    {
        var nodes = new List<List<Node>>();

        string? line; // Can I do this in one line?
        while ((line = reader.ReadLine()) != null)
        {
            var lineNodes = new List<Node>();
            foreach (var number in line)
            {
                lineNodes.Add(new Node(number - '0'));
            }
            nodes.Add(lineNodes);
        }

        return nodes.Select(l => l.ToArray()).ToArray();
    }

    static int PartOne(Node[][] nodes)
    {
        var solution = 0;
        for (int i = 0; i < nodes.Count(); i++)
        {
            for (int j = 0; j < nodes[i].Count(); j++)
            {
                var node = nodes[i][j];

                // Check above
                if (i > 0 && nodes[i - 1][j].Height <= node.Height)
                {
                    continue;
                }
                // Check below
                if (i < nodes.Count() - 1 && nodes[i + 1][j].Height <= node.Height)
                {
                    continue;
                }
                // Check left
                if (j > 0 && nodes[i][j - 1].Height <= node.Height)
                {
                    continue;
                }
                // Check right
                if (j < nodes[i].Count() - 1 && nodes[i][j + 1].Height <= node.Height)
                {
                    continue;
                }

                // We found a sink!
                solution += node.Height + 1;
            }
        }
        return solution;
    }

    static void Main(string[] args)
    {
        using var reader = new StreamReader("data09.txt");
        var nodes = BuildNodes(reader);

        Console.WriteLine($"Part One: {PartOne(nodes)}");
    }
}
