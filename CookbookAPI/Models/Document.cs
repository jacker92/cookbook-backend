using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookbookAPI.Models
{
    public abstract class Document : IDocument
    {
        public ObjectId ID { get; set; }

        public DateTime CreatedAt => ID.CreationTime;
    }
}
