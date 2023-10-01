using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torneo.App.Dominio
{
    public class Equipo
    {
        public int Id { get; set; }        
        
        [RegularExpression(@"^(?![0-9]+$)[a-zA-ZÀ-ÿ\d\s]+$", ErrorMessage = "Valor Incorrecto. Solo se permiten letras")]
        [Display(Name = "Nombre del Equipo")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El nombre del Equipo es obligatorio.")]             
        [MaxLength(50, ErrorMessage = "El nombre del equipo no puede contener más de 50 caracteres")]
        [MinLength(3, ErrorMessage = "El nombre del equipo no puede contener menos de 3 caracteres")] 
               
        public string Nombre { get; set; } = new string("");
        
        public Municipio? Municipio { get; set; }
        public DirectorTecnico? DirectorTecnico { get; set; }
        public List<Jugador>? Jugadores { get; set; } = new List<Jugador>();
        
        [InverseProperty("Local")]
        public List<Partido>? PartidosLocal { get; set; } = new List<Partido>();
        [InverseProperty("Visitante")]
        public List<Partido>? PartidosVisitante { get; set; } = new List<Partido>();
        
 
    }
}