using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Data.Repositories;
using ACME.Domain.Models;
using ACME.Infrastructure;

namespace ACME.Services
{
    public class EnrollmentService
    {
        private readonly IPaymentGateway _paymentGateway;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public EnrollmentService(ICourseRepository courseRepo, IStudentRepository studentRepo, IPaymentGateway paymentGateway)
        {
            _courseRepository = courseRepo;
            _studentRepository = studentRepo;
            _paymentGateway = paymentGateway;
        }

        public void EnrollStudentInCourse(Student student, Course course)
        {
            // Validación de existencia: asegura que el estudiante y el curso están en los repositorios
            ValidateStudentAndCourseExistence(student, course);

            // Procesar pago si es aplicable
            ProcessPayment(student, course);

            // Registrar al estudiante en el curso
            course.EnrollStudent(student);
            _courseRepository.UpdateCourse(course);
        }

        private void ValidateStudentAndCourseExistence(Student student, Course course)
        {
            var studentExists = _studentRepository.GetStudents().Contains(student);
            var courseExists = _courseRepository.GetCourses().Contains(course);

            if (!studentExists || !courseExists)
                throw new InvalidOperationException("Student or course not found in the repository.");
        }

        private void ProcessPayment(Student student, Course course)
        {
            if (course.RegistrationFee > 0)
            {
                var paymentSuccessful = _paymentGateway.ProcessPayment(student, course);
                if (!paymentSuccessful)
                    throw new InvalidOperationException("Payment failed.");
            }
        }
    }
}
