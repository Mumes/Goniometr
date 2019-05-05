using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    /// <summary>
    /// Базовый абстрактный класс для устройств, работа с которыми осуществляется через сторонний API
    /// </summary>
    public abstract class ApiDevice:IDevice
    {
        public abstract void Init();
        public abstract void Close();
    }
}
