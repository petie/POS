using System.IO.Ports;

namespace Posnet
{
    public class PosnetSerialPort : SerialPort
    {
        private string driverName = "";

        public PosnetSerialPort(string driverName, string portName)
          : base(portName)
        {
            this.driverName = driverName;
        }

        public PosnetSerialPort(string driverName, string portName, int baudRate)
          : base(portName, baudRate)
        {
            this.driverName = driverName;
        }

        public PosnetSerialPort(string driverName, string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
          : base(portName, baudRate, parity, dataBits, stopBits)
        {
            this.driverName = driverName;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public new void Open()
        {
            base.Open();
        }

        public int ReadExact(byte[] buffer, int offset, int count)
        {
            for (int index = 0; index < count; ++index)
                buffer[offset + index] = (byte)base.ReadByte();
            return count;
        }

        public new int Read(byte[] buffer, int offset, int count)
        {
            int lenght = base.Read(buffer, offset, count);
            return lenght;
        }

        public new int Read(char[] buffer, int offset, int count)
        {
            int count1 = base.Read(buffer, offset, count);
            return count1;
        }

        public new int ReadByte()
        {
            int num = base.ReadByte();
            return num;
        }

        public new int ReadChar()
        {
            int num = base.ReadChar();
            return num;
        }

        public new string ReadExisting()
        {
            string s = base.ReadExisting();

            return s;
        }

        public new string ReadLine()
        {
            string s = base.ReadLine();
            return s;
        }

        public new string ReadTo(string value)
        {
            string s = base.ReadTo(value);
            return s;
        }

        public new void Write(string text)
        {
            base.Write(text);
        }

        public new void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
        }

        public new void Write(char[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
        }

        public new void WriteLine(string text)
        {
            base.WriteLine(text);
        }
    }
}
