using System.Windows.Markup;

namespace Aoc24Cs
{
    internal class Day7
    {
        public void Run()
        {
            var file = File.ReadAllLines("in.txt");
            var l = new List<Calculation>();
            foreach (var line in file)
            {
                var spl = line.Split(": ");
                var f = int.Parse(spl[0]);
                var s = spl[1].Split().Select(int.Parse).ToArray();
                l.Add(new Calculation(f, s));
            }

            Console.WriteLine(l.Select(x => x.Calc()).Sum());
        }
    }

    class Calculation(int s, int[] nn)
    {
        public int Sum { get; set; } = s;
        public int[] Nums { get; set; } = nn;

        int breakpoint = -1;

        public int Calc()
        {
            GetBreaki();

            return Sum;
        }

        void BruteCombis()
        {
            var z = Nums.Length - 1;
            
        }

        void GetBreaki()
        {
            int localSum = Nums[0];
            int breaki = Nums.Length-1;
            for (int i = 1; i <  Nums.Length; i++)
            {
                localSum *= Nums[i];
                if (localSum > Sum)
                {
                    breaki = i;
                    break;
                }
            }
            breakpoint = breaki;
        }
    }
}
