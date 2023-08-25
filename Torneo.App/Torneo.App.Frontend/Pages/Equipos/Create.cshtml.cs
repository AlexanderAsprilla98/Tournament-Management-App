using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Torneo.App.Frontend.Pages.Equipos
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IRepositorioEquipo _repoEquipo;
        private readonly IRepositorioMunicipio _repoMunicipio;
        private readonly IRepositorioDT _repoDT;
        public bool duplicate { get; set; }
        public Equipo equipo { get; set; }
        public IEnumerable<Municipio> municipios { get; set; }
        public IEnumerable<DirectorTecnico> dts { get; set; }

        public CreateModel(IRepositorioEquipo repoEquipo, IRepositorioMunicipio repoMunicipio, IRepositorioDT repoDT)
        {
            _repoEquipo = repoEquipo;
            _repoMunicipio = repoMunicipio;
            _repoDT = repoDT;
        }


        public void OnGet(int idMunicipio, int idDT)
        {
            equipo = new Equipo();
            municipios = _repoMunicipio.GetAllMunicipios();
            dts = _repoDT.GetAllDTs();  

            // Inicializar Model.duplicate en false
            duplicate = false; 

            /*
            dts = _repoDT.GetAllDTs();

            equipo.Municipio = municipios.FirstOrDefault(); // Establecer el valor seleccionado por defecto
            equipo.DirectorTecnico = dts.FirstOrDefault(); // Establecer el valor seleccionado por defecto

            if (municipios != null && dts != null)
             {
                Console.WriteLine("Propiedades municipios y dts no son nulas " + equipo.Municipio + " " + equipo.DirectorTecnico);
                ViewData["Municipios"] = municipios.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                ViewData["DTs"] = dts.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Nombre });
                /*foreach (var dt in ViewData["Municipios"] as IEnumerable<SelectListItem>){
                Console.WriteLine(dt.Text);
                }
            }
             else
            {
                Console.WriteLine("Propiedades municipios y dts son nulas");
                // Manejar el caso de que municipios o dts sean nulos
            }     

            //equipo.Municipio.Id = idMunicipio;
            //equipo.DirectorTecnico.Id = idDT;
            */


        }

        public IActionResult OnPost(Equipo equipo, int idMunicipio, int idDT)
        {
            /*
            try
            {                
                equipo.Municipio = _repoMunicipio.GetMunicipio(idMunicipio);
                equipo.DirectorTecnico = _repoDT.GetDT(idDT);

                Console.WriteLine("Equipo ingresado " +equipo.Nombre);
                Console.WriteLine("id Municipio ingresado " + equipo.Municipio.Id);
                Console.WriteLine("Municipio ingresado " + equipo.Municipio.Nombre);
                Console.WriteLine("dt ingresado " + equipo.DirectorTecnico.Nombre);
                Console.WriteLine("documento ingresado " + equipo.DirectorTecnico.Documento);
                Console.WriteLine("telefono ingresado " + equipo.DirectorTecnico.Telefono);
                Console.WriteLine("equipo ingresado " + equipo.DirectorTecnico.Equipos);
                Console.WriteLine("id dt ingresado " + equipo.DirectorTecnico.Id);               

                // Crear SelectList para municipios y DTs
                //ViewData["Municipios"] = new SelectList(municipios, "Id", "Nombre");
                //ViewData["DTs"] = new SelectList(dts, "Id", "Nombre");
                          

                // Crear SelectList para municipios y DTs
                ViewData["Municipios"] = municipios.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                ViewData["DTs"] = dts.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Nombre });
                */
            
            /*
                //Validacion si el modelo es valido cumpliendo con la anotaciones en la entidad
                if(ModelState.IsValid)
                { 
                     Console.WriteLine("Equipo valido "+ equipo.Nombre);
             */  
                    //equipo.Nombre =  equipo.Nombre.Trim();

                      // Obtener y asignar el objeto DirectorTecnico y Municipio  por su Id
                    Municipio municipio = _repoMunicipio.GetMunicipio(idMunicipio);
                    DirectorTecnico dt = _repoDT.GetDT(idDT);                   

                     if (municipio == null || dt == null)
                    {
                        // Mostrar un mensaje de error o redirigir a otra página
                        return Page();
                    }

                     equipo.Municipio = municipio;
                     equipo.DirectorTecnico = dt;  
                     
                    duplicate =  _repoEquipo.validateDuplicates(equipo, idMunicipio, idDT);
                    if(!duplicate)
                    {
                        _repoEquipo.AddEquipo(equipo, idMunicipio, idDT);
                        return RedirectToPage("Index");
                        //var msg = municipios + " selected";
                        //return RedirectToAction("IndexSuccess", new { message = msg });
                    } 
                    else
                    {           
                        //Cargar municipios y Dts   
                        equipo = new Equipo();
                        municipios = _repoMunicipio.GetAllMunicipios();
                        dts = _repoDT.GetAllDTs();     
                        return Page();
                        //return RedirectToPage("Create");
                    }

                /*}   
                else
                {   
                    equipo = new Equipo();
                    municipios = _repoMunicipio.GetAllMunicipios();
                    dts = _repoDT.GetAllDTs(); 
                    Console.WriteLine("Equipo no valido "+ equipo.Nombre + " - Municipio " + equipo.Municipio.Nombre +" - DT " + equipo.DirectorTecnico.Nombre); 
                    Console.WriteLine("id equipo "+ equipo.Id + " idMunicipio " + idMunicipio +" idDt " + idDT);

                    foreach (var key in ModelState.Keys)
                    {
                        var error = ModelState[key].Errors.FirstOrDefault();
                        if (error != null)
                        {
                            Console.WriteLine($"Model error for {key}: {error.ErrorMessage}");
                        }
                    }                                
                    //municipios = _repoMunicipio.GetAllMunicipios();
                    //dts = _repoDT.GetAllDTs();
                    ViewData["Municipios"] = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                    ViewData["DTs"] = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");

                    return Page();
                } 

            }catch(Exception  e)
            {  
                equipo = new Equipo();
                municipios = _repoMunicipio.GetAllMunicipios();
                dts = _repoDT.GetAllDTs();  

                Console.WriteLine("Catch error " + e.Message);       
                ViewData["Municipios"] = _repoMunicipio.GetAllMunicipios().Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                ViewData["DTs"] = _repoDT.GetAllDTs().Select(dt => new SelectListItem { Value = dt.Id.ToString(), Text = dt.Nombre });

                return Page();
            }
            */
            
        }
        


    }
}
