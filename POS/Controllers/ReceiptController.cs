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
    public class ReceiptController : ControllerBase
    {
        private IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        public ActionResult<int> CreateReceipt()
        {
            return _receiptService.Create();
        }

        public ActionResult<bool> Add([FromBody] ReceiptItem receiptItem)
        {
            return _receiptService.Add(receiptItem);
        }

        public ActionResult<bool> Remove(int receiptItemId)
        {
            return _receiptService.Remove(receiptItemId);
        }

        public ActionResult<bool> Delete(int receiptId)
        {
            return _receiptService.Delete(receiptId);
        }
    }
}