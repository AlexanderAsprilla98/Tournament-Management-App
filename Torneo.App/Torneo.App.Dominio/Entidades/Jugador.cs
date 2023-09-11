using System.ComponentModel.DataAnnotations;

namespace Torneo.App.Dominio
{
    public class Jugador
    {
        public int Id { get; set; }

        [RegularExpression(@"^(?![0-9]+$)[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Valor Incorrecto. Solo se permiten letras")]
        [Display(Name = "Nombre del Jugador")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El nombre del Jugador es obligatorio.")]             
        [MaxLength(50, ErrorMessage = "El nombre del jugador no puede contener más de 50 caracteres")]
        [MinLength(3, ErrorMessage = "El nombre del jugador no puede contener menos de 3 caracteres")] 
        public string Nombre { get; set; }

        [Display(Name = "Número del Jugador")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El Número del Jugador es obligatorio.")]       
        [DisplayFormat(ConvertEmptyStringToNull=false)]
        [Range(1, 99, ErrorMessage = "El Número del Jugador debe estar en un rango entre 1 y 99")]
        public int Numero { get; set; }
        public Equipo? Equipo { get; set; } 
        public Posicion? Posicion { get; set; } 
    }
}