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
    public class UartCoordinateReader:UartDevice
    {
        /// <summary>
        /// 
        /// </summary>
        public float MaschtabX {get; private set;}
        public bool IsMaschtabXSet {get; private set;}
        public float MaschtabY {get; private set;}
        public bool IsMaschtabYSet {get; private set;}
        public float CoordinateX {get; private set;}
        public bool IsCoordinateXValid {get; private set;}
        public float CoordinateY {get; private set;}
        public bool IsCoordinateYValid {get; private set;}
        public bool IsButtonSet {get; private set;}

        public UartCoordinateReader(string _com = "COM1", int _baud = 9600, Parity _parity = Parity.None,
            int _dataBits = 8, StopBits _stpBits = StopBits.One) : base(_com,_baud , _parity, _dataBits,_stpBits )
        {          

        }
        public override void Init()
        {
            base.Init();
            //Sp.Write(new byte[] { 0xAA, 0xCC, 0x01, 0x01, 0x00, 0x00, 0x00, 0x38, 0x9F, 0x69, 0x72 }, 0, 11);
            //Sp.Read(new byte[8], 0, 8);
        }
        public void SetMaschtab(byte _axis,float _maschtab)
        {         
            UartCoordinateReaderRequestMaschtab s = new UartCoordinateReaderRequestMaschtab(_axis,_maschtab);
            Sp.Write(s.MesBytes,0,s.MesBytes.Length);
           
            UartCoordinateReaderAnswerMaschtab r = new UartCoordinateReaderAnswerMaschtab();

            ReadWithTimeout(r);
            r.Validate();
            if (r.IsValid && _axis == 0x01)
                IsMaschtabXSet = true;
            else if(!r.IsValid && _axis == 0x01)
                IsMaschtabXSet = false;

            if (r.IsValid && _axis == 0x02)
                IsMaschtabYSet = true;
            else if (!r.IsValid && _axis == 0x02)
                IsMaschtabYSet = false;             
        }
        public void GetCoordinates()
        {
            UartCoordinateReaderRequestCoordinates s = new UartCoordinateReaderRequestCoordinates();
            Sp.Write(s.MesBytes, 0, s.MesBytes.Length);
            UartCoordinateReaderAnswerCoordinates r = new UartCoordinateReaderAnswerCoordinates();
            ReadWithTimeout(r);
            r.Validate();
            CoordinateX = r.CoordinateX;
            CoordinateY = r.CoordinateY;
        }

        public void GetStatus()
        {
            UartCoordinateReaderRequestState s = new UartCoordinateReaderRequestState();
            Sp.Write(s.MesBytes, 0, s.MesBytes.Length);
            UartCoordinateReaderAnswerState r = new UartCoordinateReaderAnswerState();
            ReadWithTimeout(r);
            r.Validate();
            IsCoordinateXValid = r.IsCoordinateXValid;
            IsCoordinateYValid = r.IsCoordinateYValid;
        }
    }
}
