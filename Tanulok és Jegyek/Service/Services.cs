using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using Tanulok_és_Jegyek.Models;

namespace Tanulok_és_Jegyek.Service
{
    public class Services
    {
        #region Adattagok
        private List<Tanulok> lista = new List<Tanulok>();
        private int nextId = 1;
        #endregion

        #region Konstruktor
        public Services()
        {
            Beolvasás("Data/Tanulok.txt");
        }
        #endregion

        #region Adattagok
        public string Tanulok_Felvetele(string nev,double atlag)
        {
            try
            {
                Tanulok uj = new Tanulok()
                {
                    Id = nextId,
                    Nev = nev,
                    Atlag = atlag
                };
                lista.Add(uj);

                

                Kiiras("Data/Tanulok.txt");

                nextId++;

                return null;
            }
            catch(ArgumentException ex)
            {
                return ex.Message;
            }

            
        }
        public string Tanulo_Modositasa(int id,string nev,double atlag)
        {
            try
            {
                if(lista.Count == 0)
                {
                    return "Nincs tanuló a listában!";
                }

                if(lista.Count >= 1)
                {
                    var talalt = lista.FirstOrDefault(tanulok => tanulok.Id == id);

                    if( talalt == null)
                    {
                        return "Nincs ilyen tanuló!";
                    }

                    talalt.Nev = nev;
                    talalt.Atlag = atlag;

                    Kiiras("Data/Tanulok.txt");

                }

                return null;

                
            }
            catch(ArgumentException ex)
            {
                return ex.Message;
            }

            
        }
        public string Tanulok_Torlese(int id)
        {
            try
            {
                if(lista.Count == 0)
                {
                    return "Nincs tanuló a listában!";
                }

                var talalt = lista.FirstOrDefault(tanulok => tanulok.Id == id);

                if(talalt == null)
                {
                    return "Nincs ilyen tanuló!";
                }

                lista.Remove(talalt);
                Kiiras("Data/Tanulok.txt");

                return null;
            }
            catch(ArgumentException ex)
            {
                return ex.Message;
            }

            
        }
        public double TanuloAtlag()
        {
            if(lista.Count == 0)
            {
                return 0;
                
            }
            double összeg = 0;

            for (int i = 0; i < lista.Count; i++)
            {
                összeg += lista[i].Atlag;
            }

            return összeg / lista.Count;

        }
        public Tanulok LegjobbTanulo()
        {
            if(lista.Count == 0)
            {
                return null;
            }

            Tanulok legjobb = lista[0];

            for (int i = 1; i < lista.Count; i++)
            {
                if (legjobb.Atlag < lista[i].Atlag)
                {
                    legjobb = lista[i];
                }
            }

            return legjobb;

            
        }
        public Tanulok LegrosszabbTanulo()
        {
            
            if (lista.Count == 0)
            {
                return null;
            }

            Tanulok legrosszabb = lista[0];

            for (int i = 1; i < lista.Count; i++)
            {
                if (legrosszabb.Atlag > lista[i].Atlag)
                {
                    legrosszabb = lista[i];
                }
            }

            return legrosszabb;
        }
        public void Beolvasás(string eleres)
        {
            StreamReader olvaso = new StreamReader(eleres,Encoding.UTF8);

            while (!olvaso.EndOfStream)
            {
                string sor = olvaso.ReadLine();

                if (string.IsNullOrWhiteSpace(sor))
                {
                    continue;
                }

                string[] olvas = sor.Split(",");

                int id = int.Parse(olvas[0]);
                string nev = olvas[1];
                double atlag = Convert.ToDouble(olvas[2], CultureInfo.InvariantCulture);

                lista.Add(new Tanulok(id,nev,atlag));
            }
            olvaso.Close();

            if(lista.Count > 0)
            {
                nextId = lista.Max(t => t.Id) + 1;
            }

        }
        private void Kiiras(string eleres)
        {
            StreamWriter iro = new StreamWriter(eleres, false, Encoding.UTF8);

            foreach(var l in lista)
            {
                iro.WriteLine($"{l.Id},{l.Nev},{l.Atlag.ToString(CultureInfo.InvariantCulture)}");
            }
            iro.Close();
        }
        #endregion

        #region Tulajdonságok
        public List<Tanulok> GetTanulok() => lista;
        #endregion

    }
}
