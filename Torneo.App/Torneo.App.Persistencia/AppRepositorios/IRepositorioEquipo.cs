using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public interface IRepositorioEquipo
    {
        public Equipo AddEquipo(Equipo equipo, int idMunicipio, int idDT);
        public IEnumerable<Equipo> GetAllEquipos();
        public Equipo GetEquipo(int idEquipo);
        public Equipo UpdateEquipo(Equipo equipo, int idMunicipio, int idDT);
        public Equipo DeleteEquipo(int equipo);
        public IEnumerable<Equipo> GetEquiposMunicipio(int idMunicipio);
        public IEnumerable<Equipo> SearchEquipos(string nombre);
        public bool validateDuplicates(Equipo equipo, int idMunicipio, int idDT);
    }
}