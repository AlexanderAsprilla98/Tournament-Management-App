using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace Torneo.App.Frontend.Pages.DTs
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioDT _repoDT;
        public DirectorTecnico dt { get; set; } = new DirectorTecnico();
        public bool duplicate { get; set; }

        public CreateModel(IRepositorioDT repoDT)
        {
            _repoDT = repoDT;
        }
        public void OnGet()
        {
            dt = new DirectorTecnico();
        }
        public IActionResult OnPost(DirectorTecnico dt)
        {
            duplicate =  _repoDT.validateDuplicates(dt);

            if(!duplicate)
            {
            _repoDT.AddDT(dt);
            return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
