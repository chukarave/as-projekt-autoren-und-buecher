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
            FillAuthors();
        }

        private void lstAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillBooks(IF.GetBuecher(((Autor)lstAuthors.SelectedValue).Autoren_id));
            txtAuthorName.Text = ((Autor)lstAuthors.SelectedValue).Name;
            txtAuthorID.Text = ((Autor)lstAuthors.SelectedValue).Autoren_id.ToString();
        }

        private void lstBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtBookISBN.Text = ((Buch)lstBooks.SelectedValue).ISBN;
            txtBookTitle.Text = ((Buch)lstBooks.SelectedValue).Titel;
        }

        private void btnAllBooks_Click(object sender, RoutedEventArgs e)
        {
            FillBooks(IF.GetBuecher());
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Name = txtAuthorName.Text;
            IF.SpeichereAutor(Name);
            FillAuthors();
        }

        private void btnSaveAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Name = txtAuthorName.Text;
            int ID = int.Parse(lstAuthors.SelectedValue.ToString());
            IF.AktualisiereAutor(ID, Name);
            FillAuthors();
        }

        private void btnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            int ID = int.Parse(lstAuthors.SelectedValue.ToString());
            IF.LoescheAutor(ID);
            FillAuthors();
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            int AuthorID = int.Parse(lstAuthors.SelectedValue.ToString());
            string ISBN = txtBookISBN.Text;
            string Titel = txtBookTitle.Text;
            IF.SpeichereBuch(AuthorID, ISBN, Titel);
        }

        private void btnSaveBook_Click(object sender, RoutedEventArgs e)
        {
            string ISBN = txtBookISBN.Text;
            string Titel = txtBookTitle.Text;
            IF.AktualisiereBuch(ISBN, Titel);
        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            string ISBN = txtBookISBN.Text;
            IF.LoescheBuch(ISBN);
        }

        private void FillAuthors()
        {
            List<Autor> Autoren = IF.GetAutoren();
            lstAuthors.ItemsSource = Autoren;
            lstAuthors.DisplayMemberPath = "Name";
            lstAuthors.SelectedValuePath = "Autoren_id";
        }

        private void FillBooks(List<Buch> Buecher)
        {
            lstBooks.ItemsSource = Buecher;
            lstBooks.DisplayMemberPath = "Titel";
            lstBooks.SelectedValuePath = "ISBN";
        }
    }
}
