using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using System.Drawing.Imaging;
using System.Drawing;

namespace GonAForge
{
    public class GonImageProcessing
    {
        
        Bitmap Img { get; set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public GonImageProcessing(Bitmap _img)
        {
            Img = _img;
        }
        public void CalcCross()
        {
            Binarization();
            FindBlobs();
            // X = Img.Width / 2 - x;
            // Y = Img.Height / 2 - y;
           
        }
        private void Binarization()
        {      
            ImageConverter converter = new ImageConverter();
            byte[] test2 = (byte[])converter.ConvertTo(Img, typeof(byte[]));

            for (int i= 0;i<test2.Length;i++)
            {
                if (test2[i] >= 127) test2[i] = 255;
                else test2[i] = 0;
            }
            System.Drawing.Image iImg = (System.Drawing.Image)converter.ConvertFrom(test2);
            Img.Save(@"D:\gon.bmp", ImageFormat.Bmp);
        }

        public void FindBlobs()
        {
            BlobCounterBase bc = new BlobCounter();
            bc.FilterBlobs = true;
            bc.MinWidth = 1;
            bc.MinHeight = 1;
            bc.MaxHeight = 1000;
            bc.MaxWidth = 1000;
            // process binary image
            bc.ProcessImage(Img);
            Blob[] blobs = bc.GetObjects(Img, false);
            if (blobs.Length > 0)
            {
                X = blobs[0].Rectangle.X;
                Y = blobs[0].Rectangle.Y;
                Width = blobs[0].Rectangle.Width;
                Height = blobs[0].Rectangle.Height;
            }
            // x =blobs[0].CenterOfGravity.X;
            //y = blobs[0].CenterOfGravity.Y;
        }
    }
}
