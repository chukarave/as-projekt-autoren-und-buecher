using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
	public class Datenbankhaltung : IDatenhaltung
	{
		private FbConnection Connection;

		public Datenbankhaltung()
		{
			FbConnectionStringBuilder connectionString = new FbConnectionStringBuilder();
			connectionString.Database = Environment.CurrentDirectory + "Datenbank.fdb";
			connectionString.Password = " ";
			connectionString.UserID = "sysdba";
			connectionString.ServerType = FbServerType.Embedded;
			Connection = new FbConnection(connectionString.ToString());
		}

		public void AktualisiereAutor(int ID, string Name)
		{
			string statement = "UPDATE T_Autoren SET ";
			statement += "Name = '" + Name + "' ";
			statement += "WHERE ";
			statement += "Autoren_id = " + ID + ";";

			StatementAusfuehren(statement);
		}

		public void AktualisiereBuch(string ISBN, string Titel)
		{
			string statement = "UPDATE T_Buecher SET ";
			statement += "(Titel = '" + Titel + "') ";
			statement += "WHERE ";
			statement += "ISBN = '" + ISBN + "';";

			StatementAusfuehren(statement);
		}

		public List<Autor> GetAutoren()
		{
			throw new NotImplementedException();
		}

		public List<Buch> GetBuecher()
		{
			if (Connection == null)
				throw new NullReferenceException("Verbindung zur Datenbank fehlgechlagen.");

			if (Connection.State != System.Data.ConnectionState.Open)
			{
				try
				{
					Connection.Open();
				}
				catch
				{
					throw new NullReferenceException("Verbindung zur Datenbank fehlgeschlagen.");
				}
			}

			string statement = "SELECT * FROM T_Buecher";

			FbCommand reader = new FbCommand(statement);
			FbDataAdapter adapter = new FbDataAdapter(reader);
			//this.Data = new DataSet();
			//adapter.FillSchema(this.Data, SchemaType.Source);
			//adapter.Fill(this.Data);
		}

		public void LoescheAutor(int ID)
		{
			string statement = "DELETE FROM T_Autoren ";
			statement += "WHERE Autoren_id = " + ID + ";";

			StatementAusfuehren(statement);
		}

		public void LoescheBuch(string ISBN)
		{
			string statement = "DELETE FROM T_Buecher ";
			statement += "WHERE ISBN = '" + ISBN + "';";

			StatementAusfuehren(statement);
		}

		public void SpeichereAutor(string Name)
		{
			string statement = "INSERT INTO T_Autoren ";
			statement += "(Name) ";
			statement += "VALUES ";
			statement += "(" + Name + ");";

			StatementAusfuehren(statement);
		}

		public void SpeichereBuch(int Autoren_id, string ISBN, string Titel)
		{
			string statement = "INSERT INTO T_Buecher ";
			statement += "(Autoren_id, ISBN, Titel) ";
			statement += "VALUES ";
			statement += "(" + Autoren_id + ", " + ISBN + ", " + Titel + ");";

			StatementAusfuehren(statement);
		}

		private void StatementAusfuehren(string Statement)
		{
			FbCommand writer = null;
			try
			{

				writer = new FbCommand();
				writer.Connection = Connection;
				writer.Transaction = writer.Connection.BeginTransaction();

				writer.CommandText = Statement;
				writer.ExecuteNonQuery();

				writer.Transaction.Commit();
			}
			catch
			{
				writer?.Transaction?.Rollback();
			}
			finally
			{
				writer?.Connection?.Close();
			}
		}
	}
}
