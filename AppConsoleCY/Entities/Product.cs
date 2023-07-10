﻿namespace AppConsoleCY.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public string Product_code { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Desi { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    }

}
