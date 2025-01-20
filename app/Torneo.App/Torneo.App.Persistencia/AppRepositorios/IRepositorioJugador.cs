using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public interface IRepositorioJugador
    {
        public Jugador AddJugador(Jugador jugador, int idEquipo, int idPosicion);
        public IEnumerable<Jugador> GetAllJugadores();
        public Jugador GetJugador(int idJugador);
        public Jugador UpdateJugador(Jugador jugador, int idEquipo, int idPosicion);
        public Jugador DeleteJugador(int idJugador);
        public bool validateDuplicates(Jugador jugador, int idEquipo, int idPosicion);
    }
}