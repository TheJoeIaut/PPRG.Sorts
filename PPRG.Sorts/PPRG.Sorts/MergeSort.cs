using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PPRG.Sorts
{
    public static class MergeSort
    {
        public static void MergeSortSynchronous(int[] input, int low, int high)
        {
            if (low >= high)
                return;

            var middle = low / 2 + high / 2;
            MergeSortSynchronous(input, low, middle);
            MergeSortSynchronous(input, middle + 1, high);
            Merge(input, low, middle, high);
        }

        public static void MergeSortParallel(int[] input, int low, int high, double threshold)
        {
            if (low >= high)
                return;

            var middle = low / 2 + high / 2;
            if ((high - low) * 100.0 / input.Length < threshold)
            {
                MergeSortSynchronous(input, low, middle);
                MergeSortSynchronous(input, middle + 1, high);
            }
            else
            {
                Parallel.Invoke(() => MergeSortParallel(input, low, middle, threshold), () => MergeSortParallel(input, middle + 1, high, threshold));
            }
            Merge(input, low, middle, high);
        }

        private static void Merge(int[] input, int low, int middle, int high)
        {

            var left = low;
            var right = middle + 1;
            int[] tmp = new int[(high - low) + 1];
            int tmpIndex = 0;

            while (left <= middle && right <= high)
            {
                if (input[left] < input[right])
                {
                    tmp[tmpIndex] = input[left];
                    left = left + 1;
                }
                else
                {
                    tmp[tmpIndex] = input[right];
                    right = right + 1;
                }
                tmpIndex = tmpIndex + 1;
            }

            if (left <= middle)
            {
                while (left <= middle)
                {
                    tmp[tmpIndex] = input[left];
                    left = left + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            if (right <= high)
            {
                while (right <= high)
                {
                    tmp[tmpIndex] = input[right];
                    right = right + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                input[low + i] = tmp[i];
            }

        }
    }
}
