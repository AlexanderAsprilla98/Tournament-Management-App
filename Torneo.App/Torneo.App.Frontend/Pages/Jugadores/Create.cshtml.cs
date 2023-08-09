using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioJugador _repoJugador;
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioPosicion _repoPosicion;
        public bool duplicate { get; set; }
        public CreateModel(IRepositorioJugador repoJugador, IRepositorioEquipo repoEquipo, IRepositorioPosicion repoPosicion)
        {
            _repoJugador = repoJugador;
            _repoEquipo = repoEquipo;
            _repoPosicion = repoPosicion;
        }

        public Jugador jugador { get; set; } = new Jugador();
        public IEnumerable<Equipo> equipos { get; set; }
        public IEnumerable<Posicion> posiciones { get; set; }

        public void OnGet()
        {
            jugador = new Jugador();
            equipos = _repoEquipo.GetAllEquipos();
            posiciones = _repoPosicion.GetAllPosiciones();
            duplicate = false;
        }

        public IActionResult OnPost(Jugador jugador, int idEquipo, int idPosicion)
        {
            //validar si existe un duplicado con el mismo, nombre, numero y posicione en el mismo equipo
            duplicate = _repoJugador.validateDuplicates(jugador, idEquipo, idPosicion);
            if (!duplicate)
            {
                _repoJugador.AddJugador(jugador, idEquipo, idPosicion);
                return RedirectToPage("Index");
            }
            else
            {
                //Cargar entidades refenciadas como la lista de equipos y posiciones si existen duplicados
                 jugador = new Jugador();
                equipos = _repoEquipo.GetAllEquipos();
                 posiciones = _repoPosicion.GetAllPosiciones();
                return Page();
            }
        }
    }
}
