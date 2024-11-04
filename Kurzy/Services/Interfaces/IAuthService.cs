using Kurzy.Models;

namespace Kurzy.Services.Interfaces
{
    public interface IAuthService
    {
        bool IsAuthenticated(LoginRequest loginRequest);
        string GenerateJwt(string username);
    }
}
