using POS.Models;

namespace POS.Services
{
    public interface IShiftRepository
    {
        Shift GetActive();
        Shift GetLast();
        void Save(Shift shift);
        Shift Get(int shiftId);
        void Update(Shift shift);
    }
}
