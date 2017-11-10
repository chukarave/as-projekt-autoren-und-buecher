using System.Collections.Generic;

namespace Projekt_3_Schichten_Architektur
{
    public class Fachkonzept2 : IFachkonzept
    {
        IDatenhaltung Data;
        public Fachkonzept2(IDatenhaltung Data)
        {
            this.Data = Data;
            
        }

        public void AktualisiereAutor(int ID, string Name)
        {
            Data.AktualisiereAutor(ID, Name);
        }

        public void AktualisiereBuch(string ISBN, string Titel)
        {
            Data.AktualisiereBuch(ISBN, Titel);
        }

        public List<Autor> GetAutoren()
        {
            return SortiereAutoren(Data.GetAutoren());
        }

        public List<Buch> GetBuecher(int autorenId)
        {
            return Data.GetBuecher(autorenId);
        }

        public void LoescheAutor(int ID)
        {
            Data.LoescheAutor(ID);
        }

        public void LoescheBuch(string ISBN)
        {
            Data.LoescheBuch(ISBN);
        }

        public void SpeichereAutor(string Name)
        {
            Data.SpeichereAutor(Name);
        }

        public void SpeichereBuch(int autorenId, string ISBN, string Titel)
        {
            Data.SpeichereBuch(autorenId, ISBN, Titel);
        }
        
        private List<Autor> SortiereAutoren(List<Autor> Liste)
        {
            string abc = "aäbcdefghijklmnoöpqrstuüvwxyz";

            for (int i = 0; i < Liste.Count; i++)
            {
                int index = abc.IndexOf(Liste[i].Name.ToLower().ToCharArray()[0]);

                for (int j = Liste.Count - 1; j > i; j--)
                {
                    if (index > abc.IndexOf(Liste[j].Name.ToLower().ToCharArray()[0]))
                    {
                        Autor tempAutor = Liste[j];
                        Liste[j] = Liste[i];
                        Liste[i] = tempAutor;
                        i--;
                        break;
                    }
                }
            }
            // Reverse list order
            Liste.Reverse();
            return Liste;
        }
    }
}
