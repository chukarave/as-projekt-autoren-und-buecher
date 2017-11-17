using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_3_Schichten_Architektur
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GUI myGUI;
            TUI myTUI;

            int i = 6;

            switch (i)
            {
                case 1:
                    myGUI = new GUI(new Fachkonzept1(new Dateihaltung()));
                    break;
                case 2:
                    myGUI = new GUI(new Fachkonzept1(new Datenbankhaltung()));
                    break;
                case 3:
                    myGUI = new GUI(new Fachkonzept2(new Dateihaltung()));
                    break;
                case 4:
                    myGUI = new GUI(new Fachkonzept2(new Datenbankhaltung()));
                    break;
                case 5:
                    myTUI = new TUI(new Fachkonzept1(new Dateihaltung()));
                    break;
                case 6:
                    myTUI = new TUI(new Fachkonzept1(new Datenbankhaltung()));
                    break;
                case 7:
                    myTUI = new TUI(new Fachkonzept2(new Dateihaltung()));
                    break;
                case 8:
                    myTUI = new TUI(new Fachkonzept2(new Datenbankhaltung()));
                    break;
                default:
                    break;
            }
            this.Close();
        }
    }
}
