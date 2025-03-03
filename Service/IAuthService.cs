using System.Security.Claims;
using Models;
using System.Threading.Tasks;

namespace CoWorking.Service;
    public interface IAuthService
    {
        Task<string> Login(LoginDto login);
        Task<string> Register(RegisterDTO register);
        Task<string> GenerateToken(UserDTOOut userDTOOut);
        Task<bool> HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
    }
