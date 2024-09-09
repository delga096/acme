using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Domain.Models;

namespace ACME.Data.Repositories
{
    public class MyStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = [];

        public IEnumerable<Student> GetStudents() => _students;

        public void AddStudent(Student student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
        }
    }
}
