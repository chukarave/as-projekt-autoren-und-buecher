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
using System.Windows.Shapes;

namespace Projekt_3_Schichten_Architektur
{
    /// <summary>
    /// Interaktionslogik für GUI.xaml
    /// </summary>
    public partial class GUI : Window
    {
        IFachkonzept IF;
        public GUI(IFachkonzept _IF)
        {
            InitializeComponent();
            this.IF = _IF;
            this.Show();
        }
    }
}
