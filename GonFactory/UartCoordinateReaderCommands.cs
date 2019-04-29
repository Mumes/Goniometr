using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GonCommand
{
    class UartCoordinateReaderCommandMaschtab 
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

    class UartCoordinateReaderCommandCoordinates
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

    class UartCoordinateReaderCommandState 
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
