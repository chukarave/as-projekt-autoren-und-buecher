using System;

namespace Projekt_3_Schichten_Architektur
{
    public class DatenhaltungsFehler : Exception
    {
        public DatenhaltungsFehler(string nachricht) : base(nachricht) 
        {
        }
        

    }
}
