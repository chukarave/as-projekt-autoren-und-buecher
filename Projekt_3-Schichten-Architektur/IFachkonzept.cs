using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
    public interface IFachkonzept
    {
        List<Autor> GetAutoren();

        List<Buch> GetBuecher(int Autoren_id);

        void SpeichereAutor(string Name);

        void SpeichereBuch(int Autoren_id, string ISBN, string Titel);

        void LoescheAutor(int ID);

        void LoescheBuch(string ISBN);

        void AktualisiereAutor(int ID, string Name);

        void AktualisiereBuch(string ISBN, string Titel);
    }
}
