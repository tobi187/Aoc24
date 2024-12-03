namespace Aoc24Cs;

internal class Day3
{
    string content = File.ReadAllText("in.txt");
    bool on = true;
    int count = 0;
    public void Run()
    {
        var addon = new string(' ', 20);
        Check(content + addon);
        Console.WriteLine(count);
    }

    void Check(string txt)
    {
        for (int i = 0; i < txt.Length-20; i++)
        {
            if (txt[i..(i + 4)] == "do()") on = true;
            if (txt[i..(i + 7)] == "don't()") on = false;
            if (txt[i..(i+4)] == "mul(" && on)
            {
                var pt = txt[(i+4)..].TakeWhile(x => x != ')');
                count += Unparse(string.Concat(pt));
            }
        }
    }

    int Unparse(string cut)
    {
        var ab = cut.Split(',');
        if (ab.Length == 2
            && int.TryParse(ab[0], out var f)
            && int.TryParse(ab[1], out var s))
            return f * s;
        return 0;
    }
}
