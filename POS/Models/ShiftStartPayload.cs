namespace POS.Models
{
    public class ShiftStartPayload
    {
        public decimal DepositAmount { get; internal set; }
        public int ShiftId { get; internal set; }
    }
}