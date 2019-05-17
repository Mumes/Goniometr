using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GonCommand;

namespace Goniometr
{
   public class MainWindowViewModel : INotifyPropertyChanged
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

        public MainWindowViewModel()
        {
            SerialPortsArray = SerialPort.GetPortNames();
            BaudRateValue = "57600";
            isCoordinateXValid = System.Drawing.Color.Red;
        }

        public UartCoordinateReader Ucr { get; private set; }
        public ICUsbCamera ICUsbCam { get; private set; }
        TIS.Imaging.ICImagingControl iCUsbCamControl;
        public TIS.Imaging.ICImagingControl ICUsbCamControl
        {
            get
            {
                return iCUsbCamControl;
            }

            set
            {
                iCUsbCamControl = value;
                ICUsbCam = new ICUsbCamera(iCUsbCamControl);
            }
        }

        private CommandsCoordinates initCommand;
        public CommandsCoordinates InitCommand
        {
            get
            {
                return initCommand ??
                    (initCommand = new CommandsCoordinates(obj =>
                    {
                        //Ucr = new UartCoordinateReader(VV.SerialPortSelected, Convert.ToInt32(VV.BaudRateValue)
                        //    ,Parity.None,8,StopBits.Two);
                        //Ucr.Init();
                        ICUsbCam.Init();
                    }
                       ));
            }
        }
        private CommandsCoordinates readCommand;
        public CommandsCoordinates ReadCommand
        {
            get
            {
                return readCommand ??
                    (readCommand = new CommandsCoordinates(obj =>
                    {
                        Ucr.GetCoordinates();
                        CX = Ucr.CoordinateX.ToString();
                        CY = Ucr.CoordinateY.ToString();
                    }
                       ));
            }
        }

       

    }
}
