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
                                .AsNoTracking()
                                .ToList();

            /*_dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();*/

            return posiciones ?? throw new Exception("Posiciones not found");  // Throw an exception if posiciones is null.
        }
        public Posicion GetPosicion(int idPosicion)
        {
            var posicionEncontrado = _dataContext.Posiciones.Find(idPosicion);
            return posicionEncontrado ?? throw new Exception("Posicion not found");  // Throw an exception if posicionEncontrado is null.
        }

        public Posicion UpdatePosicion(Posicion posicion)
        {
            var posicionEncontrada = GetPosicion(posicion.Id);
            posicionEncontrada.Nombre = posicion.Nombre;
            _dataContext.SaveChanges();

            return posicionEncontrada ?? throw new Exception("Posicion not found");  // Throw an exception if posicionEncontrada is null.
        }

        public Posicion DeletePosicion(int idPosicion)
        {
            var posicionEncontrada = GetPosicion(idPosicion);
            if (posicionEncontrada != null)
            {
                
                _dataContext.Posiciones.Remove(posicionEncontrada);
                _dataContext.SaveChanges();
                
                
            } else
            {
                Console.WriteLine("No se encontró la posicion");
            }
            return posicionEncontrada ?? throw new Exception("Posicion not found");  // Throw an exception if posicionEncontrada is null.
        }

        public bool validateDuplicates(Posicion posicionIngresada)
        {
            try
            {
                IEnumerable<Posicion> allPosiciones =  GetAllPosiciones();
                bool duplicado = false;                

                foreach(Posicion posicion in allPosiciones)
                {
                    if(posicionIngresada.Id != posicion.Id){
                        if(posicion.Nombre.ToLower()  == posicionIngresada.Nombre.ToLower().Trim())   
                        {
                            duplicado = true;
                            break;
                        }   
                    }

                }               
                Console.WriteLine("Posicion duplicada al Crear/Editar " + posicionIngresada.Nombre.ToString()  +" - "+ duplicado);
                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion " + e.Message);
                return false;
            }
        }
    }
}