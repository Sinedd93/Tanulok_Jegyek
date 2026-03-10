namespace Tanulok_és_Jegyek.Models
{
    public class Tanulok
    {
        #region Adattagok
        private int id;
        private string nev;
        private double atlag;
        private string sikeresUzenet;
        private string sikertelenUzenet;
        private string atlagUzenet;
        private string legjobbTanuloUzenet;
        private string legroszabbTanuloUzenet;
        #endregion

        #region Konstruktor
        public Tanulok()
        {

        }
        public Tanulok(int id,string nev,double atlag)
        {
            this.Id = id;
            this.Nev = nev;
            this.Atlag = atlag;
        }
        #endregion

        #region Tulajdonságok
        public int Id { get => id; set => id = value; }

        public string Nev
        {
            get => nev;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A név megadása kötelező!");
                }
                else
                {
                    nev = value;
                }
            }
        }
        public double Atlag
        {
            get => atlag;
            set
            {
                if(value < 1.0 || value > 5.0)
                {
                    throw new ArgumentException("Az átlag... 1.0 és 5.0 között lehet!");
                }
                else
                {
                    atlag = value;
                }
            }
        }

        public string SikeresUzenet { get => sikeresUzenet; set => sikeresUzenet = value; }
        public string SikertelenUzenet { get => sikertelenUzenet; set => sikertelenUzenet = value; }
        public string AtlagUzenet { get => atlagUzenet; set => atlagUzenet = value; }
        public string LegjobbTanuloUzenet { get => legjobbTanuloUzenet; set => legjobbTanuloUzenet = value; }
        public string LegroszabbTanuloUzenet { get => legroszabbTanuloUzenet; set => legroszabbTanuloUzenet = value; }

        #endregion
    }
}
