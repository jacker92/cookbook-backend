using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;

namespace CookbookAPI.Services
{
    public interface ILoginService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest model);
    }
}
