using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;

namespace Torneo.App.Frontend.Pages.Posiciones
{
    public class CreateModel : PageModel
    {
        private readonly IRepositorioPosicion _repoPosicion;

        public CreateModel(IRepositorioPosicion repoPosicion)
        {
            _repoPosicion = repoPosicion;
        }

        public Posicion posicion { get; set; }

        public void OnGet()
        {
            posicion = new Posicion();
        }

        public IActionResult OnPost(Posicion posicion)
        {
            _repoPosicion.AddPosicion(posicion);
            return RedirectToPage("Index");
        }


    }
}
