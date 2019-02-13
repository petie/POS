using POS.Models;
using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Tests.Integration.Setup
{
    public class MockFiscalGateway : IFiscalGateway
    {
        public void Login()
        {
        }

        public void LogOut()
        {
            
        }

        public void Print(Receipt receipt)
        {
        }
    }
}
