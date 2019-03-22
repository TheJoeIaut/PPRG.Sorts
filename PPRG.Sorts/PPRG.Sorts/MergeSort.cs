using System;
using System.Collections.Generic;
using System.Text;

namespace PPRG.Sorts
{
    public static class MergeSort
    {
        private static void MergeSortSynchronous(int[] input, int low, int high)
        {
            if (low < high)
            {
                var middle = low / 2 + high / 2;
                MergeSortSynchronous(input, low, middle);
                MergeSortSynchronous(input, middle + 1, high);
                Merge(input, low, middle, high);
            }
        }

        public static void MergeSortSynchronous(int[] input)
        {
            MergeSortSynchronous(input, 0, input.Length - 1);
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
