using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AsProject
{
    public class Dateihaltung : IDatenhaltung
    {
        private static XDocument xDoc;
        private static string xDocString; 
        
        private string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        public Dateihaltung()
        {
           // this.xDoc = new XmlDocument();
             _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            if (File.Exists(_filePath + @"/autoren.xml"))
            {
                xDoc = XDocument.Load(_filePath + "/autoren.xml");
                xDocString = xDoc.ToString();
            }
            else
            {
                throw new FileNotFoundException("The file was not found");
                
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
            xDoc.Save(_filePath + @"/autoren.xml");
            Console.WriteLine("Autorname wurde aktualisiert.");

        }

        public void AktualisiereBuch(string ISBN, string Titel)
        {

        }

        public List<Autor> GetAutoren()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Autor>), new XmlRootAttribute("Autoren"));
            StringReader stringReader = new StringReader(xDocString);
            List<Autor> autorenList = (List<Autor>) serializer.Deserialize(stringReader);
            if (autorenList.Count == 0)
            {
                Console.WriteLine("Die Autorenliste ist leer");
            }
            return autorenList;
        }

		// WIP
        public List<Buch> GetBuecher(int Autoren_id)
        {
            var Buecher = new List<Buch>();
            var elementAufZuListen = xDoc.Elements("Autoren")
                .Elements("Autor")
                .Where(x =>
                {
                    var autorenId = x.Element("Autoren_id");
                    return autorenId != null && autorenId.Value == Autoren_id.ToString();
                }).Single();
            Console.WriteLine(elementAufZuListen);
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

        public void SpeichereBuch(int Autoren_id, string ISBN, string Titel)
        {
            throw new NotImplementedException();
        }
    }
}
