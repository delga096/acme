using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Domain.Models
{
    public class Student
    {
        public Student(string name, int age)
        {
            Id = Guid.NewGuid();
            Name = name;
            Age = age;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public int Age { get; private set; }

    }
}
