using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioJugador : IRepositorioJugador
    {
        private readonly DataContext _dataContext = new DataContext();
        public Jugador AddJugador(Jugador jugador, int idEquipo, int idPosicion)
        {
            var equipoEncontrado = _dataContext.Equipos.Find(idEquipo);
            var posicionEncontrada = _dataContext.Posiciones.Find(idPosicion);
            jugador.Equipo = equipoEncontrado;
            jugador.Posicion = posicionEncontrada;
            var jugadorInsertado = _dataContext.Jugadores.Add(jugador);
            _dataContext.SaveChanges();
            return jugadorInsertado.Entity;
        }
        public IEnumerable<Jugador> GetAllJugadores()
        {
            var jugadores = _dataContext.Jugadores
                            .Include(j => j.Equipo)
                            .Include(j => j.Posicion)
                            .ToList();
            return jugadores;
        }

        public Jugador GetJugador(int idJugador){
            var jugadorEncontrado =_dataContext.Jugadores.Find(idJugador);
            return jugadorEncontrado;
        }

        public Jugador UpdateJugador(Jugador jugador, int idEquipo, int idPosicion){
            var jugadorEncontrado = GetJugador(jugador.Id);
            var equipoEncontrado = _dataContext.Equipos.Find(idEquipo);
            var posicionEncontrada= _dataContext.Posiciones.Find(idPosicion);
            jugadorEncontrado.Nombre = jugador.Nombre;
            jugadorEncontrado.Numero = jugador.Numero;
            jugadorEncontrado.Equipo = equipoEncontrado;
            jugadorEncontrado.Posicion = posicionEncontrada;
            _dataContext.SaveChanges();
            
            return jugadorEncontrado;
        }

    } 
}