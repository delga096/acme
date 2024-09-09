using ACME.Domain.Models;
using ACME.Data.Repositories;

namespace ACME.Tests
{
    public class CourseRepositoryTest
    {
        [Fact]
        public void AddCourse_ShouldStoreCourse()
        {
            // Arrange
            var repository = new MyCourseRepository();
            var course = new Course("Math", 100m, new DateOnly(2024, 9, 1), new DateOnly(2024, 11, 30));

            // Act
            repository.AddCourse(course);

            // Assert
            Assert.Contains(course, repository.GetCourses());
        }

        [Fact]
        public void UpdateCourse_ShouldReplaceExistingCourse()
        {
            // Arrange
            var repository = new MyCourseRepository();
            var course = new Course("Math", 100m, new DateOnly(2024, 9, 1), new DateOnly(2024, 10, 31));
            repository.AddCourse(course);
            var student = new Student("Juan", 30);
            course.EnrollStudent(student);

            // Act            
            repository.UpdateCourse(course);

            // Assert
            var updatedCourse = repository.GetCourses().SingleOrDefault(c => c.Id == course.Id);
            Assert.NotNull(updatedCourse);
            Assert.Equal(course.Name, updatedCourse.Name);
            Assert.Equal(course.StartDate, updatedCourse.StartDate);
            Assert.Equal(course.EndDate, updatedCourse.EndDate);
            Assert.Equal(course.RegistrationFee, updatedCourse.RegistrationFee);
            Assert.Equal(course.EnrolledStudents, updatedCourse.EnrolledStudents);

        }

        [Fact]
        public void UpdateCourse_NotFoundCourse_ShouldThrowException()
        {
            // Arrange
            var repository = new MyCourseRepository();
            var course = new Course("Math", 100m, new DateOnly(2024, 9, 1), new DateOnly(2024, 11, 30));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => repository.UpdateCourse(course));
        }

        [Fact]
        public void GetCourses_ShouldReturnAllAddedCourses()
        {
            // Arrange
            var repository = new MyCourseRepository();
            var course1 = new Course("Math", 100m, new DateOnly(2024, 9, 1), new DateOnly(2024, 11, 30));
            var course2 = new Course("Science", 150m, new DateOnly(2024, 10, 1), new DateOnly(2024, 12, 10));

            repository.AddCourse(course1);
            repository.AddCourse(course2);

            // Act
            var courses = repository.GetCourses();

            // Assert
            Assert.Contains(course1, courses);
            Assert.Contains(course2, courses);
            Assert.Equal(2, courses.Count());
        }
    }
}




