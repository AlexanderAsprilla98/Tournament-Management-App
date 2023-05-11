using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioPosicion : IRepositorioPosicion
    {
        private  DataContext _dataContext = new DataContext();
        public Posicion AddPosicion(Posicion posicion)
        {
            var posicionInsertada = _dataContext.Posiciones.Add(posicion);
            _dataContext.SaveChanges();
            return posicionInsertada.Entity;
        }
        public IEnumerable<Posicion> GetAllPosiciones()
        {   
            var posiciones = _dataContext.Posiciones
                                .Include(p => p.Jugadores)
                                .ToList();

            _dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();   

            return posiciones;
        }
        public Posicion GetPosicion(int idPosicion)
        {
            var posicionEncontrado = _dataContext.Posiciones.Find(idPosicion);
            return posicionEncontrado;
        }

        public Posicion UpdatePosicion(Posicion posicion)
        {
            var posicionEncontrada = GetPosicion(posicion.Id);
            posicionEncontrada.Nombre = posicion.Nombre;
            _dataContext.SaveChanges();

            return posicionEncontrada;
        }

        public Posicion DeletePosicion(int idPosicion)
        {
            var posicionEncontrada = GetPosicion(idPosicion);
            if (posicionEncontrada != null)
            {
                
                _dataContext.Posiciones.Remove(posicionEncontrada);
                _dataContext.SaveChanges();
                
                
            }           
            return posicionEncontrada;
        }



    }
}