using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace GonCommand
{
    /// <summary>
    /// Общий класс для команды для любого устройства
    /// </summary>
    interface IMessage
    {
        bool Validate();
    }
    /// <summary>
    /// Интерфейс для UART сообщения
    /// </summary>
    interface IUartMessage:IMessage
    {
        int CountBytes { get; }
        byte[] MesBytes { get; }
        void GetMesBytes();
    }
}
