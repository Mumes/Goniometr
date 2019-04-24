using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    class UartCoordinateReaderCommandMaschtab : ICommand
    {
        UartCoordinateReader reader;

        public UartCoordinateReaderCommandMaschtab(UartCoordinateReader _reader)
        {
            reader = _reader;
        }
        public void Execute()
        {
            
        }
    }

    class UartCoordinateReaderCommandCoordinates : ICommand
    {
        UartCoordinateReader reader;

        public UartCoordinateReaderCommandCoordinates(UartCoordinateReader _reader)
        {
            reader = _reader;
        }
        public void Execute()
        {
            
        }
    }

    class UartCoordinateReaderCommandState : ICommand
    {
        UartCoordinateReader reader;

        public UartCoordinateReaderCommandState(UartCoordinateReader _reader)
        {
            reader = _reader;
        }
        public void Execute()
        {
            
        }
    }
}
