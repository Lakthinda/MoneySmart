﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneySmart.API.Models
{
    public class SavingAccountCreateDto
    {
        [Required(ErrorMessage = " This is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Percentage { get; set; }
        public bool IsPrimary { get; set; }
        public bool OnHold { get; set; }
    }
}
