using Kurzy.Models;
using static Kurzy.Models.DtoModels;

namespace Kurzy.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetUsers(UserRole role);
        UserDto GetUser(int id);
        UserDto GetUserByUsername(string username);
        string AddUser(User student);
        string PutUser(User student);
        string DeleteUser(int id);
    }
}
