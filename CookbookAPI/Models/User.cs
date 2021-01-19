using CookbookAPI.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CookbookAPI.Models
{
    [BsonCollection("users")]
    public class User : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public AccountType AccountType { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
