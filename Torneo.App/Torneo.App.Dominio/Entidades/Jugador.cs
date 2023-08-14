namespace Torneo.App.Dominio
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = new string("");
        public int Numero { get; set; }
        public Equipo Equipo { get; set; } = new Equipo();
        public Posicion Posicion { get; set; } = new Posicion();
    }
}