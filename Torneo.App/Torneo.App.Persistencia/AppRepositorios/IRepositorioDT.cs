using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public interface IRepositorioDT
    {
        public DirectorTecnico AddDT(DirectorTecnico directorTecnico);
        public IEnumerable<DirectorTecnico> GetAllDTs();
        public DirectorTecnico GetDT(int Id);
        public DirectorTecnico UpdateDT(DirectorTecnico directorTecnico);
    }
}