using XYZ.Models;

namespace XYZ.Services.Auth
{
    public interface IAuthService
    {
        Task<object> CredentialsAreVaild(string username, string password);
        string GetToken(string id);

    }
}
