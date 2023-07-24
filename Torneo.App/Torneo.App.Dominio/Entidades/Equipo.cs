using System.ComponentModel.DataAnnotations.Schema;

namespace Torneo.App.Dominio
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Municipio Municipio { get; set; }
        public DirectorTecnico DirectorTecnico { get; set; }       
        public List<Jugador> Jugadores { get; set; }          

        [InverseProperty("Local")]
        public List<Partido> PartidosLocal { get; set; }
        [InverseProperty("Visitante")]
        public List<Partido> PartidosVisitante { get; set; }
        
 
    }
}