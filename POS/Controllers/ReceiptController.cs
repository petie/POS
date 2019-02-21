using Microsoft.AspNetCore.Mvc;
using POS.Services;
using POS.Models;
using System.ComponentModel;

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
        [Description("Start a new receipt and return its Id")]
        public ActionResult<Receipt> CreateReceipt()
        {
            return _receiptService.Create();
        }

        [HttpGet]
        [Description("Start a new receipt and return its Id")]
        public ActionResult<Receipt> GetReceipt()
        {
            return _receiptService.GetCurrentReceipt();
        }

        [HttpPost("{ean}")]
        [Description("Add a new line item by EAN code")]
        public ActionResult<Receipt> Add(string ean)
        {
            return _receiptService.Add(ean);
        }

        [HttpDelete("{receiptId}/{receiptItemId}")]
        [Description("Remove a receipt item by it's Id from a given receipt")]
        public ActionResult<Receipt> Remove(int receiptId, int receiptItemId)
        {
            return _receiptService.Remove(receiptId, receiptItemId);
        }

        [HttpDelete("{receiptId}")]
        [Description("Delete the whole receipt")]
        public ActionResult<Receipt> Delete(int receiptId)
        {
            return _receiptService.Delete(receiptId);
        }

        [HttpPost("change")]
        [Description("Change the quantity of a given item on the receipt")]
        public ActionResult<Receipt> ChangeQuantity([FromBody] ChangeQuantityRequest request)
        {
            return _receiptService.ChangeQuantity(request.Id, request.Quantity);
        }
    }
}