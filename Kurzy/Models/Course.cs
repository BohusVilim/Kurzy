namespace Kurzy.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Credits { get; set; }

        public List<CourseStudent> CoursesStudents { get; set; } = new List<CourseStudent>();
    }
}
