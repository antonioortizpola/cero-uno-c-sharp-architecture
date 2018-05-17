using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ImmutabilityAndParallelism
{
    static class Agregate
    {
        private static IList<int> Numbers = Enumerable.Range(1, 10).ToList();

        public static void AgregateExample()
        {
            Console.WriteLine(
                Numbers
                    .Aggregate(0, SumNumbers)
            );
            Console.WriteLine(
                Numbers
                    .Select(x => x.ToString())
                    .Aggregate("", string.Concat)
            );
            Console.WriteLine(
                Numbers
                    .Select(x => x.ToString())
                    .Aggregate("", (x, y) => string.Concat(y, x))
            );
        }

        static int SumNumbers(int x, int y)
        {
            return x + y;
        }

        //      Where       Select      Aggregate
        //                                0.0
        //  x1     |                       \
        //--------------------------------  \
        //  x2     ->         x2'            +
        //--------------------------------    \
        //  x3     |                           \
        //--------------------------------      \
        //  x4     ->         x4'                +
    }
}
