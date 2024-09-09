using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Domain.Models
{
    public class Course
    {
        public Course(string name, decimal registrationFee, DateOnly startDate, DateOnly endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date must be equal or later than the start date.");

            Id = Guid.NewGuid();
            Name = name;
            RegistrationFee = registrationFee;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public decimal RegistrationFee { get; private set; }

        public DateOnly StartDate { get; private set; }

        public DateOnly EndDate { get; private set; }

        private List<Student> Students = [];

        public IReadOnlyList<Student> EnrolledStudents => Students.AsReadOnly();

        public void EnrollStudent(Student student)
        {
            if (!Students.Contains(student))
            {
                Students.Add(student);
            }
        }

    }
}
