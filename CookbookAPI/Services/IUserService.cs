using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using System.Collections.Generic;

namespace CookbookAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
        CreateNewUserResponse Create(CreateNewUserRequest request);
    }
}
