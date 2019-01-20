using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
