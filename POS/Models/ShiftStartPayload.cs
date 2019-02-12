namespace POS.Models
{
    public class ShiftStartPayload
    {
        public ShiftStartPayload()
        {

        }

        public ShiftStartPayload(int shiftId, decimal depositAmount)
        {
            ShiftId = shiftId;
            DepositAmount = depositAmount;
        }
        public decimal DepositAmount { get; set; }
        public int ShiftId { get; set; }
    }
}