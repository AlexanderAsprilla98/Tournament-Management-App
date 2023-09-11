using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Posiciones
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioPosicion _repoPosicion;

        public CreateModel(IRepositorioPosicion repoPosicion)
        {
            _repoPosicion = repoPosicion;
        }
        public bool duplicate { get; set; }
        public Posicion posicion { get; set; } = new Posicion();

        public void OnGet()
        {
            posicion = new Posicion();
            duplicate = false;
        }

        public IActionResult OnPost(Posicion posicion)
        {   
            
            try
            {        
                posicion.Nombre = posicion.Nombre.Trim();
                Console.WriteLine("EStoy dentro del try de crear posicion");
                Console.WriteLine("Posición escogida " + posicion);                       

                if(ModelState.IsValid)
                {   
                    Console.WriteLine("Modelo posicion valida");

                    duplicate = _repoPosicion.validateDuplicates(posicion.Nombre);
                    if(!duplicate)
                    {
                        _repoPosicion.AddPosicion(posicion);
                        return RedirectToPage("Index");
                    } else
                    {
                        return Page();
                    }

                }
                {
                    //Cargar nuevamente la posicion    
                    posicion = new Posicion();                   
                    Console.WriteLine("Modelo posicion no valida");  
                    return Page();
                }


            }catch(Exception ex)
            {
                //Cargar nuevamente la posicion           
                posicion = new Posicion();  
                Console.WriteLine("Catch error: " + ex.Message);  
                return  Page();
            }

        }


    }
}
