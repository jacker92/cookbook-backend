using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CookbookAPI.Models
{
    [BsonCollection("users")]
    public class User : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public AccountType AccountType { get; set; }

        public string Password { get; set; }
    }
}
