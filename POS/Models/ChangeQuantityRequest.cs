namespace POS.Models
{
    public class ChangeQuantityRequest
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }

        public ChangeQuantityRequest()
        {

        }
        public ChangeQuantityRequest(int id, decimal quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
