using POS.Services;
using POS.Models;
using System.Linq;

namespace POS.DataAccess
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly PosDbContext context;

        public ShiftRepository(PosDbContext context)
        {
            this.context = context;
        }

        public Shift Get(int shiftId)
        {
            return context.Shifts.Find(shiftId);
        }

        public Shift GetActive()
        {
            return context.Shifts.SingleOrDefault(c => !c.IsClosed);
        }

        public Shift GetLast()
        {
            return context.Shifts.LastOrDefault();
        }

        public void Save(Shift shift)
        {
            context.Add(shift);
            context.SaveChanges();
        }

        public void Update(Shift shift)
        {
            context.Update(shift);
            context.SaveChanges();
        }
    }
}
