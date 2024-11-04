namespace Kurzy.Services.Interfaces
{
    public interface ICourseStudentService
    {
        string AddStudentToCourse(int courseId, int studentId);
        string DeleteStudentFromCourse(int courseId, int studentId);
    }
}
