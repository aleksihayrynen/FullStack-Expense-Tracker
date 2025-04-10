using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseTracker.Models
{
    public class DB_SaveableObject
    {
        [BsonId]
        public ObjectId _id { get; set; }
    }
}
