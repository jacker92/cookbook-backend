using CookbookAPI.Models;
using CookbookAPI.Models.Requests;
using CookbookAPI.Models.Responses;
using CookbookAPI.Repositories;
using CookbookAPI.Utilities;
using System.Linq;

namespace CookbookAPI.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly GoogleTokenValidator _googleTokenValidator;
        private readonly IMongoRepository<User> _users;
        public LoginService(JwtTokenGenerator jwtTokenGenerator, GoogleTokenValidator googleTokenValidator, IMongoRepository<User> users)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _googleTokenValidator = googleTokenValidator;
            _users = users;
        }

        public AuthenticateResponse Authenticate(BasicAuthenticateRequest model)
        {
            var user = _users.FilterBy(x => x.UserName.Equals(model.UserName) &&
                                       x.AccountType == AccountType.Internal).FirstOrDefault();

            if (user == null ||
                !SecurePasswordHasher.Verify(model.Password, user.Password)) return null;

            var token = _jwtTokenGenerator.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public AuthenticateResponse AuthenticateWithGoogle(GoogleAuthenticateRequest request)
        {
            var result = _googleTokenValidator.ValidateGoogleToken(request)?.Result;

            if (result == null)
            {
                return null;
            }

            var user = _users.FilterBy(x => x.UserName == result.Email).FirstOrDefault();

            var userResult = user ?? CreateFromGoogleInfo(result);

            var token = _jwtTokenGenerator.GenerateJwtToken(userResult);

            return new AuthenticateResponse(userResult, token);
        }

        private User CreateFromGoogleInfo(GoogleAuthenticateResponse result)
        {
            var user = new User
            {
                AccountType = AccountType.Google,
                FirstName = result.Given_Name,
                LastName = result.Family_Name,
                UserName = result.Email
            };
            _users.InsertOne(user);
            return user;
        }
    }
}
