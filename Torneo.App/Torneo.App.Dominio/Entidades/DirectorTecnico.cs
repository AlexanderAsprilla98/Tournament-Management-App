namespace Torneo.App.Dominio
{
    public class DirectorTecnico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = new string("");
        public string Documento { get; set; } = new string("");
        public string Telefono { get; set; } = new string("");
        public List<Equipo> Equipos {get; set;} = new List<Equipo>();

    }
}