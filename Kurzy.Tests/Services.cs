using Kurzy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kurzy.Models.DtoModels;

namespace Kurzy.Tests
{
    internal class Services
    {
        public List<CourseDto> MockCoursesDto() 
        {
            var mockCourses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Math",
                    Description = "Math",
                    Credits = 2,
                    CoursesStudents =  new List<CourseStudent>
                    {
                        new CourseStudent
                        {
                            StudentId = 1,
                            Student = new User
                            {
                                Id = 1,
                                FirstName = "John",
                                LastName = "Doe 1",
                                DateOfBirth = DateTime.Now,
                                PasswordHash = "HashedPassword1",
                                Role = UserRole.Student
                            }
                        },
                        new CourseStudent
                        {
                            StudentId = 2,
                            Student = new User
                            {
                                Id = 1,
                                FirstName = "John",
                                LastName = "Doe 2",
                                DateOfBirth = DateTime.Now,
                                PasswordHash = "HashedPassword2",
                                Role = UserRole.Student
                            }
                        }
                    }
                },
            };

            var mockCoursesDto = new List<CourseDto>();

            foreach (var course in mockCourses)
            {
                var courseDto = new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Credits = course.Credits,
                    Description = course.Description,
                    Students = new List<UserLightDto>(),
                };

                foreach (var courseStudent in course.CoursesStudents)
                {
                    courseDto.Students.Add
                        (
                            new UserLightDto { Id = courseStudent.StudentId, FirstName = courseStudent.Student.FirstName, LastName = courseStudent.Student.LastName }
                        );
                }

                mockCoursesDto.Add(courseDto);
            }

            return mockCoursesDto;
        }
    }
}
