using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;


namespace Torneo.App.Frontend.Pages.Jugadores
{

    public class DetailsModel : PageModel
    {   
        //Atributo de IRepositorioJugador
        private readonly IRepositorioJugador _repoJugador;
        public Jugador jugador { get; set; }


        //Contrucctor
        public DetailsModel(IRepositorioJugador repoJugador)
        {
            _repoJugador = repoJugador;
        }

        //metodo OnGet para buscar jugadores por el id invocando el metodo GetJugador de RepositorioJugador
        public IActionResult OnGet(int id)
        {
            jugador = _repoJugador.GetJugador(id);
            if(jugador == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}
