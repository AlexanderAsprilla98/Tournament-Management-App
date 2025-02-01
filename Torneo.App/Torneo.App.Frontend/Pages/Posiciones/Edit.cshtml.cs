using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Posiciones
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRepositorioPosicion _repoPosicion;

        public EditModel(IRepositorioPosicion repoPosicion)
        {
            _repoPosicion = repoPosicion;
        }

        public Posicion posicion { get; set; }
        public bool duplicate { get; set; }


        public IActionResult OnGet(int id)
        {
            posicion = _repoPosicion.GetPosicion(id);
            duplicate = false;
            if (posicion == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPost(Posicion posicion,int id)
        {
            try
            {
                posicion.Nombre = posicion.Nombre.Trim();
                Console.WriteLine("EStoy dentro del try de edit posicion");
                Console.WriteLine("Posición escogida " + posicion);  

                if(ModelState.IsValid)
                {  
                    duplicate = _repoPosicion.validateDuplicates(posicion);
                    
                    if(!duplicate)
                    {
                        _repoPosicion.UpdatePosicion(posicion);
                        return RedirectToPage("Index");
                    }else
                    {                                              
                        return Page();
                    }
                }
                else        
                {
                    //Cargar nuevamente la posicion    
                    posicion = _repoPosicion.GetPosicion(id);
                    duplicate = false;                 
                    Console.WriteLine("Modelo posicion no valida");  
                    return Page();
                }

            }catch(Exception ex)
            {
                //Cargar nuevamente la posicion           
                posicion = _repoPosicion.GetPosicion(id);
                duplicate = false;  
                Console.WriteLine("Catch error: " + ex.Message);  
                return  Page();
            }
        }
    }
}
