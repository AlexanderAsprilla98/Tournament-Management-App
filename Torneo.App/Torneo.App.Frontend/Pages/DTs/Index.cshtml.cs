using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;

namespace Torneo.App.Frontend.Pages.DTs
{
    public class IndexModel : PageModel
    {
        private readonly IRepositorioDT _repoDT;
        public IEnumerable<DirectorTecnico> dts { get; set; }
        public bool ErrorEliminar { get; set; }

        public IndexModel(IRepositorioDT repoDT)
        {
            _repoDT = repoDT;
        }

        public void OnGet()
        {
            dts = _repoDT.GetAllDTs();
            ErrorEliminar = false;
        }

         public IActionResult OnPostDelete(int id)
        {
            try
            {
                _repoDT.DeleteDT(id);
                dts = _repoDT.GetAllDTs();
                return Page();
            }
            catch (Exception ex)
            {   
                System.Console.WriteLine(ex.Message);
                ErrorEliminar = true;
                dts = _repoDT.GetAllDTs();
                return Page();
            }             
        }

    }
}