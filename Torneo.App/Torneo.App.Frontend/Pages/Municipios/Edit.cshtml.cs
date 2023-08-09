using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Municipios
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRepositorioMunicipio _repoMunicipio;
        public Municipio municipio { get; set; }
        public bool duplicate { get; set; }         
        public EditModel(IRepositorioMunicipio repoMunicipio)
        {
            _repoMunicipio = repoMunicipio;
        }
        public IActionResult OnGet(int id)
        {
            municipio = _repoMunicipio.GetMunicipio(id);                                        
            if (municipio == null)
            {
                return NotFound();
            }
            else
            {                
                return Page();
            }
        }
        public IActionResult OnPost(Municipio municipio)
        {
            //Validar duplicados por nombre
            duplicate =  _repoMunicipio.validateDuplicates(municipio.Nombre);                      
            //Console.WriteLine("\nMunicipio ingresado en input  - "+ municipio.Nombre);            
            
                //Validacion si se edita pero el nombre queda igual            
                if(!duplicate)
                {                    
                    _repoMunicipio.UpdateMunicipio(municipio);
                    return RedirectToPage("Index");
                }
                else
                {
                    Console.WriteLine("nombre ingresado ya existe");
                    return Page();
                    
                }              
               
                    
        }       

    }
}
