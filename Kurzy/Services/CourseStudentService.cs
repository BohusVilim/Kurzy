using Kurzy.Models;
using Kurzy.Services.Interfaces;

namespace Kurzy.Services
{
    public class CourseStudentService : ICourseStudentService
    {
        private readonly KurzyDbContext _context;
        private readonly IGenericSearchService _genericSearchService;
        public CourseStudentService(KurzyDbContext context, IGenericSearchService service)
        {
            _context = context;
            _genericSearchService = service;
        }

        public string AddStudentToCourse(int courseId, int userId)
        {
            var student = _genericSearchService.FindEntity<User>(userId, "user");
            if (student == null)
            {
                throw new KeyNotFoundException("Študent nebol nájdený.");
            }

            if (student.Role != UserRole.Student)
            {
                throw new Exception("Používateľ nie je študentom.");
            }

            var course = _genericSearchService.FindEntity<Course>(courseId, "course");

            if (course == null)
            {
                throw new KeyNotFoundException("Kurz nebol nájdený.");
            }

            if (course.CoursesStudents.Any(a => a.StudentId == userId))
            {
                throw new Exception("Študent sa už v kurze nachádza");
            }

            var courseStudent = new CourseStudent()
            {
                StudentId = userId,
                Student = student,
                CourseId = courseId,
                Course = course
            };

            _context.CoursesStudents.Add(courseStudent);

            if (student.CoursesStudents == null)
            {
                student.CoursesStudents = new List<CourseStudent>();
            }

            if (course.CoursesStudents == null)
            {
                course.CoursesStudents = new List<CourseStudent>();
            }

            student.CoursesStudents.Add(courseStudent);
            course.CoursesStudents.Add(courseStudent);

            _context.SaveChanges();

            return "Študent bol úspešne zaísaný do kurzu";
        }

        public string DeleteStudentFromCourse(int courseId, int userId)
        {
            var student = _genericSearchService.FindEntity<User>(userId, "user");
            if (student == null)
            {
                throw new KeyNotFoundException("Študent nebol nájdený.");
            }

            var course = _genericSearchService.FindEntity<Course>(courseId, "course");

            if (course == null)
            {
                throw new KeyNotFoundException("Kurz nebol nájdený.");
            }

            var courseStudent = _context.CoursesStudents.SingleOrDefault(a => a.CourseId == courseId && a.StudentId == userId);

            if (courseStudent != null)
            {
                _context.CoursesStudents.Remove(courseStudent);
                _context.SaveChanges();
            }

            return "Študent bol úspešne vymazaný z kurzu";
        }
    }
}
