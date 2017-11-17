using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
	public class Fachkonzept1 : IFachkonzept
	{
		IDatenhaltung Data;
		public Fachkonzept1(IDatenhaltung Data)
		{
			this.Data = Data;
		}

		public List<Autor> GetAutoren()
		{
			return SortiereAutoren(Data.GetAutoren());
		}

		public List<Buch> GetBuecher(int Autoren_id)
		{
			return Data.GetBuecher(Autoren_id);
		}

		public bool AktualisiereAutor(int ID, string Name)
		{
            return Data.AktualisiereAutor(ID, Name);
		}

		public bool AktualisiereBuch(string ISBN, string Titel)
		{
            return Data.AktualisiereBuch(ISBN, Titel);
		}

		public bool LoescheAutor(int ID)
		{
            return Data.LoescheAutor(ID);
		}

		public bool LoescheBuch(string ISBN)
		{
            return Data.LoescheBuch(ISBN);
		}

		public bool SpeichereAutor(string Name)
		{
            return Data.SpeichereAutor(Name);
		}

		public bool SpeichereBuch(int Autoren_id, string ISBN, string Titel)
		{
            return Data.SpeichereBuch(Autoren_id, ISBN, Titel);
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

			return Liste;
		}
	}
}
