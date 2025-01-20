using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Torneo.App.Dominio;
using Torneo.App.Persistencia;
using Microsoft.AspNetCore.Authorization;


namespace Torneo.App.Frontend.Pages.Equipos
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioMunicipio _repoMunicipio;
        private readonly IRepositorioDT _repoDT;
        public Equipo equipo { get; set; }
        public SelectList MunicipioOptions { get; set; }
        public SelectList DTOptions { get; set; }
        public int MunicipioSelected { get; set; }
        public int DTSelected { get; set; }
        public bool duplicate { get; set; }
         public Equipo equipoOriginal { get; set; }
        public String nameEquipoOriginal { get; set; }
        public EditModel(IRepositorioEquipo repoEquipo, IRepositorioMunicipio repoMunicipio, IRepositorioDT repoDT)
        {
            _repoEquipo = repoEquipo;
            _repoMunicipio = repoMunicipio;
            _repoDT = repoDT;
        }

        public IActionResult OnGet(int id)
        {
            equipo = _repoEquipo.GetEquipo(id);
            MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
            MunicipioSelected = equipo.Municipio.Id;
            DTOptions = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");
            DTSelected = equipo.DirectorTecnico.Id;
            duplicate = false;
            nameEquipoOriginal = equipo.Nombre;
            if (equipo == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
           
        }

        public IActionResult OnPost(Equipo equipo, int idMunicipio, int idDT, int id)
        {
            try{               
                //Conocer y setear id entidades relacionadas en atributos de la entidad                
                equipo.Municipio = _repoMunicipio.GetMunicipio(idMunicipio);
                equipo.DirectorTecnico = _repoDT.GetDT(idDT);
 
                if(ModelState.IsValid)
                { 
                    // Obtener y asignar el objeto DirectorTecnico y Municipio  por su Id
                    Municipio municipioElegido = _repoMunicipio.GetMunicipio(idMunicipio);
                    DirectorTecnico dtElegido = _repoDT.GetDT(idDT);                   

                    if (municipioElegido == null || dtElegido == null)
                    {
                        // Mostrar un mensaje de error o redirigir a otra página
                        return Page();
                    }
                    //Imprimir datos de equipo
                    Console.WriteLine("Equipo valido "+ equipo.Nombre + " Municipcio equipo "+ equipo.Municipio.Nombre + " Dt equipo " + equipo.DirectorTecnico.Nombre);                                     
                    
                    equipoOriginal = _repoEquipo.GetEquipo(id);

                    Console.WriteLine("Nombre Original "+equipoOriginal.Nombre);
                    Console.WriteLine("Equipo editado "+ equipo.Nombre);
                  
                    if(equipoOriginal.Nombre == equipo.Nombre){
                        Console.WriteLine("Nombre de Equipo no presenta cambios");
                        _repoEquipo.UpdateEquipo(equipo, idMunicipio, idDT);
                        return RedirectToPage("Index");                       
                    }else{
                        Console.WriteLine("Nombre Equipo presenta cambios");
                        duplicate = _repoEquipo.validateDuplicates(equipo);
                        if (!duplicate)
                        {
                            _repoEquipo.UpdateEquipo(equipo, idMunicipio, idDT);
                            return RedirectToPage("Index");
                        }
                        else
                        {
                            //Cargar datos de la entidad(Municipios y Dts) en caso que de duplicados
                            equipo = _repoEquipo.GetEquipo(id);
                            MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                            MunicipioSelected = equipo.Municipio.Id;
                            DTOptions = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");
                            DTSelected = equipo.DirectorTecnico.Id;
                            return Page();
                        }
                    }
                }else
                {   
                    
                    Console.WriteLine("Equipo no valido "+ equipo.Nombre + " - Municipio " + equipo.Municipio.Nombre +" - DT " + equipo.DirectorTecnico.Nombre); 
                    //Cargar datos de la entidad en caso que no sea valido
                    equipo = _repoEquipo.GetEquipo(id);
                    MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                    MunicipioSelected = equipo.Municipio.Id;
                    DTOptions = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");
                    DTSelected = equipo.DirectorTecnico.Id;                      

                    return Page();
                } 
            
            }catch(Exception e)
            {  
              
                Console.WriteLine("Edit equipos Catch error " + e.Message); 
                //Cargar datos de la entidad en caso que de error      
                equipo = _repoEquipo.GetEquipo(id);
                MunicipioOptions = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                MunicipioSelected = equipo.Municipio.Id;
                DTOptions = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");
                DTSelected = equipo.DirectorTecnico.Id;               

                return Page();
            }
        }
    }
}
