using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
    public class Buch
    {
        private string _isbn;
        private string _titel;

        public string ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }
        public string Titel
        {
            get { return _titel; }
            set { _titel = value; }
        }

        public Buch()
        {

        }

        public Buch(string _ISBN, string _Titel)
        {
            this.ISBN = _ISBN;
            this.Titel = _Titel;
        }
    }
}
