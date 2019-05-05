using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TIS.Imaging;
namespace GonCommand
{
    /// <summary>
    /// Класс для USB камеры фирмы IC
    /// </summary>
    public class ICUsbCamera:ApiDevice
    {
        public ICImagingControl IcCam {get; set; }
        public ICUsbCamera(ICImagingControl _icCam)
        {
            this.IcCam = _icCam;
        }
        public override void Init()
        {
            if (IcCam.Devices.Length > 0)
            {
                IcCam.Device = IcCam.Devices[0];
                IcCam.LiveStart();
            }
        }
        public override void Close()
        {
        }
    }
}
