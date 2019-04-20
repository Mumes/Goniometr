using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    interface IMessage
    {
        bool Validate();
    }

    interface IUartMessage:IMessage
    {
        void GetMesBytes();
    }
}
