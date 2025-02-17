using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioJugador _repoJugador;
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioPosicion _repoPosicion;
        public bool duplicate { get; set; }
        public bool posicionesExits { get; set; }
        public bool equipoExits { get; set; }
        public CreateModel(IRepositorioJugador repoJugador, IRepositorioEquipo repoEquipo, IRepositorioPosicion repoPosicion)
        {
            _repoJugador = repoJugador;
            _repoEquipo = repoEquipo;
            _repoPosicion = repoPosicion;
        }

        public Jugador jugador { get; set; } = new Jugador();
        public IEnumerable<Equipo> equipos { get; set; }
        public IEnumerable<Posicion> posiciones { get; set; }

        public void OnGet()
        {
            jugador = new Jugador();
            equipos = _repoEquipo.GetAllEquipos();
            posiciones = _repoPosicion.GetAllPosiciones();
            equipoExits = equipos.Any();
            posicionesExits = posiciones.Any(); 
            duplicate = false;

            jugador.Equipo = equipos.FirstOrDefault(); // Establecer el valor seleccionado por defecto
            jugador.Posicion = posiciones.FirstOrDefault(); // Establecer el valor seleccionado por defecto            

            //Console.WriteLine("Existen Municipios "+equipos.Any());
            //Console.WriteLine("Existen Dts "+posiciones.Any());                 

        }

        public IActionResult OnPost(Jugador jugador, int idEquipo, int idPosicion)
        {
            try
            {
                Console.WriteLine("dentro Try  Create de Jugador"); 
                //Quitar espacios en blanco al inicio y final
                jugador.Nombre = jugador.Nombre.Trim();
                //jugador.Numero =  jugador.Numero.Trim();               
               
                Console.WriteLine("Nombre jugador " +  jugador.Nombre);    
                Console.WriteLine("Nombre Numero " +  jugador.Numero); 
                Console.WriteLine("Id equipo escogido " + idEquipo);                 
                Console.WriteLine("Id Posicion escogido " + idPosicion); 

                //setear equipo y posicion de jugador 
                Equipo equipoIngresado =  _repoEquipo.GetEquipo(idEquipo);
                Posicion posicionIngresada =  _repoPosicion.GetPosicion(idPosicion); 
                jugador.Equipo = _repoEquipo.GetEquipo(idEquipo);                                             
                jugador.Posicion = _repoPosicion.GetPosicion(idPosicion);                   
                jugador.Equipo.Nombre  = equipoIngresado.Nombre;
                jugador.Posicion.Nombre = posicionIngresada.Nombre;

                Console.WriteLine("Jugador ingresado " + jugador.Nombre);
                Console.WriteLine("Id Equipo ingresado " + jugador.Equipo.Nombre);
                Console.WriteLine("Id Posicion ingresado " +   jugador.Posicion.Nombre);    

                //Condicion si el modelo de datos/atributos de clase es valido
                if(ModelState.IsValid)
                {                     
                    //validar si existe un duplicado con el mismo, nombre, numero y posicione en el mismo equipo
                    duplicate = _repoJugador.validateDuplicates(jugador, idEquipo, idPosicion);
                    if (!duplicate)
                    {
                        _repoJugador.AddJugador(jugador, idEquipo, idPosicion);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        //Cargar entidades refenciadas como la lista de equipos y posiciones si existen duplicados
                        jugador = new Jugador();
                        equipos = _repoEquipo.GetAllEquipos();
                        posiciones = _repoPosicion.GetAllPosiciones();
                        equipoExits = equipos.Any();
                        posicionesExits = posiciones.Any();     
                        return Page();
                    }

                }else
                {   
                    //Si no es validovolver a cargar los equipos y posiciones
                    jugador = new Jugador();
                    equipos = _repoEquipo.GetAllEquipos();
                    posiciones = _repoPosicion.GetAllPosiciones();                                   
                    Console.WriteLine("jugador  no valido "+ jugador.Nombre + " - Equipo " + jugador.Equipo.Nombre +" - Posicion" + jugador.Posicion.Nombre);                     
                    equipoExits = equipos.Any();
                    posicionesExits = posiciones.Any();
                    return Page();
                }


            }catch(Exception  e) //Catch error
            {  
                Console.WriteLine("Catch error " + e.Message);  
                jugador = new Jugador();
                equipos = _repoEquipo.GetAllEquipos();
                posiciones = _repoPosicion.GetAllPosiciones(); 
                equipoExits = equipos.Any();
                posicionesExits = posiciones.Any();                     
                return Page();
            }
            
        }
    }
}
