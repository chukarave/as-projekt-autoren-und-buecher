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

        private void lstAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IF.GetBuecher();
        }

        private void btnAllBooks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
