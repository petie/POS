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
        [HttpPost]
        public ActionResult<Shift> Create()
        {
            return shiftService.Create();
        }
        [HttpPost("start")]
        public ActionResult<Shift> Start(ShiftStartPayload shiftStartPayload)
        {
            return shiftService.Start(shiftStartPayload.DepositAmount, shiftStartPayload.ShiftId);
        }
        [HttpPost("close")]
        public ActionResult<Shift> Close()
        {
            return shiftService.End();
        }

        [HttpGet]
        public ActionResult<Shift> GetCurrent()
        {
            var result = shiftService.GetCurrent();
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}