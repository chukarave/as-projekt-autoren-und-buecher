﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Projekt_3_Schichten_Architektur
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

        public bool AktualisiereAutor(int ID, string Name)
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
            return true;

        }

        public bool AktualisiereBuch(string ISBN, string Titel)
        {
            return false;
           
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

        public List<Buch> GetBuecher(int Autoren_id)
        {
            var buecher = new List<Buch>();
            var elementAufZuListen = xDoc.Elements("Autoren")
                .Elements("Autor")
                .Where(x =>
                {
                    var autorenId = x.Element("Autoren_id");
                    return autorenId != null && autorenId.Value == Autoren_id.ToString();
                });

            buecher = elementAufZuListen.Elements("Buecher").Elements("Buch").Select(b => new Buch()
                {
                    ISBN = (string)b.Element("ISBN"),
                    Titel = (string)b.Element("Titel"),
                }).ToList();
            return buecher;
        }

        public bool LoescheAutor(int ID)
        {
            return false;

        }

        public bool LoescheBuch(string ISBN)
        {
            return false;

        }

        public bool SpeichereAutor(string Name)
        {
            XElement autoren = xDoc.Element("Autoren");
            var count = xDoc.Descendants("Autor").Count();
            XElement neuerAutor = new XElement("Autor",
                new XElement("Autoren_Id", count+1),
                new XElement("Name", Name),
                new XElement("Buecher", ""));
           autoren.Add(neuerAutor); 
           
            xDoc.Save(_filePath + @"/autoren.xml");
            
            return true;
        }

        public bool SpeichereBuch(int Autoren_id, string ISBN, string Titel)
        {
            var elementAufZuListen = xDoc.Elements("Autoren")
                .Elements("Autor")
                .Where(x =>
                {
                    var autorenId = x.Element("Autoren_id");
                    return autorenId != null && autorenId.Value == Autoren_id.ToString();
                });
            XElement neuesBuch = new XElement("Buch",
                new XElement("ISBN", ISBN),
                new XElement("Titel", Titel));
            elementAufZuListen.Elements("Buecher").Last().Add(neuesBuch);
            
            xDoc.Save(_filePath + @"/autoren.xml");
            return false;
        }

    }
}
