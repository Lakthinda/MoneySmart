using System;

namespace MoneySmart.API.Models
{
    public class TransactionCreateDto
    {        
        public double Amount { get; set; }
        public int SavingAccountId { get; set; }
    }
}