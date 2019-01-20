using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
