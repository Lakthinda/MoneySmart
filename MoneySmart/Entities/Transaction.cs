using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneySmart.API.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int TransactionType { get; set; } // 0 - split, 1- Ad-hoc
        public DateTime CreatedDateTime { get; set; }
        public double OriginalAmount { get; set; }
        [ForeignKey("SavingAccountId")]
        public SavingAccount SavingAccount { get; set; }
        public int SavingAccountId { get; set; }
    }
}