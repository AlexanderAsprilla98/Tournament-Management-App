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
            equipo.Municipio = municipioEncontrado;
            equipo.DirectorTecnico = DTEncontrado;
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
            return equipo;
        }

        public Equipo UpdateEquipo(Equipo equipo, int idMunicipio, int idDT)
        {
            var equipoEncontrado = GetEquipo(equipo.Id);
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            var DTEncontrado = _dataContext.DirectoresTecnicos.Find(idDT);
            equipoEncontrado.Nombre = equipo.Nombre;
            equipoEncontrado.Municipio = municipioEncontrado;
            equipoEncontrado.DirectorTecnico = DTEncontrado;
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
                
                
            }
           /* _dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();
            */
            return equipoEncontrado;
        }

        public IEnumerable<Equipo> GetEquiposMunicipio(int idMunicipio)
        {
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            var equipos = _dataContext.Equipos
            .Where(e => e.Municipio == municipioEncontrado)
            .Include(e => e.Municipio)
            .Include(e => e.DirectorTecnico)
            .ToList();
            return equipos;
        }
        public IEnumerable<Equipo> SearchEquipos(string nombre)
        {
            return _dataContext.Equipos
            .Where(e => e.Nombre.Contains(nombre));
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




    }
}