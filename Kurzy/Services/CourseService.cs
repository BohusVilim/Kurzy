using Kurzy.Models;
using Kurzy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Kurzy.Models.DtoModels;

namespace Kurzy.Services
{
    public class CourseService : ICourseService
    {
        private readonly KurzyDbContext _context;
        private readonly IGenericSearchService _genericSearchService;
        public CourseService(KurzyDbContext context, IGenericSearchService genericSearchService) 
        {
            _context = context;
            _context.Database.EnsureCreated();
            _genericSearchService = genericSearchService;
        }

        public List<CourseDto> GetCourses()
        {
            var courses = _context.Courses
                  .Include(c => c.CoursesStudents)
                  .ThenInclude(cs => cs.Student)
                  .ToList();

            var coursesDto = new List<CourseDto>();

            foreach (var course in courses)
            {
                var courseDto = new CourseDto();
                courseDto.Id = course.Id;
                courseDto.Name = course.Name;
                courseDto.Description = course.Description;
                courseDto.Credits = course.Credits;
                courseDto.Students = new List<UserLightDto>();

                foreach (var student in course.CoursesStudents)
                {
                    var studentLightDto = new UserLightDto();
                    studentLightDto.Id = student.StudentId;
                    studentLightDto.FirstName = student.Student.FirstName;
                    studentLightDto.LastName = student.Student.LastName;

                    courseDto.Students.Add(studentLightDto);
                }

                coursesDto.Add(courseDto);
            }

            return coursesDto;
        }

        public CourseDto GetCourse(int id)
        {
            Course? course = _genericSearchService.FindEntity<Course>(id, "course");

            if (course == null)
            {
                throw new KeyNotFoundException("Kurz nebol nájdený.");
            }

            var courseDto = new CourseDto();
            courseDto.Id = course.Id;
            courseDto.Name = course.Name;
            courseDto.Description = course.Description;
            courseDto.Credits = course.Credits;
            courseDto.Students = new List<UserLightDto>();

            foreach(var student in course.CoursesStudents)
            {
                var studentLightDto = new UserLightDto();
                studentLightDto.Id = student.StudentId;
                studentLightDto.FirstName = student.Student.FirstName;
                studentLightDto.LastName = student.Student.LastName;

                courseDto.Students.Add(studentLightDto);
            }

            return courseDto;
        }

        public string AddCourse(Course course)
        {
            var existingCourse = _genericSearchService.FindEntity<Course>(course);

            if (existingCourse != null)
            {
                return "Kurz sa už v databáze nachádza";
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return "Kurz bol úspešne zapísaná do databázy";
        }

        public string PutCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            var existingCourse = _genericSearchService.FindEntity<Course>(course.Id, "course");

            if (existingCourse == null)
            {
                throw new KeyNotFoundException("Kurz nebol nájdený.");
            }

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.Credits = course.Credits;

            _context.SaveChanges();

            return "Kurz bol úspešne aktualizovaný";
        }

        public string DeleteCourse(int id)
        {
            var existingCourse = _genericSearchService.FindEntity<Course>(id, "course");

            if (existingCourse == null)
            {
                throw new KeyNotFoundException("Kurz nebol nájdený.");
            }

            _context.Courses.Remove(existingCourse);
            _context.SaveChanges();

            return "Kurz bol úspešne vymazaný";
        }
    }
}
