using POS.Models;

namespace POS.Interfaces
{
    public interface IFiscalService
    {
        void Print(Receipt receipt);
    }
}
