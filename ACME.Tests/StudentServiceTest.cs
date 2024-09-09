using ACME.Data.Repositories;
using ACME.Domain.Models;
using ACME.Services;
using NSubstitute;

namespace ACME.Tests
{
    public class StudentServiceTest
    {
        [Fact]
        public void RegisterStudent_ShouldAddStudent()
        {
            // Arrange
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = new StudentService(studentRepository);

            var studentName = "Marcos";
            var studentAge = 19;

            // Act
            studentService.RegisterStudent(studentName, studentAge);

            // Assert
            studentRepository.Received(1).AddStudent(Arg.Is<Student>(s =>
                s.Name == studentName &&
                s.Age == studentAge));
        }

        [Fact]
        public void RegisterStudent_NotAdult_ShouldRejectOperation()
        {
            // Arrange
            var studentRepository = Substitute.For<IStudentRepository>();
            var studentService = new StudentService(studentRepository);

            var studentName = "Francisco";
            var studentAge = 15;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                studentService.RegisterStudent(studentName, studentAge));
        }
    }
}
