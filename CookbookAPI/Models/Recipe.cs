using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
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

        [Required]
        public IList<RecipeIncredient> Ingredients { get; set; }

        public string URL { get; set; }
        public User Author { get; set; }
    }
}
