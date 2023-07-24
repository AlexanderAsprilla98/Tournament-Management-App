namespace Torneo.App.Dominio
{
    public class Posicion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List <Jugador> Jugadores { get; set; }
    }
}