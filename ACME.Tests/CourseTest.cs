using ACME.Domain.Models;

namespace ACME.Tests
{
    public class CourseTest
    {
        [Fact]
        public void RegisterCourse_EndDateEarlierThanStartDate_ShouldThrowException()
        {
            // Arrange
            var name = "History";
            var registrationFee = 100m;
            var startDate = DateOnly.FromDateTime(DateTime.Today);
            var endDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)); 

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Course(name, registrationFee, startDate, endDate));
        }

        [Fact]
        public void RegisterCourse_ValidDates_ShouldCreateCourseSuccessfully()
        {
            // Arrange
            var name = "Math";
            var registrationFee = 100m;
            var startDate = DateOnly.FromDateTime(DateTime.Today);
            var endDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));

            // Act
            var course = new Course(name, registrationFee, startDate, endDate);

            // Assert
            Assert.Equal(name, course.Name);
            Assert.Equal(registrationFee, course.RegistrationFee);
            Assert.Equal(startDate, course.StartDate);
            Assert.Equal(endDate, course.EndDate);
        }
    }
}
