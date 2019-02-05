using Microsoft.AspNetCore.Mvc;
using POS.Services;
using POS.Models;
using System.ComponentModel;

namespace POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet("{receiptId}")]
        [Description("Get payment summary for a given receipt")]
        public ActionResult<PaymentInfo> Get(int receiptId)
        {
            var result = _paymentService.Get(receiptId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        [Description("Create an empty payment object for the current receipt")]
        public ActionResult<PaymentInfo> Create()
        {
            return Ok(_paymentService.Create());
        }

        [HttpPost("pay")]
        [Description("Confirm payment of receipt")]
        public ActionResult<PaymentInfo> PayAmount([FromBody] PaymentPayload payment)
        {
            return Ok(_paymentService.PayAmount(payment));
        }
    }
}