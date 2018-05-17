using System;
using System.Collections.Generic;
using System.Text;

namespace ImmutabilityAndParallelism.PersonExample
{
    enum Gender
    {
        Male, Female
    }

    class Person
    {
        public string Name { get; }
        public Gender Gender { get; }
        public int Age { get; }

        public Person(string name, Gender gender, int age)
        {
            Name = name;
            Gender = gender;
            Age = age;
        }
    }
}
