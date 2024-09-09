using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Data.Repositories;
using ACME.Domain.Models;

namespace ACME.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void RegisterCourse(string name, decimal registrationFee, DateOnly startDate, DateOnly endDate)
        {
            var course = new Course(name, registrationFee, startDate, endDate);
            _courseRepository.AddCourse(course);
        }

        public IEnumerable<Course> GetCoursesWithEnrolledStudents(DateOnly fromDate, DateOnly toDate)
        {
            return _courseRepository.GetCourses().Where(c => c.StartDate <= toDate && c.EndDate >= fromDate);
        }

    }
}
