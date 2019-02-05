using POS.Models;

namespace POS.Services
{
    public class FiscalService : IFiscalService
    {
        private readonly IFiscalGateway fiscalGateway;

        public FiscalService(IFiscalGateway fiscalGateway)
        {
            this.fiscalGateway = fiscalGateway;
        }
        public void Print(Receipt receipt)
        {
            fiscalGateway.Print(receipt);
        }
    }
}
