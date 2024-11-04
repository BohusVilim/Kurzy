using Kurzy.Models;
using static Kurzy.Models.DtoModels;

namespace Kurzy.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseDto> GetCourses();
        CourseDto GetCourse(int id);
        string AddCourse (Course course);
        string PutCourse (Course course);
        string DeleteCourse (int id);
    }
}
