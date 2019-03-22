using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PPRG.Sorts
{
    public static class QuickSort
    {
        public static void QuickSortSynchron(int[] arr, int start, int end)
        {
            if (start >= end)
                return;

            var i = Partition(arr, start, end);

            QuickSortSynchron(arr, start, i - 1);
            QuickSortSynchron(arr, i + 1, end);
        }

        private static int Partition(int[] arr, int left, int right)
        {
            var pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        public static void QuickSortParallel(int[] arr, int start, int end, double threshold )
        {
            if (start >= end)
                return;

            var i = Partition(arr, start, end);

            if ((end - start) * 100.0 / arr.Length < threshold)
            {
                QuickSortSynchron(arr, start, i - 1);
                QuickSortSynchron(arr, i + 1, end);
            }
            else
            {
                Parallel.Invoke(() => QuickSortParallel(arr, start, i - 1,threshold), () => QuickSortParallel(arr, i + 1, end, threshold));
            }
        }
    }
}
