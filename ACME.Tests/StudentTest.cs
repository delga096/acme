using ACME.Domain.Models;

namespace ACME.Tests
{
    public class StudentTest
    {
        [Fact]
        public void RegisterStudent_ShouldCreateStudentSuccessfully()
        {
            // Arrange
            var name = "Lucía";
            var age = 24;

            // Act
            var student = new Student(name, age);

            // Assert
            Assert.Equal(name, student.Name);
            Assert.Equal(age, student.Age);
        }
    }
}
