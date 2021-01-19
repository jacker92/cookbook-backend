using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CookbookAPI.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId ID { get; set; }
        DateTime CreatedAt { get; }
    }
}
