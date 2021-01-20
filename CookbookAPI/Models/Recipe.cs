using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CookbookAPI.Models
{
    [BsonCollection("recipes")]
    public class Recipe : Document
    {
        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
