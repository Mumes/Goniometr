using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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
                IcCam.OverlayBitmapPosition = TIS.Imaging.PathPositions.Device;
                IcCam.OverlayBitmapAtPath[PathPositions.Device].ColorMode = OverlayColorModes.Color;
                IcCam.LivePrepared +=icCam_LivePrepared;
                IcCam.LiveStart();
            }
        }
        private void SetOverlay()
        {
            
            IcCam.LiveCaptureContinuous = true;
            TIS.Imaging.OverlayBitmap ob = IcCam.OverlayBitmapAtPath[PathPositions.Device];
            // Enable the overlay bitmap for drawing.
            ob.Enable = true;

            // Set magenta as dropout color.
            ob.DropOutColor = Color.Magenta;

            // Fill the overlay bitmap with the dropout color.
            ob.Fill(ob.DropOutColor);
            ob.FontTransparent = true;
            ob.DrawText(Color.Red, 10, 10, "Copyright trademark incorporated all rights reserved");
            ob.DrawFrameRect(Color.Red, IcCam.Width/2-40, IcCam.Height / 2 - 40, IcCam.Width / 2 + 40, IcCam.Height / 2 + 40);
            
        }

        private void icCam_LivePrepared(object sender, EventArgs e)
        {

            SetOverlay();


        }
        public override void Close()
        {
        }
    }
}
