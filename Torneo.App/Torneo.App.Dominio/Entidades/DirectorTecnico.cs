using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Torneo.App.Dominio
{
    public class DirectorTecnico
    {
        public int Id { get; set; }

        [RegularExpression(@"^(?!^\s)(?!.*\s$)[a-zA-ZÀ-ÿ\s\.]+$", ErrorMessage = "Valor Incorrecto. Ingrese solo letras")]
        [Display(Name = "Nombre del D.T.")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El nombre del D.T es obligatorio.")]       
        [DisplayFormat(ConvertEmptyStringToNull=false)]
        [MaxLength(30, ErrorMessage = "El nombre no puede contener más de 30 caracteres")]
        [MinLength(3, ErrorMessage = "El nombre no puede contener menos de 3 caracteres")]
        public string Nombre { get; set; }

        [RegularExpression(@"^(?!0+$|(\d)\1+$|(\d)(?:\d\1)+$)\d{7,11}$", ErrorMessage = "Valor Incorrecto. Ingrese un documento valido(Solo números sin puntos ni espacios. Min.7 Max. 11.No se permite el mismo número repetido unicamente.)")]
        [Display(Name = "Documento del D.T.")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El documento del D.T es obligatorio.")]       
        [DisplayFormat(ConvertEmptyStringToNull=false)]
        [MaxLength(11, ErrorMessage = "El nombre no puede contener más de 11 caracteres")]
        [MinLength(7, ErrorMessage = "El nombre no puede contener menos de 7 caracteres")]       
        public string Documento { get; set; }

        [RegularExpression(@"^(?!^(\d)\1+$)[\d()+\s\-]{7,12}$", ErrorMessage = "Valor Incorrecto. Ingrese solo numeros. No se permite repetir el mismo numero")]
        [Display(Name = "Telefono del D.T.")]
        [Required(ErrorMessage = "El nombre del D.T es obligatorio.")]        
        [MaxLength(12, ErrorMessage = "El teléfono no puede contener más de 12 caracteres")]
        [MinLength(7, ErrorMessage = "El teléfono no puede contener menos de 7 caracteres")]    
        [Phone]           
        public string Telefono { get; set; }
        public List<Equipo> Equipos {get; set;} = new List<Equipo>();

    }
}