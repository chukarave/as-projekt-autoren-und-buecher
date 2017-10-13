using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projekt_3_Schichten_Architektur
{
    public class Dateihaltung : IDatenhaltung
    {
        XmlDocument xDoc;
        public Dateihaltung()
        {
            this.xDoc = new XmlDocument();

            if (File.Exists(System.Environment.CurrentDirectory + "\\Test.xml"))
            {

            }
        }

        public void AktualisiereAutor(int ID, string Name)
        {

        }

        public void AktualisiereBuch(string ISBN, string Titel)
        {

        }

        public List<Autor> GetAutoren()
        {
            List<Autor> Autoren = new List<Autor>();

            return Autoren;
        }

        public List<Buch> GetBuecher()
        {
            List<Buch> Buecher = new List<Buch>();

            return Buecher;
        }

        public void LoescheAutor(int ID)
        {

        }

        public void LoescheBuch(string ISBN)
        {

        }

        public void SpeichereAutor(string Name)
        {

        }

        public void SpeichereBuch(int Autoren_id, string Titel)
        {

        }
    }
}
