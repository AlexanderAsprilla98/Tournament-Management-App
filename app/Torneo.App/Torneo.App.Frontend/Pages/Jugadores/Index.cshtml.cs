using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    public class IndexModel : PageModel
    {
        private readonly IRepositorioJugador _repoJugador;
        public IEnumerable<Jugador> jugadores { get; set;}
        public bool ErrorEliminar { get; set; }
        public IndexModel(IRepositorioJugador repoJugador)
        {
            _repoJugador = repoJugador;
        }

        public void OnGet()
        {
            jugadores = _repoJugador.GetAllJugadores();
            ErrorEliminar = false;
        }

        
         public IActionResult OnPostDelete(int id)
        {
            try
            {
                _repoJugador.DeleteJugador(id);
                jugadores = _repoJugador.GetAllJugadores();
                return Page();
            }
            catch (Exception ex)
            {
                ErrorEliminar = true;
                jugadores = _repoJugador.GetAllJugadores();
                return Page();
            }
        }
    }

}
