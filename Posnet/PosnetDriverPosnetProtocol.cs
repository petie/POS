using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;

namespace Posnet
{
    public class PosnetDriverPosnetProtocol : IFiscalDriver, IDisposable, IFiscalDriverProperties
    {
        private static byte STX = 2;
        private static byte EXT = 3;
        private int standardTimeout = 5000;
        private string TAB = new string('\t', 1);
        private List<KeyValuePair<int, string>> FooterLines = new List<KeyValuePair<int, string>>();
        private bool printSystemNumber = true;
        private bool printPayments = true;
        private bool printPack = true;
        private bool printOperatorId = true;
        private PosnetSerialPort port;
        private Dictionary<char, byte> mazoviaCode;
        private int id;
        private string printerId;
        private string operatorId;
        private int lineCounter;
        private long receiptTotal;
        private long packAddTotal;
        private long packReturnTotal;
        private string systemNumber;
        private List<PackItem> packAddList;
        private List<PackItem> packReturnList;
        private Dictionary<int, long> payments;
        private int NonFiscalPrintId;
        private bool communicationLog;
        private readonly PosnetSettings settings;

        public PosnetDriverPosnetProtocol(PosnetSettings settings)
        {
            id = new Random().Next();
            Trace.WriteLine(id.ToString() + " - PosnetDriver");
            Name = settings.Name;
            OperatorId = "";
            mazoviaCode = new Dictionary<char, byte>();
            for (int index = 0; index < sbyte.MaxValue; ++index)
                mazoviaCode.Add((char)index, (byte)index);
            mazoviaCode.Add('ą', 134);
            mazoviaCode.Add('ł', 146);
            mazoviaCode.Add('ś', 158);
            mazoviaCode.Add('ć', 141);
            mazoviaCode.Add('ń', 164);
            mazoviaCode.Add('ź', 166);
            mazoviaCode.Add('ę', 145);
            mazoviaCode.Add('ó', 162);
            mazoviaCode.Add('ż', 167);
            mazoviaCode.Add('Ą', 143);
            mazoviaCode.Add('Ł', 156);
            mazoviaCode.Add('Ś', 152);
            mazoviaCode.Add('Ć', 149);
            mazoviaCode.Add('Ń', 165);
            mazoviaCode.Add('Ź', 160);
            mazoviaCode.Add('Ę', 144);
            mazoviaCode.Add('Ó', 163);
            mazoviaCode.Add('Ż', 161);
            mazoviaCode.Add('ü', 129);
            mazoviaCode.Add('ä', 132);
            mazoviaCode.Add('ë', 137);
            mazoviaCode.Add('ö', 148);
            mazoviaCode.Add('ß', 225);
            mazoviaCode.Add('Ü', 154);
            mazoviaCode.Add('Ä', 142);
            mazoviaCode.Add('Ö', 153);
            this.settings = settings;
        }

        private void AddOptionalCommandParameter(ref string Parameters, string ParameterId, string ParameterValue)
        {
            if (string.IsNullOrEmpty(ParameterValue))
                return;
            AddCommandParameter(ref Parameters, ParameterId, ParameterValue);
        }

        private void AddOptionalCommandParameter(ref string Parameters, string ParameterId, int ParameterValue, int EmptyValue)
        {
            if (ParameterValue == EmptyValue)
                return;
            AddCommandParameter(ref Parameters, ParameterId, ParameterValue.ToString());
        }

        private void AddOptionalCommandParameter(ref string Parameters, string ParameterId, long ParameterValue, long EmptyValue)
        {
            if (ParameterValue == EmptyValue)
                return;
            AddCommandParameter(ref Parameters, ParameterId, ParameterValue.ToString());
        }

        private void AddCommandParameter(ref string Parameters, string ParameterId, string ParameterValue)
        {
            if (string.IsNullOrEmpty(Parameters))
            {
                Parameters = ParameterId + ParameterValue;
            }
            else
            {
                ref string local = ref Parameters;
                local = local + TAB + ParameterId + ParameterValue;
            }
        }

        private string SendFrame(string PrinterCommand, string Parameters)
        {
            try
            {
                string str = !string.IsNullOrEmpty(Parameters) ? PrinterCommand + TAB + Parameters : PrinterCommand;
                Trace.WriteLine("Send Frame = " + str);
                byte[] mazovia = stringToMazovia(str + TAB + "#");
                byte[] numArray = new byte[1]
                {
          STX
                };
                port.Write(numArray, 0, 1);
                port.Write(mazovia, 0, mazovia.Length);
                port.Write(stringToMazovia(CRC16.CalculateCRC(stringToMazovia(str + TAB))), 0, 4);
                numArray[0] = EXT;
                port.Write(numArray, 0, 1);
                return ResponseParameters(PrinterCommand);
            }
            catch (TimeoutException ex)
            {
                string str = "";
                if (port != null && port.PortName != null)
                    str = port.PortName + ". ";
                throw new FiscalException("Wystąpił błąd połączenia z portem " + str + ex.Message);
            }
            catch (IOException ex)
            {
                string str = "";
                if (port != null && port.PortName != null)
                    str = port.PortName + ". ";
                throw new FiscalException("Wystąpił błąd połączenia z portem " + str + ex.Message);
            }
            catch (FiscalException ex)
            {
                byte[] mazovia = stringToMazovia("prncancel" + TAB + "#");
                byte[] numArray = new byte[1]
                {
          STX
                };
                port.Write(numArray, 0, 1);
                port.Write(mazovia, 0, mazovia.Length);
                port.Write(stringToMazovia(CRC16.CalculateCRC(stringToMazovia("prncancel" + TAB))), 0, 4);
                numArray[0] = EXT;
                port.Write(numArray, 0, 1);
                throw;
            }
            catch (Exception ex)
            {
                throw new FiscalException("Wystąpił błąd połączenia z portem. " + ex.ToString() + " " + ex.Message);
            }
        }

        private string ResponseParameters(string PrinterCommand)
        {
            bool NotAsyncResponseOnAsyncCommand1 = false;
            string ResponseFrame1 = "";
            bool flag1 = true;
            bool flag2 = false;
            while (true)
            {
                bool NotAsyncResponseOnAsyncCommand2;
                do
                {
                    int num;
                    do
                    {
                        try
                        {
                            if (flag1)
                            {
                                flag1 = false;
                                ResponseFrame1 = port.ReadTo(new string((char)EXT, 1));
                            }
                        }
                        catch (TimeoutException ex)
                        {
                            flag2 = true;
                        }
                        catch (IOException ex)
                        {
                            flag2 = true;
                        }
                        if (flag2)
                        {
                            byte[] mazovia = stringToMazovia("!sdev" + TAB + "#");
                            byte[] numArray = new byte[1]
                            {
                STX
                            };
                            port.Write(numArray, 0, 1);
                            port.Write(mazovia, 0, mazovia.Length);
                            port.Write(stringToMazovia(CRC16.CalculateCRC(stringToMazovia("!sdev" + TAB))), 0, 4);
                            numArray[0] = EXT;
                            port.Write(numArray, 0, 1);
                            string ResponseFrame2 = port.ReadTo(new string((char)EXT, 1));
                            NotAsyncResponseOnAsyncCommand2 = false;
                            string str1 = ParseResponseFrame("!sdev", ResponseFrame2, ref NotAsyncResponseOnAsyncCommand2);
                            if (NotAsyncResponseOnAsyncCommand2)
                            {
                                ResponseFrame1 = str1;
                                string responseFrame = ParseResponseFrame("!sdev", port.ReadTo(new string((char)EXT, 1)), ref NotAsyncResponseOnAsyncCommand1);
                                if (NotAsyncResponseOnAsyncCommand1)
                                    throw new PosnetException("Błąd podczas wykonywania polecenia: " + PrinterCommand + ". Zbyt długa kolejka ramek.");
                                str1 = responseFrame;
                            }
                            string[] strArray = str1.Split('\t');
                            num = -1;
                            foreach (string str2 in strArray)
                            {
                                if (str2.Length > 2 && str2[0] == 'd' && str2[1] == 's')
                                {
                                    num = int.Parse(str2.Substring(2));
                                    break;
                                }
                            }
                        }
                        else
                            goto label_18;
                    }
                    while (num >= 2);
                    flag2 = false;
                }
                while (NotAsyncResponseOnAsyncCommand2);
                flag1 = true;
            }
            label_18:
            return ParseResponseFrame(PrinterCommand, ResponseFrame1, ref NotAsyncResponseOnAsyncCommand1);
        }

        private string ParseResponseFrame(string PrinterCommand, string ResponseFrame, ref bool NotAsyncResponseOnAsyncCommand)
        {
            NotAsyncResponseOnAsyncCommand = false;
            if (ResponseFrame[0] != STX)
                throw new PosnetException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Ramka odpowiedzi jest błędna, brak sekwencji początkowej STX");
            if (ResponseFrame[ResponseFrame.Length - 5] != '#')
                throw new PosnetException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Ramka odpowiedzi jest błędna, brak znaku # przed sumą CRC");
            if (CRC16.CalculateCRC(stringToMazovia(ResponseFrame.Substring(1, ResponseFrame.Length - 6))) != ResponseFrame.Substring(ResponseFrame.Length - 4, 4))
                throw new PosnetException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Ramka odpowiedzi ma nieprawidłową sumę kontrolną CRC");
            bool flag = false;
            if (PrinterCommand[0] == '!')
            {
                PrinterCommand = PrinterCommand.Substring(1);
                flag = true;
            }
            if (ResponseFrame.Substring(1, PrinterCommand.Length) == PrinterCommand)
            {
                string str1 = ResponseFrame.Substring(1 + PrinterCommand.Length);
                string str2 = str1.Substring(1, str1.Length - 5);
                if (str2[0] == '?')
                {
                    int result = -1;
                    int.TryParse(str2.Substring(1).Split('\t')[0], out result);
                    throw new PosnetException(result, PrinterCommand);
                }
                return str2;
            }
            if (ResponseFrame.Substring(1, 3).ToUpper() == "ERR")
                throw new PosnetException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Drukarka otrzymała błędną ramkę");
            if (!flag)
                throw new PosnetException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Nieznany błąd ramki");
            NotAsyncResponseOnAsyncCommand = true;
            return ResponseFrame;
        }

        private byte[] stringToMazovia(string str)
        {
            return PosnetReceiptItem.stringToMazovia(str);
        }

        public void Open()
        {
            Trace.WriteLine(id.ToString() + " - Open");
            try
            {
                if (port != null)
                {
                    if (port.IsOpen)
                        port.Close();
                    port.Dispose();
                }
                port = new PosnetSerialPort(Name, settings.Port, settings.BaudRate);
                port.ReadTimeout = standardTimeout;
                port.WriteTimeout = standardTimeout;
                port.Handshake = settings.Handshake;
                port.Open();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new FiscalException("Brak dostępu do portu COM. " + ex.Message);
            }
            catch (IOException ex)
            {
                throw new FiscalException("Wystąpił błąd połączenia z portem." + ex.Message);
            }
            catch (FiscalException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FiscalException("Wystąpił błąd połączenia z portem. " + ex.ToString() + " " + ex.Message);
            }
        }

        public void Close()
        {
            Trace.WriteLine(id.ToString() + " - Close");
            if (port != null)
            {
                port.Close();
                port.Dispose();
            }
            port = null;
        }

        public void TestReady()
        {
            string str1 = SendFrame("!sdev", "");
            char[] chArray = new char[1] { '\t' };
            foreach (string str2 in str1.Split(chArray))
            {
                if (str2[0] == 'd' && str2[1] == 's')
                {
                    switch (int.Parse(str2.Substring(2)))
                    {
                        case 0:
                            continue;
                        case 1:
                            throw new PosnetException("Drukarka w tyrbie menu, nie można drukować.");
                        case 2:
                            throw new PosnetException("Drukarka oczekuje na klawisz, nie można drukować.");
                        case 3:
                            throw new PosnetException("Drukarka oczekuje na reakcję użytkownika - wystąpił błąd.");
                        default:
                            throw new PosnetException("Nie rozpoznany stan drukarki");
                    }
                }
            }
        }

        public void OpenReceipt(string systemNumber, string ean = null, bool printNumberAsBarcode = false)
        {
            FooterLines.Clear();
            this.systemNumber = systemNumber;
            if (this.systemNumber.Length > 30)
                this.systemNumber = this.systemNumber.Substring(0, 30);
            lineCounter = 0;
            receiptTotal = 0L;
            packAddTotal = 0L;
            packReturnTotal = 0L;
            packAddList = new List<PackItem>();
            packReturnList = new List<PackItem>();
            payments = new Dictionary<int, long>();
            LoginWithoutPrint();
            if (printNumberAsBarcode)
            {
                string Parameters = "";
                if (ean != null)
                    AddOptionalCommandParameter(ref Parameters, "bc", ean);
                else
                    AddOptionalCommandParameter(ref Parameters, "bc", systemNumber);
                SendFrame("ftrcfg", Parameters);
            }
            string Parameters1 = "";
            AddOptionalCommandParameter(ref Parameters1, "bm", "0");
            SendFrame("trinit", Parameters1);
        }

        public void AddFooterLine(int id, string param)
        {
            FooterLines.Add(new KeyValuePair<int, string>(id, param));
        }

        public void CloseReceipt(long total)
        {
            receiptTotal = total;
            CloseReceipt();
        }

        public void CloseReceipt()
        {
            long ParameterValue1 = 0;
            if (PrintPayments)
            {
                foreach (KeyValuePair<int, long> payment in payments)
                {
                    ParameterValue1 += payment.Value;
                    string Parameters = "";
                    AddCommandParameter(ref Parameters, "ty", payment.Key.ToString());
                    AddCommandParameter(ref Parameters, "wa", payment.Value.ToString());
                    SendFrame("trpayment", Parameters);
                }
            }
            if (PrintPack)
            {
                using (List<PackItem>.Enumerator enumerator = packAddList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        PackItem current = enumerator.Current;
                        string Parameters = "";
                        AddCommandParameter(ref Parameters, "na", current.Number.ToString());
                        AddCommandParameter(ref Parameters, "ne", "0");
                        AddCommandParameter(ref Parameters, "pr", current.Price.ToString());
                        AddCommandParameter(ref Parameters, "il", current.Amount.ToString());
                        SendFrame("trpack", Parameters);
                    }
                }
                using (List<PackItem>.Enumerator enumerator = packReturnList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        PackItem current = enumerator.Current;
                        string Parameters = "";
                        AddCommandParameter(ref Parameters, "na", current.Number.ToString());
                        AddCommandParameter(ref Parameters, "ne", "1");
                        AddCommandParameter(ref Parameters, "pr", current.Price.ToString());
                        AddCommandParameter(ref Parameters, "il", current.Amount.ToString());
                        SendFrame("trpack", Parameters);
                    }
                }
            }
            else
            {
                packAddTotal = 0L;
                packReturnTotal = 0L;
            }
            string Parameters1 = "";
            AddOptionalCommandParameter(ref Parameters1, "to", receiptTotal, 0L);
            AddOptionalCommandParameter(ref Parameters1, "op", packAddTotal, 0L);
            AddOptionalCommandParameter(ref Parameters1, "om", packReturnTotal, 0L);
            AddOptionalCommandParameter(ref Parameters1, "fp", ParameterValue1, 0L);
            string ParameterValue2 = "1";
            bool flag = false;
            if (PrintSystemNumber)
                flag = true;
            if (PrintPack && (packAddList.Count > 0 || packReturnList.Count > 0))
                flag = true;
            if (flag)
                ParameterValue2 = "0";
            AddOptionalCommandParameter(ref Parameters1, "fe", ParameterValue2);
            SendFrame("trend", Parameters1);
            if (!flag)
                return;
            if (PrintPack)
            {
                using (List<PackItem>.Enumerator enumerator = packAddList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        PackItem current = enumerator.Current;
                        string Parameters2 = "";
                        AddCommandParameter(ref Parameters2, "na", current.Number.ToString());
                        AddCommandParameter(ref Parameters2, "ne", "0");
                        AddCommandParameter(ref Parameters2, "pr", current.Price.ToString());
                        AddCommandParameter(ref Parameters2, "il", current.Amount.ToString());
                        SendFrame("trpackprnend", Parameters2);
                    }
                }
                using (List<PackItem>.Enumerator enumerator = packReturnList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        PackItem current = enumerator.Current;
                        string Parameters2 = "";
                        AddCommandParameter(ref Parameters2, "na", current.Number.ToString());
                        AddCommandParameter(ref Parameters2, "ne", "1");
                        AddCommandParameter(ref Parameters2, "pr", current.Price.ToString());
                        AddCommandParameter(ref Parameters2, "il", current.Amount.ToString());
                        SendFrame("trpackprnend", Parameters2);
                    }
                }
            }
            if (PrintSystemNumber)
            {
                string Parameters2 = "";
                AddCommandParameter(ref Parameters2, "id", "30");
                AddCommandParameter(ref Parameters2, "na", systemNumber);
                SendFrame("trftrln", Parameters2);
            }
            if (FooterLines != null && FooterLines.Count > 0)
            {
                foreach (KeyValuePair<int, string> footerLine in FooterLines)
                {
                    string Parameters2 = "";
                    AddCommandParameter(ref Parameters2, "id", footerLine.Key.ToString());
                    string ParameterValue3 = footerLine.Value;
                    if (ParameterValue3.Length > 40)
                        ParameterValue3 = ParameterValue3.Substring(0, 40);
                    AddCommandParameter(ref Parameters2, "na", ParameterValue3);
                    SendFrame("trftrln", Parameters2);
                }
            }
            SendFrame("trftrend", "");
        }

        public void OpenInvoice(int ItemsCount, string InvoiceNumber, string[] CustomerData, string NIP, DateTime PaymentDate, string PaymentName, string CustomerName, string SellerName, string ean = null, bool printNumberAsBarcode = false)
        {
            lineCounter = 0;
            receiptTotal = 0L;
            packAddTotal = 0L;
            packReturnTotal = 0L;
            packAddList = new List<PackItem>();
            packReturnList = new List<PackItem>();
            payments = new Dictionary<int, long>();
            if (InvoiceNumber.Length > 15)
                throw new PosnetException("Przesłano zbyt długi numer faktury, drukarka obsługuje 15 znaków");
            LoginWithoutPrint();
            if (printNumberAsBarcode)
            {
                string Parameters = "";
                if (ean != null)
                    AddOptionalCommandParameter(ref Parameters, "bc", ean);
                else
                    AddOptionalCommandParameter(ref Parameters, "bc", systemNumber);
                SendFrame("ftrcfg", Parameters);
            }
            string ParameterValue = "";
            int num = 0;
            foreach (string str in CustomerData)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    ParameterValue = ParameterValue + str + "\n";
                    ++num;
                    if (num >= 3)
                        break;
                }
            }
            string Parameters1 = "";
            AddCommandParameter(ref Parameters1, "nb", InvoiceNumber);
            AddCommandParameter(ref Parameters1, "ni", NIP);
            AddCommandParameter(ref Parameters1, "na", ParameterValue);
            AddCommandParameter(ref Parameters1, "pd", PaymentDate.ToString("yyyy-MM-dd"));
            AddCommandParameter(ref Parameters1, "pt", PaymentName);
            AddOptionalCommandParameter(ref Parameters1, "sc", CustomerName);
            AddOptionalCommandParameter(ref Parameters1, "ss", SellerName);
            if (!string.IsNullOrEmpty(SellerName))
            {
                if (!string.IsNullOrEmpty(ParameterValue))
                    AddOptionalCommandParameter(ref Parameters1, "ps", "1");
            }
            try
            {
                SendFrame("trfvinit", Parameters1);
            }
            catch (PosnetException ex)
            {
                throw new PosnetException(ex.Number, "Sprawdź czy drukarka obsługuje drukowanie faktur. Drukarka zwróciła błąd: " + ex.Message, ex.Type);
            }
            catch (FiscalException ex)
            {
                throw new FiscalException("Sprawdź czy drukarka obsługuje drukowanie faktur. Drukarka zwróciła błąd: " + ex.Message, new int?(ex.Number), new ExceptionType?(ex.Type));
            }
        }

        public void CloseInvoice(long total)
        {
            receiptTotal = total;
            CloseInvoice();
        }

        public void CloseInvoice()
        {
            string Parameters = "";
            AddOptionalCommandParameter(ref Parameters, "to", receiptTotal, 0L);
            SendFrame("trend", Parameters);
        }

        public void CancelReceipt()
        {
            try
            {
                SendFrame("prncancel", "");
            }
            catch
            {
                Close();
                throw;
            }
        }

        public void AddItem(PosnetReceiptItem item)
        {
            try
            {
                ++lineCounter;
                string Parameters = "";
                string str = item.Name;
                if (str.Length > 40)
                    str = str.Substring(0, 40);
                string ParameterValue1 = PosnetReceiptItem.DiacriticsOnlyMazovia(str.Replace("\r", "").Replace("\n", " ").Replace(TAB, " "));
                AddCommandParameter(ref Parameters, "na", ParameterValue1);
                int num;
                switch ((int)item.Vat - 1)
                {
                    case 0:
                        num = 0;
                        break;
                    case 1:
                        num = 1;
                        break;
                    case 2:
                        num = 2;
                        break;
                    case 3:
                        num = 3;
                        break;
                    case 4:
                        num = 4;
                        break;
                    case 5:
                        num = 5;
                        break;
                    case 6:
                        num = 6;
                        break;
                    default:
                        throw new FiscalException("Nieprawidłowy numer stawki VAT", new int?(-1000), new ExceptionType?((ExceptionType)1));
                }
                AddCommandParameter(ref Parameters, "vt", num.ToString());
                AddCommandParameter(ref Parameters, "pr", item.Price.ToString());
                AddOptionalCommandParameter(ref Parameters, "st", "0");
                AddOptionalCommandParameter(ref Parameters, "wa", item.Total.ToString());
                AddOptionalCommandParameter(ref Parameters, "il", AmountAsString(item));
                if (!string.IsNullOrEmpty(item.Unit))
                {
                    string ParameterValue2 = item.Unit;
                    if (ParameterValue2.Length > 4)
                        ParameterValue2 = ParameterValue2.Substring(0, 4);
                    AddOptionalCommandParameter(ref Parameters, "jm", ParameterValue2);
                }
                if ((int)item.DiscountType == 1 || (int)item.DiscountType == 2)
                {
                    AddOptionalCommandParameter(ref Parameters, "rd", "1");
                    if ((int)item.DiscountType == 1)
                        this.AddOptionalCommandParameter(ref Parameters, "rw", -NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue), 0L);
                    else
                        AddOptionalCommandParameter(ref Parameters, "rp", item.DiscountValue, 0L);
                }
                if ((int)item.DiscountType == 3 || (int)item.DiscountType == 4)
                {
                    AddOptionalCommandParameter(ref Parameters, "rd", "0");
                    if ((int)item.DiscountType == 3)
                        AddOptionalCommandParameter(ref Parameters, "rw", NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue), 0L);
                    else
                        AddOptionalCommandParameter(ref Parameters, "rp", item.DiscountValue, 0L);
                }
                receiptTotal += item.Total + NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue);
                SendFrame("trline", Parameters);
            }
            catch (FiscalException ex)
            {
                if ((int)ex.Type == 1)
                {
                    receiptTotal -= item.Total + NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue);
                    --lineCounter;
                }
                throw;
            }
        }

        private string AmountAsString(PosnetReceiptItem item)
        {
            long amount = item.Amount;
            int precision = item.Precision;
            string str1 = amount.ToString();
            string str2 = str1;
            if (precision > 0)
            {
                while (precision >= str1.Length)
                    str1 = "0" + str1;
                str2 = str1.Substring(0, str1.Length - precision) + "." + str1.Substring(str1.Length - precision);
            }
            return str2;
        }

        public void RemoveItem(PosnetReceiptItem item)
        {
            try
            {
                ++lineCounter;
                string Parameters = "";
                string str = item.Name;
                if (str.Length > 40)
                    str = str.Substring(0, 40);
                string ParameterValue1 = str.Replace("\r", "").Replace("\n", " ").Replace(TAB, " ");
                AddCommandParameter(ref Parameters, "na", ParameterValue1);
                int num;
                switch ((int)item.Vat - 1)
                {
                    case 0:
                        num = 0;
                        break;
                    case 1:
                        num = 1;
                        break;
                    case 2:
                        num = 2;
                        break;
                    case 3:
                        num = 3;
                        break;
                    case 4:
                        num = 4;
                        break;
                    case 5:
                        num = 5;
                        break;
                    case 6:
                        num = 6;
                        break;
                    default:
                        throw new FiscalException("Nieprawidłowy numer stawki VAT", new int?(-1000), new ExceptionType?((ExceptionType)1));
                }
                AddCommandParameter(ref Parameters, "vt", num.ToString());
                AddCommandParameter(ref Parameters, "pr", item.Price.ToString());
                AddOptionalCommandParameter(ref Parameters, "st", "1");
                AddOptionalCommandParameter(ref Parameters, "wa", item.Total.ToString());
                AddOptionalCommandParameter(ref Parameters, "il", AmountAsString(item));
                if (!string.IsNullOrEmpty(item.Unit))
                {
                    string ParameterValue2 = item.Unit;
                    if (ParameterValue2.Length > 4)
                        ParameterValue2 = ParameterValue2.Substring(0, 4);
                    AddOptionalCommandParameter(ref Parameters, "jm", ParameterValue2);
                }
                if ((int)item.DiscountType == 1 || (int)item.DiscountType == 2)
                {
                    AddOptionalCommandParameter(ref Parameters, "rd", "1");
                    if ((int)item.DiscountType == 1)
                        this.AddOptionalCommandParameter(ref Parameters, "rw", -NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue), 0L);
                    else
                        AddOptionalCommandParameter(ref Parameters, "rp", item.DiscountValue, 0L);
                }
                if ((int)item.DiscountType == 3 || (int)item.DiscountType == 4)
                {
                    AddOptionalCommandParameter(ref Parameters, "rd", "0");
                    if ((int)item.DiscountType == 3)
                        AddOptionalCommandParameter(ref Parameters, "rw", NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue), 0L);
                    else
                        AddOptionalCommandParameter(ref Parameters, "rp", item.DiscountValue, 0L);
                }
                receiptTotal -= item.Total + NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue);
                SendFrame("trline", Parameters);
            }
            catch (FiscalException ex)
            {
                if ((int)ex.Type == 1)
                {
                    receiptTotal += item.Total + NormalizeDiscount(item.Total, item.DiscountType, item.DiscountValue);
                    --lineCounter;
                }
                throw;
            }
        }

        public void AddPayment(PosnetPayment payment)
        {
            if (this.payments.ContainsKey(payment.Type))
            {
                Dictionary<int, long> payments;
                int type;
                (payments = this.payments)[type = payment.Type] = payments[type] + payment.Value;
            }
            else
                this.payments.Add(payment.Type, payment.Value);
        }

        public void RemovePayment(PosnetPayment payment)
        {
            if (!payments.ContainsKey(payment.Type))
                return;
            payments.Remove(payment.Type);
        }

        public void PackOperation(PackOperation op, PackItem pack)
        {
            long num = pack.Amount * pack.Price;
            switch ((int)op)
            {
                case 0:
                    packAddTotal += num;
                    packAddList.Add(pack);
                    break;
                case 1:
                    packAddTotal -= num;
                    packRemove(packAddList, pack);
                    break;
                case 2:
                    packReturnTotal += num;
                    packReturnList.Add(pack);
                    break;
                case 3:
                    packReturnTotal -= num;
                    packRemove(packReturnList, pack);
                    break;
                default:
                    throw new FiscalException("Unsupported operation");
            }
        }

        public void Login()
        {
            if (PrintOperatorId)
            {
                string Parameters = "";
                AddCommandParameter(ref Parameters, "na", OperatorId);
                if (PrinterId == null)
                    PrinterId = "";
                AddCommandParameter(ref Parameters, "nk", PrinterId);
                AddOptionalCommandParameter(ref Parameters, "dr", "1");
                SendFrame("login", Parameters);
            }
            else
            {
                string Parameters = "";
                AddCommandParameter(ref Parameters, "na", "");
                AddOptionalCommandParameter(ref Parameters, "nk", "0");
                AddOptionalCommandParameter(ref Parameters, "dr", "0");
                SendFrame("login", Parameters);
            }
        }

        private void LoginWithoutPrint()
        {
            if (PrintOperatorId)
            {
                string Parameters = "";
                AddCommandParameter(ref Parameters, "na", OperatorId);
                if (PrinterId == null)
                    PrinterId = "";
                AddCommandParameter(ref Parameters, "nk", PrinterId);
                AddOptionalCommandParameter(ref Parameters, "dr", "0");
                SendFrame("login", Parameters);
            }
            else
            {
                string Parameters = "";
                AddCommandParameter(ref Parameters, "na", "");
                AddOptionalCommandParameter(ref Parameters, "nk", "0");
                AddOptionalCommandParameter(ref Parameters, "dr", "0");
                SendFrame("login", Parameters);
            }
        }

        public void Logout()
        {
            if (!PrintOperatorId)
                return;
            string Parameters = "";
            AddOptionalCommandParameter(ref Parameters, "na", OperatorId);
            AddOptionalCommandParameter(ref Parameters, "nk", PrinterId);
            SendFrame("logout", Parameters);
        }

        public void FiscalReport()
        {
            DateTime now = DateTime.Now;
            string Parameters = "";
            AddOptionalCommandParameter(ref Parameters, "da", now.Year.ToString() + "-" + now.Month + "-" + now.Day);
            SendFrame("dailyrep", Parameters);
        }

        public void PeriodReport(DateTime start, DateTime end)
        {
            string Parameters = "";
            AddCommandParameter(ref Parameters, "fd", start.Year.ToString() + "-" + start.Month + "-" + start.Day);
            AddCommandParameter(ref Parameters, "td", end.Year.ToString() + "-" + end.Month + "-" + end.Day);
            AddCommandParameter(ref Parameters, "su", "0");
            SendFrame("periodicrepbydates", Parameters);
        }

        public void ShiftReportFromPrinter(string shift, string operatorId, bool reset)
        {
            string Parameters = "";
            if (shift.Length > 8)
                shift = shift.Substring(0, 8);
            AddCommandParameter(ref Parameters, "sh", shift);
            if (reset)
                AddCommandParameter(ref Parameters, "zr", "1");
            else
                AddCommandParameter(ref Parameters, "zr", "0");
            SendFrame("shiftrep", Parameters);
        }

        public void ShiftReportFromDatabase(string shift, string operatorId, DateTime dateFrom, DateTime dateTo, List<PosnetPayment> payments, Decimal insertCashValue, Decimal takeoutCashValue, Decimal cashState, Decimal depositInValue, Decimal depositOutValue, int fiscalizedReceiptsCount, int cancelledReceiptsCount, int stornoItemsCount)
        {
            throw new NotImplementedException();
        }

        public void PrintKP(string documentNr, Decimal value, string operatorId, string contractor)
        {
            string Parameters = "";
            long num = (long)(value * new Decimal(100));
            AddCommandParameter(ref Parameters, "kw", num.ToString());
            AddCommandParameter(ref Parameters, "wp", "1");
            SendFrame("cash", Parameters);
        }

        public void PrintKW(string documentNr, Decimal value, string operatorId, string contractor)
        {
            string Parameters = "";
            long num = (long)(value * new Decimal(100));
            AddCommandParameter(ref Parameters, "kw", num.ToString());
            AddCommandParameter(ref Parameters, "wp", "0");
            SendFrame("cash", Parameters);
        }

        public void OpenDrawer()
        {
            try
            {
                SendFrame("opendrwr", "");
            }
            catch
            {
                Close();
                throw;
            }
        }

        public string Name { get; set; }

        public string PrinterId
        {
            get
            {
                return printerId;
            }
            set
            {
                if (value.Length > 8)
                    printerId = value.Substring(0, 8);
                else
                    printerId = value;
            }
        }

        public string OperatorId
        {
            get
            {
                return operatorId;
            }
            set
            {
                if (value.Length > 32)
                    operatorId = value.Substring(0, 32);
                else
                    operatorId = value;
            }
        }

        public List<KeyValuePair<VatRate, int>> TaxRates
        {
            get
            {
                List<KeyValuePair<VatRate, int>> keyValuePairList = new List<KeyValuePair<VatRate, int>>();
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)1, 2200));
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)2, 700));
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)3, 600));
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)4, 500));
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)5, 300));
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)6, 0));
                keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)7, -1));
                return keyValuePairList;
            }
        }

        public List<PosnetPayment> AvailablePayments
        {
            get
            {
                List<PosnetPayment> ofpPaymentList = new List<PosnetPayment>();
                ofpPaymentList.Add(new PosnetPayment(0, "Gotówka", -1L));
                ofpPaymentList.Add(new PosnetPayment(2, "Karta", -1L));
                ofpPaymentList.Add(new PosnetPayment(3, "Czek", -1L));
                ofpPaymentList.Add(new PosnetPayment(4, "Bon", -1L));
                ofpPaymentList.Add(new PosnetPayment(5, "Kredyt", -1L));
                ofpPaymentList.Add(new PosnetPayment(6, "Inna", -1L));
                ofpPaymentList.Add(new PosnetPayment(7, "Voucher", -1L));
                ofpPaymentList.Add(new PosnetPayment(8, "Konto klienta", -1L));
                return ofpPaymentList;
            }
        }

        public bool SupportsPayments
        {
            get
            {
                return true;
            }
        }

        public List<PosnetProperty> Properties
        {
            get
            {
                List<PosnetProperty> ofpPropertyList = new List<PosnetProperty>();
                ofpPropertyList.Add(new PosnetProperty("port", "Port"));
                ofpPropertyList.Add(new PosnetProperty("baudrate", "Prędkość portu"));
                return ofpPropertyList;
            }
        }

        public string AvailableValues(string name)
        {
            switch (name)
            {
                case "port":
                    return string.Join(";", SerialPort.GetPortNames());
                case "baudrate":
                    return string.Join(";", new string[8]
                    {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"
                    });
                default:
                    throw new FiscalException("Właściwość nie jest obsługiwana przez sterownik");
            }
        }

        public bool SupportsFeature(DriverIdentifier df)
        {
            return df == null;
        }

        public void Dispose()
        {
            Trace.WriteLine(id.ToString() + " - Dispose");
            if (port == null)
                return;
            port.Dispose();
            port = null;
        }

        private void packRemove(List<PackItem> packList, PackItem pack)
        {
            bool flag = false;
            using (List<PackItem>.Enumerator enumerator = packList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PackItem current = enumerator.Current;
                    if (current.Number == pack.Number && current.Price == pack.Price && current.Amount >= pack.Amount)
                    {
                        PackItem ofpPackItem = current;
                        ofpPackItem.Amount = ofpPackItem.Amount - pack.Amount;
                        if (current.Amount == 0)
                            packList.Remove(current);
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
                throw new FiscalException("Wystąpił problem z wycofaniem kaucji");
        }

        private long NormalizeDiscount(long totalValue, Discount discountType, long discountValue)
        {
            switch ((int)discountType)
            {
                case 0:
                    return 0;
                case 1:
                    return -discountValue;
                case 2:
                    return -totalValue * discountValue / 10000L;
                case 3:
                    return discountValue;
                case 4:
                    return totalValue * discountValue / 10000L;
                default:
                    throw new FiscalException("Unknown discountType");
            }
        }

        public string GetDefaultValue(string name)
        {
            if (name == "baudrate")
                return "9600";
            return null;
        }

        public bool PrintSystemNumber
        {
            get
            {
                return printSystemNumber;
            }
            set
            {
                printSystemNumber = value;
            }
        }

        public bool PrintPayments
        {
            get
            {
                return printPayments;
            }
            set
            {
                printPayments = value;
            }
        }

        public bool PrintPack
        {
            get
            {
                return printPack;
            }
            set
            {
                printPack = value;
            }
        }

        public bool PrintOperatorId
        {
            get
            {
                return printOperatorId;
            }
            set
            {
                printOperatorId = value;
            }
        }

        public bool DisplayInfo
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public List<KeyValuePair<VatRate, int>> LoadTaxRates()
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.CurrencyDecimalSeparator = ".";
            numberFormatInfo.CurrencyGroupSeparator = "";
            numberFormatInfo.NumberDecimalSeparator = ".";
            numberFormatInfo.NumberGroupSeparator = "";
            List<KeyValuePair<VatRate, int>> keyValuePairList = new List<KeyValuePair<VatRate, int>>();
            string str1 = SendFrame("vatget", "");
            char[] chArray = new char[1] { '\t' };
            foreach (string str2 in str1.Split(chArray))
            {
                if (str2.Length > 2)
                {
                    int num = (int)(Decimal.Parse(str2.Substring(2, str2.Length - 2).Replace(",", "."), numberFormatInfo) * new Decimal(100));
                    switch (num)
                    {
                        case 10000:
                            num = -1;
                            break;
                        case 10100:
                            continue;
                    }
                    switch (str2.Substring(0, 2).ToLower())
                    {
                        case "va":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)1, num));
                            continue;
                        case "vb":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)2, num));
                            continue;
                        case "vc":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)3, num));
                            continue;
                        case "vd":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)4, num));
                            continue;
                        case "ve":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)5, num));
                            continue;
                        case "vf":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)6, num));
                            continue;
                        case "vg":
                            keyValuePairList.Add(new KeyValuePair<VatRate, int>((VatRate)7, num));
                            continue;
                        default:
                            continue;
                    }
                }
            }
            return keyValuePairList;
        }

        public void SaveTaxRates(List<KeyValuePair<VatRate, int>> TaxRatesList)
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.CurrencyDecimalSeparator = ",";
            numberFormatInfo.CurrencyGroupSeparator = "";
            numberFormatInfo.NumberDecimalSeparator = ",";
            numberFormatInfo.NumberGroupSeparator = "";
            string Parameters = "";
            using (List<KeyValuePair<VatRate, int>>.Enumerator enumerator = TaxRatesList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<VatRate, int> current = enumerator.Current;
                    string ParameterValue = (current.Value / new Decimal(100)).ToString(numberFormatInfo);
                    if (current.Value == -1)
                        ParameterValue = "100";
                    switch ((int)current.Key - 1)
                    {
                        case 0:
                            AddCommandParameter(ref Parameters, "va", ParameterValue);
                            continue;
                        case 1:
                            AddCommandParameter(ref Parameters, "vb", ParameterValue);
                            continue;
                        case 2:
                            AddCommandParameter(ref Parameters, "vc", ParameterValue);
                            continue;
                        case 3:
                            AddCommandParameter(ref Parameters, "vd", ParameterValue);
                            continue;
                        case 4:
                            AddCommandParameter(ref Parameters, "ve", ParameterValue);
                            continue;
                        case 5:
                            AddCommandParameter(ref Parameters, "vf", ParameterValue);
                            continue;
                        case 6:
                            AddCommandParameter(ref Parameters, "vg", ParameterValue);
                            continue;
                        default:
                            continue;
                    }
                }
            }
            DateTime now = DateTime.Now;
            AddOptionalCommandParameter(ref Parameters, "da", now.Year.ToString() + "-" + now.Month + "-" + now.Day);
            SendFrame("vatset", Parameters);
        }

        public DateTime GetTime()
        {
            string str1 = SendFrame("rtcget", "");
            char[] chArray = new char[1] { '\t' };
            foreach (string str2 in str1.Split(chArray))
            {
                if (str2.Length > 2)
                {
                    string lower = str2.Substring(0, 2).ToLower();
                    string s = str2.Substring(2);
                    switch (lower)
                    {
                        case "da":
                            return DateTime.ParseExact(s, "yyyy-MM-dd;hh:mm", CultureInfo.InvariantCulture);
                        default:
                            continue;
                    }
                }
            }
            throw new FiscalException("Nie można odczytać czasu z drukarki");
        }

        public int GetLastFiscalReportNum()
        {
            int rd = 0;
            int hn = 0;
            int bn = 0;
            int fn = 0;
            string nu = "";
            GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
            return rd;
        }

        public int GetLastPrintoutHeaderNum()
        {
            int rd = 0;
            int hn = 0;
            int bn = 0;
            int fn = 0;
            string nu = "";
            GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
            return hn;
        }

        public int GetLastRecepitNum()
        {
            int rd = 0;
            int hn = 0;
            int bn = 0;
            int fn = 0;
            string nu = "";
            GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
            return bn;
        }

        public int GetLastInvoiceNum()
        {
            int rd = 0;
            int hn = 0;
            int bn = 0;
            int fn = 0;
            string nu = "";
            GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
            return fn;
        }

        public string GetPrinterSerialNum()
        {
            int rd = 0;
            int hn = 0;
            int bn = 0;
            int fn = 0;
            string nu = "";
            GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
            return nu;
        }

        private void GetCountersStatus(ref int rd, ref int hn, ref int bn, ref int fn, ref string nu)
        {
            string str1 = SendFrame("scnt", "");
            char[] chArray = new char[1] { '\t' };
            foreach (string str2 in str1.Split(chArray))
            {
                if (str2.Length > 2)
                {
                    string lower = str2.Substring(0, 2).ToLower();
                    string s = str2.Substring(2);
                    switch (lower)
                    {
                        case nameof(rd):
                            rd = int.Parse(s);
                            continue;
                        case nameof(hn):
                            hn = int.Parse(s);
                            continue;
                        case nameof(bn):
                            bn = int.Parse(s);
                            continue;
                        case nameof(fn):
                            fn = int.Parse(s);
                            continue;
                        case nameof(nu):
                            nu = s;
                            continue;
                        default:
                            continue;
                    }
                }
            }
        }

        public void OpenNonFiscalPrint(int printId, int headerId, string additionaDescription)
        {
            NonFiscalPrintId = printId;
            string Parameters = "";
            AddCommandParameter(ref Parameters, "fn", NonFiscalPrintId.ToString());
            AddCommandParameter(ref Parameters, "fh", headerId.ToString());
            AddOptionalCommandParameter(ref Parameters, "al", additionaDescription);
            SendFrame("formstart", Parameters);
        }

        public void NonFiscalLinePrint(int lineId, string lineData)
        {
            string Parameters = "";
            AddCommandParameter(ref Parameters, "fn", NonFiscalPrintId.ToString());
            AddCommandParameter(ref Parameters, "fl", lineId.ToString());
            AddOptionalCommandParameter(ref Parameters, "s1", lineData);
            SendFrame("formline", Parameters);
        }

        public void CloseNonFiscalPrint()
        {
            string Parameters = "";
            AddCommandParameter(ref Parameters, "fn", NonFiscalPrintId.ToString());
            SendFrame("formend", Parameters);
            NonFiscalPrintId = 0;
        }

        public void OpenRefundTransaction(string sysnum)
        {
            throw new NotSupportedException("Metoda nie obsługiwana przez ten sterownik drukarki");
        }

        public string KeyServer
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public void LogoutERP()
        {
        }

        public bool CommunicationLog
        {
            get
            {
                return communicationLog;
            }
            set
            {
                communicationLog = value;
                if (port == null)
                    return;
            }
        }

        public void PrintImageSK(int number, string station, string justify, int mode)
        {
            throw new NotSupportedException("Metoda nie obsługiwana przez ten sterownik drukarki");
        }
    }
}
