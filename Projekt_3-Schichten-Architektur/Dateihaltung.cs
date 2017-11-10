using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Projekt_3_Schichten_Architektur
{
    public class Dateihaltung : IDatenhaltung
    {
        private static XDocument xDoc;
        private static string xDocString; 
        
        private string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        public Dateihaltung()
        {
             _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName + @"/autoren.xml";
            try
            {
                File.Exists(_filePath);
                xDoc = XDocument.Load(_filePath);
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("Die Datei wurde nicht gefunden.");
            }
        }

        public void AktualisiereAutor(int ID, string Name)
        {
            var elementZuAendern = xDoc.Elements("Autoren")
                .Elements("Autor")
                .Where(x =>
                {
                    var autorenId = x.Element("Autoren_id");
                    return autorenId != null && autorenId.Value == ID.ToString();
                }).Single();
            var nameZuAendern= elementZuAendern.Element("Name");
            if (nameZuAendern != null)
            {
                nameZuAendern.Value = Name;
            }
            SpeicherXml();
        }

        public void AktualisiereBuch(string ISBN, string Titel)
        {
            var buchElement = xDoc.Elements("Autoren")
                .Elements("Autor").Elements("Buecher").Elements("Buch")
                .Where(x =>
                {
                    var buchIsbn = x.Element("ISBN");
                    return buchIsbn != null && buchIsbn.Value == ISBN;
                }).Single();
            var titelZuAendern= buchElement.Element("Titel");
            if (titelZuAendern != null)
            {
                titelZuAendern.Value = Titel;
            }
            SpeicherXml();
        }

        public List<Autor> GetAutoren()
        {
            var autorenList = new List<Autor>();
            var elementAufZuListen = xDoc.Elements("Autoren").Elements("Autor");

            autorenList = elementAufZuListen.Select(a => new Autor()
                {
                    Autoren_id = (int) a.Element("Autoren_id"),
                    Name = (string)a.Element("Name"),
                }).ToList();
            return autorenList;
        }

        public List<Buch> GetBuecher(int Autoren_id = 0)
        {
            var buecher = new List<Buch>();
            IEnumerable<XElement> elementAufZuListen;
            if (Autoren_id > 0)
            {
                elementAufZuListen = xDoc.Elements("Autoren")
                    .Elements("Autor")
                    .Where(x =>
                    {
                        var autorenId = x.Element("Autoren_id");
                        return autorenId != null && autorenId.Value == Autoren_id.ToString();
                    });
            }
            else
            {
                elementAufZuListen = xDoc.Elements("Autoren")
                    .Elements("Autor");
            }

            buecher = elementAufZuListen.Elements("Buecher").Elements("Buch").Select(b => new Buch()
                {
                    ISBN = (string)b.Element("ISBN"),
                    Titel = (string)b.Element("Titel"),
                }).ToList();
            return buecher;
        }

        public void LoescheAutor(int ID)
        {
            var elementZuLoeschen = xDoc.Elements("Autoren")
                .Elements("Autor")
                .Where(x =>
                {
                    var autorenId = x.Element("Autoren_id");
                    return autorenId != null && autorenId.Value == ID.ToString();
                }).Single();
            elementZuLoeschen.Remove();
            SpeicherXml();

        }

        public void LoescheBuch(string ISBN)
        {
            var buchElement = xDoc.Elements("Autoren")
                .Elements("Autor").Elements("Buecher").Elements("Buch")
                .Where(x =>
                {
                    var buchIsbn = x.Element("ISBN");
                    return buchIsbn != null && buchIsbn.Value == ISBN;
                }).Single();
            Console.WriteLine(buchElement);
            buchElement.Remove();
            SpeicherXml();
        }

        public void SpeichereAutor(string Name)
        {
            XElement autoren = xDoc.Element("Autoren");
            var lastId = xDoc.Descendants("Autoren").Descendants("Autor").Elements("Autoren_id").Last();
            XElement neuerAutor = new XElement("Autor",
                new XElement("Autoren_id", (int) lastId+1),
                new XElement("Name", Name),
                new XElement("Buecher", ""));
           autoren.Add(neuerAutor); 
           SpeicherXml();
            Console.WriteLine("Der Autor wurde gespeichert.");
        }

        public void SpeichereBuch(int Autoren_id, string ISBN, string Titel)
        {
            var autorElement = xDoc.Elements("Autoren")
                .Elements("Autor")
                .Where(x =>
                {
                    var autorenId = x.Element("Autoren_id");
                    return autorenId != null && autorenId.Value == Autoren_id.ToString();
                });
            XElement neuesBuch = new XElement("Buch",
                new XElement("ISBN", ISBN),
                new XElement("Titel", Titel));
            autorElement.Elements("Buecher").Last().Add(neuesBuch);
            SpeicherXml();
            Console.WriteLine("Das Buch wurde gespeichert.");
        }

        private void SpeicherXml()
        {
            try
            {
                xDoc.Save(_filePath);
            }
            catch (Exception e)
            {
                // rethrow exception as DateihaltungsFehler
                throw new DatenhaltungsFehler("Die Datei konnte nicht gespeichert werden.");
            }
            
        }
    }
}
