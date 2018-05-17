using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace ImmutabilityAndParallelism.Lambda
{
    class LambdaRules
    {
        public static int TotalValues(IList<int> values, Func<int, bool> predicate)
        {
            var result = 0;
            foreach (var value in values)
            {
                if(predicate(value))
                    result += value;
            }

            return result;
        }

        public static void Example()
        {
            var numbers = Enumerable.Range(1, 10).ToList();

            //Total all values
            // Normal function
            //Console.WriteLine(TotalValues(numbers));
            // Higher order function (Can receive and/or returnn functions)
            Console.WriteLine(TotalValues(numbers, x => true));
            //Console.WriteLine(TotalValues(numbers, x => x % 2 == 0)); 
            //Console.WriteLine(TotalValues(numbers, x => x % 2 != 0)); 
            Console.WriteLine(TotalValues(numbers, Util.IsEven));
            Console.WriteLine(TotalValues(numbers, Util.IsOdd));
        }
    }

    class Util
    {
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }
    }

    class LambdaNoRules
    {
        public static int TotalValues(IList<int> values)
        {
            int result = 0;

            foreach (var value in values)
            {
                result += value;
            }

            return result;
        }

        public static int TotalEvenValues(IList<int> values)
        {
            int result = 0;

            foreach (var value in values)
            {
                if (value % 2 == 0)
                    result += value;
            }

            return result;
        }

        public static int TotalOddValues(IList<int> values)
        {
            int result = 0;

            foreach (var value in values)
            {
                if (value % 2 != 0)
                    result += value;
            }

            return result;
        }

        public static void Example()
        {
            var numbers = Enumerable.Range(1, 10).ToList();

            //Total all values
            Console.WriteLine(TotalValues(numbers));

            //Total even values values
            Console.WriteLine(TotalEvenValues(numbers));

            //Total even values values
            Console.WriteLine(TotalOddValues(numbers));
        }
    }
}
