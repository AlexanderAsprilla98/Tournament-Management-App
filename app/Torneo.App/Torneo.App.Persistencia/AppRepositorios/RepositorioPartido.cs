using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioPartido : IRepositorioPartido
    {
        private  DataContext _dataContext = new DataContext();
        public Partido AddPartido(Partido partido, int idEquipoLocal, int idEquipoVisitante)
        {
            var equipoLocalEncontrado = _dataContext.Equipos.Find(idEquipoLocal);
            var equipoVisitanteEncontrado = _dataContext.Equipos.Find(idEquipoVisitante);
            partido.Local = equipoLocalEncontrado ?? null;
            partido.Visitante = equipoVisitanteEncontrado ?? null;
            var partidoInsertado = _dataContext.Partidos.Add(partido);
            _dataContext.SaveChanges();
            return partidoInsertado.Entity ?? throw new Exception("Partido not found");  // Throw an exception if partidoInsertado is null.
        }
        public IEnumerable<Partido> GetAllPartidos()
        {
            var partidos = _dataContext.Partidos
                            .Include(p => p.Local)
                            .Include(p => p.Visitante)
                            .AsNoTracking()
                            .ToList();
            /*_dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();*/
                            
            return partidos;
        }
        public Partido GetPartido(int idPartido)
        {
            var partido = _dataContext.Partidos
                            .Include(p => p.Local)
                            .Include(p => p.Visitante)
                            .AsNoTracking()
                            .FirstOrDefault(p => p.Id == idPartido);
            return partido ?? throw new Exception("Partido not found");  // Throw an exception if partido is null.
        }
        public Partido UpdatePartido(Partido partido, int idEquipoLocal, int idEquipoVisitante)
        {
            var partidoEncontrado = _dataContext.Partidos.Find(partido.Id);
            if (partidoEncontrado != null)
            {
                var equipoLocalEncontrado = _dataContext.Equipos.Find(idEquipoLocal);
                var equipoVisitanteEncontrado = _dataContext.Equipos.Find(idEquipoVisitante);
                partidoEncontrado.Local = equipoLocalEncontrado;
                partidoEncontrado.Visitante = equipoVisitanteEncontrado;
                partidoEncontrado.FechaHora = partido.FechaHora;
                partidoEncontrado.MarcadorLocal = partido.MarcadorLocal;
                partidoEncontrado.MarcadorVisitante = partido.MarcadorVisitante;
                _dataContext.SaveChanges();
            } else
            {
                Console.WriteLine("No se encontró el partido");
            }

            /*_dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();*/
            return partidoEncontrado ?? throw new Exception("Partido not found");  // Throw an exception if partidoEncontrado is null.
        }

        public Partido DeletePartidos(int idPartido)
        {
            var partidoEncontrado = GetPartido(idPartido);
            if (partidoEncontrado != null)
            {
                
                _dataContext.Partidos.Remove(partidoEncontrado);
                _dataContext.SaveChanges();
                
                /*_dataContext.ChangeTracker.Clear();
                _dataContext.Dispose();
                _dataContext = new DataContext();*/
                
                
            } else
            {
                Console.WriteLine("No se encontró el partido");
            }
            return partidoEncontrado ?? throw new Exception("Partido not found");  // Throw an exception if partidoEncontrado is null.
        }


         public bool validateDuplicates(Partido partido, int idEquipoLocal, int idEquipoVisitante)
        {
            try
            {
                IEnumerable<Partido> allPartidos =  GetAllPartidos();
                bool duplicado = false; 
                               
                
                foreach(Partido p in allPartidos)
                {                    
                    if((p.Local.Id == idEquipoLocal) && (p.Visitante.Id == idEquipoVisitante) && (p.FechaHora == partido.FechaHora))
                    {                   
                        duplicado = true;
                    }
                    else if((p.Local.Id == idEquipoVisitante) && (p.Visitante.Id == idEquipoLocal) && (p.FechaHora == partido.FechaHora))
                    {                   
                        duplicado = true;
                    }
                    
                           
                }               
                Console.WriteLine("Partido duplicado al Crear/Editar " + partido.Local  +" - "+ duplicado);
                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion " + e.Message);
                return false;
            }
        }




    }
}