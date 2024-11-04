namespace Kurzy.Models
{
    public class CourseStudent
    {
        public int StudentId { get; set; }
        public User Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

    }
}
