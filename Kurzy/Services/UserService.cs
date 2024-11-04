using Kurzy.Models;
using Kurzy.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Kurzy.Models.DtoModels;

namespace Kurzy.Services
{
    public class UserService : IUserService
    {
        private readonly KurzyDbContext _context;
        private readonly IGenericSearchService _genericSearchService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(KurzyDbContext context, IGenericSearchService genericSearchService, IPasswordHasher<User> passwordHasher) 
        {
            _context = context;
            _context.Database.EnsureCreated();
            _genericSearchService = genericSearchService;
            _passwordHasher = passwordHasher;
        }

        public List<UserDto> GetUsers(UserRole role)
        {
            var users = _context.Users
                .Include(c => c.CoursesStudents)
                .ThenInclude(cs => cs.Course).Where(a => a.Role == role)
                .ToList();

            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = new UserDto();
                userDto.Id = user.Id;
                userDto.FirstName = user.FirstName;
                userDto.LastName = user.LastName;
                userDto.Courses = new List<CourseLightDto>();

                foreach( var course in user.CoursesStudents)
                {
                    var courseLightDto = new CourseLightDto();
                    courseLightDto.Id = course.CourseId;
                    courseLightDto.Name = course.Course.Name;
                    courseLightDto.Description = course.Course.Description;
                    courseLightDto.Credits = course.Course.Credits;

                    userDto.Courses.Add(courseLightDto);
                }

                usersDto.Add(userDto);
            }

            return usersDto;
        }

        public UserDto GetUser(int id)
        {
            var user = _genericSearchService.FindEntity<User>(id, "user");

            if (user == null)
            {
                throw new KeyNotFoundException("Používateľ nebol nájdený.");
            }

            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.Courses = new List<CourseLightDto>();

            foreach( var course in user.CoursesStudents)
            {
                var courseLightDto = new CourseLightDto();
                courseLightDto.Id = course.CourseId;
                courseLightDto.Name = course.Course.Name;
                courseLightDto.Description = course.Course.Description;
                courseLightDto.Credits = course.Course.Credits;

                userDto.Courses.Add(courseLightDto);
            }

            return userDto;
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = _genericSearchService.FindEntity<User>(username);

            if (user == null)
            {
                throw new KeyNotFoundException("Používateľ nebol nájdený.");
            }

            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.Courses = new List<CourseLightDto>();

            foreach (var course in user.CoursesStudents)
            {
                var courseLightDto = new CourseLightDto();
                courseLightDto.Id = course.CourseId;
                courseLightDto.Name = course.Course.Name;
                courseLightDto.Description = course.Course.Description;
                courseLightDto.Credits = course.Course.Credits;

                userDto.Courses.Add(courseLightDto);
            }

            return userDto;
        }

        public string AddUser(User user)
        {
            var existinguser = _genericSearchService.FindEntity<User>(user);

            if (existinguser != null)
            {
                return "Používateľ sa už v databáze nachádza";
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            _context.Users.Add(user);
            _context.SaveChanges();

            return "Používateľ bol úspešne zapísaný do databázy";
        }

        public string PutUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userFromDb = _genericSearchService.FindEntity<User>(user.Id, "user");
            if (userFromDb == null)
            {
                throw new KeyNotFoundException("Používateľ nebol nájdený.");
            }

            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.DateOfBirth = user.DateOfBirth;

            _context.SaveChanges();

            return "Údaje používateľa boli úspešne aktualizované";
        }

        public string DeleteUser(int id)
        {
            var user = _genericSearchService.FindEntity<User>(id, "user");
            if (user == null)
            {
            throw new KeyNotFoundException("Používateľ nebol nájdený.");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();

            return "Používateľ bol úspešne vymazaný";
        }
    }
}
