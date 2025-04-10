using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class User : DB_SaveableObject
    {
        
        public required string Username { get; set; }

        public string? Email { get; set; }
        
        public required string Password { get; set; }
    }
}
