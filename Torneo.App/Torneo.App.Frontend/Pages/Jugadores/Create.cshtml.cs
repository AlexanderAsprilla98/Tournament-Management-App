using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    public class CreateModel : PageModel
    {
        private readonly IRepositorioJugador _repoJugador;
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioPosicion _repoPosicion;

        public CreateModel(IRepositorioJugador repoJugador, IRepositorioEquipo repoEquipo, IRepositorioPosicion repoPosicion)
        {
            _repoJugador = repoJugador;
            _repoEquipo = repoEquipo;
            _repoPosicion = repoPosicion;
        }

        public Jugador jugador { get; set; }
        public IEnumerable<Equipo> equipos { get; set; }
        public IEnumerable<Posicion> posiciones { get; set; }

        public void OnGet()
        {
            jugador = new Jugador();
            equipos = _repoEquipo.GetAllEquipos();
            posiciones = _repoPosicion.GetAllPosiciones();
        }

        public IActionResult OnPost(Jugador jugador, int idEquipo, int idPosicion)
        {
            _repoJugador.AddJugador(jugador, idEquipo, idPosicion);
            return RedirectToPage("Index");
        }

    }
}
