namespace POS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public Tax Tax { get; set; }
        public int TaxId { get; set; }
    }
}
