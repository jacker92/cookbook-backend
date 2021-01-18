using Cookbook_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook_api.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        User GetById(string id);
        AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest model);
    }
}
