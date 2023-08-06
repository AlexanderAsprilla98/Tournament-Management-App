using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Posiciones
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRepositorioPosicion _repoPosicion;

        public EditModel(IRepositorioPosicion repoPosicion)
        {
            _repoPosicion = repoPosicion;
        }

        public Posicion posicion { get; set; }
        public bool duplicate { get; set; }


        public IActionResult OnGet(int id)
        {
            posicion = _repoPosicion.GetPosicion(id);
            if (posicion == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPost(Posicion posicion)
        {
            duplicate = _repoPosicion.validateDuplicates(posicion.Nombre);
            if(!duplicate)
            {
                _repoPosicion.UpdatePosicion(posicion);
                return RedirectToPage("Index");
            } else
            {
                return Page();
            }
        }
    }
}
