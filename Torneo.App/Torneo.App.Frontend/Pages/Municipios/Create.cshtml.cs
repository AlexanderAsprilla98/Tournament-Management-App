using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Municipios
{
   [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioMunicipio _repoMunicipio;
        public Municipio municipio { get; set; } = new Municipio();
        public bool duplicate { get; set; }
        public CreateModel(IRepositorioMunicipio repoMunicipio)
        {
            _repoMunicipio = repoMunicipio;
        }
        public void OnGet()
        {
            municipio = new Municipio();
        }
        public IActionResult OnPost(Municipio municipio)
        {
            try
            {
                municipio.Nombre =  municipio.Nombre.Trim();
                Console.WriteLine(municipio.Nombre);
                //Validacion si el modelo es valido cumpliendo con la anotaciones en la entidad
                if(ModelState.IsValid)
                { 
                    duplicate =  _repoMunicipio.validateDuplicates(municipio);

                    if(!duplicate)
                    {
                        
                        _repoMunicipio.AddMunicipio(municipio);
                        return RedirectToPage("Index"); 
                    }
                    else
                    {
                        return Page();
                    }                                      

                }          
          
                else
                {
                    return Page();
                }

            }
            catch
            {
                return  Page();
            }
        }
    }
}
