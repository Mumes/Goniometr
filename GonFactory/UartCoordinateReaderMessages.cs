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
        protected const ushort StartRequest = 0xAACC;
        protected byte Command { get; set; }
        protected byte SecondCommand { get; set; }
        protected byte[] BodyCommandFirst { get; set; }
        protected byte[] BodyCommandSecond { get; set; }
        public byte[] RequestBytes { get; private set; }

        public UartCoordinateReaderRequest()
        {
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
                RequestBytes = stream.ToArray();
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


    abstract class UartCoordinateReaderAnswer : IUartMessage
    {
        public const int AnswerCount = 8;
        public byte[] AnswerBytes { get; private set; }
        protected const byte StartAnswer = 0x0d;
        public UartCoordinateReaderAnswer()
        {
            
        }
        public UartCoordinateReaderAnswer(byte[] _ans)
        {
            _ans = AnswerBytes;
        }
        public  void SetMesBytes(byte[] _ans)
        {
            _ans = AnswerBytes;
        }
        public void GetMesBytes()
        {

        }
        public virtual bool Validate()
        {
            if (AnswerBytes[1]==StartAnswer)
                return true;
            else
                return false;
        }
    }
    class UartCoordinateReaderAnswerMaschtab : UartCoordinateReaderAnswer
    {
        public bool IsValid { get; private set; }

        public UartCoordinateReaderAnswerMaschtab() 
        {
            
        }
        public override bool Validate()
        {
            return IsValid = base.Validate();
        }
        public UartCoordinateReaderAnswerMaschtab(byte[] _ans) : base(_ans)
        {
            IsValid =base.Validate();
        }
    }

}
