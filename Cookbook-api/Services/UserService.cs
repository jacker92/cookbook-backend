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
        private readonly string GOOGLE_TOKEN_API_BASE = @"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";

        public UserService(ICookbookDatabaseSettings settings, JwtTokenGenerator jwtTokenGenerator, ILogger<UserService> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.Find(x => x.Username == model.Username && 
                                   x.Password == model.Password &&
                                   x.AccountType == AccountType.Internal).Single();

            if (user == null) return null;

            var token = _jwtTokenGenerator.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public AuthenticateResponse AuthenticateWithGoogle(AuthenticateRequest request)
        {
            var result = ValidateGoogleToken(request)?.Result;

            if (result == null)
            {
                return null;
            }

            var user = _users.Find(x => x.Username == result.Email).FirstOrDefault();

            _logger.LogInformation("User", user);
            _logger.LogInformation("User", user ?? Create(result));
            return null;
            //return;
        }

        private User Create(GoogleAuthenticateResponse result)
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

        private async Task<GoogleAuthenticateResponse> ValidateGoogleToken(AuthenticateRequest model)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{GOOGLE_TOKEN_API_BASE}{model.GoogleToken}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GoogleAuthenticateResponse>(content);
                }
            }
            return null;
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
