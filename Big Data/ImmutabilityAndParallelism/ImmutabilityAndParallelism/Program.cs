using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ImmutabilityAndParallelism.Lambda;

namespace ImmutabilityAndParallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            //IncrementMutabilityAndNot();
            //ReferentialTransparency.Example2();
            //Laziness.CheckLaziness();
            //Agregate.AgregateExample();
            //Parallelism.Example1();
            //IEnumerableExample.Example1();
            //LambdaNoRules.Example();
            //LambdaRules.Example();
            //DecoratorPattern.Example();
            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }


        static void IncrementMutabilityAndNot()
        {
            var numbers = Enumerable.Range(1, 10).ToList();

            //Total de numeros pares por 2
            // Requiere más esfuerzo para leer
            // Imperative approach
            var total = 0;
            for (var i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    total += numbers[i] * 2;
                }
            }
            // Total e i son mutables!
            Console.WriteLine(total);

            //Sigue siendo mutable, pero en otra capa
            //en su utilización se ve como inmutable
            var total2 = numbers
                .Where(x => x % 2 == 0)
                .Sum(x => x * 2);
            Console.WriteLine(total2);
        }
    }
}
