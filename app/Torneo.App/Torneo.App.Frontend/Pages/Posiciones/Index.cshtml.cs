using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;

namespace Torneo.App.Frontend.Pages.Posiciones
{
    public class IndexModel : PageModel
    {
        private readonly IRepositorioPosicion _repoPosicion;
        public IEnumerable<Posicion> posiciones { get; set; }
         public bool ErrorEliminar { get; set; }
        public IndexModel(IRepositorioPosicion repoPosicion)
        {
            _repoPosicion = repoPosicion;
        }
        public void OnGet()
        {
            posiciones = _repoPosicion.GetAllPosiciones();
            ErrorEliminar = false;
        }

        
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                _repoPosicion.DeletePosicion(id);
                posiciones = _repoPosicion.GetAllPosiciones();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorEliminar = true;
                posiciones = _repoPosicion.GetAllPosiciones();
                return Page();
            }
        }




    }
}
