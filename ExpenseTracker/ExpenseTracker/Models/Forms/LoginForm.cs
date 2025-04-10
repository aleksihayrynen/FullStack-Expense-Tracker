using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ExpenseTracker.Models.Forms
{
    public class LoginForm
    {
        [Required]
        [DisplayName("Username")]
        public required string name { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public required string password { get; set; }

    }
}
