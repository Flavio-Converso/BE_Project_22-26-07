using Project.Models.Auth;

namespace Project.Services.Auth
{
    public interface IAuthService
    {
        Utente Login(string username, string password);
        Utente Register(string username, string password);
    }
}
