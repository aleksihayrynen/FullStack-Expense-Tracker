using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Expense : DB_SaveableObject
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public string Currency {  get; set; }

        public string? Category { get; set; }

        public DateTime Date { get; set; }
    }
}
