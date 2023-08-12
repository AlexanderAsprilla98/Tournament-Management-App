using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torneo.App.Dominio
{
    public class Municipio
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del municipio.")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        [MinLength(3, ErrorMessage = "El campo Nombre no puede tener menos de 3 caracteres.")]
        public string Nombre { get; set; } = null!;

        //Relacion de navegacion 0 a muchos(un municipio puede tener 0 o varios equipos, pero un equipo pertenece a un único municipio)
        public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}