using qwikhr.Models;

namespace qwikhr.Interfaces
{
    public interface ITokenService
    {
        String CreateToken(User user);
    }
}