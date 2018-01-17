using System;
using System.Data.Common;
using System.Linq;
using System.Net.Mime;

namespace Projekt_3_Schichten_Architektur
{
    public class TUI
    {
        IFachkonzept IF;
        private bool endProgram = false;
        public TUI(IFachkonzept _IF)
        {
            this.IF = _IF;
            ConsoleManager.Toggle();
            try
            {
            Console.CursorVisible = true;
            }
            catch(Exception ex)
            {
                string s = ex.Message;
            }
            while (endProgram == false)
            {
                endProgram = ZeigeHauptMenue();
                if (endProgram)
                {
                    Console.WriteLine("Auf Wiedersehen");
                }
            }

        }

        public bool ZeigeHauptMenue()
        {
            try
            {
                ZeigeMenue();
                var auswahl = FragEingabe();
                Console.Clear();
                if (auswahl != "j")
                {
                    return Select(auswahl);
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unerwarteter Fehler ist aufgetreten. Programm wird beendet!");
                return true;
            }
        }

        public void ZeigeMenue()
        {
            Console.WriteLine("┌───────────────────────────────────┐");
            Console.WriteLine("│ Autoren & Bücher                  │");
            Console.WriteLine("├───────────────────────────────────┤");
            Console.WriteLine("│                                   │");
            Console.WriteLine("│ Bücherauflistung nach Autoren     │");
            Console.WriteLine("│                                   │");
            Console.WriteLine("│ -----------------------           │");
            Console.WriteLine("│ Autoren auflisten             (a) │");
            Console.WriteLine("│ Autoren hinzufügen            (b) │");
            Console.WriteLine("│ Autoren bearbeiten            (c) │");
            Console.WriteLine("│ Autoren entfernen             (d) │");
            Console.WriteLine("│ -----------------------           │");
            //  Console.WriteLine("│ Bücher auflisten              (e) │");
            Console.WriteLine("│ Bücher auflisten nach Autor   (f) │");
            Console.WriteLine("│ Bücher hinzufügen             (g) │");
            Console.WriteLine("│ Bücher bearbeiten             (h) │");
            Console.WriteLine("│ Bücher entfernen              (i) │");
            Console.WriteLine("│ -----------------------           │");
            Console.WriteLine("│ Program beenden               (j) │");
            Console.WriteLine("├───────────────────────────────────┤");
            Console.WriteLine("└───────────────────────────────────┘");
        }

        // input is a string (and not a char) to avoid user 
        //confusion due to multiple user input points.
        public string FragEingabe(string input = "")
        {
            if (input != "")
            {
                return input;
            }
            Console.WriteLine("Wählen Sie einen Menüpunkt:       ");
            input = Console.ReadLine();
            //Console.Read();
            return input;
        }

        public bool ZeigeAutoren()
        {
            Console.WriteLine();
            var autorenIndex = IF.GetAutoren();
            if (!autorenIndex.Any())
            {
                Console.WriteLine("Die Autorenliste ist leer");
            }
            foreach (var autor in autorenIndex)
            {
                Console.WriteLine(autor.Autoren_id + ". " + autor.Name);
            }
            Console.WriteLine();
            return false;
        }

        public bool ZeigeBuecher()
        {
            Console.WriteLine();
            var buecherIndex = IF.GetBuecher(0);
            if (!buecherIndex.Any())
            {
                Console.WriteLine("Die Bücherliste ist leer");
            }
            foreach (var buch in buecherIndex)
            {
                Console.WriteLine(buch.Titel + " (ISBN: " + buch.ISBN + ")");
            }
            Console.WriteLine();
            return false;
        }

        public bool BuecherNachAutor()
        {
            Console.WriteLine();
            // List authors as help
            var autorenList = ZeigeAutoren();
            Console.WriteLine("Bitte geben Sie die ID Nummer von einem Autor ein, um Bücher für diesen Autor aufzulisten: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var buecher = IF.GetBuecher(id);
            if (!buecher.Any())
            {
                Console.WriteLine("Keine Bücher gespeichert von diesem Autor");
            }
            foreach (var buch in buecher)
            {
                Console.WriteLine("Titel:  " + buch.Titel);
                Console.WriteLine("ISBN:  " + buch.ISBN);
            }
            return false;
        }

        public bool FuegBuchHinzu()
        {
            Console.WriteLine();
            // List authors as help
            var autorenList = IF.GetAutoren();
            foreach (var autor in autorenList)
            {
                Console.WriteLine("ID: " + autor.Autoren_id + " Name: " + autor.Name);
            }
            // Console.ReadLine();
            Console.WriteLine("Bitte geben Sie die ID Nummer von einem Autor ein, um ein neues Buch für diesen Autor hinzufügen: ");
            var id = Convert.ToInt32(Console.ReadLine());
            //  Console.ReadLine();
            Console.WriteLine("Bitte Geben Sie den Titel ein: ");
            var titel = Console.ReadLine();
            //  Console.ReadLine();
            Console.WriteLine("Bitte Geben Sie die ISBN ein: ");
            var isbn = Console.ReadLine();
            IF.SpeichereBuch(id, isbn, titel);
            Console.WriteLine("Das Buch wurde hinzugefügt.");
            return false;
        }

        public bool FuegAutorHinzu()
        {
            Console.WriteLine("Bitte Geben Sie den Autor Name ein: ");
            var autorName = Console.ReadLine();
            IF.SpeichereAutor(autorName);
            return false;
        }

        public bool BearbeiteAutor()
        {
            Console.WriteLine();
            // List authors as help
            var autorenList = IF.GetAutoren();
            foreach (var autor in autorenList)
            {
                Console.WriteLine("ID: " + autor.Autoren_id + " Name: " + autor.Name);
            }
            Console.WriteLine("Bitte geben Sie die ID Nummer ein, um den Autor zu bearbeiten: ");
            var id = Convert.ToInt32(Console.ReadLine());
            // Enter edited author name and call editing method
            try
            {
                //Console.ReadLine();
                Console.WriteLine("Bitte geben Sie die gewünschte Änderung ein: ");
                var aktuellerName = Console.ReadLine();
                if (string.IsNullOrEmpty(aktuellerName))
                {
                    BearbeiteAutor();
                }
                IF.AktualisiereAutor(id, aktuellerName);
                Console.WriteLine("Autorname wurde aktualisiert.");
                return false;
            }
            catch (FormatException e)
            {
                BearbeiteAutor();
            }
            return false;
        }

        public bool BearbeiteBuch()
        {
            var buecher = IF.GetBuecher(0);
            foreach (var buch in buecher)
            {
                Console.WriteLine("ISBN: " + buch.ISBN + "  -  " + buch.Titel);
            }
            Console.WriteLine();
            Console.WriteLine("Bitte geben Sie die ISBN des Buchs zu bearbeiten: ");
            var isbn = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie den neuen Buchtitel ein: ");
            var titel = Console.ReadLine();
            IF.AktualisiereBuch(isbn, titel);
            Console.WriteLine("Buchtitel wurde aktualisiert.");
            return false;
        }
        public bool EntferneAutor()
        {
            Console.WriteLine();
            // List authors as help
            var autorenList = IF.GetAutoren();
            foreach (var autor in autorenList)
            {
                Console.WriteLine("ID: " + autor.Autoren_id + " Name: " + autor.Name);
            }

            Console.WriteLine("Bitte geben Sie die ID Nummer ein, um den Autor zu entfernen: ");
            var id = Convert.ToInt32(Console.ReadLine());
            IF.LoescheAutor(id);
            Console.WriteLine("Der Autor wurde entfernt.");
            return false;
        }

        public bool EntferneBuch()
        {
            var buecher = IF.GetBuecher(0);
            foreach (var buch in buecher)
            {
                Console.WriteLine("ISBN: " + buch.ISBN + "  -  " + buch.Titel);
            }
            Console.ReadLine();
            Console.WriteLine("Bitte geben Sie die ISBN des Buchs zu entfernen: ");
            var isbn = Console.ReadLine();
            IF.LoescheBuch(isbn);
            Console.WriteLine("Das Buch wurde entfernt.");
            return false;
        }

        public bool Select(string input)
        {
            switch (input)
            {
                case ("a"):
                    ZeigeAutoren();
                    break;
                case ("b"):
                    FuegAutorHinzu();
                    break;
                case ("c"):
                    BearbeiteAutor();
                    break;
                case ("d"):
                    EntferneAutor();
                    break;
                case ("e"):
                    ZeigeBuecher();
                    break;
                case ("f"):
                    BuecherNachAutor();
                    break;
                case ("g"):
                    FuegBuchHinzu();
                    break;
                case ("h"):
                    BearbeiteBuch();
                    break;
                case ("i"):
                    EntferneBuch();
                    break;
                case ("j"):
                    break;

            }
            return false;
        }
    }
}
