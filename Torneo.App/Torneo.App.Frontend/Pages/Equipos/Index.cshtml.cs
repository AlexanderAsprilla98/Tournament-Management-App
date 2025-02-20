using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Torneo.App.Frontend.Pages.Equipos
{
    public class IndexModel : PageModel
    {
        private readonly IRepositorioEquipo _repoEquipo;
        public IEnumerable<Equipo> equipos { get; set; }
        private readonly IRepositorioMunicipio _repoMunicipio;
        public SelectList MunicipioOptions { get; set; }
        public int MunicipioSelected { get; set; }
        public string BusquedaActual { get; set; }
        public bool ErrorEliminar { get; set; }
        public IndexModel(IRepositorioEquipo repoEquipo, IRepositorioMunicipio repoMunicipio)
        {
            _repoEquipo = repoEquipo;
            _repoMunicipio = repoMunicipio;
        }

      /*  public bool PartidoEncontrado(Equipo id){
                _repoEquipo.PartidoEncontrado(id);
        }
        */

        public void OnGet()
        {
            MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
            equipos = _repoEquipo.GetAllEquipos();
            MunicipioSelected = -1;
            BusquedaActual = "";
            ErrorEliminar = false;
            //Console.WriteLine("conteo municipios "+ equipos);            
        }
        public void OnPostFiltro(int idMunicipio)
        {
            MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
            if (idMunicipio != -1)
            {
                MunicipioSelected = idMunicipio;
                equipos = _repoEquipo.GetEquiposMunicipio(MunicipioSelected);
            }
            else
            {
                equipos = _repoEquipo.GetAllEquipos();
                MunicipioSelected = -1;
            }
        }
        public void OnPostBuscar(string nombre)
        {
            MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
            MunicipioSelected = -1;
            if (string.IsNullOrEmpty(nombre))
            {
                BusquedaActual = "";
                equipos = _repoEquipo.GetAllEquipos();
            }
            else
            {
                BusquedaActual = nombre;
                equipos = _repoEquipo.SearchEquipos(nombre);
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                _repoEquipo.DeleteEquipo(id);
                equipos = _repoEquipo.GetAllEquipos();
                MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                return Page();
            }
            catch (Exception ex)
            {
                ErrorEliminar = true;
                equipos = _repoEquipo.GetAllEquipos();
                return Page();
            }
        }

    }
}
