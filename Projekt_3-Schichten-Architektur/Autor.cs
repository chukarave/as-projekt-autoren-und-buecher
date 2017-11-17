namespace Projekt_3_Schichten_Architektur
{
    public class Autor
    {
        private int _autorenid;
        private string _name;

        public int Autoren_id
        {
            get { return _autorenid; }
            set { _autorenid = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Autor()
        {

        }

        public Autor(int _Id, string _Name)
        {
            this.Autoren_id = _Id;
            this.Name = _Name;
        }
    }
}
