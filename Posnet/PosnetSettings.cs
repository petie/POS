using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Posnet
{
    public class PosnetSettings
    {
        public string Port { get; set; }
        public int BaudRate { get; set; }
        public Handshake Handshake { get; set; }
        public string Name { get; set; }
    }
}
