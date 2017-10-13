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

		public List<Buch> GetBuecher(int Autoren_id)
		{
			return Data.GetBuecher(Autoren_id);
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

		public void SpeichereBuch(int Autoren_id, string ISBN, string Titel)
		{
			Data.SpeichereBuch(Autoren_id, ISBN, Titel);
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
