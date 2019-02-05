using POS.Models;

namespace POS.Services
{
    public interface IFiscalService
    {
        void Print(Receipt receipt);
    }
}
