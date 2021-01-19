using CookbookAPI.Models;
using System.Collections.Generic;

namespace CookbookAPI.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(string id);
        AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest model);
        CreateNewUserResponse Create(CreateNewUserRequest request);
    }
}
