using System.ComponentModel.DataAnnotations;
namespace Torneo.App.Dominio
{
    public class Municipio
    {
        public int Id { get; set; }

        //[Display(Name = "Nombre del municipio")]
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //[MaxLength(100, ErrorMessage = "El campo no puede tener más de 50 caracteres")]
        //[MinLength(3, ErrorMessage = "El campo no puede tener menos de 3 caracteres")]
        public string Nombre { get; set; } = new string("");

        public List<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}