// Decompiled with JetBrains decompiler
// Type: OnlineFPCommon.OFPSerialPort
// Assembly: OnlineFPCommon, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 96CC98E8-1BF6-4466-B7FE-A0A74E8FCBEF
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\OnlineFPCommon.dll

using System.IO.Ports;

namespace OnlineFPCommon
{
  public class OFPSerialPort : SerialPort
  {
    private string driverName = "";
    private OFPLogger logger;
    private bool communicationLog;

    public bool CommunicationLog
    {
      get
      {
        return this.communicationLog;
      }
      set
      {
        this.communicationLog = value;
      }
    }

    private bool UseLog()
    {
      if (this.communicationLog && this.logger == null)
      {
        this.logger = new OFPLogger();
        this.logger.LogOperation("Start log " + this.driverName + " " + this.PortName + " " + (object) this.BaudRate);
      }
      return this.communicationLog;
    }

    public OFPSerialPort(string driverName, string portName)
      : base(portName)
    {
      this.driverName = driverName;
    }

    public OFPSerialPort(string driverName, string portName, int baudRate)
      : base(portName, baudRate)
    {
      this.driverName = driverName;
    }

    public OFPSerialPort(string driverName, string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
      : base(portName, baudRate, parity, dataBits, stopBits)
    {
      this.driverName = driverName;
    }

    public new void Close()
    {
      if (this.UseLog())
        this.logger.LogOperation(nameof (Close));
      base.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (this.UseLog())
      {
        this.logger.LogOperation("Dispose(" + (object) disposing + ")");
        this.logger.Dispose();
        this.logger = (OFPLogger) null;
      }
      base.Dispose(disposing);
    }

    public new void Open()
    {
      if (this.UseLog())
        this.logger.LogOperation(nameof (Open));
      base.Open();
    }

    public int ReadExact(byte[] buffer, int offset, int count)
    {
      for (int index = 0; index < count; ++index)
        buffer[offset + index] = (byte) base.ReadByte();
      if (this.UseLog())
        this.logger.LogDataOperation(buffer, offset, count, false);
      return count;
    }

    public new int Read(byte[] buffer, int offset, int count)
    {
      int lenght = base.Read(buffer, offset, count);
      if (this.UseLog())
        this.logger.LogDataOperation(buffer, offset, lenght, false);
      return lenght;
    }

    public new int Read(char[] buffer, int offset, int count)
    {
      int count1 = base.Read(buffer, offset, count);
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(buffer, offset, count1);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, false);
      }
      return count1;
    }

    public new int ReadByte()
    {
      int num = base.ReadByte();
      if (this.UseLog())
      {
        byte[] dataSrc = new byte[1]{ (byte) num };
        this.logger.LogDataOperation(dataSrc, 0, dataSrc.Length, false);
      }
      return num;
    }

    public new int ReadChar()
    {
      int num = base.ReadChar();
      if (this.UseLog())
      {
        byte[] dataSrc = new byte[1]{ (byte) num };
        this.logger.LogDataOperation(dataSrc, 0, dataSrc.Length, false);
      }
      return num;
    }

    public new string ReadExisting()
    {
      string s = base.ReadExisting();
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(s);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, false);
      }
      return s;
    }

    public new string ReadLine()
    {
      string s = base.ReadLine();
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(s);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, false);
      }
      return s;
    }

    public new string ReadTo(string value)
    {
      string s = base.ReadTo(value);
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(s);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, false);
      }
      return s;
    }

    public new void Write(string text)
    {
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(text);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, true);
      }
      base.Write(text);
    }

    public new void Write(byte[] buffer, int offset, int count)
    {
      if (this.UseLog())
        this.logger.LogDataOperation(buffer, offset, count, true);
      base.Write(buffer, offset, count);
    }

    public new void Write(char[] buffer, int offset, int count)
    {
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(buffer, offset, count);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, true);
      }
      base.Write(buffer, offset, count);
    }

    public new void WriteLine(string text)
    {
      if (this.UseLog())
      {
        byte[] bytes = this.Encoding.GetBytes(text);
        this.logger.LogDataOperation(bytes, 0, bytes.Length, true);
      }
      base.WriteLine(text);
    }
  }
}
