using POS.Models;

namespace POS.Services
{
    public interface IFiscalGateway
    {
        void Print(Receipt receipt);
        void Login();
        void LogOut();
    }
}
