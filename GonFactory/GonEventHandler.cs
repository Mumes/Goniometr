using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    public delegate void GonEventHandler(object sender, GonEventArgs e);
    /// <summary>
    /// Общий класс для события
    /// </summary>
    public class GonEventArgs
    {
        // Сообщение
        public string Message { get; private set; }

        public GonEventArgs(string _mes)
        {
            Message = _mes;
        }
    }
}
