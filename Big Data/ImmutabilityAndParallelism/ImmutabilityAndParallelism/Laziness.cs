using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutabilityAndParallelism
{
    class Laziness
    {

        private static IList<int> numbers = new List<int>() { 1, 2, 3, 5, 4, 6, 7, 8, 9, 10};

        public static void CheckLaziness()
        {
            Laziness.Imperative();
            // No executa la función en una colección de datos,
            // ejecuta una colección de funciones en cada
            // pieza de datos, haciendo tanto como sea necesario
            // Contrario a ruby, groovy, javascript, y otros
            Laziness.Functional();
        }
        
        // 8 unidades de trabajo
        public static void Imperative()
        {
            int result = 0;
            foreach (var number in numbers)
            {
                if (IsGT3(number) && IsEven(number))
                {
                    result = DoubleIt(number);
                    break;
                }
            }
            Console.WriteLine(result);
        }

        public static void Functional()
        {
            Console.WriteLine(
                numbers
                    .Where(IsGT3)
                    .Where(IsEven)
                    .Select(DoubleIt)
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

        public static int DoubleIt(int number)
        {
            Console.WriteLine("DoubleIt called for " + number);
            return number * 2;
        }
    }
}
