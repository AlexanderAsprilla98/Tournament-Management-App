using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioEquipo : IRepositorioEquipo
    {
        private  DataContext _dataContext = new DataContext();
        public Equipo AddEquipo(Equipo equipo, int idMunicipio, int idDT)
        {
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            var DTEncontrado = _dataContext.DirectoresTecnicos.Find(idDT);
            equipo.Municipio = municipioEncontrado == null ? null : municipioEncontrado;
            equipo.DirectorTecnico = DTEncontrado == null ? null : DTEncontrado;
            var equipoInsertado = _dataContext.Equipos.Add(equipo);
            _dataContext.SaveChanges();
            return equipoInsertado.Entity;
        }
        public IEnumerable<Equipo> GetAllEquipos()
        {
            var equipos = _dataContext.Equipos
                            .Include(e => e.Municipio)
                            .Include(e => e.DirectorTecnico)                            
                            .Include(e => e.Jugadores)
                            .Include(e => e.PartidosLocal)                         
                            .Include(e => e.PartidosVisitante)                         
                            .ToList();
            _dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();           
                            
            return equipos;
        }

        public Equipo GetEquipo(int idEquipo)
        {
            var equipo = _dataContext.Equipos
                            .Include(e => e.Municipio)
                            .Include(e => e.DirectorTecnico)
                            .FirstOrDefault(e => e.Id == idEquipo);
            return equipo ?? throw new Exception("Equipo not found");  // Throw an exception if equipo is null.
        }

        public Equipo UpdateEquipo(Equipo equipo, int idMunicipio, int idDT)
        {
            var equipoEncontrado = GetEquipo(equipo.Id);
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            var DTEncontrado = _dataContext.DirectoresTecnicos.Find(idDT);
            equipoEncontrado.Nombre = equipo.Nombre;
            equipoEncontrado.Municipio = municipioEncontrado == null ? null : municipioEncontrado;
            equipoEncontrado.DirectorTecnico = DTEncontrado == null ? null : DTEncontrado;
            _dataContext.SaveChanges();
            return equipoEncontrado;
        }

        public Equipo DeleteEquipo(int idEquipo)
        {
            var equipoEncontrado = GetEquipo(idEquipo);
            if (equipoEncontrado != null)
            {
                
                _dataContext.Equipos.Remove(equipoEncontrado);
                _dataContext.SaveChanges();
                
                
            } else
            {
                Console.WriteLine("No se encontró el equipo");
            }           
            return equipoEncontrado ?? throw new Exception("Equipo not found");  // Throw an exception if equipoEncontrado is null.
        }

        public IEnumerable<Equipo> GetEquiposMunicipio(int idMunicipio)
        {
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            var equipos = _dataContext.Equipos
                        .Where(e => e.Municipio == municipioEncontrado)
                        .Include(e => e.Municipio)
                        .Include(e => e.DirectorTecnico)
                        .Include(e => e.Jugadores) // Carga explicita
                        .Include(e => e.PartidosLocal)       // Carga explicita                   
                        .Include(e => e.PartidosVisitante) // Carga explicita
                        .ToList();
            return equipos;
        }
        public IEnumerable<Equipo> SearchEquipos(string nombre)
        {
            return _dataContext.Equipos
            .Include(e => e.Municipio) // Carga explicita de la propiedad Municipio
            .Include(e => e.DirectorTecnico) // Carga explicita de la propiedad DirectorTecnico
            .Include(e => e.Jugadores) // Carga explicita
            .Include(e => e.PartidosLocal)       // Carga explicita                   
            .Include(e => e.PartidosVisitante) // Carga explicita
            .Where(e => e.Nombre.Contains(nombre))
            .ToList();
        }

       /* public IEnumerable<Equipo> GetEquiposPartido(int idEquipo)
        {
            var PartidoEncontrado = _dataContext.Partido.Find(idEquipo);
            var equipos = _dataContext.Equipos
            .Where(e => e.Partido == PartidoEncontrado)
            .Include(e => e.Partido)
            .Include(e => e.Equipo)
            .ToList();
            return equipos;
        }
        

            public bool PartidoEncontrado(Equipo id)
            {
            if(!_dataContext.Partidos.Any(p => p.Local == id || p.Visitante == id))
                {

                    return false;
                }

                return true;
             }

        */

        /*Metodo para limpiar cache(ChatGPT) Pendiente usar y validar
        Este método debería recibir como parámetro el objeto DataContext
        que se está utilizando para acceder a la base de datos y realizar las operaciones necesarias para eliminar la caché.
        */
        /*public void ClearCache(DataContext context)
        {
            var cache = context.ChangeTracker.Entries()
                                .Where(e => e.State != EntityState.Unchanged)
                                .ToList();

            foreach (var entry in cache)
            {
                entry.State = EntityState.Detached;
            }
        }*/

        public bool validateDuplicates(Equipo equipo, int idMunicipio, int idDT)
        {
            try
            {
                IEnumerable<Equipo> allEquipos =  GetAllEquipos();
                bool duplicado = false;                

                foreach(Equipo e in allEquipos)
                {
                    if(e.Nombre.ToLower()  == equipo.Nombre.ToLower().Trim() && e.Municipio == equipo.Municipio && e.DirectorTecnico == equipo.DirectorTecnico)   
                    {
                        duplicado = true;
                    }              
                }               
                Console.WriteLine("Equipo duplicado al Crear/Editar " + equipo.Nombre  +" - "+ duplicado);
                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion " + e.Message);
                return false;
            }
        }
    }
}