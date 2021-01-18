using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Cookbook_api.Models
{
   
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Instructions { get; set; }
    }
}
