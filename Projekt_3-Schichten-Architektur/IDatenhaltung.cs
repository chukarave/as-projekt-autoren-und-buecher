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

        List<Buch> GetBuecher(int Autoren_id = 0);

        bool SpeichereAutor(string Name);

        bool SpeichereBuch(int Autoren_id, string ISBN, string Titel);

        bool LoescheAutor(int ID);

        bool LoescheBuch(string ISBN);

        bool AktualisiereAutor(int ID, string Name);

        bool AktualisiereBuch(string ISBN, string Titel);
    }
}
