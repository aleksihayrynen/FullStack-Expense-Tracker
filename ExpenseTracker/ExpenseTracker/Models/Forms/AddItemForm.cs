using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models.Forms;

public class AddItemForm
{
    public string Type { get; set; } = "Expense";

    [MaxLength(300)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.00, 10000000000, ErrorMessage = "Amount must be greater than 0.")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }
    [MaxLength(50)]
    public string? Category { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

}