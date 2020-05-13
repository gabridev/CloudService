using AuthService.API.Application.Models.Authentication;
using System.Threading.Tasks;

namespace AuthService.API.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> Authenticate(string email, string password);

        void ValidateToken(string token);
    }
}
