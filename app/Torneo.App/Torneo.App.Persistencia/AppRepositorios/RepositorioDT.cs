using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioDT : IRepositorioDT
    {
        private DataContext _dataContext = new DataContext();
        public DirectorTecnico AddDT(DirectorTecnico directorTecnico)
        {
            var dtInsertado = _dataContext.DirectoresTecnicos.Add(directorTecnico);
            _dataContext.SaveChanges();
            
            return dtInsertado.Entity;
        }

        public IEnumerable<DirectorTecnico> GetAllDTs()
        {
             var Dts = _dataContext.DirectoresTecnicos
                            .Include(e => e.Equipos)
                            .AsNoTracking()
                            .ToList();

            /*_dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();*/
            
            return Dts;
        }
        public DirectorTecnico GetDT(int IdDT)
        {
            var DTEncontrado = _dataContext.DirectoresTecnicos.Find(IdDT);
            return DTEncontrado;
        }       
        public DirectorTecnico UpdateDT(DirectorTecnico dt)
        {
            var dtEncontrado = _dataContext.DirectoresTecnicos.Find(dt.Id);
             //Si el director tecnico encontrado es diferente de nulo
            if (dtEncontrado != null)
            {
                dtEncontrado.Nombre = dt.Nombre;
                dtEncontrado.Documento = dt.Documento;
                dtEncontrado.Telefono = dt.Telefono;
                _dataContext.SaveChanges();
            } else
            {
                Console.WriteLine("No se encontró el DT");
            }
            return dtEncontrado ?? throw new Exception("DT not found");  // Throw an exception if dtEncontrado is null.
        }
        public DirectorTecnico DeleteDT(int idDT)
        {
            var dtEncontrado = GetDT(idDT);
            if (dtEncontrado != null)
            {
                _dataContext.DirectoresTecnicos.Remove(dtEncontrado);
                _dataContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("No se encontró el DT");
            }
            return dtEncontrado ?? throw new Exception("DT not found");  // Throw an exception if dtEncontrado is null.
        }

        public bool validateDuplicates(DirectorTecnico dtIngresado)
        {
            try
            {
                IEnumerable<DirectorTecnico> allDT =  GetAllDTs();
                bool duplicado = false;                

                foreach(DirectorTecnico directoresTecnicos in allDT)
                {     
                    //Validacion si el documento ingresado ya existe en BD              
                    if(directoresTecnicos.Documento  == dtIngresado.Documento.Trim())   
                    {
                        //Validacion para no marcarlo como duplicado si se edita otro campo del DT
                        if(directoresTecnicos.Id == dtIngresado.Id)
                        {
                            duplicado = false;
                        }
                        else
                        {
                            duplicado = true;
                        }                      
                        
                    }              
                }               
                //Console.WriteLine("Documento del DT duplicado al Crear/Editar Nombre:" +dtIngresado.Nombre+" Documento: "+dtIngresado.Documento+"- "+ duplicado);

                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion duplicado " + e.Message);
                return false;
            }            
        }



        /*Metodo para limpiar cache
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
        }

        public bool validateDuplicates(string nombreDirectorTecnico, string documentoDirectorTecnico)
        {
            try
            {
                IEnumerable<DirectorTecnico> allDTs =  GetAllDTs();
                bool duplicado = false;                

                foreach(DirectorTecnico dt in allDTs)
                {
                    if(dt.Nombre.ToLower()  == nombreDirectorTecnico.ToLower().Trim() && dt.Documento == documentoDirectorTecnico)   
                    {
                        duplicado = true;
                    }              
                }               
                Console.WriteLine("DT duplicado al Crear/Editar " + nombreDirectorTecnico  +" - "+ duplicado);
                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion " + e.Message);
                return false;
            }
        }*/


    }
}