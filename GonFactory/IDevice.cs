using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
/// <summary>
/// Интерфейс, описывающий общее внешнее устройство. 
/// </summary>
    public interface IDevice
    { 
        /// <summary>
        /// Инициализация устройства.
        /// </summary>
        void Init();
        /// <summary>
        /// Функция окончания работы с устройством.
        /// </summary>
        void Close();
    }
}
