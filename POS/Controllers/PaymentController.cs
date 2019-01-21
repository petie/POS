using Microsoft.AspNetCore.Mvc;
using POS.Interfaces;
using POS.Models;

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
        public ActionResult<PaymentInfo> Get(int receiptId)
        {
            var result = _paymentService.Get(receiptId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        public ActionResult<PaymentInfo> Create()
        {
            return Ok(_paymentService.Create());
        }

        [HttpPost("pay")]
        public ActionResult<PaymentInfo> PayAmount([FromBody] PaymentPayload payment)
        {
            return Ok(_paymentService.PayAmount(payment));
        }
    }
}