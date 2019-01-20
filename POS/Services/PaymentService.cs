using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;

namespace POS.Interfaces
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IReceiptService receiptSerivce;
        private readonly IFiscalService fiscalService;

        public PaymentService(IPaymentRepository paymentRepository, IReceiptService receiptSerivce, IFiscalService fiscalService)
        {
            this.paymentRepository = paymentRepository;
            this.receiptSerivce = receiptSerivce;
            this.fiscalService = fiscalService;
        }
        public PaymentInfo Create()
        {
            Receipt currentReceipt = receiptSerivce.GetCurrentReceipt();
            if (currentReceipt != null && currentReceipt.Total > 0)
            {
                PaymentInfo info = new PaymentInfo(currentReceipt);
                paymentRepository.Save(info);
                return info;
            }
            return null;
        }

        public PaymentInfo Get(int receiptId)
        {
            return receiptSerivce.GetByReceiptId(receiptId);
        }

        public PaymentInfo PayAmount(PaymentPayload payment)
        {
            PaymentInfo info = paymentRepository.GetCurrent();
            if (info.Pay(payment))
            {
                paymentRepository.Save(info);
                fiscalService.Print(info.Receipt);
                return info;
            }
            return null;
        }
    }
}
