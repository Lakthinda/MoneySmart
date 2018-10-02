using System;

namespace MoneySmart.API.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int TransactionType { get; set; } // 0 - split, 1- Ad-hoc
        public DateTime CreatedDateTime { get; set; }
    }
}