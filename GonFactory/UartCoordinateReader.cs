using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace GonCommand
{
    class UartCoordinateReader:UartDevice
    {
        public UartCoordinateReader(string _com = "COM1", int _baud = 9600, Parity _parity = Parity.None,
            int _dataBits = 8, StopBits _stpBits = StopBits.One) : base(_com,_baud , _parity, _dataBits,_stpBits )
        {
           

        }
        public override void Init()
        {
            base.Init();
        }
    }
}
