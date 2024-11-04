namespace Kurzy.Models
{
    public class DtoModels
    {
        public class UserDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public List<CourseLightDto> Courses { get; set; } = new List<CourseLightDto>();
        }

        public class UserLightDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
        }

        public class CourseDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public int Credits { get; set; }
            public List<UserLightDto> Students { get; set; } = new List<UserLightDto>();
        }

        public class CourseLightDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public int Credits { get; set; }
        }

    }
}
