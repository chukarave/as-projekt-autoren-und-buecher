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
            if (lstAuthors.SelectedItem != null)
            {
                FillBooks(IF.GetBuecher(((Autor)lstAuthors.SelectedItem).Autoren_id));
                txtAuthorName.Text = ((Autor)lstAuthors.SelectedItem).Name;
                txtAuthorID.Text = ((Autor)lstAuthors.SelectedItem).Autoren_id.ToString();
            }
        }

        private void lstBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                txtBookISBN.Text = ((Buch)lstBooks.SelectedItem).ISBN;
                txtBookTitle.Text = ((Buch)lstBooks.SelectedItem).Titel;
            }
        }

        private void btnAllBooks_Click(object sender, RoutedEventArgs e)
        {
            FillBooks(IF.GetBuecher());
            EmptyFields();
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Name = txtAuthorName.Text;
            IF.SpeichereAutor(Name);
            FillAuthors();
            if (lstAuthors.SelectedItem != null)
            {
                FillBooks(IF.GetBuecher(int.Parse(lstAuthors.SelectedValue.ToString())));
            }
            else
            {
                FillBooks(IF.GetBuecher());
            }
            EmptyFields();
        }

        private void btnSaveAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Name = txtAuthorName.Text;
            int ID = int.Parse(txtAuthorID.Text);
            IF.AktualisiereAutor(ID, Name);
            FillAuthors();
            EmptyFields();
        }

        private void btnDeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (lstAuthors.SelectedItem != null)
            {
                int ID = int.Parse(lstAuthors.SelectedValue.ToString());
                IF.LoescheAutor(ID);
                FillAuthors();
                FillBooks(IF.GetBuecher());
            }
            else
            {
                MessageBox.Show("Kein Autor ausgewählt.");
            }
            EmptyFields();
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            if (lstAuthors.SelectedItem != null)
            {
                int AuthorID = int.Parse(lstAuthors.SelectedValue.ToString());
                string ISBN = txtBookISBN.Text;
                string Titel = txtBookTitle.Text;
                IF.SpeichereBuch(AuthorID, ISBN, Titel);
                FillBooks(IF.GetBuecher(int.Parse(lstAuthors.SelectedValue.ToString())));
            }
            else
            {
                MessageBox.Show("Kein Autor ausgewählt.");
            }
            EmptyFields();
        }

        private void btnSaveBook_Click(object sender, RoutedEventArgs e)
        {
            string ISBN = ((Buch)lstBooks.SelectedItem).ISBN;
            string Titel = txtBookTitle.Text;
            IF.AktualisiereBuch(ISBN, Titel);
            if (lstAuthors.SelectedItem != null)
            {
                FillBooks(IF.GetBuecher(int.Parse(lstAuthors.SelectedValue.ToString())));
            }
            else
            {
                FillBooks(IF.GetBuecher());
            }
            EmptyFields();
        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                string ISBN = ((Buch)lstBooks.SelectedItem).ISBN;
                IF.LoescheBuch(ISBN);
            }
            else
            {
                MessageBox.Show("Kein Buch ausgewählt.");
            }
            if (lstAuthors.SelectedItem != null)
            {
                FillBooks(IF.GetBuecher(int.Parse(lstAuthors.SelectedValue.ToString())));
            }
            else
            {
                FillBooks(IF.GetBuecher());
            }
            EmptyFields();
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

        private void EmptyFields()
        {
            txtAuthorID.Text = "";
            txtAuthorName.Text = "";
            txtBookISBN.Text = "";
            txtBookTitle.Text = "";
            lstAuthors.SelectedItem = null;
        }
    }
}
