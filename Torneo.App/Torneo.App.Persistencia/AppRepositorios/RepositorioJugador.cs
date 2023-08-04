using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioJugador : IRepositorioJugador
    {
        private DataContext _dataContext = new DataContext();
        public Jugador AddJugador(Jugador jugador, int idEquipo, int idPosicion)
        {
            var equipoEncontrado = _dataContext.Equipos.Find(idEquipo);
            var posicionEncontrada = _dataContext.Posiciones.Find(idPosicion);
            jugador.Equipo = equipoEncontrado == null ? null : equipoEncontrado;
            jugador.Posicion = posicionEncontrada == null ? null : posicionEncontrada;
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
            var jugadorEncontrado =_dataContext.Jugadores
                                   .Include(j => j.Equipo)
                                   .Include(j => j.Posicion)
                                   .FirstOrDefault(j => j.Id == idJugador);

            
            return jugadorEncontrado;
        }

        public Jugador UpdateJugador(Jugador jugador, int idEquipo, int idPosicion)
        {
            var jugadorEncontrado = GetJugador(jugador.Id);
            var equipoEncontrado = _dataContext.Equipos.Find(idEquipo);
            var posicionEncontrada= _dataContext.Posiciones.Find(idPosicion);
            jugadorEncontrado.Nombre = jugador.Nombre;
            jugadorEncontrado.Numero = jugador.Numero;
            jugadorEncontrado.Equipo = equipoEncontrado == null ? null : equipoEncontrado;
            jugadorEncontrado.Posicion = posicionEncontrada == null ? null : posicionEncontrada;
            _dataContext.SaveChanges();
            
            return jugadorEncontrado;
        }

        public Jugador DeleteJugador(int idJugador)
        {
            var jugadorEncontrado = _dataContext.Jugadores.Find(idJugador);
            if (jugadorEncontrado != null)
            {
                _dataContext.Jugadores.Remove(jugadorEncontrado);
                _dataContext.SaveChanges();
            } else {
                Console.WriteLine("No se encontró el Jugador");
            }
            return jugadorEncontrado ?? throw new Exception("Jugador not found");  // Throw an exception if dtEncontrado is null.
        }

        public bool validateDuplicates(Jugador jugador, int idEquipo, int idPosicion)
        {
            try
            {
                IEnumerable<Jugador> allJugadores =  GetAllJugadores();
                bool duplicado = false;                

                foreach(Jugador j in allJugadores)
                {
                    if(j.Nombre.ToLower()  == jugador.Nombre.ToLower().Trim() && j.Numero == jugador.Numero && j.Equipo.Id == idEquipo && j.Posicion.Id == idPosicion)   
                    {
                        duplicado = true;
                    }              
                }               
                Console.WriteLine("Jugador duplicado al Crear/Editar " + jugador.Nombre  +" - "+ duplicado);
                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion " + e.Message);
                return false;
            }
        }
    } 
}