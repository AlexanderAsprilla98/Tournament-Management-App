using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;

namespace Torneo.App.Frontend.Pages.Dts
{
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
            _repoDT.UpdateDT(DT);
            return RedirectToPage("Index");
        }
    }
}
