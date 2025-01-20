using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public class RepositorioMunicipio : IRepositorioMunicipio
    {
        private  DataContext _dataContext = new DataContext();
        public Municipio AddMunicipio(Municipio municipio)
        {
            var municipioInsertado = _dataContext.Municipios.Add(municipio);
            _dataContext.SaveChanges();
            return municipioInsertado.Entity;
        }
       public IEnumerable<Municipio> GetAllMunicipios()
        {
            var municipios = _dataContext.Municipios
            .Include(m => m.Equipos)
            .AsNoTracking()
            .ToList();

            /*_dataContext.ChangeTracker.Clear();
            _dataContext.Dispose();
            _dataContext = new DataContext();*/

            return municipios;
        }
        public Municipio GetMunicipio(int idMunicipio)
        {
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            return municipioEncontrado == null ? null : municipioEncontrado;
        }
        public Municipio UpdateMunicipio(Municipio municipio)
        {
            var municipioEncontrado = _dataContext.Municipios.Find(municipio.Id);
            if (municipioEncontrado != null)
            {
                municipioEncontrado.Nombre = municipio.Nombre;
                _dataContext.SaveChanges();
            } else
            {
                Console.WriteLine("No se encontró el municipio");
            }
            return municipioEncontrado ?? throw new Exception("Municipio not found");  // Throw an exception if municipioEncontrado is null.
        }
        public Municipio DeleteMunicipio(int idMunicipio)
        {
            var municipioEncontrado = _dataContext.Municipios.Find(idMunicipio);
            if (municipioEncontrado != null)
            {
                _dataContext.Municipios.Remove(municipioEncontrado);
                _dataContext.SaveChanges();
            } else
            {
                Console.WriteLine("No se encontró el municipio");
            }
            return municipioEncontrado ?? throw new Exception("Municipio not found");  // Throw an exception if municipioEncontrado is null.
        }

        public bool validateDuplicates(Municipio municipioIngresado)
        {
            try
            {
                IEnumerable<Municipio> allMunucipios =  GetAllMunicipios();
                bool duplicado = false;                

                foreach(Municipio municipio in allMunucipios)
                {
                    if(municipio.Id != municipioIngresado.Id){
                        if(municipio.Nombre.ToLower()  == municipioIngresado.Nombre.ToLower().Trim())   
                        {
                            duplicado = true;
                            break;
                        }        
                    }      
                }               
                Console.WriteLine("Municipio duplicado al Crear/Editar " + municipioIngresado.Nombre  +" - "+ duplicado);
                return duplicado;

            }catch(Exception e){
                Console.WriteLine("Error Validacion " + e.Message);
                return false;
            }


            
        }
    
    
    }


    
}