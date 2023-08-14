using Torneo.App.Dominio;
namespace Torneo.App.Persistencia
{
    public interface IRepositorioPosicion
    {
        public Posicion AddPosicion(Posicion posicion);
        public IEnumerable<Posicion> GetAllPosiciones();
        public Posicion GetPosicion(int idPosicion);
        public Posicion UpdatePosicion(Posicion posicion);
        public Posicion DeletePosicion(int idPosicion);
        public bool validateDuplicates(string nombrePosicion);
    }
}