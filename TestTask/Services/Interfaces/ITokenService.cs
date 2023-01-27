using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
