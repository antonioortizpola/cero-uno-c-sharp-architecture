using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutabilityAndParallelism
{
    static class ReferentialTransparency
    {
        private static int a = 1;
        private static int b = 2;

        private static readonly int c = 1;
        private static readonly int d = 2;

        public static void Example1()
        {
            Console.WriteLine(a + b); //Cumple la regla 1, pero no la 2
            //No es pura

            Console.WriteLine(c + d); //Cumple la regla 1, pero no la 2
            //Es pura
        }

        public static void Example2()
        {
            var numbers = Enumerable.Range(1, 10).Reverse().ToList();
            Console.WriteLine(string.Join(", ", numbers.QuickSortM()));
            Console.WriteLine(string.Join(", ", numbers.QuickSortI()));
        }
    }

    public static class QuickSortMutable
    {
        public static IEnumerable<T> QuickSortM<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            var array = sequence.ToArray();
            QuickSortM(array, 0, array.Length - 1);
            return array;
        }

        static void QuickSortM<T>(this T[] array, int start, int end) where T : IComparable<T>
        {
            if (end - start < 1)
                return;
            var pivot = array[start];
            var j = start;
            for (var i = start + 1; i <= end; ++i)
            {
                if (array[i].CompareTo(pivot) < 0)
                {
                    var temp = array[j++];
                    array[j] = array[i];
                    array[i] = temp;
                }
            }
            array[start] = array[j];
            array[j] = pivot;
            QuickSortM(array, start, j - 1);
            QuickSortM(array, j + 1, end);
        }
    }

    static class QuickSortImmutable
    {
        public static IEnumerable<T> QuickSortI<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            var sequenceAsList = sequence.ToList();
            if (!sequenceAsList.Any())
                return sequenceAsList;
            var pivot = sequenceAsList.First();
            var remaining = sequenceAsList.Skip(1).ToList();
            return
                (from x in remaining where x.CompareTo(pivot) < 0 select x)
                .QuickSortI()
                .Concat(pivot.Enumerate())
                .Concat((from x in remaining where x.CompareTo(pivot) >= 0 select x).QuickSortI());
        }

        public static IEnumerable<T> Enumerate<T>(this T t)
        {
            yield return t;
        }
    }
}
