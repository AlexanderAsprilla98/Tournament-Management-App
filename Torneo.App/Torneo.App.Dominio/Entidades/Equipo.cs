using System.ComponentModel.DataAnnotations.Schema;

namespace Torneo.App.Dominio
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = new string("");
        public Municipio Municipio { get; set; } = new Municipio();
        public DirectorTecnico DirectorTecnico { get; set; } = new DirectorTecnico();
        public List<Jugador> Jugadores { get; set; } = new List<Jugador>();

        [InverseProperty("Local")]
        public List<Partido> PartidosLocal { get; set; } = new List<Partido>();
        [InverseProperty("Visitante")]
        public List<Partido> PartidosVisitante { get; set; } = new List<Partido>();
        
 
    }
}