using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torneo.App.Dominio
{
    public class Municipio
    {
        public int Id { get; set; }

        //[RegularExpression("^[a-zA-ZÀ-ÿ\.]+$", ErrorMessage = "Solo se permiten números")]
        [RegularExpression(@"^(?!^\s)(?!.*\s$)[a-zA-ZÀ-ÿ\s\.]+$", ErrorMessage = "Solo se permiten letras")]
        [Display(Name = "Nombre del municipio")]
        [Required(AllowEmptyStrings=false, ErrorMessage = "El campo Nombre del municipio es obligatorio.")]       
        [DisplayFormat(ConvertEmptyStringToNull=false)]
        [MaxLength(80, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        [MinLength(3, ErrorMessage = "El campo Nombre no puede tener menos de 3 caracteres.")]
        public string Nombre { get; set; } = null!;

        //Relacion de navegacion 0 a muchos(un municipio puede tener 0 o varios equipos, pero un equipo pertenece a un único municipio)
        public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}