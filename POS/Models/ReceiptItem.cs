using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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
            ReceiptId = receipt.Id;
            Receipt = receipt;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            OrdinalNumber = receipt.GetNextOrdinalNumber();
            Quantity = 1;
            //Recalculate();
        }

        public void ChangeQuantity(decimal quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException("Ilość nie może być ujemna", (Exception)null);
            if (Unit.ToLower().StartsWith("szt") && quantity % 1 != 0)
                throw new ArgumentOutOfRangeException("Ilość nie może być ułamkiem dla jednostki szt.", (Exception)null);
            Quantity = quantity;
            //Recalculate();
        }
        //private void Recalculate()
        //{
        //    //Value = Price * Quantity;
        //    //TaxValue = TaxRate / 100 * Value;
        //}

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
        [NotMapped]
        public string Ean => Product?.Ean;
        [NotMapped]
        public string Name => Product?.Name;
        [JsonIgnore]
        public Product Product { get; set; }
        [NotMapped]
        [JsonIgnore]
        public decimal Price => Product?.Price ?? 0;
        [JsonProperty("price")]
        public string PriceString => Price.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
        [NotMapped]
        public string Unit => Product?.Unit;
        public int ProductId { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal Quantity { get; set; }
        [NotMapped]
        [JsonIgnore]
        public decimal Value => Math.Round(Quantity * Price,2);
        [JsonProperty("value")]
        public string ValueString => Value.ToString("C", CultureInfo.CreateSpecificCulture("pl-PL"));
        public bool IsRemoved { get; set; }
        [JsonIgnore]
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [NotMapped]
        public decimal TaxValue => TaxRate / 100 * Value;
        [NotMapped]
        public decimal TaxRate => Product?.Tax?.Value ?? 0;
    }
}
