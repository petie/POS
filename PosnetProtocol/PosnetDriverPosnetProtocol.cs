// Decompiled with JetBrains decompiler
// Type: OnlineFPPosnetProtocol.PosnetDriverPosnetProtocol
// Assembly: OnlineFPPosnetProtocol, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 619FDB08-8046-432B-AE81-BF4303AA2094
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\drivers\OnlineFPPosnetProtocol.dll

using OnlineFPCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;

namespace OnlineFPPosnetProtocol
{
  [OFPDriver(FriendlyName = "Posnet Thermal HS i FV", IsNetworkDriver = false, Name = "comarch:msp:posnet:thermalhsfvpostnet")]
  internal class PosnetDriverPosnetProtocol : IOFPDriver, IDisposable, IOFPDriverProperties
  {
    private static byte STX = 2;
    private static byte EXT = 3;
    private Dictionary<string, string> settings = new Dictionary<string, string>();
    private int standardTimeout = 5000;
    private string TAB = new string('\t', 1);
    private List<KeyValuePair<int, string>> FooterLines = new List<KeyValuePair<int, string>>();
    private bool printSystemNumber = true;
    private bool printPayments = true;
    private bool printPack = true;
    private bool printOperatorId = true;
    private OFPSerialPort port;
    private Dictionary<char, byte> mazoviaCode;
    private int id;
    private string printerId;
    private string operatorId;
    private int lineCounter;
    private long receiptTotal;
    private long packAddTotal;
    private long packReturnTotal;
    private string systemNumber;
    private List<OFPPackItem> packAddList;
    private List<OFPPackItem> packReturnList;
    private Dictionary<int, long> payments;
    private int NonFiscalPrintId;
    private bool communicationLog;

    public PosnetDriverPosnetProtocol(string name)
    {
      this.id = new Random().Next();
      Trace.WriteLine(this.id.ToString() + " - PosnetDriver");
      this.Name = name;
      this.OperatorId = "";
      this.mazoviaCode = new Dictionary<char, byte>();
      for (int index = 0; index < (int) sbyte.MaxValue; ++index)
        this.mazoviaCode.Add((char) index, (byte) index);
      this.mazoviaCode.Add('ą', (byte) 134);
      this.mazoviaCode.Add('ł', (byte) 146);
      this.mazoviaCode.Add('ś', (byte) 158);
      this.mazoviaCode.Add('ć', (byte) 141);
      this.mazoviaCode.Add('ń', (byte) 164);
      this.mazoviaCode.Add('ź', (byte) 166);
      this.mazoviaCode.Add('ę', (byte) 145);
      this.mazoviaCode.Add('ó', (byte) 162);
      this.mazoviaCode.Add('ż', (byte) 167);
      this.mazoviaCode.Add('Ą', (byte) 143);
      this.mazoviaCode.Add('Ł', (byte) 156);
      this.mazoviaCode.Add('Ś', (byte) 152);
      this.mazoviaCode.Add('Ć', (byte) 149);
      this.mazoviaCode.Add('Ń', (byte) 165);
      this.mazoviaCode.Add('Ź', (byte) 160);
      this.mazoviaCode.Add('Ę', (byte) 144);
      this.mazoviaCode.Add('Ó', (byte) 163);
      this.mazoviaCode.Add('Ż', (byte) 161);
      this.mazoviaCode.Add('ü', (byte) 129);
      this.mazoviaCode.Add('ä', (byte) 132);
      this.mazoviaCode.Add('ë', (byte) 137);
      this.mazoviaCode.Add('ö', (byte) 148);
      this.mazoviaCode.Add('ß', (byte) 225);
      this.mazoviaCode.Add('Ü', (byte) 154);
      this.mazoviaCode.Add('Ä', (byte) 142);
      this.mazoviaCode.Add('Ö', (byte) 153);
    }

    private void AddOptionalCommandParameter(ref string Parameters, string ParameterId, string ParameterValue)
    {
      if (string.IsNullOrEmpty(ParameterValue))
        return;
      this.AddCommandParameter(ref Parameters, ParameterId, ParameterValue);
    }

    private void AddOptionalCommandParameter(ref string Parameters, string ParameterId, int ParameterValue, int EmptyValue)
    {
      if (ParameterValue == EmptyValue)
        return;
      this.AddCommandParameter(ref Parameters, ParameterId, ParameterValue.ToString());
    }

    private void AddOptionalCommandParameter(ref string Parameters, string ParameterId, long ParameterValue, long EmptyValue)
    {
      if (ParameterValue == EmptyValue)
        return;
      this.AddCommandParameter(ref Parameters, ParameterId, ParameterValue.ToString());
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
        local = local + this.TAB + ParameterId + ParameterValue;
      }
    }

    private string SendFrame(string PrinterCommand, string Parameters)
    {
      try
      {
        string str = !string.IsNullOrEmpty(Parameters) ? PrinterCommand + this.TAB + Parameters : PrinterCommand;
        byte[] mazovia = this.stringToMazovia(str + this.TAB + "#");
        byte[] numArray = new byte[1]
        {
          PosnetDriverPosnetProtocol.STX
        };
        this.port.Write(numArray, 0, 1);
        this.port.Write(mazovia, 0, mazovia.Length);
        this.port.Write(this.stringToMazovia(CRC16.CalculateCRC(this.stringToMazovia(str + this.TAB))), 0, 4);
        numArray[0] = PosnetDriverPosnetProtocol.EXT;
        this.port.Write(numArray, 0, 1);
        return this.ResponseParameters(PrinterCommand);
      }
      catch (TimeoutException ex)
      {
        string str = "";
        if (this.port != null && ((SerialPort) this.port).PortName != null)
          str = ((SerialPort) this.port).PortName + ". ";
        throw new OFPException("Wystąpił błąd połączenia z portem " + str + ex.Message);
      }
      catch (IOException ex)
      {
        string str = "";
        if (this.port != null && ((SerialPort) this.port).PortName != null)
          str = ((SerialPort) this.port).PortName + ". ";
        throw new OFPException("Wystąpił błąd połączenia z portem " + str + ex.Message);
      }
      catch (OFPException ex)
      {
        byte[] mazovia = this.stringToMazovia("prncancel" + this.TAB + "#");
        byte[] numArray = new byte[1]
        {
          PosnetDriverPosnetProtocol.STX
        };
        this.port.Write(numArray, 0, 1);
        this.port.Write(mazovia, 0, mazovia.Length);
        this.port.Write(this.stringToMazovia(CRC16.CalculateCRC(this.stringToMazovia("prncancel" + this.TAB))), 0, 4);
        numArray[0] = PosnetDriverPosnetProtocol.EXT;
        this.port.Write(numArray, 0, 1);
        throw;
      }
      catch (Exception ex)
      {
        throw new OFPException("Wystąpił błąd połączenia z portem. " + ex.ToString() + " " + ex.Message);
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
                ResponseFrame1 = this.port.ReadTo(new string((char) PosnetDriverPosnetProtocol.EXT, 1));
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
              byte[] mazovia = this.stringToMazovia("!sdev" + this.TAB + "#");
              byte[] numArray = new byte[1]
              {
                PosnetDriverPosnetProtocol.STX
              };
              this.port.Write(numArray, 0, 1);
              this.port.Write(mazovia, 0, mazovia.Length);
              this.port.Write(this.stringToMazovia(CRC16.CalculateCRC(this.stringToMazovia("!sdev" + this.TAB))), 0, 4);
              numArray[0] = PosnetDriverPosnetProtocol.EXT;
              this.port.Write(numArray, 0, 1);
              string ResponseFrame2 = this.port.ReadTo(new string((char) PosnetDriverPosnetProtocol.EXT, 1));
              NotAsyncResponseOnAsyncCommand2 = false;
              string str1 = this.ParseResponseFrame("!sdev", ResponseFrame2, ref NotAsyncResponseOnAsyncCommand2);
              if (NotAsyncResponseOnAsyncCommand2)
              {
                ResponseFrame1 = str1;
                string responseFrame = this.ParseResponseFrame("!sdev", this.port.ReadTo(new string((char) PosnetDriverPosnetProtocol.EXT, 1)), ref NotAsyncResponseOnAsyncCommand1);
                if (NotAsyncResponseOnAsyncCommand1)
                  throw new PostnetProtException("Błąd podczas wykonywania polecenia: " + PrinterCommand + ". Zbyt długa kolejka ramek.");
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
      return this.ParseResponseFrame(PrinterCommand, ResponseFrame1, ref NotAsyncResponseOnAsyncCommand1);
    }

    private string ParseResponseFrame(string PrinterCommand, string ResponseFrame, ref bool NotAsyncResponseOnAsyncCommand)
    {
      NotAsyncResponseOnAsyncCommand = false;
      if ((int) ResponseFrame[0] != (int) PosnetDriverPosnetProtocol.STX)
        throw new PostnetProtException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Ramka odpowiedzi jest błędna, brak sekwencji początkowej STX");
      if (ResponseFrame[ResponseFrame.Length - 5] != '#')
        throw new PostnetProtException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Ramka odpowiedzi jest błędna, brak znaku # przed sumą CRC");
      if (CRC16.CalculateCRC(this.stringToMazovia(ResponseFrame.Substring(1, ResponseFrame.Length - 6))) != ResponseFrame.Substring(ResponseFrame.Length - 4, 4))
        throw new PostnetProtException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Ramka odpowiedzi ma nieprawidłową sumę kontrolną CRC");
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
          throw new PostnetProtException(result, PrinterCommand);
        }
        return str2;
      }
      if (ResponseFrame.Substring(1, 3).ToUpper() == "ERR")
        throw new PostnetProtException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Drukarka otrzymała błędną ramkę");
      if (!flag)
        throw new PostnetProtException("Błąd podczas wykonywania polecenia: " + PrinterCommand + " .Nieznany błąd ramki");
      NotAsyncResponseOnAsyncCommand = true;
      return ResponseFrame;
    }

    private byte[] stringToMazovia(string str)
    {
      return OFPReceiptItem.stringToMazovia(str);
    }

    public void Open()
    {
      Trace.WriteLine(this.id.ToString() + " - Open");
      try
      {
        if (this.port != null)
        {
          if (((SerialPort) this.port).IsOpen)
            this.port.Close();
          ((Component) this.port).Dispose();
        }
        this.port = new OFPSerialPort(this.Name, this.settings["port"], int.Parse(this.settings["baudrate"]));
        this.port.set_CommunicationLog(this.CommunicationLog);
        ((SerialPort) this.port).ReadTimeout = this.standardTimeout;
        ((SerialPort) this.port).WriteTimeout = this.standardTimeout;
        if (this.settings.ContainsKey("handshake"))
        {
          switch (this.settings["handshake"])
          {
            case "XOnXOff":
              ((SerialPort) this.port).Handshake = Handshake.XOnXOff;
              break;
            case "RequestToSend":
              ((SerialPort) this.port).Handshake = Handshake.RequestToSend;
              break;
            case "RequestToSendXOnXOff":
              ((SerialPort) this.port).Handshake = Handshake.RequestToSendXOnXOff;
              break;
            case "None":
              ((SerialPort) this.port).Handshake = Handshake.None;
              break;
            case "System":
              break;
            default:
              throw new PostnetProtException("Niepoprawne ustawienie portu COM: Handshake");
          }
        }
        this.port.Open();
      }
      catch (UnauthorizedAccessException ex)
      {
        throw new OFPException("Brak dostępu do portu COM. " + ex.Message);
      }
      catch (IOException ex)
      {
        throw new OFPException("Wystąpił błąd połączenia z portem." + ex.Message);
      }
      catch (OFPException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new OFPException("Wystąpił błąd połączenia z portem. " + ex.ToString() + " " + ex.Message);
      }
    }

    public void Close()
    {
      Trace.WriteLine(this.id.ToString() + " - Close");
      if (this.port != null)
      {
        this.port.Close();
        ((Component) this.port).Dispose();
      }
      this.port = (OFPSerialPort) null;
    }

    public void TestReady()
    {
      string str1 = this.SendFrame("!sdev", "");
      char[] chArray = new char[1]{ '\t' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2[0] == 'd' && str2[1] == 's')
        {
          switch (int.Parse(str2.Substring(2)))
          {
            case 0:
              continue;
            case 1:
              throw new PostnetProtException("Drukarka w tyrbie menu, nie można drukować.");
            case 2:
              throw new PostnetProtException("Drukarka oczekuje na klawisz, nie można drukować.");
            case 3:
              throw new PostnetProtException("Drukarka oczekuje na reakcję użytkownika - wystąpił błąd.");
            default:
              throw new PostnetProtException("Nie rozpoznany stan drukarki");
          }
        }
      }
    }

    public void OpenReceipt(string systemNumber, string ean = null, bool printNumberAsBarcode = false)
    {
      this.FooterLines.Clear();
      this.systemNumber = systemNumber;
      if (this.systemNumber.Length > 30)
        this.systemNumber = this.systemNumber.Substring(0, 30);
      this.lineCounter = 0;
      this.receiptTotal = 0L;
      this.packAddTotal = 0L;
      this.packReturnTotal = 0L;
      this.packAddList = new List<OFPPackItem>();
      this.packReturnList = new List<OFPPackItem>();
      this.payments = new Dictionary<int, long>();
      this.LoginWithoutPrint();
      if (printNumberAsBarcode)
      {
        string Parameters = "";
        if (ean != null)
          this.AddOptionalCommandParameter(ref Parameters, "bc", ean);
        else
          this.AddOptionalCommandParameter(ref Parameters, "bc", systemNumber);
        this.SendFrame("ftrcfg", Parameters);
      }
      string Parameters1 = "";
      this.AddOptionalCommandParameter(ref Parameters1, "bm", "0");
      this.SendFrame("trinit", Parameters1);
    }

    public void AddFooterLine(int id, string param)
    {
      this.FooterLines.Add(new KeyValuePair<int, string>(id, param));
    }

    public void CloseReceipt(long total)
    {
      this.receiptTotal = total;
      this.CloseReceipt();
    }

    public void CloseReceipt()
    {
      long ParameterValue1 = 0;
      if (this.PrintPayments)
      {
        foreach (KeyValuePair<int, long> payment in this.payments)
        {
          ParameterValue1 += payment.Value;
          string Parameters = "";
          this.AddCommandParameter(ref Parameters, "ty", payment.Key.ToString());
          this.AddCommandParameter(ref Parameters, "wa", payment.Value.ToString());
          this.SendFrame("trpayment", Parameters);
        }
      }
      if (this.PrintPack)
      {
        using (List<OFPPackItem>.Enumerator enumerator = this.packAddList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            OFPPackItem current = enumerator.Current;
            string Parameters = "";
            this.AddCommandParameter(ref Parameters, "na", current.get_Number().ToString());
            this.AddCommandParameter(ref Parameters, "ne", "0");
            this.AddCommandParameter(ref Parameters, "pr", current.get_Price().ToString());
            this.AddCommandParameter(ref Parameters, "il", current.get_Amount().ToString());
            this.SendFrame("trpack", Parameters);
          }
        }
        using (List<OFPPackItem>.Enumerator enumerator = this.packReturnList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            OFPPackItem current = enumerator.Current;
            string Parameters = "";
            this.AddCommandParameter(ref Parameters, "na", current.get_Number().ToString());
            this.AddCommandParameter(ref Parameters, "ne", "1");
            this.AddCommandParameter(ref Parameters, "pr", current.get_Price().ToString());
            this.AddCommandParameter(ref Parameters, "il", current.get_Amount().ToString());
            this.SendFrame("trpack", Parameters);
          }
        }
      }
      else
      {
        this.packAddTotal = 0L;
        this.packReturnTotal = 0L;
      }
      string Parameters1 = "";
      this.AddOptionalCommandParameter(ref Parameters1, "to", this.receiptTotal, 0L);
      this.AddOptionalCommandParameter(ref Parameters1, "op", this.packAddTotal, 0L);
      this.AddOptionalCommandParameter(ref Parameters1, "om", this.packReturnTotal, 0L);
      this.AddOptionalCommandParameter(ref Parameters1, "fp", ParameterValue1, 0L);
      string ParameterValue2 = "1";
      bool flag = false;
      if (this.PrintSystemNumber)
        flag = true;
      if (this.PrintPack && (this.packAddList.Count > 0 || this.packReturnList.Count > 0))
        flag = true;
      if (flag)
        ParameterValue2 = "0";
      this.AddOptionalCommandParameter(ref Parameters1, "fe", ParameterValue2);
      this.SendFrame("trend", Parameters1);
      if (!flag)
        return;
      if (this.PrintPack)
      {
        using (List<OFPPackItem>.Enumerator enumerator = this.packAddList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            OFPPackItem current = enumerator.Current;
            string Parameters2 = "";
            this.AddCommandParameter(ref Parameters2, "na", current.get_Number().ToString());
            this.AddCommandParameter(ref Parameters2, "ne", "0");
            this.AddCommandParameter(ref Parameters2, "pr", current.get_Price().ToString());
            this.AddCommandParameter(ref Parameters2, "il", current.get_Amount().ToString());
            this.SendFrame("trpackprnend", Parameters2);
          }
        }
        using (List<OFPPackItem>.Enumerator enumerator = this.packReturnList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            OFPPackItem current = enumerator.Current;
            string Parameters2 = "";
            this.AddCommandParameter(ref Parameters2, "na", current.get_Number().ToString());
            this.AddCommandParameter(ref Parameters2, "ne", "1");
            this.AddCommandParameter(ref Parameters2, "pr", current.get_Price().ToString());
            this.AddCommandParameter(ref Parameters2, "il", current.get_Amount().ToString());
            this.SendFrame("trpackprnend", Parameters2);
          }
        }
      }
      if (this.PrintSystemNumber)
      {
        string Parameters2 = "";
        this.AddCommandParameter(ref Parameters2, "id", "30");
        this.AddCommandParameter(ref Parameters2, "na", this.systemNumber);
        this.SendFrame("trftrln", Parameters2);
      }
      if (this.FooterLines != null && this.FooterLines.Count > 0)
      {
        foreach (KeyValuePair<int, string> footerLine in this.FooterLines)
        {
          string Parameters2 = "";
          this.AddCommandParameter(ref Parameters2, "id", footerLine.Key.ToString());
          string ParameterValue3 = footerLine.Value;
          if (ParameterValue3.Length > 40)
            ParameterValue3 = ParameterValue3.Substring(0, 40);
          this.AddCommandParameter(ref Parameters2, "na", ParameterValue3);
          this.SendFrame("trftrln", Parameters2);
        }
      }
      this.SendFrame("trftrend", "");
    }

    public void OpenInvoice(int ItemsCount, string InvoiceNumber, string[] CustomerData, string NIP, DateTime PaymentDate, string PaymentName, string CustomerName, string SellerName, string ean = null, bool printNumberAsBarcode = false)
    {
      this.lineCounter = 0;
      this.receiptTotal = 0L;
      this.packAddTotal = 0L;
      this.packReturnTotal = 0L;
      this.packAddList = new List<OFPPackItem>();
      this.packReturnList = new List<OFPPackItem>();
      this.payments = new Dictionary<int, long>();
      if (InvoiceNumber.Length > 15)
        throw new PostnetProtException("Przesłano zbyt długi numer faktury, drukarka obsługuje 15 znaków");
      this.LoginWithoutPrint();
      if (printNumberAsBarcode)
      {
        string Parameters = "";
        if (ean != null)
          this.AddOptionalCommandParameter(ref Parameters, "bc", ean);
        else
          this.AddOptionalCommandParameter(ref Parameters, "bc", this.systemNumber);
        this.SendFrame("ftrcfg", Parameters);
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
      this.AddCommandParameter(ref Parameters1, "nb", InvoiceNumber);
      this.AddCommandParameter(ref Parameters1, "ni", NIP);
      this.AddCommandParameter(ref Parameters1, "na", ParameterValue);
      this.AddCommandParameter(ref Parameters1, "pd", PaymentDate.ToString("yyyy-MM-dd"));
      this.AddCommandParameter(ref Parameters1, "pt", PaymentName);
      this.AddOptionalCommandParameter(ref Parameters1, "sc", CustomerName);
      this.AddOptionalCommandParameter(ref Parameters1, "ss", SellerName);
      if (!string.IsNullOrEmpty(SellerName))
      {
        if (!string.IsNullOrEmpty(ParameterValue))
          this.AddOptionalCommandParameter(ref Parameters1, "ps", "1");
      }
      try
      {
        this.SendFrame("trfvinit", Parameters1);
      }
      catch (PostnetProtException ex)
      {
        throw new PostnetProtException(ex.get_Number(), "Sprawdź czy drukarka obsługuje drukowanie faktur. Drukarka zwróciła błąd: " + ((Exception) ex).Message, ex.get_Type());
      }
      catch (OFPException ex)
      {
        throw new OFPException("Sprawdź czy drukarka obsługuje drukowanie faktur. Drukarka zwróciła błąd: " + ((Exception) ex).Message, new int?(ex.get_Number()), new OFPExceptionType?(ex.get_Type()));
      }
    }

    public void CloseInvoice(long total)
    {
      this.receiptTotal = total;
      this.CloseInvoice();
    }

    public void CloseInvoice()
    {
      string Parameters = "";
      this.AddOptionalCommandParameter(ref Parameters, "to", this.receiptTotal, 0L);
      this.SendFrame("trend", Parameters);
    }

    public void CancelReceipt()
    {
      try
      {
        this.SendFrame("prncancel", "");
      }
      catch
      {
        this.Close();
        throw;
      }
    }

    public void AddItem(OFPReceiptItem item)
    {
      try
      {
        ++this.lineCounter;
        string Parameters = "";
        string str = item.get_Name();
        if (str.Length > 40)
          str = str.Substring(0, 40);
        string ParameterValue1 = OFPReceiptItem.DiacriticsOnlyMazovia(str.Replace("\r", "").Replace("\n", " ").Replace(this.TAB, " "));
        this.AddCommandParameter(ref Parameters, "na", ParameterValue1);
        int num;
        switch (item.get_Vat() - 1)
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
            throw new OFPException("Nieprawidłowy numer stawki VAT", new int?(-1000), new OFPExceptionType?((OFPExceptionType) 1));
        }
        this.AddCommandParameter(ref Parameters, "vt", num.ToString());
        this.AddCommandParameter(ref Parameters, "pr", item.get_Price().ToString());
        this.AddOptionalCommandParameter(ref Parameters, "st", "0");
        this.AddOptionalCommandParameter(ref Parameters, "wa", item.get_Total().ToString());
        this.AddOptionalCommandParameter(ref Parameters, "il", this.AmountAsString(item));
        if (!string.IsNullOrEmpty(item.get_Unit()))
        {
          string ParameterValue2 = item.get_Unit();
          if (ParameterValue2.Length > 4)
            ParameterValue2 = ParameterValue2.Substring(0, 4);
          this.AddOptionalCommandParameter(ref Parameters, "jm", ParameterValue2);
        }
        if (item.get_DiscountType() == 1 || item.get_DiscountType() == 2)
        {
          this.AddOptionalCommandParameter(ref Parameters, "rd", "1");
          if (item.get_DiscountType() == 1)
            this.AddOptionalCommandParameter(ref Parameters, "rw", -this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue()), 0L);
          else
            this.AddOptionalCommandParameter(ref Parameters, "rp", item.get_DiscountValue(), 0L);
        }
        if (item.get_DiscountType() == 3 || item.get_DiscountType() == 4)
        {
          this.AddOptionalCommandParameter(ref Parameters, "rd", "0");
          if (item.get_DiscountType() == 3)
            this.AddOptionalCommandParameter(ref Parameters, "rw", this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue()), 0L);
          else
            this.AddOptionalCommandParameter(ref Parameters, "rp", item.get_DiscountValue(), 0L);
        }
        this.receiptTotal += item.get_Total() + this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue());
        this.SendFrame("trline", Parameters);
      }
      catch (OFPException ex)
      {
        if (ex.get_Type() == 1)
        {
          this.receiptTotal -= item.get_Total() + this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue());
          --this.lineCounter;
        }
        throw;
      }
    }

    private string AmountAsString(OFPReceiptItem item)
    {
      long amount = item.get_Amount();
      int precision = item.get_Precision();
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

    public void RemoveItem(OFPReceiptItem item)
    {
      try
      {
        ++this.lineCounter;
        string Parameters = "";
        string str = item.get_Name();
        if (str.Length > 40)
          str = str.Substring(0, 40);
        string ParameterValue1 = str.Replace("\r", "").Replace("\n", " ").Replace(this.TAB, " ");
        this.AddCommandParameter(ref Parameters, "na", ParameterValue1);
        int num;
        switch (item.get_Vat() - 1)
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
            throw new OFPException("Nieprawidłowy numer stawki VAT", new int?(-1000), new OFPExceptionType?((OFPExceptionType) 1));
        }
        this.AddCommandParameter(ref Parameters, "vt", num.ToString());
        this.AddCommandParameter(ref Parameters, "pr", item.get_Price().ToString());
        this.AddOptionalCommandParameter(ref Parameters, "st", "1");
        this.AddOptionalCommandParameter(ref Parameters, "wa", item.get_Total().ToString());
        this.AddOptionalCommandParameter(ref Parameters, "il", this.AmountAsString(item));
        if (!string.IsNullOrEmpty(item.get_Unit()))
        {
          string ParameterValue2 = item.get_Unit();
          if (ParameterValue2.Length > 4)
            ParameterValue2 = ParameterValue2.Substring(0, 4);
          this.AddOptionalCommandParameter(ref Parameters, "jm", ParameterValue2);
        }
        if (item.get_DiscountType() == 1 || item.get_DiscountType() == 2)
        {
          this.AddOptionalCommandParameter(ref Parameters, "rd", "1");
          if (item.get_DiscountType() == 1)
            this.AddOptionalCommandParameter(ref Parameters, "rw", -this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue()), 0L);
          else
            this.AddOptionalCommandParameter(ref Parameters, "rp", item.get_DiscountValue(), 0L);
        }
        if (item.get_DiscountType() == 3 || item.get_DiscountType() == 4)
        {
          this.AddOptionalCommandParameter(ref Parameters, "rd", "0");
          if (item.get_DiscountType() == 3)
            this.AddOptionalCommandParameter(ref Parameters, "rw", this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue()), 0L);
          else
            this.AddOptionalCommandParameter(ref Parameters, "rp", item.get_DiscountValue(), 0L);
        }
        this.receiptTotal -= item.get_Total() + this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue());
        this.SendFrame("trline", Parameters);
      }
      catch (OFPException ex)
      {
        if (ex.get_Type() == 1)
        {
          this.receiptTotal += item.get_Total() + this.NormalizeDiscount(item.get_Total(), item.get_DiscountType(), item.get_DiscountValue());
          --this.lineCounter;
        }
        throw;
      }
    }

    public void AddPayment(OFPPayment payment)
    {
      if (this.payments.ContainsKey(payment.get_Type()))
      {
        Dictionary<int, long> payments;
        int type;
        (payments = this.payments)[type = payment.get_Type()] = payments[type] + payment.get_Value();
      }
      else
        this.payments.Add(payment.get_Type(), payment.get_Value());
    }

    public void RemovePayment(OFPPayment payment)
    {
      if (!this.payments.ContainsKey(payment.get_Type()))
        return;
      this.payments.Remove(payment.get_Type());
    }

    public void PackOperation(OFPPackOperation op, OFPPackItem pack)
    {
      long num = (long) pack.get_Amount() * pack.get_Price();
      switch ((int) op)
      {
        case 0:
          this.packAddTotal += num;
          this.packAddList.Add(pack);
          break;
        case 1:
          this.packAddTotal -= num;
          this.packRemove(this.packAddList, pack);
          break;
        case 2:
          this.packReturnTotal += num;
          this.packReturnList.Add(pack);
          break;
        case 3:
          this.packReturnTotal -= num;
          this.packRemove(this.packReturnList, pack);
          break;
        default:
          throw new OFPException("Unsupported operation");
      }
    }

    public void Login()
    {
      if (this.PrintOperatorId)
      {
        string Parameters = "";
        this.AddCommandParameter(ref Parameters, "na", this.OperatorId);
        if (this.PrinterId == null)
          this.PrinterId = "";
        this.AddCommandParameter(ref Parameters, "nk", this.PrinterId);
        this.AddOptionalCommandParameter(ref Parameters, "dr", "1");
        this.SendFrame("login", Parameters);
      }
      else
      {
        string Parameters = "";
        this.AddCommandParameter(ref Parameters, "na", "");
        this.AddOptionalCommandParameter(ref Parameters, "nk", "0");
        this.AddOptionalCommandParameter(ref Parameters, "dr", "0");
        this.SendFrame("login", Parameters);
      }
    }

    private void LoginWithoutPrint()
    {
      if (this.PrintOperatorId)
      {
        string Parameters = "";
        this.AddCommandParameter(ref Parameters, "na", this.OperatorId);
        if (this.PrinterId == null)
          this.PrinterId = "";
        this.AddCommandParameter(ref Parameters, "nk", this.PrinterId);
        this.AddOptionalCommandParameter(ref Parameters, "dr", "0");
        this.SendFrame("login", Parameters);
      }
      else
      {
        string Parameters = "";
        this.AddCommandParameter(ref Parameters, "na", "");
        this.AddOptionalCommandParameter(ref Parameters, "nk", "0");
        this.AddOptionalCommandParameter(ref Parameters, "dr", "0");
        this.SendFrame("login", Parameters);
      }
    }

    public void Logout()
    {
      if (!this.PrintOperatorId)
        return;
      string Parameters = "";
      this.AddOptionalCommandParameter(ref Parameters, "na", this.OperatorId);
      this.AddOptionalCommandParameter(ref Parameters, "nk", this.PrinterId);
      this.SendFrame("logout", Parameters);
    }

    public void FiscalReport()
    {
      DateTime now = DateTime.Now;
      string Parameters = "";
      this.AddOptionalCommandParameter(ref Parameters, "da", now.Year.ToString() + "-" + (object) now.Month + "-" + (object) now.Day);
      this.SendFrame("dailyrep", Parameters);
    }

    public void PeriodReport(DateTime start, DateTime end)
    {
      string Parameters = "";
      this.AddCommandParameter(ref Parameters, "fd", start.Year.ToString() + "-" + (object) start.Month + "-" + (object) start.Day);
      this.AddCommandParameter(ref Parameters, "td", end.Year.ToString() + "-" + (object) end.Month + "-" + (object) end.Day);
      this.AddCommandParameter(ref Parameters, "su", "0");
      this.SendFrame("periodicrepbydates", Parameters);
    }

    public void ShiftReportFromPrinter(string shift, string operatorId, bool reset)
    {
      string Parameters = "";
      if (shift.Length > 8)
        shift = shift.Substring(0, 8);
      this.AddCommandParameter(ref Parameters, "sh", shift);
      if (reset)
        this.AddCommandParameter(ref Parameters, "zr", "1");
      else
        this.AddCommandParameter(ref Parameters, "zr", "0");
      this.SendFrame("shiftrep", Parameters);
    }

    public void ShiftReportFromDatabase(string shift, string operatorId, DateTime dateFrom, DateTime dateTo, List<OFPPayment> payments, Decimal insertCashValue, Decimal takeoutCashValue, Decimal cashState, Decimal depositInValue, Decimal depositOutValue, int fiscalizedReceiptsCount, int cancelledReceiptsCount, int stornoItemsCount)
    {
      throw new NotImplementedException();
    }

    public void PrintKP(string documentNr, Decimal value, string operatorId, string contractor)
    {
      string Parameters = "";
      long num = (long) (value * new Decimal(100));
      this.AddCommandParameter(ref Parameters, "kw", num.ToString());
      this.AddCommandParameter(ref Parameters, "wp", "1");
      this.SendFrame("cash", Parameters);
    }

    public void PrintKW(string documentNr, Decimal value, string operatorId, string contractor)
    {
      string Parameters = "";
      long num = (long) (value * new Decimal(100));
      this.AddCommandParameter(ref Parameters, "kw", num.ToString());
      this.AddCommandParameter(ref Parameters, "wp", "0");
      this.SendFrame("cash", Parameters);
    }

    public void OpenDrawer()
    {
      try
      {
        this.SendFrame("opendrwr", "");
      }
      catch
      {
        this.Close();
        throw;
      }
    }

    public string Name { get; set; }

    public string PrinterId
    {
      get
      {
        return this.printerId;
      }
      set
      {
        if (value.Length > 8)
          this.printerId = value.Substring(0, 8);
        else
          this.printerId = value;
      }
    }

    public string OperatorId
    {
      get
      {
        return this.operatorId;
      }
      set
      {
        if (value.Length > 32)
          this.operatorId = value.Substring(0, 32);
        else
          this.operatorId = value;
      }
    }

    public List<KeyValuePair<OFPVatRate, int>> TaxRates
    {
      get
      {
        List<KeyValuePair<OFPVatRate, int>> keyValuePairList = new List<KeyValuePair<OFPVatRate, int>>();
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 1, 2200));
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 2, 700));
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 3, 600));
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 4, 500));
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 5, 300));
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 6, 0));
        keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 7, -1));
        return keyValuePairList;
      }
    }

    public List<OFPPayment> AvailablePayments
    {
      get
      {
        List<OFPPayment> ofpPaymentList = new List<OFPPayment>();
        ofpPaymentList.Add(new OFPPayment(0, "Gotówka", -1L));
        ofpPaymentList.Add(new OFPPayment(2, "Karta", -1L));
        ofpPaymentList.Add(new OFPPayment(3, "Czek", -1L));
        ofpPaymentList.Add(new OFPPayment(4, "Bon", -1L));
        ofpPaymentList.Add(new OFPPayment(5, "Kredyt", -1L));
        ofpPaymentList.Add(new OFPPayment(6, "Inna", -1L));
        ofpPaymentList.Add(new OFPPayment(7, "Voucher", -1L));
        ofpPaymentList.Add(new OFPPayment(8, "Konto klienta", -1L));
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

    public string Config
    {
      get
      {
        Trace.WriteLine(this.id.ToString() + " - get_Config");
        string str = "";
        foreach (KeyValuePair<string, string> setting in this.settings)
          str = str + setting.Key + "=" + setting.Value + ",";
        if (str.EndsWith(","))
          str = str.Substring(0, str.Length - 1);
        return str;
      }
      set
      {
        Trace.WriteLine(this.id.ToString() + " - set_Config");
        this.settings.Clear();
        foreach (string str in value.Split(",;".ToCharArray()))
        {
          if (str.Split("=".ToCharArray()).Length == 2)
            this.settings.Add(str.Split("=".ToCharArray())[0], str.Split("=".ToCharArray())[1]);
        }
      }
    }

    public List<OFPProperty> Properties
    {
      get
      {
        List<OFPProperty> ofpPropertyList = new List<OFPProperty>();
        ofpPropertyList.Add(new OFPProperty("port", "Port"));
        ofpPropertyList.Add(new OFPProperty("baudrate", "Prędkość portu"));
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
          throw new OFPException("Właściwość nie jest obsługiwana przez sterownik");
      }
    }

    public void SetValue(string name, string value)
    {
      if (this.settings.ContainsKey(name))
        this.settings[name] = value;
      else
        this.settings.Add(name, value);
      if (value != null)
        return;
      this.settings.Remove(name);
    }

    public string GetValue(string name)
    {
      if (this.settings.ContainsKey(name))
        return this.settings[name];
      return this.GetDefaultValue(name);
    }

    public bool SupportsFeature(OFPDriverFeature df)
    {
      return df == null;
    }

    public void Dispose()
    {
      Trace.WriteLine(this.id.ToString() + " - Dispose");
      if (this.port == null)
        return;
      ((Component) this.port).Dispose();
      this.port = (OFPSerialPort) null;
    }

    private void packRemove(List<OFPPackItem> packList, OFPPackItem pack)
    {
      bool flag = false;
      using (List<OFPPackItem>.Enumerator enumerator = packList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          OFPPackItem current = enumerator.Current;
          if (current.get_Number() == pack.get_Number() && current.get_Price() == pack.get_Price() && current.get_Amount() >= pack.get_Amount())
          {
            OFPPackItem ofpPackItem = current;
            ofpPackItem.set_Amount(ofpPackItem.get_Amount() - pack.get_Amount());
            if (current.get_Amount() == 0)
              packList.Remove(current);
            flag = true;
            break;
          }
        }
      }
      if (!flag)
        throw new OFPException("Wystąpił problem z wycofaniem kaucji");
    }

    private long NormalizeDiscount(long totalValue, OFPDiscount discountType, long discountValue)
    {
      switch ((int) discountType)
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
          throw new OFPException("Unknown discountType");
      }
    }

    public string GetDefaultValue(string name)
    {
      if (name == "baudrate")
        return "9600";
      return (string) null;
    }

    public bool PrintSystemNumber
    {
      get
      {
        return this.printSystemNumber;
      }
      set
      {
        this.printSystemNumber = value;
      }
    }

    public bool PrintPayments
    {
      get
      {
        return this.printPayments;
      }
      set
      {
        this.printPayments = value;
      }
    }

    public bool PrintPack
    {
      get
      {
        return this.printPack;
      }
      set
      {
        this.printPack = value;
      }
    }

    public bool PrintOperatorId
    {
      get
      {
        return this.printOperatorId;
      }
      set
      {
        this.printOperatorId = value;
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

    public List<KeyValuePair<OFPVatRate, int>> LoadTaxRates()
    {
      NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
      numberFormatInfo.CurrencyDecimalSeparator = ".";
      numberFormatInfo.CurrencyGroupSeparator = "";
      numberFormatInfo.NumberDecimalSeparator = ".";
      numberFormatInfo.NumberGroupSeparator = "";
      List<KeyValuePair<OFPVatRate, int>> keyValuePairList = new List<KeyValuePair<OFPVatRate, int>>();
      string str1 = this.SendFrame("vatget", "");
      char[] chArray = new char[1]{ '\t' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Length > 2)
        {
          int num = (int) (Decimal.Parse(str2.Substring(2, str2.Length - 2).Replace(",", "."), (IFormatProvider) numberFormatInfo) * new Decimal(100));
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
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 1, num));
              continue;
            case "vb":
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 2, num));
              continue;
            case "vc":
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 3, num));
              continue;
            case "vd":
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 4, num));
              continue;
            case "ve":
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 5, num));
              continue;
            case "vf":
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 6, num));
              continue;
            case "vg":
              keyValuePairList.Add(new KeyValuePair<OFPVatRate, int>((OFPVatRate) 7, num));
              continue;
            default:
              continue;
          }
        }
      }
      return keyValuePairList;
    }

    public void SaveTaxRates(List<KeyValuePair<OFPVatRate, int>> TaxRatesList)
    {
      NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
      numberFormatInfo.CurrencyDecimalSeparator = ",";
      numberFormatInfo.CurrencyGroupSeparator = "";
      numberFormatInfo.NumberDecimalSeparator = ",";
      numberFormatInfo.NumberGroupSeparator = "";
      string Parameters = "";
      using (List<KeyValuePair<OFPVatRate, int>>.Enumerator enumerator = TaxRatesList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<OFPVatRate, int> current = enumerator.Current;
          string ParameterValue = ((Decimal) current.Value / new Decimal(100)).ToString((IFormatProvider) numberFormatInfo);
          if (current.Value == -1)
            ParameterValue = "100";
          switch (current.Key - 1)
          {
            case 0:
              this.AddCommandParameter(ref Parameters, "va", ParameterValue);
              continue;
            case 1:
              this.AddCommandParameter(ref Parameters, "vb", ParameterValue);
              continue;
            case 2:
              this.AddCommandParameter(ref Parameters, "vc", ParameterValue);
              continue;
            case 3:
              this.AddCommandParameter(ref Parameters, "vd", ParameterValue);
              continue;
            case 4:
              this.AddCommandParameter(ref Parameters, "ve", ParameterValue);
              continue;
            case 5:
              this.AddCommandParameter(ref Parameters, "vf", ParameterValue);
              continue;
            case 6:
              this.AddCommandParameter(ref Parameters, "vg", ParameterValue);
              continue;
            default:
              continue;
          }
        }
      }
      DateTime now = DateTime.Now;
      this.AddOptionalCommandParameter(ref Parameters, "da", now.Year.ToString() + "-" + (object) now.Month + "-" + (object) now.Day);
      this.SendFrame("vatset", Parameters);
    }

    public DateTime GetTime()
    {
      string str1 = this.SendFrame("rtcget", "");
      char[] chArray = new char[1]{ '\t' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Length > 2)
        {
          string lower = str2.Substring(0, 2).ToLower();
          string s = str2.Substring(2);
          switch (lower)
          {
            case "da":
              return DateTime.ParseExact(s, "yyyy-MM-dd;hh:mm", (IFormatProvider) CultureInfo.InvariantCulture);
            default:
              continue;
          }
        }
      }
      throw new OFPException("Nie można odczytać czasu z drukarki");
    }

    public int GetLastFiscalReportNum()
    {
      int rd = 0;
      int hn = 0;
      int bn = 0;
      int fn = 0;
      string nu = "";
      this.GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
      return rd;
    }

    public int GetLastPrintoutHeaderNum()
    {
      int rd = 0;
      int hn = 0;
      int bn = 0;
      int fn = 0;
      string nu = "";
      this.GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
      return hn;
    }

    public int GetLastRecepitNum()
    {
      int rd = 0;
      int hn = 0;
      int bn = 0;
      int fn = 0;
      string nu = "";
      this.GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
      return bn;
    }

    public int GetLastInvoiceNum()
    {
      int rd = 0;
      int hn = 0;
      int bn = 0;
      int fn = 0;
      string nu = "";
      this.GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
      return fn;
    }

    public string GetPrinterSerialNum()
    {
      int rd = 0;
      int hn = 0;
      int bn = 0;
      int fn = 0;
      string nu = "";
      this.GetCountersStatus(ref rd, ref hn, ref bn, ref fn, ref nu);
      return nu;
    }

    private void GetCountersStatus(ref int rd, ref int hn, ref int bn, ref int fn, ref string nu)
    {
      string str1 = this.SendFrame("scnt", "");
      char[] chArray = new char[1]{ '\t' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Length > 2)
        {
          string lower = str2.Substring(0, 2).ToLower();
          string s = str2.Substring(2);
          switch (lower)
          {
            case nameof (rd):
              rd = int.Parse(s);
              continue;
            case nameof (hn):
              hn = int.Parse(s);
              continue;
            case nameof (bn):
              bn = int.Parse(s);
              continue;
            case nameof (fn):
              fn = int.Parse(s);
              continue;
            case nameof (nu):
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
      this.NonFiscalPrintId = printId;
      string Parameters = "";
      this.AddCommandParameter(ref Parameters, "fn", this.NonFiscalPrintId.ToString());
      this.AddCommandParameter(ref Parameters, "fh", headerId.ToString());
      this.AddOptionalCommandParameter(ref Parameters, "al", additionaDescription);
      this.SendFrame("formstart", Parameters);
    }

    public void NonFiscalLinePrint(int lineId, string lineData)
    {
      string Parameters = "";
      this.AddCommandParameter(ref Parameters, "fn", this.NonFiscalPrintId.ToString());
      this.AddCommandParameter(ref Parameters, "fl", lineId.ToString());
      this.AddOptionalCommandParameter(ref Parameters, "s1", lineData);
      this.SendFrame("formline", Parameters);
    }

    public void CloseNonFiscalPrint()
    {
      string Parameters = "";
      this.AddCommandParameter(ref Parameters, "fn", this.NonFiscalPrintId.ToString());
      this.SendFrame("formend", Parameters);
      this.NonFiscalPrintId = 0;
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
        return this.communicationLog;
      }
      set
      {
        this.communicationLog = value;
        if (this.port == null)
          return;
        this.port.set_CommunicationLog(value);
      }
    }

    public void PrintImageSK(int number, string station, string justify, int mode)
    {
      throw new NotSupportedException("Metoda nie obsługiwana przez ten sterownik drukarki");
    }
  }
}
