using System.Security.Cryptography.X509Certificates;

namespace Aoc24Cs;

public class Day10
{
    public int[][] grid { get; set; }

    public Day10()
    {
        grid =
            File.ReadAllLines("in.txt")
            .Select(x => x.Select(y =>
                int.Parse(y.ToString()))
                .ToArray())
            .ToArray();
    }

    public void Run()
    {
        var l = new List<Way>();
        for (int y = 0; y < grid.Length; y++)
            for (int x = 0; x < grid[0].Length; x++)
                if (grid[y][x] == 0)
                    l.Add(new(x, y));

        for (int i = 0; i < 7; i++)
            foreach (var ll in l)
                ll.Step(grid);

        Console.WriteLine(l.Sum(x => x.finished));
    }
}

class Way(int xx, int yy)
{
    List<(int x, int y)> points = [(xx, yy)];
    public int finished => points.Count;
    public int nextNum = 1;

    public void Step(int[][] g)
    {
        var np = new List<(int, int)>();
        foreach (var (x, y) in points)
        {
            (int, int)[] neighs = [(x + 1, y), (x - 1, y), (x, y + 1), (x, y - 1)];
            foreach (var (nx, ny) in neighs)
            {
                if (g.GetOrDef(nx, ny) == nextNum)
                {
                    np.Add((nx, ny));
                }
            }
        }
        points = np;
        ++nextNum;
    }
}

static class Extensions
{
    public static int GetOrDef(this int[][] arr, int x, int y)
    {
        if (x < 0 || y < 0 || y >= arr.Length || x >= arr[0].Length)
        {
            return -1;
        }
        return arr[y][x];
    }
}