using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Interfaces
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository receiptRepository;
        private readonly IProductService productService;

        public ReceiptService(IReceiptRepository receiptRepository, IProductService productService)
        {
            this.receiptRepository = receiptRepository;
            this.productService = productService;
        }

        public ReceiptItem Add(string ean)
        {
            Receipt receipt = receiptRepository.GetCurrent();
            Product product = productService.Get(ean);
            ReceiptItem receiptItem = receipt.AddItem(product);
            receiptRepository.Save(receipt);
            return receiptItem;
        }

        public ReceiptItem ChangeQuantity(int receiptItemId, decimal quantity)
        {
            Receipt receipt = receiptRepository.GetCurrent();
            ReceiptItem result = receipt.ChangeQuantity(receiptItemId, quantity);
            receiptRepository.Save(receipt);
            return result;
        }

        public int Create()
        {
            return receiptRepository.Create();
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
            return receiptRepository.GetCurrent();
        }

        public bool Remove(int receiptItemId)
        {
            Receipt receipt = receiptRepository.FindByReceiptItemId(receiptItemId);
            return receipt.RemoveItem(receiptItemId) && receiptRepository.Save(receipt);
        }
    }
}
