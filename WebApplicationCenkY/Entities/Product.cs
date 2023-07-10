namespace WebApplicationCenkY.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Desi { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    }

}
