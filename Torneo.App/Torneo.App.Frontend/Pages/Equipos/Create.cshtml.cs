using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Equipos
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioMunicipio _repoMunicipio;
        private readonly IRepositorioDT _repoDT;
        public bool duplicate { get; set; }
        public Equipo equipo { get; set; }
        public IEnumerable<Municipio> municipios { get; set; }
        public IEnumerable<DirectorTecnico> dts { get; set; }

        public CreateModel(IRepositorioEquipo repoEquipo, IRepositorioMunicipio repoMunicipio, IRepositorioDT repoDT)
        {
            _repoEquipo = repoEquipo;
            _repoMunicipio = repoMunicipio;
            _repoDT = repoDT;
        }


        public void OnGet()
        {
            equipo = new Equipo();
            municipios = _repoMunicipio.GetAllMunicipios();
            dts = _repoDT.GetAllDTs();  

            // Inicializar Model.duplicate en false
            duplicate = false;          
        }

        public IActionResult OnPost(Equipo equipo, int idMunicipio, int idDT)
        {
            duplicate =  _repoEquipo.validateDuplicates(equipo, idMunicipio, idDT);
            if(!duplicate)
            {
                _repoEquipo.AddEquipo(equipo, idMunicipio, idDT);
                return RedirectToPage("Index");
            } 
            else
            {           
                //Cargar municipios y Dts   
                equipo = new Equipo();
                municipios = _repoMunicipio.GetAllMunicipios();
                dts = _repoDT.GetAllDTs();     
                return Page();
                //return RedirectToPage("Create");
            }
        }


    }
}
