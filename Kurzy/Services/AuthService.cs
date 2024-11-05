using Kurzy.Models;
using Kurzy.Services.Interfaces;
using Kurzy.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kurzy.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericSearchService _genericSearchService;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthService(IGenericSearchService genericSearchService, IOptions<JwtSettings> jwtSettings, IPasswordHasher<User> passwordHasher) 
        {
            _genericSearchService = genericSearchService;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public bool IsAuthenticated(LoginRequest loginRequest)
        {
            var user = _genericSearchService.FindEntity<User>(loginRequest.UserName);

            if (user == null)
            {
                return false;
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);
            return verificationResult == PasswordVerificationResult.Success;
        }
        public string GenerateJwt(LoginRequest loginRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, loginRequest.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, loginRequest.UserName),
                    new Claim(ClaimTypes.Role, loginRequest.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
