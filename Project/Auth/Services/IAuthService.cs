
namespace Project.Auth.Services
{
    public interface IAuthService
    {
        Utente Login(string username, string password);
        Utente Register(string username, string password);
    }
}
