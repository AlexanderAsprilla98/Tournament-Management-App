using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;
using Microsoft.AspNetCore.Authorization;


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
        //[BindProperty]
        //public Municipio idMunicipio { get; set; }

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
            equipo.Municipio = municipios.FirstOrDefault(); // Establecer el valor seleccionado por defecto
            equipo.DirectorTecnico = dts.FirstOrDefault(); // Establecer el valor seleccionado por defecto

            //ViewBag.Municipio = new SelectList(municipios, "Id", "Nombre");
            //ViewBag.DirectorTecnico = new SelectList(dts, "Id", "Nombre");

            if (municipios != null && dts != null)
             {
                //Console.WriteLine("Propiedades municipios y dts no son nulas " + equipo.Municipio.Nombre + " " + equipo.DirectorTecnico.Nombre);
                ViewData["Municipios"] = municipios.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                ViewData["DTs"] = dts.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Nombre });
                foreach (var muni in ViewData["Municipios"] as IEnumerable<SelectListItem>)
                {
                //Console.WriteLine(muni.Text);
                }
            }
             else
            {
                 // Manejar el caso de que municipios o dts sean nulos
                Console.WriteLine("Propiedades municipios y dts son nulas");
               
            }     

            //equipo.Municipio.Id = idMunicipio;
            //equipo.DirectorTecnico.Id = idDT;
            


        }

        public IActionResult OnPost(Equipo equipo, int idMunicipio, int idDT)
        {
            
            try
            { 
                //this.equipo = equipo;
                //Console.WriteLine("Crear IdMunicipio escogido " + idMunicipio);                 
                //Console.WriteLine("Crear IdDT escogido " + idDT);                                               
                equipo.Municipio = _repoMunicipio.GetMunicipio(idMunicipio);
                equipo.DirectorTecnico = _repoDT.GetDT(idDT);

                /*Console.WriteLine("Equipo ingresado " + equipo.Nombre);
                Console.WriteLine("id Municipio ingresado " + equipo.Municipio.Id);
                Console.WriteLine("id DT ingresado " + equipo.DirectorTecnico.Id);
                Console.WriteLine("Nombre Municipio ingresado " + equipo.Municipio.Nombre);
                Console.WriteLine("Nombre dt ingresado " + equipo.DirectorTecnico.Nombre);
                Console.WriteLine("documento ingresado " + equipo.DirectorTecnico.Documento);
                Console.WriteLine("telefono ingresado " + equipo.DirectorTecnico.Telefono);
                */
                //Console.WriteLine("equipo ingresado " + equipo.DirectorTecnico.Equipos);                                       
            
                //Validacion si el modelo es valido cumpliendo con la anotaciones en la entidad
                if(ModelState.IsValid)
                { 
                    Console.WriteLine("Equipo valido "+ equipo.Nombre + " Municipcio equipo "+ equipo.Municipio.Nombre + " Dt equipo " + equipo.DirectorTecnico.Nombre);
              
                    //equipo.Nombre =  equipo.Nombre.Trim();

                    // Obtener y asignar el objeto DirectorTecnico y Municipio  por su Id
                    Municipio municipioElegido = _repoMunicipio.GetMunicipio(idMunicipio);
                    DirectorTecnico dtElegido = _repoDT.GetDT(idDT);                   

                    if (municipioElegido == null || dtElegido == null)
                    {
                        // Mostrar un mensaje de error o redirigir a otra página
                        return Page();
                    }

                     equipo.Municipio = municipioElegido;
                     equipo.DirectorTecnico = dtElegido;  
                     
                    duplicate =  _repoEquipo.validateDuplicates(equipo);
                    if(!duplicate)
                    {
                        Console.WriteLine("No duplicado se creara el equipo");
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
                        ViewData["Municipios"] = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                        ViewData["DTs"] = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");

                        return Page();
                        //return RedirectToPage("Create");
                    }

                }   
                else
                {   
                    equipo = new Equipo();
                    municipios = _repoMunicipio.GetAllMunicipios();
                    dts = _repoDT.GetAllDTs();                     
                    ViewData["Municipios"] = new SelectList(_repoMunicipio.GetAllMunicipios(), "Id", "Nombre");
                    ViewData["DTs"] = new SelectList(_repoDT.GetAllDTs(), "Id", "Nombre");
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
            
            
        }
        


    }
}
