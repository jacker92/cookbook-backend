using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;

namespace CookbookAPI.Services
{
    public interface ILoginService
    {
        AuthenticateResponse Authenticate(BasicAuthenticateRequest request);
        AuthenticateResponse AuthenticateWithGoogle(GoogleAuthenticateRequest request);
    }
}
