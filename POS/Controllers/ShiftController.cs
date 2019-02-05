using Microsoft.AspNetCore.Mvc;
using POS.Services;
using POS.Models;
using System.ComponentModel;

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
        [Description("Create and return a new Shift object")]
        public ActionResult<Shift> Create()
        {
            var shift = shiftService.Create();
            if (shift == null)
            {
                return BadRequest("Zmiana jest już otwarta");
            }
            else
                return shift;
        }
        [HttpPost("start")]
        [Description("Start the provided shift")]
        public ActionResult<Shift> Start([FromBody] ShiftStartPayload shiftStartPayload)
        {
            return shiftService.Start(shiftStartPayload.DepositAmount, shiftStartPayload.ShiftId);
        }
        [HttpPost("close")]
        [Description("End the currently active shift")]
        public ActionResult<Shift> Close()
        {
            return shiftService.End();
        }

        [HttpGet]
        [Description("Returns the active shift")]
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