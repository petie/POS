using POS.Models;

namespace POS.Interfaces
{
    public interface IShiftService
    {
        Shift Create();
        Shift Start(decimal paymentAmount, int shiftId);
        Shift End();
        Shift GetCurrent();
    }
}
