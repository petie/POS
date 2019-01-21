namespace Posnet
{
    public interface IFiscalDriverProperties
    {
        bool PrintSystemNumber { get; set; }

        bool PrintPayments { get; set; }

        bool PrintPack { get; set; }

        bool PrintOperatorId { get; set; }

        bool DisplayInfo { get; set; }
    }
}
