using System.ComponentModel.DataAnnotations;

namespace MoneySmart.API.Models
{
    public class TransactionCreateDto
    {
        [Required(ErrorMessage = " This is required.")]
        public double Amount { get; set; }
        public int SavingAccountId { get; set; }
    }
}