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
            Product = product;
            ProductId = product.Id;
            ReceiptId = receipt.Id;
            Receipt = receipt;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            OrdinalNumber = receipt.GetNextOrdinalNumber();
            Quantity = 1;
           
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
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value => Quantity * Product.Price;
        public bool IsRemoved { get; set; }
        [JsonIgnore]
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public decimal TaxValue => Product.Tax.Value / 100 * Value;
    }
}
