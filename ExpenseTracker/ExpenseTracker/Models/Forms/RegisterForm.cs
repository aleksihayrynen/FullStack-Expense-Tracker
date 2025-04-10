using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models.Forms
{
    public class RegisterForm
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [DisplayName("Username")]
        public required string name { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public required string password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public required string email { get; set; }

        public bool Register { get; set; }

    }
}
