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
        public TUI(IFachkonzept _IF)
        {
            this.IF = _IF;
         //   ConsoleManager.Toggle();
            char input;
            Console.CursorVisible = true;
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
            Console.WriteLine("Wählen Sie einen Menüpunkt:       ");
            input = Console.ReadKey().KeyChar;
            Select(input);
            Console.Read();
        }
        
        public void Select(char input)
        {
            var fachkonzept = new Fachkonzept2(new Dateihaltung());
            switch (input)
            {
                case ('a'):
                    Console.WriteLine();
                    var autorenIndex = fachkonzept.GetAutoren();
                    if (autorenIndex == null) {
                        Console.WriteLine("Index is null");
                }
                    // Reverse list order
                    autorenIndex.Reverse();
                    foreach (var autor in autorenIndex)
                    {
                        Console.WriteLine(autor.Autoren_id + ". " + autor.Name);
                    }
                    break;
                case ('b'):
                    Console.WriteLine("Bitte Geben Sie den Autor Name ein: ");
                    var autorName = Console.ReadLine();
                    fachkonzept.SpeichereAutor(autorName);
                    break;
                case ('c'):
                    var autorenList = fachkonzept.GetAutoren();
                    foreach (var autor in autorenList) 
                    {
                        Console.WriteLine("ID: " + autor.Autoren_id + " Name: " + autor.Name);
                    }
                    Console.WriteLine("Bitte geben Sie die ID Nummer des gewünschten Autors ein: ");
                    Console.ReadLine();
                    var id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Bitte geben Sie die gewünschte Änderung ein: ");
                    Console.ReadLine();
                    var aktuellerName = Console.ReadLine();
                    if (string.IsNullOrEmpty(aktuellerName))
                    {
                        Select('c');
                    }
                    fachkonzept.AktualisiereAutor(id, aktuellerName);
                    break;
                case ('d'):
                    break;
                case ('e'):
                    break;
                case ('f'):
                    break;
                case ('g'):
                    break;
                case ('h'):
                    break;
                case ('i'):
                    break;
                
            }
        }
    }
}
