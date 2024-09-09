using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Domain.Models;

namespace ACME.Data.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course course);
    }
}
