using POS.Models;

namespace POS.Interfaces
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository shiftRepository;

        public ShiftService(IShiftRepository shiftRepository)
        {
            this.shiftRepository = shiftRepository;
        }

        public Shift Create()
        {
            Shift activeShift = shiftRepository.GetActive();
            if (activeShift == null)
            {
                Shift lastShift = shiftRepository.GetLast();
                Shift shift = new Shift(lastShift.EndMoney);
                shiftRepository.Save(shift);
                return shift;
            }
            return null;
        }

        public Shift End()
        {
            Shift active = shiftRepository.GetActive();
            active.Close();
            shiftRepository.Save(active);
            return active;
        }

        public Shift GetCurrent()
        {
            return shiftRepository.GetActive();
        }

        public Shift Start(decimal paymentAmount, int shiftId)
        {
            Shift shift = shiftRepository.Get(shiftId);
            shift.Start(paymentAmount);
            shiftRepository.Save(shift);
            return shift;
        }
    }
}
