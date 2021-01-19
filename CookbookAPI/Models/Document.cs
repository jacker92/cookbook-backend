using MongoDB.Bson;
using System;

namespace CookbookAPI.Models
{
    public abstract class Document : IDocument
    {
        public ObjectId ID { get; set; }

        public DateTime CreatedAt => ID.CreationTime;
    }
}
