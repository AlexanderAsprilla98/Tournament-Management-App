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
            try
            {
                if(ModelState.IsValid)
                { 
                    Console.WriteLine("DTs es valido");
                    _repoDT.UpdateDT(DT);
                    return RedirectToPage("Index");

                }
                else
                {
                    return Page();
                }

            }catch
            {
                return NotFound();
            }        
        }
    }
}
