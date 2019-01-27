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

        [HttpPost]
        public ActionResult<int> CreateReceipt()
        {
            return _receiptService.Create();
        }

        [HttpPost("{ean}")]
        public ActionResult<ReceiptItem> Add(string ean)
        {
            return _receiptService.Add(ean);
        }

        [HttpDelete("receiptItem/{receiptItemId}")]
        public ActionResult<bool> Remove(int receiptId, int receiptItemId)
        {
            return _receiptService.Remove(receiptId, receiptItemId);
        }

        [HttpDelete("{receiptId}")]
        public ActionResult<bool> Delete(int receiptId)
        {
            return _receiptService.Delete(receiptId);
        }

        [HttpPost("change")]
        public ActionResult<ReceiptItem> ChangeQuantity([FromBody] ChangeQuantityRequest request)
        {
            return _receiptService.ChangeQuantity(request.Id, request.Quantity);
        }
    }
}