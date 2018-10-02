using System.Collections.Generic;

namespace MoneySmart.API.Models
{
    public class SavingAccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Percentage { get; set; }
        public bool IsPrimary { get; set; }
        public bool OnHold { get; set; }
        public ICollection<TransactionDto> Transactions { get; set; }
    }
}
