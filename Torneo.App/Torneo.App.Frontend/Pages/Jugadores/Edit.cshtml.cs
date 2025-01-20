using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Jugadores
{
    [Authorize]
    public class EditModel : PageModel
    {
        //Atributos
        private readonly IRepositorioJugador _repoJugador;
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioPosicion _repoPosicion;

        //Propiedades tipo jugador y tipo SelectList para poblar los selects de equipos y posiciones
        public Jugador jugador { get; set; }
        public SelectList EquipoOptions { get; set; }
        public SelectList PosicionOptions { get; set; }

        //Equipo y posicion que tiene asignado el jugador para que aparescan como seleccionados en los selects
        public int EquipoSelected { get; set; }
        public int PosicionSelected { get; set; }
        public bool duplicate { get; set; }
        

        //Instanciar en el constructor _repoEquipo, _repoMunicipio, _repoDT
        public EditModel(IRepositorioJugador repoJugador, IRepositorioEquipo repoEquipo, IRepositorioPosicion repoPosicion)
        {
            _repoJugador = repoJugador;
            _repoEquipo = repoEquipo;
            _repoPosicion = repoPosicion;
        }

        //Se inicializan las propiedades e instacian los SelectList.
        public IActionResult OnGet(int id)
        {
            jugador = _repoJugador.GetJugador(id);
            EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
            EquipoSelected = jugador.Equipo.Id;
            PosicionOptions = new SelectList(_repoPosicion.GetAllPosiciones(), "Id", "Nombre");
            PosicionSelected = jugador.Posicion.Id;
            
            //jugador.Equipo = EquipoOptions.FirstOrDefault(); // Establecer el valor seleccionado por defecto
            //jugador.Posicion = PosicionOptions.FirstOrDefault(); // Establecer el valor seleccionado por defecto

            //Validacion si el equipo es null retorne un No encontrado, de lo contrario carga el Page
            if (jugador == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost(Jugador jugador, int idEquipo, int idPosicion,int id)
        {
            try{
                Console.WriteLine("dentro del Try Edit   de Jugador"); 
                //Quitar espacios en blanco al inicio y final
                //jugador.Nombre = jugador.Nombre.Trim();           
                
               
                jugador.Equipo = _repoEquipo.GetEquipo(idEquipo);                                             
                jugador.Posicion = _repoPosicion.GetPosicion(idPosicion);                   
                //setear equipo y posicion de jugador 
                Equipo equipoIngresado =  _repoEquipo.GetEquipo(idEquipo);
                Posicion posicionIngresada =  _repoPosicion.GetPosicion(idPosicion); 
                jugador.Equipo.Nombre  = equipoIngresado.Nombre;
                jugador.Posicion.Nombre = posicionIngresada.Nombre;
                

                Console.WriteLine("Jugador ingresado " + jugador.Nombre);
                Console.WriteLine("Id Equipo ingresado " + jugador.Equipo.Nombre);
                Console.WriteLine("Id Posicion ingresado " +   jugador.Posicion.Nombre);    

            if(ModelState.IsValid)
            {
                Console.WriteLine("Jugador valido"); 
                //validar si existe un duplicado con el mismo, nombre, numero y posicione en el mismo equipo
                duplicate = _repoJugador.validateDuplicates(jugador, idEquipo, idPosicion);
                if (!duplicate)
                {
                    _repoJugador.UpdateJugador(jugador, idEquipo, idPosicion);
                    return RedirectToPage("Index");
                }
                else
                {                    
                    //Cargar entidades refenciadas como la lista de equipos, posiciones y las opciones guardadas del jugador si existen duplicados
                    jugador = _repoJugador.GetJugador(id);
                    EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
                    EquipoSelected = jugador.Equipo.Id;
                    PosicionOptions = new SelectList(_repoPosicion.GetAllPosiciones(), "Id", "Nombre");
                    PosicionSelected = jugador.Posicion.Id;
                    return Page();
                }
            }else
                {                       
                    Console.WriteLine("Jugador no valido"); 
                    //Cargar datos de la entidad en caso que no sea valido
                    jugador = _repoJugador.GetJugador(id);
                    EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
                    EquipoSelected = jugador.Equipo.Id;
                    PosicionOptions = new SelectList(_repoPosicion.GetAllPosiciones(), "Id", "Nombre");
                    PosicionSelected = jugador.Posicion.Id;                                 

                    return Page();
                } 
            }catch(Exception e){
                Console.WriteLine("jugador  no valido "+ jugador.Nombre + " - Equipo " + jugador.Equipo.Nombre +" - Posicion" + jugador.Posicion.Nombre);                     
                jugador = _repoJugador.GetJugador(id);
                EquipoOptions = new SelectList(_repoEquipo.GetAllEquipos(), "Id", "Nombre");
                EquipoSelected = jugador.Equipo.Id;
                PosicionOptions = new SelectList(_repoPosicion.GetAllPosiciones(), "Id", "Nombre");
                PosicionSelected = jugador.Posicion.Id;                                                
                return Page();

            }
        }
    }
}
