using CookbookAPI.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CookbookAPI.Models
{
    [BsonCollection("recipes")]
    public class Recipe : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        public string Instructions { get; set; }
    }
}
