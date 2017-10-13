using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
    public class Fachkonzept1 : IFachkonzept
    {
        IDatenhaltung IData;
        public Fachkonzept1(IDatenhaltung _IData)
        {
            this.IData = _IData;
        }
    }
}
