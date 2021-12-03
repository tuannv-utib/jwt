using JwtAuthentication.Models;
using System.Threading.Tasks;

namespace JwtAuthentication.Repositories.Interface
{
    public interface IAuthenticationRepository
    {
        Task<string> GennerateJwtToken(UserModel model);
    }
}
