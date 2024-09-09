using ACME.Data.Repositories;
using ACME.Domain.Models;

namespace ACME.Tests
{
    public class StudentRepositoryTest
    {
        [Fact]
        public void AddStudent_ShouldStoreStudent()
        {
            // Arrange
            var repository = new MyStudentRepository();
            var student = new Student("Carla", 31);

            // Act
            repository.AddStudent(student);

            // Assert
            Assert.Contains(student, repository.GetStudents());
        }

        [Fact]
        public void GetStudents_ShouldReturnAllAddedStudents()
        {
            // Arrange
            var repository = new MyStudentRepository();
            var student1 = new Student("Eric", 23);
            var student2 = new Student("Valeria", 29);

            repository.AddStudent(student1);
            repository.AddStudent(student2);

            // Act
            var students = repository.GetStudents();

            // Assert
            Assert.Contains(student1, students);
            Assert.Contains(student2, students);
            Assert.Equal(2, students.Count());
        }
    }
}
