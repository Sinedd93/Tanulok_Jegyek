using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tanulok_és_Jegyek.Models;
using Tanulok_és_Jegyek.Service;

namespace Tanulok_és_Jegyek.Pages
{
    public class IndexModel : PageModel
    {
        #region Adattagok
        private readonly Services services;

        [BindProperty] public int Id { get; set; }
        [BindProperty] public string? Nev { get; set; }
        [BindProperty] public double Atlag { get; set; }
        [BindProperty] public string? SikeresUzenet { get; set; } = string.Empty;
        [BindProperty] public string? SikertelenUzenet { get; set; } = string.Empty;

        [BindProperty] public List<Tanulok> TanulokLista { get; set; } = new List<Tanulok>();

        [BindProperty] public string AtlagUzenet { get; set;} = string.Empty;

        [BindProperty] public string LegjobbUzenet { get; set; } = string.Empty;
        [BindProperty] public string LegrosszabbUzenet { get; set; } = string.Empty;

        #endregion

        public IndexModel(Services service)
        {
            this.services = service;
        }
        public void OnGet()
        {
            TanulokLista = services.GetTanulok();
        }

        #region Alprogramok
        public IActionResult OnPostAdd()
        {

            var hiba = services.Tanulok_Felvetele(Nev,Atlag);

            if(hiba != null)
            {
                SikertelenUzenet = hiba;
            }
            else
            {
                SikeresUzenet = $"Sikeres felvétel";
            }

            Nev = "";
            Atlag = 0.0;

            TanulokLista = services.GetTanulok();

            return Page();
        }
        public IActionResult OnPostUpdate()
        {
            var hiba = services.Tanulo_Modositasa(Id,Nev,Atlag);

            if(hiba != null)
            {
                SikertelenUzenet = hiba;
            }
            else
            {
                SikeresUzenet = "Sikeres tanuló módosítás";
            }

                Nev = "";
            Atlag = 0.0;

            TanulokLista = services.GetTanulok();

            return Page();
        }
        public IActionResult OnPostDelete()
        {
            var hiba = services.Tanulok_Torlese(Id);

            if(hiba != null)
            {
                SikertelenUzenet = hiba;
            }
            else
            {
                SikeresUzenet = "Sikeres törlés";
            }

            Nev = "";
            Atlag = 0.0;

            TanulokLista = services.GetTanulok();

            return Page();
        }
        public IActionResult OnPostAvarage()
        {
            double atlag = services.TanuloAtlag();

            if(atlag == 0.0)
            {
                AtlagUzenet = "Nincs tanuló a listában!";
            }
            else
            {
                AtlagUzenet = $"A tanulók átlaga: {atlag}";
            }

            TanulokLista = services.GetTanulok();

            return Page();
        }
        public IActionResult OnPostBest()
        {
            var legjobb = services.LegjobbTanulo();

            if(legjobb == null)
            {
                LegjobbUzenet = "Nincs tanuló a listában!";
            }
            else
            {
                LegjobbUzenet = $"A legjobb tanuló: ---> Név: {legjobb.Nev} ---> Átlag: {legjobb.Atlag}";
            }

            TanulokLista = services.GetTanulok();

            return Page();
        }
        public IActionResult OnPostWorst()
        {
            var legrosszabb = services.LegrosszabbTanulo();

            if(legrosszabb == null)
            {
                LegrosszabbUzenet = "Nincs tanuló a listában!";
            }
            else
            {
                LegrosszabbUzenet = $"A legroszabb tanuló Név: {legrosszabb.Nev} ---> Átlag: {legrosszabb.Atlag}";
            }


            TanulokLista = services.GetTanulok();

            return Page();
        }
        #endregion


    }
}
