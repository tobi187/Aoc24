using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc24Cs
{
    internal class Day12
    {
        public void Run()
        {
            var s = new HashSet<(int, int)> ();
            var f = File.ReadAllLines("in.txt");
            var ff = f.Select(x => x.ToArray()).ToArray();
            var regs = new List<Region>();
            for (int y = 0; y < ff.Length; y++)
            {
                for (var x = 0; x < ff[0].Length; x++)
                {
                    if (!s.Contains((x, y)))
                    {
                        var reg = new Region(ff[y][x]);
                        var ss = reg.FillRegion(ff, x, y);
                        regs.Add(reg);
                        s.UnionWith(ss);
                    }
                }
            }

            Console.WriteLine(regs.Sum(x => x.Brute() * x.l));
        }
    }

    class Region(char c)
    {
        public readonly char c = c;
        List<(int, int)> list = [];
        public int edges = 0;
        public int l = 1;
        public (int, int)? fst = null;

        public HashSet<(int, int)> FillRegion(char[][] g, int sx, int sy)
        {
            var hs = new HashSet<(int, int)> { (sx, sy) };
            var pints = new List<(int, int)> { (sx, sy) };
            while (pints.Count > 0)
            {
                var np = new List<(int, int)>();
                foreach (var (xx, yy) in pints)
                {
                    (int, int)[] edgs = [(xx + 1, yy), (xx - 1, yy), (xx, yy - 1), (xx, yy + 1)];
                    foreach(var (nx, ny) in edgs)
                    {
                        if (hs.Contains((nx, ny)))
                            continue;
                        else if (g.GetOrDef(nx, ny) != c)
                        {
                            if (fst == null)
                                fst = (nx, ny);
                            else
                                list.Add((nx, ny));
                        }
                        else
                        {
                            np.Add((nx, ny));  
                            hs.Add((nx, ny));
                            ++l;
                        }
                    }
                }
                pints = np;
            }
            list = list.Distinct().ToList();
            return hs;
        }

        public int Brute()
        {
            list.Add(fst.Value);
            draw();
            int cnt = 1;
            for (int i = 0; i < list.Count; i++)
            {
                var (fx, fy) = list[i];
                for (int j = i + 1; j < list.Count; j++)
                {
                    var (sx, sy) = list[j];
                    if (Math.Abs(fx - sx) == 1 && Math.Abs(fy - sy) == 1) ++cnt;
                }
            }

            return cnt;
        }

        public int CalcSites()
        {
            draw();
            var cnt = 1;
            (int x, int y) nxt = fst.Value;
            do
            {
                var ab = list.Select(x => (Math.Abs(nxt.x - x.Item1), Math.Abs(nxt.y - x.Item2))).ToArray();
                var i1 = Array.FindIndex(ab, x => Math.Min(x.Item1, x.Item2) == 0 && Math.Max(x.Item1, x.Item2) == 1);
                if (i1 == -1)
                {
                    i1 = Array.FindIndex(ab, x => x.Item1 == 1 && x.Item2 == 1);
                    cnt++;
                }
         
                (int x, int y) abc = list[i1];
                list.Remove(abc);
                nxt = abc;
            } while (list.Count > 0);
            return cnt;
        }

        void draw()
        {
            int maxx = 0;
            int maxy = 0;
            foreach (var (a, b) in list)
            {
                maxx = Math.Max(a + 2, maxx);
                maxy = Math.Max(b + 2, maxy);
            }
            for (int y = 0; y < maxy; y++)
            {
                for (int x = 0; x < maxx; x++)
                {
                    if (list.Contains((x-1, y-1)))
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------------");
        }

    }
}
