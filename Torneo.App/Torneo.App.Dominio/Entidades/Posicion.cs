using System.ComponentModel.DataAnnotations;

namespace Torneo.App.Dominio
{
    public class Posicion
    {
        public int Id { get; set; }

        [RegularExpression(@"^(?![0-9]+$)[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Valor Incorrecto.")]
        [Display(Name = "Nombre de la posición")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El nombre de la posicion es obligatoria.")]             
        [MaxLength(30, ErrorMessage = "La posición no puede contener más de 30 caracteres")]
        [MinLength(3, ErrorMessage = "La posición no puede contener menos de 3 caracteres")] 
        public string Nombre { get; set; } = new string("");
        public List <Jugador>? Jugadores { get; set; } = new List<Jugador>();
    }
}