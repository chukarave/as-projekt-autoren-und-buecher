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

		public void AktualisiereAutor(int ID, string Name)
		{
			throw new NotImplementedException();
		}

		public void AktualisiereBuch(string ISBN, string Titel)
		{
			throw new NotImplementedException();
		}

		public List<Autor> GetAutoren()
		{
			throw new NotImplementedException();
		}

		public List<Buch> GetBuecher()
		{
			throw new NotImplementedException();
		}

		public void LoescheAutor(int ID)
		{
			throw new NotImplementedException();
		}

		public void LoescheBuch(string ISBN)
		{
			throw new NotImplementedException();
		}

		public void SpeichereAutor(string Name)
		{
			throw new NotImplementedException();
		}

		public void SpeichereBuch(int Autoren_id, string Titel)
		{
			throw new NotImplementedException();
		}
	}
}
