using POS.Models;

namespace POS.Interfaces
{
    public interface IFiscalGateway
    {
        void Print(Receipt receipt);
    }
}
