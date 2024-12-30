using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Aoc24Cs
{
    internal class Day9
    {
        int[] arr = [];
        int lp = 0;
        int rp = 0;
        int rest = 0;
        List<int> res = [];

        public void Run()
        {
            Run2();
            return;
            var file = File.ReadAllText("in.txt");
            arr = file.Select(x => int.Parse(x.ToString())).ToArray();
            if (arr.Length % 2 == 0) throw new Exception("throw up");
            rp = arr.Length + 1;
            while (rp > lp)
            {
                if (lp % 2 == 0)
                {
                    for (int _ = 0; _ < arr[lp]; _++)
                        res.Add(lp / 2);
                    lp++;
                }
                else
                {
                    for (var _ = 0; _ < arr[lp]; _++)
                    {
                        if (rest == 0)
                        {
                            rp -= 2;
                            if (rp <= lp) break;
                            rest = arr[rp];
                        }
                        res.Add(rp / 2);
                        rest--;
                    }
                    ++lp;
                }
            }
            for (var _ = 0; _ < rest; _++)
                res.Add(rp / 2);

            //Console.WriteLine(string.Concat(res));
            Console.WriteLine(res.Select((x, i) => (long)(x * i)).Sum());
        }

        private void Run3()
        {
            var file = File.ReadAllText("in.txt");
            arr = file.Select(x => int.Parse(x.ToString())).ToArray();
            if (arr.Length % 2 == 0) throw new Exception("throw up");
            rp = arr.Length + 1;
            var l = new List<int>();

            var ll = new int[arr.Length];

            int start = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                var toAdd = i % 2 == 0 ? i / 2 : -1;
                for (int j = 0; j < arr[i]; j++)
                    l.Add(toAdd);
                ll[i] = start;
                start += arr[i];
            }

            var all = string.Concat(res);
            //Console.WriteLine(res.Select((x, i) => (long)(x * i)).Sum());
        }

        void Run2()
        {
            var file = File.ReadAllText("in.txt");
            arr = file.Select(x => int.Parse(x.ToString())).ToArray();
            if (arr.Length % 2 == 0) throw new Exception("throw up");
            rp = arr.Length + 1;

            var tl = new List<Block>();
            for (int i = 0; i < arr.Length; i++)
                if (i % 2 == 0)
                    tl.Add(new Block(arr[i], i / 2));
                else
                    tl.Add(new Block(arr[i], -1));

            for (int i = tl.Count - 1; i > 0; i -= 2)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (tl[j].IsSpace && tl[j].AddKidIfSufficient(tl[i]))
                    {
                        tl[i] = new Block(tl[i].length, 0);
                        break;
                    }
                }
            }
            Console.WriteLine(long.MaxValue);
            // 5024982422077
            // between
            // 6390782022205
            long sum = 0;
            var ind = 0;
            foreach (var b in tl)
            {
                sum += b.GetSum(ind);
                ind += b.length;
            }
            //var a = string.Concat(tl.Select(x=>x.GS()));
            //Console.WriteLine(a.Length);
            //var sum = a.Select((x, i) => x == '.' ? 0 : (long.Parse(x.ToString()) * i)).Sum();
            System.Console.WriteLine(sum);
        }
    }

    class Block(int l, int b)
    {
        public int length { get; set; } = l;
        int block { get; set; } = b;

        public bool IsSpace => block == -1;
        public int GetFree => length - kids.Sum(x => x.length);
        List<Block> kids = [];

        public bool AddKidIfSufficient(Block b)
        {
            if (b.length > GetFree)
                return false;
            kids.Add(b);
            return true;
        }

        public long GetSum(int startIndex)
        {
            long sum = 0;

            if (!IsSpace)
            {
                for (int i = 0; i < length; i++)
                    sum += startIndex++ * block;

                return sum;
            }
            foreach (var kid in kids)
            {
                sum += kid.GetSum(startIndex);
                startIndex += kid.length;
            }
            return sum;
        }

        public string GS(bool iskid = false)
        {
            if (!IsSpace)
                return new string(block.ToString()[0], length);
            var s = kids.Select(x => x.GS(true));
            return string.Concat(s) + new string('.', GetFree);
        }

        public override string ToString()
        {
            return $"{block} {length}";
        }
    }
}
