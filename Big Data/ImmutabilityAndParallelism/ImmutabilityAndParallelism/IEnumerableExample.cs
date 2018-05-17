using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutabilityAndParallelism
{
    class IEnumerableExample
    {

        public static void Example1()
        {
            var numbers = Enumerable.Range(1, 10).ToList();

            numbers
                .Where(x => x % 2 == 0)
                .ToList()
                .ForEach(Console.WriteLine);
            // Sized, ordered, non distinct, non sorted

            numbers
                .Where(x => x % 2 == 0)
                .OrderBy(x => x)
                .ToList()
                .ForEach(Console.WriteLine);
            // Sized, ordered, non distinct, sorted

            numbers
                .Where(x => x % 2 == 0)
                .Distinct()
                .ToList()
                .ForEach(Console.WriteLine);
            // Sized, ordered, distinct, sorted
        }
    }
}
