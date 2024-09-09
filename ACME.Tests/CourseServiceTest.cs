using NSubstitute;
using ACME.Services;
using ACME.Domain.Models;
using ACME.Data.Repositories;

namespace ACME.Tests
{
    public class CourseServiceTest
    {
        [Fact]
        public void RegisterCourse_ShouldAddCourse()
        {
            // Arrange
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = new CourseService(courseRepository);

            var courseName = "Math";
            var registrationFee = 100m;
            var startDate = new DateOnly(2024, 9, 1);
            var endDate = new DateOnly(2024, 11, 30);

            // Act
            courseService.RegisterCourse(courseName, registrationFee, startDate, endDate);

            // Assert
            courseRepository.Received(1).AddCourse(Arg.Is<Course>(c =>
                c.Name == courseName &&
                c.RegistrationFee == registrationFee &&
                c.StartDate == startDate &&
                c.EndDate == endDate));
        }

        [Fact]
        public void RegisterCourse_InvalidDates_ShouldThrowException()
        {
            // Arrange
            var courseRepository = Substitute.For<ICourseRepository>();
            var courseService = new CourseService(courseRepository);

            var startDate = new DateOnly(2024, 9, 1);
            var endDate = new DateOnly(2024, 6, 30); // End date before start date

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                courseService.RegisterCourse("Math", 100m, startDate, endDate));
        }
    }
}




