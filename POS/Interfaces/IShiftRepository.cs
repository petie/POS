using POS.Models;

namespace POS.Interfaces
{
    public interface IShiftRepository
    {
        Shift GetActive();
        Shift GetLast();
        void Save(Shift shift);
        Shift Get(int shiftId);
    }
}
