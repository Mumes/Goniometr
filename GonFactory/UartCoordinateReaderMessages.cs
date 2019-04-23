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
            CountBytes = 12;
            BodyCommandFirst = new byte[4];
            BodyCommandSecond = new byte[4];
        }
        public virtual void GetMesBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Command);
                writer.Write(SecondCommand);
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
    }


    abstract class UartCoordinateReaderAnswer : IUartMessage
    { 
        public byte[] MesBytes { get; set; }
        public  int CountBytes { get; protected set; }
        public bool IsValid { get; private set; }
        protected const byte StartAnswer = 0x0d;
        public UartCoordinateReaderAnswer()
        {
            CountBytes = 8;
        }
        public UartCoordinateReaderAnswer(byte[] _ans)
        {
            _ans = MesBytes;
            CountBytes = 8;
        }
        public  void SetMesBytes(byte[] _ans)
        {
            _ans = MesBytes;
        }
        public void GetMesBytes()
        {

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
            CoordinateX = BitConverter.ToSingle(_ans,4);
            CoordinateY = BitConverter.ToSingle(_ans, 8);
        }
    }
    class UartCoordinateReaderAnswerState : UartCoordinateReaderAnswer
    {
    }

}
