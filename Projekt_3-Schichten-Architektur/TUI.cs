using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
    public class TUI
    {
        IFachkonzept IF;
        public TUI(IFachkonzept _IF)
        {
            this.IF = _IF;
            ConsoleManager.Toggle();
            Console.Read();
        }
    }
}
