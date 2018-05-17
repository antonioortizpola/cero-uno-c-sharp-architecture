using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutabilityAndParallelism
{
    class Lambdas
    {
        private static IList<int> numbers = new List<int>() {1, 2, 3, 5, 4, 6, 7, 8, 9, 10};

        public static void LambdaBeauty()
        {
            Console.WriteLine(
                numbers
                    .Where(IsGT3)
                    .Where(IsEven)
                    .Select(DoubleItAndSumFive)
                    .First()
            );
        }

        public static void LambdaUgly()
        {
            Console.WriteLine(
                numbers
                    .Where(number => number % 2 == 0)
                    .Where(number =>
                    {
                        Console.WriteLine("IsGT3 called for " + number);
                        return number > 3;
                    })
                    .Select(number =>
                    {
                        var temp = number * 2;
                        return temp + 5;
                    })
                    .First()
            );
        }

        public static bool IsEven(int number)
        {
            Console.WriteLine("isEven called for " + number);
            return number % 2 == 0;
        }

        public static bool IsGT3(int number)
        {
            Console.WriteLine("IsGT3 called for " + number);
            return number > 3;
        }

        public static int DoubleItAndSumFive(int number)
        {
            Console.WriteLine("DoubleIt called for " + number);
            var temp = number * 2;
            return temp + 5;
        }
    }
}