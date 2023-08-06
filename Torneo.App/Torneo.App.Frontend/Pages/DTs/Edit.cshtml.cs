using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.Dts
{
     [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRepositorioDT _repoDT;
        public DirectorTecnico DT { get; set; }
         public bool duplicate { get; set; }   
        public EditModel(IRepositorioDT repoDT)
        {
            _repoDT = repoDT;
        }

        //Invocar GetDT dentro del metodo OnGet tipo IActionResult
        public IActionResult OnGet(int id)
        {
            DT = _repoDT.GetDT(id);
            if (DT == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPost(DirectorTecnico DT)
        {
            //Validar duplicados por nombre
            duplicate =  _repoDT.validateDuplicates(DT);                                     
            
            //Validacion si se edita pero el nombre queda igual            
            if(!duplicate)
            {                    
                _repoDT.UpdateDT(DT);
                return RedirectToPage("Index");
            }
            else
            {
                Console.WriteLine("Document DT ingresado ya existe - " +DT.Documento);
                return Page();
                
            }         
        }
    }
}
