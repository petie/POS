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

        /// <summary>
        /// Constructor used for testing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ean"></param>
        /// <param name="price"></param>
        /// <param name="unit"></param>
        /// <param name="taxId"></param>
        public Product(int id, string ean, decimal price, string unit, int taxId)
        {
            Id = id;
            Ean = ean;
            Price = price;
            Unit = unit;
            TaxId = taxId;
        }

        public Product(string ean, string name, decimal price, string unit, int taxId)
        {
            Ean = ean;
            Name = name;
            Price = price;
            Unit = unit;
            TaxId = taxId;
        }

        public Product()
        {

        }
    }
}
