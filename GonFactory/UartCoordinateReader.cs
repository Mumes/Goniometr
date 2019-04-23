using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace GonCommand
{
    /// <summary>
    /// Описание работы устройства чтения координат
    /// </summary>
    class UartCoordinateReader:UartDevice
    {
        /// <summary>
        /// 
        /// </summary>
        public float MaschtabX {get; private set;}
        public bool isMaschtabXSet {get; private set;}
        public float MaschtabY {get; private set;}
        public bool isMaschtabYSet {get; private set;}
        public float CoordinateX {get; private set;}
        public bool isCoordinateXValid {get; private set;}
        public float CoordinateY {get; private set;}
        public bool isCoordinateYValid {get; private set;}
        public bool isButtonSet {get; private set;}

        public UartCoordinateReader(string _com = "COM1", int _baud = 9600, Parity _parity = Parity.None,
            int _dataBits = 8, StopBits _stpBits = StopBits.One) : base(_com,_baud , _parity, _dataBits,_stpBits )
        {          

        }
        public override void Init()
        {
            base.Init();
        }
        public void SetMaschtab(byte _axis,float _maschtab)
        {         
            UartCoordinateReaderRequestMaschtab s = new UartCoordinateReaderRequestMaschtab(_axis,_maschtab);
            Sp.Write(s.RequestBytes,0,s.RequestBytes.Length);
           
            UartCoordinateReaderAnswerMaschtab r = new UartCoordinateReaderAnswerMaschtab();
            byte[] ans=new byte[UartCoordinateReaderAnswer.AnswerCount];
            Sp.Read(ans, 0, Sp.BytesToRead);
            r.SetMesBytes(ans);
            r.Validate();
            if (r.IsValid && _axis == 0x01)
                isMaschtabXSet = true;
            else
                isMaschtabXSet = false;

            if (r.IsValid && _axis == 0x02)
                isMaschtabYSet = true;
            else
                isMaschtabYSet = false;             
        }
    }
}
