using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    abstract class UartCoordinateReaderRequest : IUartMessage
    {
        protected ushort StartRequest = 0xAACC;
        protected byte Command { get; set; }
        protected byte SecondCommand { get; set; }
        protected byte BodyCommandFirst { get; set; }
        protected byte BodyCommandSecond { get; set; }
        public byte[] RequestBytes { get; private set; }
        public virtual void GetMesBytes()
        {

        }
        public bool Validate()
        {
            return true;
        }
    }

    class UartCoordinateReaderRequestMaschtab: UartCoordinateReaderRequest
    {
        public byte Axis { get; set; }
        public float Val { get; set; }
        public UartCoordinateReaderRequestMaschtab(byte _axis, float _val)
        {
            Command=Axis =_axis;
            Val = _val;
            Array.Copy(BitConverter.GetBytes(Val),RequestBytes,4,
        }
    }





    abstract class UartCoordinateReaderAnswer : IUartMessage
    {

        public void GetMesBytes()
        {
            
        }

        public bool Validate()
        {
            return true;
        }
    }
}
