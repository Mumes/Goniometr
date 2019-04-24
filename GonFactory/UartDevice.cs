using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel.DataAnnotations;
namespace GonCommand
{
/// <summary>
/// Абстрактный класс, описывающий общее устройство, работающее по UART.
/// </summary>
    abstract class UartDevice : IDevice
    {
        protected SerialPort Sp { get; private set; }
        [RegularExpression(@"^COM\d*$", ErrorMessage = "Неверный формат имени COM-порта")]
        string Com { get; set; }
        [Range(200, 921600, ErrorMessage = "Скорость передачи должна находиться в пределах от 200 до 921600")]
        int Baud { get; set;}
        Parity Parity { get; set; }
        [Range(5, 8, ErrorMessage = "Количество бит данных должно быть не более 8 и не менее 5.")]
        int DataBits { get; set; }
        StopBits StpBits { get; set; }
        public UartDevice(string _com="COM1", int _baud = 9600,Parity _parity = Parity.None,
            int _dataBits=8, StopBits _stpBits = StopBits.One)
        {
            this.Com = _com;
            this.Baud = _baud;
            this.Parity = _parity;
            this.DataBits = _dataBits;
            this.StpBits = _stpBits;
        }
        public virtual void Close()
        {
            Sp.Close();
        }
        public virtual void Init()
        {
            Sp = new SerialPort(Com, Baud, Parity, DataBits, StpBits);
            Sp.ReadTimeout = 500;
            Sp.WriteTimeout = 500;
            if (!Sp.IsOpen)
                Sp.Open();
        }
        public virtual bool ReadWithTimeout(IUartMessage _mes)
        {
            try
            {
                Sp.Read(_mes.MesBytes, 0, _mes.CountBytes);
                return true;
            }
            catch(TimeoutException)
            {
                return false;
            }
            
        }
    }
}
