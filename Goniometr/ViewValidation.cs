using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goniometr
{
   public class ViewValidation
    {
        public string[] SerialPortsArray { get; private set; }
        public string SerialPortSelected { get; set; }
        public string BaudRateValue { get; set; }
        public string MaschtabX { get; set; }
        public string MaschtabY { get; set; }
        public System.Drawing.Color isCoordinateXValid { get; set; }
        public ViewValidation()
        {
            SerialPortsArray = SerialPort.GetPortNames();
            BaudRateValue = "9600";
            isCoordinateXValid = System.Drawing.Color.Red;
        }

    }
}
