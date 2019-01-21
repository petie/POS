﻿using POS.Models;

namespace POS.Interfaces
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
