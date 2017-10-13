using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
    public interface IDatenhaltung
    {
        List<Autor> GetAutoren();

        List<Buch> GetBuecher();

        void SpeichereAutor(string Name);

        void SpeichereBuch(int Autoren_id, string Titel);

        void LoescheAutor(int ID);

        void LoescheBuch(string ISBN);

        void AktualisiereAutor(int ID, string Name);

        void AktualisiereBuch(string ISBN, string Titel);
    }
}
