
namespace MoneySmart.API.Models
{
    public class SavingAccountUpdateDto
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public double Percentage { get; set; }
        public bool IsPrimary { get; set; }
        public bool OnHold { get; set; }
    }
}