using POS.Exceptions;
using POS.Models;
using System;

namespace POS.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository receiptRepository;
        private readonly IProductService productService;
        private readonly IShiftService shiftService;

        public ReceiptService(IReceiptRepository receiptRepository, IProductService productService, IShiftService shiftService)
        {
            this.receiptRepository = receiptRepository;
            this.productService = productService;
            this.shiftService = shiftService;
        }

        public ReceiptItem Add(string ean)
        {
            if (string.IsNullOrEmpty(ean))
                throw new ArgumentNullException("Brak numeru EAN");
            Receipt receipt = GetCurrentReceipt();
            Product product = productService.Get(ean);
            ReceiptItem receiptItem = receipt.AddItem(product);
            receiptRepository.Save(receipt);
            return receiptItem;
        }

        private void EnsureCurrentReceiptDoesNotExist()
        {
            var currentReceipt = receiptRepository.GetCurrent();
            if (currentReceipt != null)
                throw new ActiveReceiptAlreadyExistsException(currentReceipt.Id);
        }

        private Shift GetCurrentShift()
        {
            var shift = shiftService.GetCurrent();
            if (shift == null)
                throw new NoActiveShiftException();
            return shift;
        }

        private void EnsureShiftExists()
        {
            var shift = shiftService.GetCurrent();
            if (shift == null)
                throw new NoActiveShiftException();
        }

        public virtual ReceiptItem ChangeQuantity(int receiptItemId, decimal quantity)
        {
            Receipt receipt = GetCurrentReceipt();
            ReceiptItem result = receipt.ChangeQuantity(receiptItemId, quantity);
            receiptRepository.Save(receipt);
            return result;
        }

        public int Create()
        {
            var shift = GetCurrentShift();
            EnsureCurrentReceiptDoesNotExist();
            var receipt = new Receipt(shift);
            return receiptRepository.Create(receipt);
        }

        public bool Delete(int receiptId)
        {
            Receipt receipt = receiptRepository.Get(receiptId);
            return receipt.Cancel() && receiptRepository.Save(receipt);
        }

        public PaymentInfo GetByReceiptId(int receiptId)
        {
            return receiptRepository.Get(receiptId).Payment;
        }

        public Receipt GetCurrentReceipt()
        {
            EnsureShiftExists();
            var currentReceipt = receiptRepository.GetCurrent();
            if (currentReceipt == null)
                throw new NoActiveReceiptException();
            return currentReceipt;
        }

        public bool Remove(int receiptId, int receiptItemId)
        {

            Receipt receipt = receiptRepository.Get(receiptId);
            return receipt.RemoveItem(receiptItemId) && receiptRepository.Save(receipt);
        }
    }
}
