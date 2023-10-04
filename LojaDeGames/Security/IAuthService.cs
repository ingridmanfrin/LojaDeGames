using LojaDeGames.Model;

namespace LojaDeGames.Security
{
    public interface IAuthService
    {
        Task<UserLogin?> Autenticar(UserLogin userLogin);
    }
}
