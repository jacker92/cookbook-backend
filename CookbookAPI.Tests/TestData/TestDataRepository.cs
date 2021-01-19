using CookbookAPI.Models;
using CookbookAPI.Utilities;
using MongoDB.Bson;

namespace CookbookAPI.Tests.TestData
{
    public static class TestDataRepository
    {
        public static CreateNewUserRequest GetCreateNewUserRequest()
        {
            return new CreateNewUserRequest
            {
                UserName = "jaakko",
                FirstName = "jaakko",
                LastName = "lahtinen",
                Password = "P@ssw0rd"
            };
        }

        public static CreateNewUserResponse GetCreateNewUserResponse()
        {
            return new CreateNewUserResponse
            {
                User = new User
                {
                    ID = new ObjectId(),
                    UserName = "jaakko",
                    FirstName = "jaakko",
                    LastName = "lahtinen",
                    Password = "P@ssw0rd".Hash()
                }
            };
        }
    }
}
