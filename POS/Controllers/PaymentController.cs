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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }
        public ActionResult<PaymentInfo> Get(int receiptId)
        {
            return Ok(_paymentService.Get(receiptId));
        }

        public ActionResult<PaymentInfo> Create()
        {
            return Ok(_paymentService.Create());
        }

        public ActionResult<PaymentInfo> PayAmount([FromBody] PaymentPayload payment)
        {
            return Ok(_paymentService.PayAmount(payment));
        }
    }
}