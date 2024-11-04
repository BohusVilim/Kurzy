using Kurzy.Models;
using Kurzy.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kurzy.Services
{
    public class GenericSearchService : IGenericSearchService
    {
        private readonly KurzyDbContext _context;

        public GenericSearchService(KurzyDbContext context)
        {
            _context = context;
        }

        public T? FindEntity<T>(object input, string model = "none") where T : class
        {
            T? result = null;

            if (input is int id)
            {
                if (model == "course")
                {
                    result = _context.Courses
                                     .Include(c => c.CoursesStudents)  
                                     .ThenInclude(cs => cs.Student)    
                                     .FirstOrDefault(c => c.Id == id) as T;
                }

                if (model == "user")
                {
                    result = _context.Users
                                     .Include(s => s.CoursesStudents)  
                                     .ThenInclude(cs => cs.Course)     
                                     .FirstOrDefault(s => s.Id == id) as T;
                }
            }

            else if (input is string userName)
            {
                result = _context.Users
                                     .Include(s => s.CoursesStudents)
                                     .ThenInclude(cs => cs.Course)
                                     .FirstOrDefault(s => s.UserName == userName) as T;
            }

            else
            {
                if (input is Course course)
                {
                    result = _context.Courses
                                     .Include(c => c.CoursesStudents)
                                     .ThenInclude(cs => cs.Student)
                                     .FirstOrDefault(a =>
                                         a.Name == course.Name &&
                                         a.Description == course.Description &&
                                         a.Credits == course.Credits) as T;
                }

                if (input is User user)
                {
                    result = _context.Users
                                     .Include(s => s.CoursesStudents)
                                     .ThenInclude(cs => cs.Course)
                                     .FirstOrDefault(a =>
                                         a.FirstName == user.FirstName &&
                                         a.LastName == user.LastName &&
                                         a.DateOfBirth == user.DateOfBirth) as T;
                }
            }

            return result;
        }
    }
}