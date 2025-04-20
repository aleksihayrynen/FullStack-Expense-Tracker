using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ExpenseTracker.Models
{
    public class SummaryItem :DB_SaveableObject
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; } // "Expense" or "Income"
        public bool IsBeingEdited { get; set; } = false;


        public Expense ToExpense()
        {
            if (Type == "Expense")
            {
                return new Expense
                {
                    _id = this._id,
                    Title = this.Title,
                    Description = this.Description,
                    Amount = this.Amount,
                    Currency = this.Currency,
                    Category = this.Category,
                    Date = this.Date,
                    UserId = this.UserId
                };
            }

            throw new InvalidOperationException("This item is not an Expense.");
        }

        public Income ToIncome()
        {
            if (Type == "Income")
            {
                return new Income
                {
                    _id = this._id,
                    Title = this.Title,
                    Description = this.Description,
                    Amount = this.Amount,
                    Currency = this.Currency,
                    Category = this.Category,
                    Date = this.Date,
                    UserId = this.UserId
                };
            }

            throw new InvalidOperationException("This item is not an Income.");
        }
    }
}
