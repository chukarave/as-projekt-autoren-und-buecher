using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3_Schichten_Architektur
{
    public class TUI
    {
        IFachkonzept IF;
        private bool endProgram = false;
        public TUI(IFachkonzept _IF)
        {
            this.IF = _IF;
         //   ConsoleManager.Toggle();
            Console.CursorVisible = true;
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
            ZeigeMenue();
            var auswahl = FragEingabe();
            if (auswahl != "i") {
                return Select(auswahl);
            } else {
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
            Console.WriteLine("│ Bücher auflisten              (e) │");
            Console.WriteLine("│ Bücher hinzufügen             (f) │");
            Console.WriteLine("│ Bücher bearbeiten             (g) │");
            Console.WriteLine("│ Bücher entfernen              (h) │");
            Console.WriteLine("│ -----------------------           │");
            Console.WriteLine("│ Program beenden               (i) │");
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
            Console.Read();
            return input;
        }

        public string FragWeitereAutorAktion()
        {
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│ Autoren Menü                         │");
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.WriteLine("│ Autoren hinzufügen               (b) │");
            Console.WriteLine("│ Autoren bearbeiten               (c) │");
            Console.WriteLine("│ Autoren entfernen                (d) │");
            Console.WriteLine("│ -----------------------              │");
            Console.WriteLine("│ Bücher auflisten nach Autor      (e) │");
            Console.WriteLine("│ Hauptmenü zeigen                 (m) │");
            Console.WriteLine("│ Program beenden                  (i) │");
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.WriteLine("└──────────────────────────────────────┘");
            var input = Console.ReadLine();
            return input;
        }

        public bool ZeigeAutoren()
        {
            Console.WriteLine();
            var autorenIndex = IF.GetAutoren();
            if (autorenIndex == null)
            {
                Console.WriteLine("Die Autorenliste ist leer");
            }
            foreach (var autor in autorenIndex)
            {
                Console.WriteLine(autor.Autoren_id + ". " + autor.Name);
            }
            Console.WriteLine();
            var ret = FragWeitereAutorAktion();
            switch (ret)
            {
                case ("b"):
                    FuegAutorHinzu();
                    break;
                case ("c"):
                    BearbeiteAutor();
                    break;
                case ("d"):
                    EntfernAutor();
                    break;
                case ("e"):
                    BuecherNachAutor();
                    break;
                case ("m"):
                    ZeigeHauptMenue();
                    break;
                case ("i"):
                    return true;
            }
            return false;
        }

        public bool BuecherNachAutor()
        {
            Console.WriteLine();
            // List authors as help
            var autorenList = IF.GetAutoren();
            foreach (var autor in autorenList) 
            {
                Console.WriteLine("ID: " + autor.Autoren_id + " Name: " + autor.Name);
            }
            Console.WriteLine("Bitte geben Sie die ID Nummer von einem Autor ein, um Bücher für diesen Autor aufzulisten: ");
            Console.ReadLine();
            var id = Convert.ToInt32(Console.ReadLine());
            var buecher = IF.GetBuecher(id);
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
            Console.WriteLine("Bitte geben Sie die ID Nummer von einem Autor ein, um ein neues Buch für diesen Autor hinzufügen: ");
            var id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bitte Geben Sie den Titel ein: ");
            var titel = Console.ReadLine();
            Console.WriteLine("Bitte Geben Sie die ISBN ein: ");
            Console.ReadLine();
            var isbn = Console.ReadLine();
            IF.SpeichereBuch(id, isbn, titel);
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
                Console.WriteLine("Bitte geben Sie die gewünschte Änderung ein: ");
                Console.ReadLine();
                var aktuellerName = Console.ReadLine();
                if (string.IsNullOrEmpty(aktuellerName))
                {
                   BearbeiteAutor();
                }
                IF.AktualisiereAutor(id, aktuellerName);
                return false;
            }
            catch (FormatException e)
            {
                   BearbeiteAutor();
            }
            return false;
        }

        public bool EntfernAutor()
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
            return false;
        }

        public bool Select(string input)
        {
            switch (input)
            {
                case ("a"):
                    if (ZeigeAutoren())
                    {
                        return true;
                    }
                    break;
                case ("b"):
                    FuegAutorHinzu();
                    break;
                case ("c"):
                    BearbeiteAutor();
                    break;
                case ("d"):
                    EntfernAutor();
                    break;
                case ("e"):
                    BuecherNachAutor();
                    break;
                case ("f"):
                    FuegBuchHinzu();
                    break;
                case ("g"):
                    break;
                case ("h"):
                    break;
                case ("i"):
                    break;
                
            }
            return false;
        }
    }
}
