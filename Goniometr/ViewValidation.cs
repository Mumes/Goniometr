using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Goniometr
{
   public class ViewValidation : INotifyPropertyChanged
    {
        //properties that we get from view and send to model
        public string[] SerialPortsArray { get; private set; }
        public string SerialPortSelected { get; set; }
        public string BaudRateValue { get; set; }
        public string MaschtabX { get; set; }
        public string MaschtabY { get; set; }

        //properties that we get from model and send to view
        public System.Drawing.Color isCoordinateXValid { get; set; }
        private string cx;
        public string CX
        {
            get { return cx; }
            set { cx = value; NotifyPropertyChanged(); }
        }
        private string cy;
        public string CY
        {
            get { return cy; }

            set { cy = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewValidation()
        {
            SerialPortsArray = SerialPort.GetPortNames();
            BaudRateValue = "57600";
            isCoordinateXValid = System.Drawing.Color.Red;
        }

    }
}
