using System;
using System.Collections.Generic;

namespace Posnet
{
    public interface IFiscalDriver : IDisposable, IFiscalDriverProperties
    {
        void Open();

        void Close();

        void TestReady();

        void OpenReceipt(string systemNumber, string ean = null, bool printNumberAsBarcode = false);

        void CloseReceipt();

        void CloseReceipt(long total);

        void CancelReceipt();

        void OpenInvoice(int ItemsCount, string InvoiceNumber, string[] CustomerData, string NIP, DateTime PaymentDate, string PaymentName, string CustomerName, string SellerName, string ean = null, bool printNumberAsBarcode = false);

        void CloseInvoice();

        void CloseInvoice(long total);

        void AddFooterLine(int id, string param);

        void AddItem(PosnetReceiptItem item);

        void RemoveItem(PosnetReceiptItem item);

        void AddPayment(PosnetPayment payment);

        void RemovePayment(PosnetPayment payment);

        void PackOperation(PackOperation op, PackItem pack);

        void Login();

        void Logout();

        void FiscalReport();

        void PeriodReport(DateTime start, DateTime end);

        void ShiftReportFromPrinter(string shift, string operatorId, bool reset);

        void ShiftReportFromDatabase(string shift, string operatorId, DateTime dateFrom, DateTime dateTo, List<PosnetPayment> payments, Decimal insertCashValue, Decimal takeoutCashValue, Decimal cashState, Decimal depositInValue, Decimal depositOutValue, int fiscalizedReceiptsCount, int cancelledReceiptsCount, int stornoItemsCount);

        void PrintKP(string documentNr, Decimal value, string operatorId, string contractor);

        void PrintKW(string documentNr, Decimal value, string operatorId, string contractor);

        void OpenDrawer();

        string Name { get; }

        string PrinterId { get; set; }

        string OperatorId { get; set; }

        List<KeyValuePair<VatRate, int>> TaxRates { get; }

        List<PosnetPayment> AvailablePayments { get; }

        bool SupportsPayments { get; }

        List<PosnetProperty> Properties { get; }

        string AvailableValues(string name);

        bool SupportsFeature(DriverIdentifier df);

        void SaveTaxRates(List<KeyValuePair<VatRate, int>> TaxRatesList);

        List<KeyValuePair<VatRate, int>> LoadTaxRates();

        string GetPrinterSerialNum();

        int GetLastInvoiceNum();

        int GetLastRecepitNum();

        int GetLastFiscalReportNum();

        DateTime GetTime();

        void OpenNonFiscalPrint(int printId, int headerId, string additionaDescription);

        void NonFiscalLinePrint(int lineId, string lineData);

        void CloseNonFiscalPrint();

        string KeyServer { get; set; }

        void OpenRefundTransaction(string systemNumber);

        void LogoutERP();

        bool CommunicationLog { get; set; }

        void Setup(PosnetSettings settings);
    }
}
