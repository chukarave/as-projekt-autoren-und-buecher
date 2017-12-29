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

        private string _filePath = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        
        public Dateihaltung()
        {
            if (File.Exists(_filePath + @"/autoren.xml"))
            {
                try
                {
                    xDoc = XDocument.Load(_filePath + @"/autoren.xml");
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    xDoc = new XDocument(new XElement("Autoren"));
                }
            }
            else
            {
                xDoc = new XDocument(new XElement("Autoren"));
            }
        }

        public List<Autor> GetAutoren()
        {
            List<Autor> autorenList = new List<Autor>();

            autorenList = xDoc.Elements("Autoren").Elements("Autor").Select(a => new Autor()
            {
                Autoren_id = int.Parse(a.Element("Autoren_id").Value),
                Name = a.Element("Name").Value,
            }).ToList();

            return autorenList;
        }
        public List<Buch> GetBuecher(int Autoren_id = 0)
        {
            List<Buch> buecher = new List<Buch>();
            IEnumerable<XElement> xAutoren;
            if (Autoren_id > 0)
            {
                xAutoren = xDoc.Elements("Autoren").Elements("Autor")
                    .Where(x =>
                    {
                        return int.Parse(string.IsNullOrWhiteSpace(x.Element("Autoren_id").Value) ? "0" : x.Element("Autoren_id").Value) == Autoren_id;
                    });
            }
            else
            {
                xAutoren = xDoc.Elements("Autoren").Elements("Autor");
            }

            buecher = xAutoren.Elements("Buecher").Elements("Buch").Select(b => new Buch()
            {
                ISBN = b.Element("ISBN").Value,
                Titel = b.Element("Titel").Value,
            }).ToList();

            return buecher;
        }

        public bool AktualisiereAutor(int ID, string Name)
        {
            XElement xAutor = xDoc.Elements("Autoren").Elements("Autor")
                .Where(x =>
                {
                    return int.Parse(string.IsNullOrWhiteSpace(x.Element("Autoren_id").Value) ? "0" : x.Element("Autoren_id").Value) == ID;
                }).Single();

            if (xAutor == null)
            {
                return false;
            }
                XElement xName = xAutor.Element("Name");
                xName.Value = Name;

            return SpeicherXml();
        }

        public bool AktualisiereBuch(string ISBN, string Titel)
        {
            XElement xBuch = xDoc.Elements("Autoren").Elements("Autor").Elements("Buecher").Elements("Buch")
                .Where(x =>
                {
                    return x.Element("ISBN").Value == ISBN;
                }).Single();

            if (xBuch == null)
            {
                return false;
            }
                XElement xTitel = xBuch.Element("Titel");
                xTitel.Value = Titel;

            return SpeicherXml();
        }

        public bool LoescheAutor(int ID)
        {
            XElement xAutor = xDoc.Elements("Autoren").Elements("Autor")
                .Where(x =>
                {
                    return int.Parse(string.IsNullOrWhiteSpace(x.Element("Autoren_id").Value) ? "0" : x.Element("Autoren_id").Value) == ID;
                }).Single();

            if (xAutor == null)
            {
                return false;
            }
            xAutor.Remove();

            return SpeicherXml();
        }

        public bool LoescheBuch(string ISBN)
        {
            XElement xBuch = xDoc.Elements("Autoren").Elements("Autor").Elements("Buecher").Elements("Buch")
                .Where(x =>
                {
                    return x.Element("ISBN").Value == ISBN;
                }).Single();

            if (xBuch == null)
            {
                return false;
            }
            xBuch.Remove();

            return SpeicherXml();
        }

        public bool SpeichereAutor(string Name)
        {
            XElement xAutoren = xDoc.Element("Autoren");
            // Removed try/catch block as lastid was unusable from inside it
            int lastId = int.Parse(xDoc.Descendants("Autoren").Descendants("Autor").Elements("Autoren_id").Max().Value);
            XElement xAutor = new XElement("Autor",
                new XElement("Autoren_id", lastId + 1),
                new XElement("Name", Name),
                new XElement("Buecher"));
            xAutoren.Add(xAutor);

            return SpeicherXml();
        }

        public bool SpeichereBuch(int Autoren_id, string ISBN, string Titel)
        {
            XElement xAutor = xDoc.Elements("Autoren").Elements("Autor")
                .Where(x =>
                {
                    return int.Parse(string.IsNullOrWhiteSpace(x.Element("Autoren_id").Value) ? "0" : x.Element("Autoren_id").Value) == Autoren_id;
                }).Single();

            XElement neuesBuch = new XElement("Buch",
                new XElement("ISBN", ISBN),
                new XElement("Titel", Titel));
            xAutor.Elements("Buecher").Last().Add(neuesBuch);

            return SpeicherXml();
        }

        private bool SpeicherXml()
        {
            bool b = false;

            try
            {
                xDoc.Save(_filePath);
                b = true;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                b = false;
            }

            return b;
        }
    }
}
