using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Data.Repositories;
using ACME.Domain.Models;

namespace ACME.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void RegisterStudent(string name, int age)
        {
            // Validación de la edad: lógica de negocio
            if (age < 18)
                throw new InvalidOperationException("Only adults can register.");

            var student = new Student(name, age);
            _studentRepository.AddStudent(student);
        }

    }
}
