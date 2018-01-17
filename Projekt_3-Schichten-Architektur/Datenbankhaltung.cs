using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
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
			connectionString.Database = Environment.CurrentDirectory + "\\Datenbank\\AUTORENBUECHER.FDB";
			connectionString.Password = " ";
			connectionString.UserID = "sysdba";
			connectionString.ServerType = FbServerType.Embedded;
			Connection = new FbConnection(connectionString.ToString());
		}

		~Datenbankhaltung()
		{
			if (Connection == null)
				return;

			if (Connection.State == System.Data.ConnectionState.Open)
			{
				try
				{
					Connection.Close();
				}
				catch (NullReferenceException n)
				{
					throw new NullReferenceException("Verbindung zur Datenbank konnte nicht beendet werden.", n);
				}
			}
		}

		public bool AktualisiereAutor(int ID, string Name)
		{
			if (string.IsNullOrEmpty(Name))
				return false;

			string statement = "UPDATE T_Autoren SET ";
			statement += "Name = '" + Name + "' ";
			statement += "WHERE ";
			statement += "Autoren_id = " + ID + ";";

			return StatementAusfuehren(statement);
		}

		public bool AktualisiereBuch(string ISBN, string Titel)
		{
			if (string.IsNullOrEmpty(Titel))
				return false;

			string statement = "UPDATE T_Buecher SET ";
			statement += "Titel = '" + Titel + "' ";
			statement += "WHERE ";
			statement += "ISBN = '" + ISBN + "';";
            Console.WriteLine(statement);
			return StatementAusfuehren(statement);
		}

		public void VerbindungOeffnen()
		{
			if (Connection == null)
				throw new NullReferenceException("Verbindung zur Datenbank fehlgechlagen.");

			if (Connection.State != System.Data.ConnectionState.Open)
			{
				try
				{
					Connection.Open();
				}
				catch (NullReferenceException n)
				{
					throw new NullReferenceException("Verbindung zur Datenbank fehlgeschlagen.", n);
				}
			}
		}

		public List<Autor> GetAutoren()
		{
			VerbindungOeffnen();

			string statement = "SELECT * " +
								"FROM T_Autoren";

			FbCommand reader = new FbCommand(statement, Connection);
			FbDataAdapter adapter = new FbDataAdapter(reader);
			DataSet Data = new DataSet();
			adapter.FillSchema(Data, SchemaType.Source);
			adapter.Fill(Data);

			List<Autor> autoren = new List<Autor>();
			foreach (DataRow item in Data.Tables[0].Rows)
			{
				autoren.Add(new Autor((int)item["Autoren_id"], item["Name"].ToString()));
			}

			return autoren;
		}

		public List<Buch> GetBuecher(int Autoren_id = 0)
		{
			VerbindungOeffnen();

			string statement = "SELECT * " +
								"FROM T_Buecher ";

			if (Autoren_id > 0)
				statement += "WHERE F_Autoren_id = " + Autoren_id;

			FbCommand reader = new FbCommand(statement, Connection);
			FbDataAdapter adapter = new FbDataAdapter(reader);
			DataSet Data = new DataSet();
			adapter.FillSchema(Data, SchemaType.Source);
			adapter.Fill(Data);

			List<Buch> buecher = new List<Buch>();
			foreach (DataRow item in Data.Tables[0].Rows)
			{
				buecher.Add(new Buch(item["ISBN"].ToString(), item["Titel"].ToString()));
			}

			return buecher;
		}

		public bool LoescheAutor(int ID)
		{
			string statement = "DELETE FROM T_Autoren ";
			statement += "WHERE Autoren_id = " + ID + ";";

			return StatementAusfuehren(statement);
		}

		public bool LoescheBuch(string ISBN)
		{
			string statement = "DELETE FROM T_Buecher ";
			statement += "WHERE ISBN = '" + ISBN + "';";

			return StatementAusfuehren(statement);
		}

		public bool SpeichereAutor(string Name)
		{
			if (string.IsNullOrEmpty(Name))
				return false;

			string statement = "INSERT INTO T_Autoren ";
			statement += "(Name) ";
			statement += "VALUES ";
			statement += "('" + Name + "');";

			return StatementAusfuehren(statement);
		}

		public bool SpeichereBuch(int Autoren_id, string ISBN, string Titel)
		{
			if (string.IsNullOrEmpty(Titel))
				return false;

			string statement = "INSERT INTO T_Buecher ";
			statement += "(F_Autoren_id, ISBN, Titel) ";
			statement += "VALUES ";
			statement += "(" + Autoren_id + ", '" + ISBN + "', '" + Titel + "');";

			return StatementAusfuehren(statement);
		}

		private bool StatementAusfuehren(string Statement)
		{
			VerbindungOeffnen();

			bool b = false;
			FbCommand writer = null;
			try
			{
				writer = new FbCommand(Statement, Connection, Connection.BeginTransaction());
				writer.ExecuteNonQuery();

				writer.Transaction.Commit();
				b = true;
			}
			catch
			{
				writer?.Transaction?.Rollback();
			}
			finally
			{
				writer?.Connection?.Close();
			}
			return b;
		}
	}
}
