namespace AppConsoleCY.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
