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

        public Receipt Add(string ean)
        {
            if (string.IsNullOrEmpty(ean))
                throw new ArgumentNullException("Brak numeru EAN");
            Receipt receipt;
            try
            {
                receipt = GetCurrentReceipt();
            }
            catch (NoActiveReceiptException ea)
            {
                receipt = Create();
            }
            Product product = productService.Get(ean);
            ReceiptItem receiptItem = receipt.AddItem(product);
            receiptRepository.Create(receiptItem);
            return receipt;
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

        public virtual Receipt ChangeQuantity(int receiptItemId, decimal quantity)
        {
            Receipt receipt = GetCurrentReceipt();
            ReceiptItem result = receipt.ChangeQuantity(receiptItemId, quantity);
            receiptRepository.Save(receipt);
            return receipt;
        }

        public Receipt Create()
        {
            var shift = GetCurrentShift();
            EnsureCurrentReceiptDoesNotExist();
            var receipt = new Receipt(shift);
            return receiptRepository.Create(receipt);
        }

        public Receipt Delete(int receiptId)
        {
            Receipt receipt = receiptRepository.Get(receiptId);
            receipt.Cancel();
            receiptRepository.Save(receipt);
            return receipt;
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

        public Receipt Remove(int receiptId, int receiptItemId)
        {

            Receipt receipt = receiptRepository.Get(receiptId);
            receipt.RemoveItem(receiptItemId);
            receiptRepository.Save(receipt);
            return receipt;
        }
    }
}
