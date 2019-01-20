using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Interfaces;
using POS.Models;

namespace POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService shiftService;

        public ShiftController(IShiftService shiftService)
        {
            this.shiftService = shiftService;
        }
        public ActionResult<Shift> Create()
        {
            return shiftService.Create();
        }

        public ActionResult<Shift> Start(ShiftStartPayload shiftStartPayload)
        {
            return shiftService.Start(shiftStartPayload.DepositAmount, shiftStartPayload.ShiftId);
        }

        public ActionResult<Shift> Close()
        {
            return shiftService.End();
        }

        public ActionResult<Shift> GetCurrent()
        {
            return shiftService.GetCurrent();
        }
    }
}