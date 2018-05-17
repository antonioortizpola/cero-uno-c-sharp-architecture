using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ImmutabilityAndParallelism.Lambda
{
    public class DecoratorPattern
    {
        public static void DoWork(int value, Func<int, int> func)
        {
            Console.WriteLine(func(value));
        }


        public static void Example()
        {
            Func<int, int> inc = (x) => x + 1;
            Func<int, int> doubleIt = (x) => x * 2;

            DoWork(5, inc);
            DoWork(5, doubleIt);

            //Increment and double
            var temp = inc(5); //Garbage variable, noise

            Console.WriteLine(doubleIt(temp));
            DoWork(5, inc.Then(doubleIt));
            //DoWork(5, doubleIt.After(inc));
        }
    }

    public static class FuncExtensions
    {
        public static Func<T, TResult2> After<T, TResult1, TResult2>(
            this Func<TResult1, TResult2> function2, Func<T, TResult1> function1) =>
            value => function2(function1(value));

        public static Func<T, TResult2> Then<T, TResult1, TResult2>( // Before.
            this Func<T, TResult1> function1, Func<TResult1, TResult2> function2) =>
            value => function2(function1(value));
    }
}
