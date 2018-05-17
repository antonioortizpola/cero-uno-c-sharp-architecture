using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImmutabilityAndParallelism
{
    static class Parallelism
    {


        public static void Example1()
        {
            var numbers = Enumerable.Range(1, 10).ToList();

            MeassureMethod(() =>
                Console.WriteLine(
                    numbers
                        .Where(x => x % 2 == 0)
                        .Select(Compute)//Map en casi todos los demás leguajes
                        .Sum()
                )
            );

            MeassureMethod(() =>
                Console.WriteLine(
                    numbers
                        .Where(x => x % 2 == 0)
                        .AsParallel()
                        .Select(Compute)
                        .Sum()
                )
            );
            // Considerar los recursos, no dará magicamente resultados
        }

        public static int Compute(int number)
        {
            // Time intensive
            Thread.Sleep(1000);
            return number * 2;
        }

        public static void MeassureMethod(Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            Console.WriteLine("Excecuted in " + sw.Elapsed);
        }
    }
}
