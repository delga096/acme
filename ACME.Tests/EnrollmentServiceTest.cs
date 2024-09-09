using ACME.Data.Repositories;
using ACME.Domain.Models;
using ACME.Infrastructure;
using ACME.Services;
using NSubstitute;

namespace ACME.Tests
{
    public class EnrollmentServiceTest
    {
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;
        private IPaymentGateway _paymentGateway;
        private EnrollmentService _enrollmentService;

        // Método de configuración común para todos los tests
        private void SetUp()
        {
            _studentRepository = Substitute.For<IStudentRepository>();
            _courseRepository = Substitute.For<ICourseRepository>();
            _paymentGateway = Substitute.For<IPaymentGateway>();
            _enrollmentService = new EnrollmentService(_courseRepository, _studentRepository, _paymentGateway);
        }

        // Método auxiliar para crear un estudiante de prueba
        private Student CreateTestStudent(string name = "Jonas", int age = 20)
        {
            return new Student(name, age);
        }

        // Método auxiliar para crear un curso de prueba
        private Course CreateTestCourse(string name = "French", decimal fee = 100m)
        {
            return new Course(name, fee, new DateOnly(2024, 9, 1), new DateOnly(2024, 10, 31));
        }

        [Fact]
        public void EnrollStudentInCourse_PaymentRequired_ShouldRegister()
        {
            // Arrange
            SetUp();
            var student = CreateTestStudent();
            var course = CreateTestCourse();
            _courseRepository.GetCourses().Returns(new List<Course> { course });
            _studentRepository.GetStudents().Returns(new List<Student> { student });
            _paymentGateway.ProcessPayment(student, course).Returns(true);

            // Act
            _enrollmentService.EnrollStudentInCourse(student, course);

            // Assert
            _paymentGateway.Received(1).ProcessPayment(student, course);
            _courseRepository.Received(1).UpdateCourse(Arg.Is<Course>(c =>
                c.Id == course.Id && c.EnrolledStudents.Contains(student)));
        }

        [Fact]
        public void EnrollStudentInCourse_PaymentNotRequired_ShouldRegister()
        {
            // Arrange
            SetUp();
            var student = CreateTestStudent();
            var course = CreateTestCourse(fee: 0m); // Sin tarifa de registro
            _courseRepository.GetCourses().Returns(new List<Course> { course });
            _studentRepository.GetStudents().Returns(new List<Student> { student });

            // Act
            _enrollmentService.EnrollStudentInCourse(student, course);

            // Assert
            _paymentGateway.DidNotReceive().ProcessPayment(student, course);
            _courseRepository.Received(1).UpdateCourse(Arg.Is<Course>(c =>
                c.Id == course.Id && c.EnrolledStudents.Contains(student)));
        }

        [Fact]
        public void EnrollStudentInCourse_PaymentFailed_ShouldThrowException()
        {
            // Arrange
            SetUp();
            var student = CreateTestStudent();
            var course = CreateTestCourse();
            _courseRepository.GetCourses().Returns(new List<Course> { course });
            _studentRepository.GetStudents().Returns(new List<Student> { student });
            _paymentGateway.ProcessPayment(student, course).Returns(false);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _enrollmentService.EnrollStudentInCourse(student, course));
        }

        [Fact]
        public void EnrollStudentInCourse_NotFoundCourse_ShouldThrowException()
        {
            // Arrange
            SetUp();
            var student = CreateTestStudent();
            var course = CreateTestCourse();
            _studentRepository.GetStudents().Returns(new List<Student> { student });
            _courseRepository.GetCourses().Returns(new List<Course>()); // El curso no existe en el repositorio

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _enrollmentService.EnrollStudentInCourse(student, course));
        }

        [Fact]
        public void EnrollStudentInCourse_NotFoundStudent_ShouldThrowException()
        {
            // Arrange
            SetUp();
            var student = CreateTestStudent();
            var course = CreateTestCourse();
            _studentRepository.GetStudents().Returns(new List<Student>()); // El estudiante no existe en el repositorio
            _courseRepository.GetCourses().Returns(new List<Course> { course });

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _enrollmentService.EnrollStudentInCourse(student, course));
        }
    }
}
