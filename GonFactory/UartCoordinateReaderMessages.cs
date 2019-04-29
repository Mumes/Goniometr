using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace GonCommand
{
    /// <summary>
    /// Абстрактный класс для сообщения-запроса для устройства чтения координат
    /// </summary>
    abstract class UartCoordinateReaderRequest : IUartMessage
    {
        public int CountBytes { get; private set; }
        protected const ushort StartRequest = 0xAACC;
        protected byte Command { get; set; }
        protected byte SecondCommand { get; set; }
        protected byte[] BodyCommandFirst { get; set; }
        protected byte[] BodyCommandSecond { get; set; }
        public byte[] MesBytes { get; private set; }

        public UartCoordinateReaderRequest()
        {
            CountBytes = 11;
            BodyCommandFirst = new byte[4];
            BodyCommandSecond = new byte[4];
            MesBytes = new byte[CountBytes];
        }
        public virtual void GetMesBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(StartRequest);
                writer.Write(Command);
               // writer.Write(SecondCommand);
                writer.Write(BodyCommandFirst);
                writer.Write(BodyCommandSecond);
                MesBytes = stream.ToArray();
            }
        }
        public bool Validate()
        {
            return true;
        }
    }
    /// <summary>
    /// Сообщение-запрос на установку масштабного кожффициента
    /// </summary>
    class UartCoordinateReaderRequestMaschtab: UartCoordinateReaderRequest
    {
        public byte Axis { get; set; }
        public float Val { get; set; }
        public UartCoordinateReaderRequestMaschtab(byte _axis, float _val)
        {
            Command=Axis =_axis;
            Val = _val;
            BodyCommandSecond = BitConverter.GetBytes(Val);
            GetMesBytes();
        }
    }

    class UartCoordinateReaderRequestCoordinates : UartCoordinateReaderRequest
    {
        public UartCoordinateReaderRequestCoordinates()
        {
            Command = 0x07;
            GetMesBytes();
        }
    }

    class UartCoordinateReaderRequestState : UartCoordinateReaderRequest
    {
        public UartCoordinateReaderRequestState()
        {
            Command = 0x03;
            GetMesBytes();
        }
    }


    abstract class UartCoordinateReaderAnswer : IUartMessage
    { 
        public byte[] MesBytes { get; set; }
        public  int CountBytes { get; protected set; }
        public bool IsValid { get; private set; }
        protected const byte StartAnswer = 0x0d;
        public UartCoordinateReaderAnswer()
        {
            CountBytes = 9;
            MesBytes = new byte[CountBytes];
        }
        public UartCoordinateReaderAnswer(byte[] _ans)
        {
            _ans = MesBytes;
            CountBytes = 9;
            MesBytes = new byte[CountBytes];
        }
        public virtual bool Validate()
        {
            if (MesBytes[0]==StartAnswer)
                return IsValid =true;
            else
                return IsValid=false;
        }
    }
    class UartCoordinateReaderAnswerMaschtab : UartCoordinateReaderAnswer
    {

        public UartCoordinateReaderAnswerMaschtab() 
        {
        }
        
        public UartCoordinateReaderAnswerMaschtab(byte[] _ans) : base(_ans)
        {
        }
    }
    class UartCoordinateReaderAnswerCoordinates : UartCoordinateReaderAnswer
    {
        public float CoordinateX { get; private set; }
        public float CoordinateY { get; private set; }
        public UartCoordinateReaderAnswerCoordinates()
        {
        }

        public UartCoordinateReaderAnswerCoordinates(byte[] _ans) : base(_ans)
        {
            Validate();
            CoordinateX = BitConverter.ToSingle(_ans,1);
            CoordinateY = BitConverter.ToSingle(_ans, 5);
        }
    }
    class UartCoordinateReaderAnswerState : UartCoordinateReaderAnswer
    {
        public bool IsButtonSet { get; private set; }     
        public bool IsCoordinateXValid { get; private set; }
        public bool IsCoordinateYValid { get; private set; }

        public UartCoordinateReaderAnswerState()
        { }
        public UartCoordinateReaderAnswerState(byte[] _ans) : base(_ans)
        {
            Validate();
            if (MesBytes[1] == 0x73) IsCoordinateXValid = true;
            else if(MesBytes[1] == 0x63) IsCoordinateXValid = false;

            if ((MesBytes[2]&0xFB) == 0x73) IsCoordinateXValid = true;
            else if ((MesBytes[2] & 0xFB) == 0x63) IsCoordinateXValid = false;

            if ((MesBytes[2] & 0x04) == 0x04) IsButtonSet = true;
            else IsButtonSet = false;
        }

    }

}
