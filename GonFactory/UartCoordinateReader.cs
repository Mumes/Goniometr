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
        public void setMaschtab(byte _axis,float _maschtab)
        {
            UartCoordinateReaderRequestMaschtab s = new UartCoordinateReaderRequestMaschtab(_axis,_maschtab);
            Sp.Write(s.RequestBytes,0,s.RequestBytes.Length);
            byte[] ans;
           //Sp.Read(ans,0,)
            UartCoordinateReaderAnswerMaschtab r = new UartCoordinateReaderAnswerMaschtab();
            Sp.Read()

        }
    }
}
