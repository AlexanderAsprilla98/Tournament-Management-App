using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Partidos
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioPartido _repoPartido;
        private readonly IRepositorioEquipo _repoEquipo;

        public Partido partido { get; set; } = new Partido();
        public IEnumerable<Equipo> equipos { get; set; }
        public bool duplicate { get; set; }
        public CreateModel(IRepositorioPartido repoPartido, IRepositorioEquipo repoEquipo)
        {
            _repoPartido = repoPartido;
            _repoEquipo = repoEquipo;
        }

        public void OnGet()
        {
            partido = new Partido();
            equipos = _repoEquipo.GetAllEquipos();
            duplicate = false;
        }

        public IActionResult OnPost(Partido partido, int EquipoLocalId, int EquipoVisitanteId)
        {
            //Validar duplicados de partidos por EquipoLocal, EquipoVisitante y fechaHora exacta
            duplicate =  _repoPartido.validateDuplicates(partido, EquipoLocalId, EquipoVisitanteId);

            //Condicion para crear Parditos si no existen duplicados en BD
            if(!duplicate)
            {
               _repoPartido.AddPartido(partido, EquipoLocalId, EquipoVisitanteId);
                return RedirectToPage("Index");
            }

            //Si partido ya exite retornar a la misma Page
            else
            {
                //Cargar nuevamente la lista de los equipos de partidos y Equipos seleccionados
                partido = new Partido();
                equipos = _repoEquipo.GetAllEquipos();
            
                return Page();
            }

        }
    }
}
