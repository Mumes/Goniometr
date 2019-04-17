using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    abstract class ApiDevice:IDevice
    {
        public abstract void Init();
        public abstract void Close();
    }
}
