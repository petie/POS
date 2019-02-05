using POS.Models;

namespace POS.Services
{
    public interface IShiftService
    {
        Shift Create();
        Shift Start(decimal paymentAmount, int shiftId);
        Shift End();
        Shift GetCurrent();
    }
}
