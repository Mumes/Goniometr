using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TIS.Imaging;
using GonAForge;
using System.Drawing.Imaging;

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
               // Binarization();
                IcCam.OverlayBitmapPosition = TIS.Imaging.PathPositions.Display;
                IcCam.OverlayBitmapAtPath[PathPositions.Display].ColorMode = OverlayColorModes.Color;
                IcCam.LivePrepared +=IcCam_LivePrepared;
                IcCam.OverlayUpdate += IcCam_OverlayUpdate;
                IcCam.LiveStart();
            }
        }
        private void SetOverlay()
        {          
            IcCam.LiveCaptureContinuous = true;
            TIS.Imaging.OverlayBitmap ob = IcCam.OverlayBitmapAtPath[PathPositions.Display];
            // Enable the overlay bitmap for drawing.
            ob.Enable = true;

            // Set magenta as dropout color.
            ob.DropOutColor = Color.Magenta;

            // Fill the overlay bitmap with the dropout color.
            ob.Fill(ob.DropOutColor);
            ob.FontTransparent = true;
            ob.DrawText(Color.Red, 10, 10, "Copyright trademark incorporated all rights reserved");
            ob.DrawFrameRect(Color.Red, IcCam.Width/2-40, IcCam.Height / 2 - 40, IcCam.Width / 2 + 40, IcCam.Height / 2 + 40);
            ob.DrawLine(Color.Red, IcCam.Width / 2, IcCam.Height / 2 - 40, IcCam.Width / 2, 0);
            ob.DrawLine(Color.Red, IcCam.Width / 2, IcCam.Height / 2 + 40, IcCam.Width / 2, IcCam.Height);

            ob.DrawLine(Color.Red, 0, IcCam.Height / 2 , IcCam.Width / 2-40, IcCam.Height / 2);
            ob.DrawLine(Color.Red, IcCam.Width / 2+40, IcCam.Height / 2, IcCam.Width , IcCam.Height/2);
            ob.DrawLine(Color.Red, IcCam.Width / 2 - 10, IcCam.Height / 2, IcCam.Width / 2 + 10, IcCam.Height / 2);
            ob.DrawLine(Color.Red, IcCam.Width / 2 , IcCam.Height / 2+10, IcCam.Width / 2 , IcCam.Height / 2-10);
        }

        private void IcCam_LivePrepared(object sender, EventArgs e)
        {
            SetOverlay();
        }
        public override void Close()
        {
        }
        private Bitmap GetLiveImage()
        {
            ImageBuffer ImgBuffer;
            ImgBuffer = IcCam.ImageActiveBuffer;
            unsafe
            {
                int bufferSize = ImgBuffer.ActualDataSize;
                byte* pIn=ImgBuffer.GetImageData();
                while (bufferSize-- > 0)
                {
                    if (*pIn >= 127)
                    {
                        *pIn++ = 255;
                    }
                    else
                    {
                        *pIn++ = 0;
                    }
                }
            }
            return ImgBuffer.Bitmap;
        }
        int i = 0;
        private void IcCam_OverlayUpdate(object sender, TIS.Imaging.ICImagingControl.OverlayUpdateEventArgs e)
        {
            //// IcCam.LiveStop();
            
            TIS.Imaging.OverlayBitmap ob = IcCam.OverlayBitmapAtPath[PathPositions.Display];
            GonImageProcessing gip = new GonImageProcessing(GetLiveImage());
            SetOverlay();
            //ob.DrawFrameRect(Color.Blue, i, i, i+ 20, i + 20);
            //i++;
            gip.CalcCross();
            ob.DrawFrameRect(Color.Blue, 0, 0, 40, 40);
            ob.DrawFrameRect(Color.Blue, (int)gip.X, (int)gip.Y, (int)gip.X+ (int)gip.Width, (int)gip.Y + (int)gip.Height);
            // //IcCam.LiveStart();
        }
        private void Binarization()
        {
            TIS.Imaging.FrameFilter mFrameFilter;
            BinarizationFilter binFilterImpl = new BinarizationFilter();
           
           // Create a FrameFilter object wrapping the implementation
           mFrameFilter = IcCam.FrameFilterCreate(binFilterImpl);
            
            IcCam.DisplayFrameFilters.Add(mFrameFilter);
            mFrameFilter.BeginParameterTransfer();
            mFrameFilter.SetBoolParameter("enable", true);
            mFrameFilter.SetIntParameter("threshold", 127);
            mFrameFilter.EndParameterTransfer();
        }
    }
}
