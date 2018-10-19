using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneySmart.API.Entities
{
    public class SavingAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Percentage { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        [Required]
        public bool OnHold { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}