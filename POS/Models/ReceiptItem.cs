using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class ReceiptItem
    {
        public ReceiptItem()
        {

        }
        public ReceiptItem(Product product, Receipt receipt)
        {
            if (receipt == null)
                throw new ArgumentNullException(nameof(receipt));
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (product.Tax == null)
                throw new ArgumentNullException(nameof(product.Tax));
            Product = product;
            ProductId = product.Id;
            Ean = product.Ean;
            Name = product.Name;
            ReceiptId = receipt.Id;
            Receipt = receipt;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            OrdinalNumber = receipt.GetNextOrdinalNumber();
            Quantity = 1;
            Price = product.Price;
            TaxRate = product.Tax.Value;
            Unit = product.Unit;
            Recalculate();
        }

        public void ChangeQuantity(decimal quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException("Ilość nie może być ujemna", (Exception)null);
            if (Unit.ToLower().StartsWith("szt") && quantity % 1 != 0)
                throw new ArgumentOutOfRangeException("Ilość nie może być ułamkiem dla jednostki szt.", (Exception)null);
            Quantity = quantity;
            Recalculate();
        }
        private void Recalculate()
        {
            Value = Price * Quantity;
            TaxValue = TaxRate / 100 * Value;
        }

        /// <summary>
        /// Constructor used for testing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <param name="receipt"></param>
        public ReceiptItem(int id, Product product, Receipt receipt) : this(product, receipt)
        {
            Id = id;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrdinalNumber { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public bool IsRemoved { get; set; }
        [JsonIgnore]
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public decimal TaxValue { get; set; }
        public decimal TaxRate { get; set; }
    }
}
