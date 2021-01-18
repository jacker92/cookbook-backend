using CookbookAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookbookAPI.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        User GetById(string id);
        AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest model);
        CreateNewUserResponse Create(CreateNewUserRequest request);
    }
}
