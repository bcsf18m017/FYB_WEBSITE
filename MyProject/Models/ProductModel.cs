using System;
using System.Collections.Generic;

namespace MyProject.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Image { get; set; }
        public int Price { get; set; }
        public string CategoryId { get; set; }
        public int Percentage { get; set; }
        public bool Daily { get; set; }
        public bool Monthly { get; set; }
        public String CreatedBy { get; set; }
        public int MinimumInstallments { get; set; }
    }
}
