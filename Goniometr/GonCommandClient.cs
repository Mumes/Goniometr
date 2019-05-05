using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GonCommand;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Goniometr
{
    public class GonCommandClient
    {
        public ViewValidation VV { get; private set; }
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
                    (initCommand = new CommandsCoordinates(obj=>
                    {
                        //Ucr = new UartCoordinateReader(VV.SerialPortSelected, Convert.ToInt32(VV.BaudRateValue)
                        //    ,Parity.None,8,StopBits.Two);
                        //Ucr.Init();
                        ICUsbCam.Init();
                    }                     
                       ) );
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
                        VV.CX = Ucr.CoordinateX.ToString();
                        VV.CY = Ucr.CoordinateY.ToString();
                    }
                       ));
            }
        }

        public GonCommandClient()
        {
            VV = new ViewValidation();
        }
    }
}
