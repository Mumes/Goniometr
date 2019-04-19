using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    class Invoker
    {
        public Stack<ICommand> CommandsHistory;
        public ICommand Command {get;private set;}
        public void SetCommand(ICommand com)
        {
            Command = com;
            CommandsHistory = new Stack<ICommand>();
        }
        public void StartCommand()
        {
                Command?.Execute();
                CommandsHistory.Push(Command);
        }
    }
}
