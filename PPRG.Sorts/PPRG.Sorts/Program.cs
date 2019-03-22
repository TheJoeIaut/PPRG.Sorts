using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PPRG.Sorts
{
    class Program
    {
        private static int[] _numbers = new int[10000000];

        private static readonly List<double> Thresholds = new List<double>
        {
            0.01,
            0.1,
            1,
            5,
            10,
            20,
            30,
            40,
            50
        };

        static void Main(string[] args)
        {
            InitList();

            var synchronousClock = new Stopwatch();
            synchronousClock.Start();
            for (int i = 0; i < 10; i++)
            {
                QuickSort.QuickSortSynchron((int[]) _numbers.Clone(), 0, _numbers.Length - 1);
            }

            synchronousClock.Stop();

            foreach (var threshold in Thresholds)
            {
                var asynchronousClock = new Stopwatch();
                asynchronousClock.Start();
                for (int i = 0; i < 10; i++)
                {
                    QuickSort.QuickSortParallel((int[]) _numbers.Clone(), 0, _numbers.Length - 1, threshold);
                }

                asynchronousClock.Stop();

                Console.WriteLine($"{synchronousClock.Elapsed / asynchronousClock.Elapsed}");
            }


            Console.ReadLine();
        }

        private static void InitList()
        {
            Random rnd = new Random();

            for (int i = 0; i < _numbers.Length; i++)
                _numbers[i] = i;

            _numbers = _numbers.OrderBy(x => rnd.Next()).ToArray();
        }
    }
}