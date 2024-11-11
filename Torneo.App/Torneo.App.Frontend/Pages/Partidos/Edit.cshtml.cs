using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Partidos
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRepositorioPartido _repoPartido;
        private readonly IRepositorioEquipo _repoEquipo;
        public Partido partido { get; set; }
        public SelectList equipoOptions { get; set; }
        public int EquipoLocalSelected { get; set; }
        public int EquipoVisitanteSelected { get; set; }
         public bool duplicate { get; set; }
        public EditModel(IRepositorioPartido repoPartido, IRepositorioEquipo repoEquipo)
        {
            _repoPartido = repoPartido;
            _repoEquipo = repoEquipo;
        }
        public IActionResult OnGet(int id)
        {
            partido = _repoPartido.GetPartido(id);
            equipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
            EquipoLocalSelected = partido.Local.Id;
            EquipoVisitanteSelected = partido.Visitante.Id;
            duplicate = false;
            if (partido == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPost(Partido partido, int idEquipoLocal, int idEquipoVisitante, int id)
        {
            try
            {
                Console.WriteLine("Estoy dentro del try de editar partidos"); 
                Console.WriteLine("Equipo local escogido " + idEquipoLocal);                 
                Console.WriteLine("Equipo visitante escogido " + idEquipoVisitante);                                               
                partido.Local = _repoEquipo.GetEquipo(idEquipoLocal);
                partido.Visitante = _repoEquipo.GetEquipo(idEquipoVisitante);

                if(ModelState.IsValid)
                    {  
                    //Validar duplicados de partidos por EquipoLocal, EquipoVisitante y fechaHora exacta
                    duplicate =  _repoPartido.validateDuplicates(partido, idEquipoLocal, idEquipoVisitante);

                    //Condicion para crear Parditos si no existen duplicados en BD
                    if(!duplicate)
                    {
                    _repoPartido.UpdatePartido(partido, idEquipoLocal, idEquipoVisitante);
                    return RedirectToPage("Index");
                    }
                    //Si partido ya exite retornar a la misma Page
                    else
                    {
                        //Cargar nuevamente la lista de los equipos de partidos y Equipos seleccionados
                        partido = _repoPartido.GetPartido(id);
                        equipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
                        EquipoLocalSelected = partido.Local.Id;
                        EquipoVisitanteSelected = partido.Visitante.Id;                    
                        return Page();
                    }
                }else
                {
                    Console.WriteLine("Modelo equipo no valido al editar");  
                    //Cargar nuevamente la lista de los equipos de partidos y Equipos seleccionados
                    partido = _repoPartido.GetPartido(id);
                    equipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
                    EquipoLocalSelected = partido.Local.Id;
                    EquipoVisitanteSelected = partido.Visitante.Id;                    
                    return Page();
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Catch error en Editar Partidos: " + ex.Message);  
               //Cargar nuevamente la lista de los equipos de partidos y Equipos seleccionados
                partido = _repoPartido.GetPartido(id);
                equipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
                EquipoLocalSelected = partido.Local.Id;
                EquipoVisitanteSelected = partido.Visitante.Id;                
                return  Page();
            }

            
        }
    }
}
