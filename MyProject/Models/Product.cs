using System;
using System.Collections.Generic;

namespace FYP.Models
{
    public class Product
    {
        public short ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Image { get; set; }
        public int Price { get; set; }
        public byte CategoryId { get; set; }
        public int Percentage { get; set; }
        public bool Daily { get; set; }
        public bool Monthly { get; set; }
        public byte CreatedBy { get; set; }
    }
}
