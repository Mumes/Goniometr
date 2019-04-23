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
    class ICUsbCamera:ApiDevice
    {
        ICImagingControl IcCam {get; set; }
        public ICUsbCamera(ICImagingControl _icCam)
        {
            this.IcCam = _icCam;
        }
        public override void Init()
        {
        }
        public override void Close()
        {
        }
    }
}
