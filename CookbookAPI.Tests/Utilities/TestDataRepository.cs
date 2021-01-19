using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Utilities;
using MongoDB.Bson;

namespace CookbookAPI.Tests.TestData
{
    public static class TestDataRepository
    {
        public static CreateNewUserRequest BuildCreateNewUserRequest()
        {
            return new CreateNewUserRequest
            {
                UserName = "jaakko",
                FirstName = "jaakko",
                LastName = "lahtinen",
                Password = "P@ssw0rd"
            };
        }

        public static CreateNewUserResponse BuildCreateNewUserResponse()
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

        public static AuthenticateRequest BuildAuthenticateRequest()
        {
            return new AuthenticateRequest
            {
                Username = "test",
                Password = "test",
                GoogleToken = "test"
            };
        }

        public static User BuildUser()
        {
            return new User
            {
                ID = new ObjectId(),
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                AccountType = AccountType.Internal
            };
        }
    }
}
