using POS.Models;

namespace POS.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository shiftRepository;
        private readonly IFiscalGateway fiscalGateway;

        public ShiftService(IShiftRepository shiftRepository, IFiscalGateway fiscalGateway)
        {
            this.shiftRepository = shiftRepository;
            this.fiscalGateway = fiscalGateway;
        }

        public Shift Create()
        {
            Shift activeShift = shiftRepository.GetActive();
            if (activeShift == null)
            {
                Shift lastShift = shiftRepository.GetLast();
                Shift shift = new Shift(lastShift?.EndMoney ?? 0);
                shiftRepository.Save(shift);
                return shift;
            }
            return null;
        }

        public Shift End()
        {
            Shift active = shiftRepository.GetActive();
            active.Close();
            shiftRepository.Update(active);
            return active;
        }

        public virtual Shift GetCurrent()
        {
            return shiftRepository.GetActive();
        }

        public Shift Start(decimal paymentAmount, int shiftId)
        {
            Shift shift = shiftRepository.Get(shiftId);
            shift.Start(paymentAmount);
            shiftRepository.Update(shift);
            fiscalGateway.Login();
            return shift;
        }
    }
}
