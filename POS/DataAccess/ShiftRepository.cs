using POS.Interfaces;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
