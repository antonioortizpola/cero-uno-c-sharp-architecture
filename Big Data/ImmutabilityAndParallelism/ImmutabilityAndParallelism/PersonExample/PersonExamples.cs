using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmutabilityAndParallelism.PersonExample
{
    class PersonExamples
    {
        public static IList<Person> CreatePeople()
        {
            return new List<Person>()
            {
                new Person("Sara", Gender.Female, 20),
                new Person("Sara", Gender.Female, 22),
                new Person("Bob", Gender.Male, 20),
                new Person("Paula", Gender.Female, 32),
                new Person("Paul", Gender.Male, 32),
                new Person("Jack", Gender.Male, 2),
                new Person("Jack", Gender.Male, 72),
                new Person("Jill", Gender.Female, 12),
            };
        }

        public static void ToDictionary()
        {
            var people = CreatePeople();
            // Create a Dictionary with name and age as the key, person as the value
            var result = people
                .ToDictionary(x => x.Name + "-" + x.Age, x => x);
            foreach (var personReg in result)
            {
                Console.WriteLine(personReg.Key + " with " + personReg);
            }
        }

        public static void GroupBy()
        {
            var people = CreatePeople();
            // Create a Dictionary with name as the key, all the persons as value
            var resullt = people
                .GroupBy(x => x.Name);
            foreach (var group in resullt)
            {
                Console.WriteLine(group.Key + " with " + group.Count() + " repeated");
            }
        }
    }
}
