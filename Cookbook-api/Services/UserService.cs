using Cookbook_api.Models;
using Cookbook_api.Utilities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cookbook_api.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<UserService> _logger;
        private readonly GoogleTokenValidator _googleTokenValidator;

        public UserService(ICookbookDatabaseSettings settings, 
            JwtTokenGenerator jwtTokenGenerator, 
            ILogger<UserService> logger,
            GoogleTokenValidator googleTokenValidator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
            _googleTokenValidator = googleTokenValidator;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.Find(x => x.Username == model.Username && 
                                   x.Password == model.Password &&
                                   x.AccountType == AccountType.Internal).FirstOrDefault();

            if (user == null) return null;

            var token = _jwtTokenGenerator.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest request)
        {
            var result = _googleTokenValidator.ValidateGoogleToken(request)?.Result;

            if (result == null)
            {
                return null;
            }

            var user = _users.Find(x => x.Username == result.Email).FirstOrDefault();

            var userResult =  user ?? CreateFromGoogleInfo(result);

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
                Username = result.Email
            };
           _users.InsertOne(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _users.Find(new BsonDocument()).ToListAsync();
        }

        public User GetById(string id)
        {
            return _users.Find(x => x.ID == id).FirstOrDefault();
        }
    }
}
