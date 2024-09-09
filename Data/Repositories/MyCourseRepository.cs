using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Domain.Models;

namespace ACME.Data.Repositories
{
    public class MyCourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses = [];

        public IEnumerable<Course> GetCourses() => _courses;

        public void AddCourse(Course course)
        {
            if (!_courses.Contains(course))
                _courses.Add(course);
        }

        public void UpdateCourse(Course modifiedCourse)
        {
            var existingCourse = _courses.SingleOrDefault(c => c.Id == modifiedCourse.Id);

            if (existingCourse != null)
            {
                // Simula la actualización del curso
                _courses.Remove(existingCourse);
                _courses.Add(modifiedCourse);
            }
            else
                throw new InvalidOperationException("Course not found for update.");
        }
    }
}
