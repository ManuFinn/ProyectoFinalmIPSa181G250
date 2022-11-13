using APIalumnos.Models;
using APIalumnos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvisosController : ControllerBase
    {
        public tecContext Context { get; set; }

        AvisosRepository repo;

        public AvisosController(tecContext con)
        {
            Context = con;
            repo = new AvisosRepository(Context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var avisos = repo.GetAll();
            return Ok(avisos.Select(x => new
            {
                x.Id,
                x.MensajeAviso,
                x.Fecha,
                x.IdMateriaAviso,
                x.IdDocenteAviso,
                x.IdDocenteAvisoNavigation.NombreDocente,
                x.IdMateriaAvisoNavigation.NombreMateria
            }
            ));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aviso = repo.Get(id);
            if(aviso != null) { 
            return Ok( new {
                aviso.Id,
                aviso.MensajeAviso,
                aviso.Fecha,
                aviso.IdMateriaAviso,
                aviso.IdDocenteAviso
            });}
            else {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Avisostable av)
        {
            try
            {
                if (repo.IsValid(av, out List<string> errores))
                {
                    repo.Insert(av);
                    return Ok( new
                    {
                        av.Id,
                        av.Fecha,
                        av.MensajeAviso,
                        av.IdDocenteAviso,
                        av.IdMateriaAviso
                    });
                }
                else { return Ok(); }
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null) { return StatusCode(500, ex.InnerException.Message); }
                return StatusCode(500, ex.Message);
            }
        }
    }
}
