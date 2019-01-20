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

        public ActionResult<ReceiptItem> Add(string ean)
        {
            return _receiptService.Add(ean);
        }

        public ActionResult<bool> Remove(int receiptItemId)
        {
            return _receiptService.Remove(receiptItemId);
        }

        public ActionResult<bool> Delete(int receiptId)
        {
            return _receiptService.Delete(receiptId);
        }

        public ActionResult<ReceiptItem> ChangeQuantity([FromBody] ChangeQuantityRequest request)
        {
            return _receiptService.ChangeQuantity(request.Id, request.Quantity);
        }
    }
}