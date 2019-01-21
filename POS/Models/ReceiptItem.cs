using System;

namespace POS.Models
{
    public class ReceiptItem
    {
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

        public int Id { get; set; }
        public int OrdinalNumber { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value => Quantity * Product.Price;
        public bool IsRemoved { get; set; }
        public Receipt Receipt { get; set; }
        public int ReceiptId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public decimal TaxValue => Product.Tax.Value / 100 * Value;
    }
}
