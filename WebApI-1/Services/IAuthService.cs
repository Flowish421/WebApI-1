using WebApI_1.DTOs;

namespace WebApI_1.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
    }
}
