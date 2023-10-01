using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Torneo.App.Dominio


{
    public class Partido
    {
        public int Id { get; set; }

        //[RegularExpression(@"^[\da-zA-Z]+$", ErrorMessage = "Valor Incorrecto. Ingrese solo letras")]
        [Display(Name = "Fecha y hora del partido")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "La fecha del partido es obligatoria.")]       
        [DisplayFormat(ConvertEmptyStringToNull=false)]
        public DateTime FechaHora { get; set; }
        
        public Equipo? Local { get; set; } 

        //[RegularExpression(@"^[\da-zA-Z]+$", ErrorMessage = "Valor Incorrecto. Ingrese solo letras")]
        [Display(Name = "Marcador Local")]
        [Required(ErrorMessage = "El Marcador Local es obligatorio.")]       
        //[DisplayFormat(ConvertEmptyStringToNull=false)]
        [Range(0, 100, ErrorMessage = "Marcador Local debe estar en un rango entre 0 y 100")]
        public int MarcadorLocal { get; set; }
        public Equipo? Visitante { get; set; } 

        //[RegularExpression(@"^[\da-zA-Z]+$", ErrorMessage = "Valor Incorrecto. Ingrese solo letras")]
        [Display(Name = "Marcador Visitante")]
        [Required(ErrorMessage = "El Marcador Visitante es obligatoria.")]       
        //[DisplayFormat(ConvertEmptyStringToNull=false)]
        [Range(0, 100, ErrorMessage = "Marcador Visitante debe estar en un rango entre 0 y 100")]
        public int MarcadorVisitante { get; set; }
    }
}