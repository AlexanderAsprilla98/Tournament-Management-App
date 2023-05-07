using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    public class EditModel : PageModel
    {
        //Atributos
        private readonly IRepositorioJugador _repoJugador;
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioPosicion _repoPosicion;

        //Propiedades tipo jugador y tipo SelectList para poblar los selects de equipos y posiciones
        public Jugador jugador { get; set; }
        public SelectList EquipoOptions { get; set; }
        public SelectList PosicionOptions { get; set; }

        //Equipo y posicion que tiene asignado el jugador para que aparescan como seleccionados en los selects
        public int EquipoSelected { get; set; }
        public int PosicionSelected { get; set; }
        

        //Instanciar en el constructor _repoEquipo, _repoMunicipio, _repoDT
        public EditModel(IRepositorioJugador repoJugador, IRepositorioEquipo repoEquipo, IRepositorioPosicion repoPosicion)
        {
            _repoJugador = repoJugador;
            _repoEquipo = repoEquipo;
            _repoPosicion = repoPosicion;
        }

        //Se inicializan las propiedades e instacian los SelectList.
        public IActionResult OnGet(int id)
        {
            jugador = _repoJugador.GetJugador(id);
            EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
            EquipoSelected = jugador.Equipo.Id;
            PosicionOptions = new SelectList(_repoPosicion.GetAllPosiciones(), "Id", "Nombre");
            PosicionSelected = jugador.Posicion.Id;

            //Validacion si el equipo es null retorne un No encontrado, de lo contrario carga el Page
            if (jugador == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost(Jugador jugador, int idEquipo, int idPosicion)
        {
            _repoJugador.UpdateJugador(jugador, idEquipo, idPosicion);
            return RedirectToPage("Index");
        }
    }
}
