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
        float x;
        float y;
        Bitmap Img { get; set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public GonImageProcessing(Bitmap _img)
        {
            Img = _img;
        }
        public void calcCross()
        {
            FindBlobs();
            // X = Img.Width / 2 - x;
            // Y = Img.Height / 2 - y;
            X =  x;
            Y =  y;
        }
        public void FindBlobs()
        {
            BlobCounterBase bc = new BlobCounter();
            bc.FilterBlobs = true;
            bc.MinWidth = 5;
            bc.MinHeight = 5;
            // process binary image
            bc.ProcessImage(Img);
            Blob[] blobs = bc.GetObjects(Img, false);
            if (blobs.Length > 0)
            {
                x = blobs[0].Rectangle.X + blobs[0].Rectangle.Width / 2;
                y = blobs[0].Rectangle.Y + blobs[0].Rectangle.Height / 2;
            }
            // x =blobs[0].CenterOfGravity.X;
            //y = blobs[0].CenterOfGravity.Y;
        }
    }
}
