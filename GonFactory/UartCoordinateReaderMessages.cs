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
        protected byte Command { get; set;}
        public byte[] RequestBytes { get; private set;}
        public void GetMesBytes()
        {
            
        }

        public bool Validate()
        {
            return true;
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
