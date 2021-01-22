using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CookbookAPI.Models
{
    public abstract class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId ID { get; set; }

        public DateTime CreatedAt => ID.CreationTime;
    }
}
